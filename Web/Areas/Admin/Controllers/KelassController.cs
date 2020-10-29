using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public partial class KelassController : Controller
    {
        IUnitOfWork _uow;//db
        IDoorehService _dooreh;
        IDoorehDorusService _doorehDorus;
        IMahaleBargozariService _mahaleBargozari;
        IDourseService _doruse;
        IMasterService _master;
        IImageService _Images;
        IApplicationUserManager _user;
        IKelassService _kelass;
        public KelassController(IUnitOfWork uow, IDoorehService dooreh, IImageService images, IDourseService doruse, IMasterService master,
        IDoorehDorusService doorehDorus, IMahaleBargozariService mahaleBargozari, IApplicationUserManager user, IKelassService kelass)
        {
            _uow = uow;
            _dooreh = dooreh;
            _user = user;
            _Images = images;
            _doorehDorus = doorehDorus;
            _mahaleBargozari = mahaleBargozari;
            _doruse = doruse;
            _master = master;
            _kelass = kelass;
        }
        // GET: Admin/Kelass
        public virtual ActionResult Index()
        {
            return View(_kelass.GetAll().Select(item => new KelassVM
            {
                KelassId = item.KelassId,
                MasterName = _user.FindById(item.MasterId).Name + " " + _user.FindById(item.MasterId).Family,
                dorusTitle = _doruse.Find(item.DourseId).Title,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                IsHozori = item.IsHozori,
                IsOnline = item.IsOnline,
                Cost = item.Cost,
                MahaleBargozari = _mahaleBargozari.Find(item.MahaleBargozariId).MahalTitle

            }));
        }
        public virtual ActionResult Create()
        {

            ViewBag.DourseId = new SelectList(_doruse.GetAll().Where(ll => ll.IsActive == true), "Id", "Title");
            ViewBag.MasterId = new SelectList(_master.GetInfoAll(), "UserId", "Name");
            ViewBag.MahaleBargozariId = new SelectList(_mahaleBargozari.GetAll(), "Id", "MahalTitle");
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(KelassVM kelassVM)
        {

            //int result= DateTime.Compare(kelassVM.StartDate, kelassVM.EndDate);
            if (kelassVM.StartDate > kelassVM.EndDate)
            {
                ModelState.AddModelError("StartDate", "تاریخ شروع بزرگتر است");
                ViewBag.DourseId = new SelectList(_doruse.GetAll().Where(ll => ll.IsActive == true), "Id", "Title");
                ViewBag.MasterId = new SelectList(_master.GetInfoAll(), "UserId", "Name");
                ViewBag.MahaleBargozariId = new SelectList(_mahaleBargozari.GetAll(), "Id", "MahalTitle");
                return View(kelassVM);
            }
            if (ModelState.IsValid && kelassVM != null)
            {
                kelassVM.IsHozori = false;
                kelassVM.IsOnline = false;
                if (kelassVM.KelassType == "Online")
                {
                    kelassVM.IsOnline = true;
                    kelassVM.IsHozori = false;

                }
                else
                {
                    if (kelassVM.KelassType == "Hozori")
                    {
                        kelassVM.IsOnline = false;
                        kelassVM.IsHozori = true;

                    }
                }
                var newClass = new Kelass()
                {
                    DourseId = kelassVM.DourseId,
                    MahaleBargozariId = kelassVM.MahaleBargozariId,
                    MasterId = kelassVM.MasterId,
                    StartDate = kelassVM.StartDate,
                    EndDate = kelassVM.EndDate,
                    Cost = kelassVM.Cost,
                    IsHozori = kelassVM.IsHozori,
                    IsOnline = kelassVM.IsOnline,
                    IsActive = true,
                    IsDeleted = false

                };
                _kelass.Add(newClass);
                _uow.SaveAllChanges();
                TempData["res"] = "<script>showNotification('رکورد جدید افزوده شد', 'success','center')</script>";
                return RedirectToAction("Index");


            }

            TempData["res"] = "<script>showNotification('مقادیر را درست وارد کنید', 'error','center')</script>";
            ViewBag.DourseId = new SelectList(_doruse.GetAll().Where(ll => ll.IsActive == true), "Id", "Title");
            ViewBag.MasterId = new SelectList(_master.GetInfoAll(), "UserId", "Name");
            ViewBag.MahaleBargozariId = new SelectList(_mahaleBargozari.GetAll(), "Id", "MahalTitle");

            return View(kelassVM);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var currentKlass = _kelass.Find(id);
            string kelassType = "";
            if (currentKlass.IsOnline == true)
            {
                kelassType = "Online";
            }
            else if (currentKlass.IsHozori == true)
            {
                kelassType = "Hozori";
            }
            ViewBag.DourseId = new SelectList(_doruse.GetAll().Where(ll => ll.IsActive == true), "Id", "Title", currentKlass.DourseId);
            ViewBag.MasterId = new SelectList(_master.GetInfoAll(), "UserId", "Name", currentKlass.MasterId);
            ViewBag.MahaleBargozariId = new SelectList(_mahaleBargozari.GetAll(), "Id", "MahalTitle", currentKlass.MahaleBargozariId);
            return View(new KelassVM
            {
                KelassId = currentKlass.KelassId,
                StartDate = currentKlass.StartDate,
                EndDate = currentKlass.EndDate,
                IsHozori = currentKlass.IsHozori,
                IsOnline = currentKlass.IsOnline,
                IsActive = currentKlass.IsActive,
                KelassType = kelassType,
                Cost = currentKlass.Cost
            });
        }


        [HttpPost]
        public virtual ActionResult Edit(KelassVM kelassVM)
        {

            if (kelassVM.StartDate > kelassVM.EndDate)
            {

                ModelState.AddModelError("StartDate", "تاریخ شروع بزرگتر است");
                ViewBag.DourseId = new SelectList(_doruse.GetAll().Where(ll => ll.IsActive == true), "Id", "Title", kelassVM.DourseId);
                ViewBag.MasterId = new SelectList(_master.GetInfoAll(), "UserId", "Name", kelassVM.MasterId);
                ViewBag.MahaleBargozariId = new SelectList(_mahaleBargozari.GetAll(), "Id", "MahalTitle", kelassVM.MahaleBargozariId);
                return View(kelassVM);
            }
            var currentKlass = _kelass.Find(kelassVM.KelassId);


            if (ModelState.IsValid && kelassVM != null)
            {
                if (kelassVM.KelassType == "Online")
                {
                    kelassVM.IsOnline = true;
                    kelassVM.IsHozori = false;

                }
                else
                {
                    if (kelassVM.KelassType == "Hozori")
                    {
                        kelassVM.IsOnline = false;
                        kelassVM.IsHozori = true;

                    }
                }

                currentKlass.DourseId = kelassVM.DourseId;
                currentKlass.MahaleBargozariId = kelassVM.MahaleBargozariId;
                currentKlass.MasterId = kelassVM.MasterId;
                currentKlass.StartDate = kelassVM.StartDate;
                currentKlass.EndDate = kelassVM.EndDate;
                currentKlass.Cost = kelassVM.Cost;
                currentKlass.IsHozori = kelassVM.IsHozori;
                currentKlass.IsOnline = kelassVM.IsOnline;
                currentKlass.IsActive = true;
                currentKlass.IsDeleted = false
;
                ;
                //  _kelass.Add(newClass);
                _uow.SaveAllChanges();
                TempData["res"] = "<script>showNotification('رکورد ویرایش شد', 'success','center')</script>";
                return RedirectToAction("Index");


            }

            ViewBag.DourseId = new SelectList(_doruse.GetAll().Where(ll => ll.IsActive == true), "Id", "Title", currentKlass.DourseId);
            ViewBag.MasterId = new SelectList(_master.GetInfoAll(), "UserId", "Name", currentKlass.MasterId);
            ViewBag.MahaleBargozariId = new SelectList(_mahaleBargozari.GetAll(), "Id", "MahalTitle", currentKlass.MahaleBargozariId);
            return View(new KelassVM
            {
                KelassId = currentKlass.KelassId,
                StartDate = currentKlass.StartDate,
                EndDate = currentKlass.EndDate,
                IsHozori = currentKlass.IsHozori,
                IsOnline = currentKlass.IsOnline,
                IsActive = currentKlass.IsActive,
                Cost = currentKlass.Cost
            });
        }

        public virtual ActionResult Delete(int id)
        {
            //if (db.PackageProducts.Any(ll => ll.ProductId == id))

            //{
            //    return Json(new { ret = "false", codeid = 3 });
            //}
            //db.Products.Remove(products);            
            try
            {
                //_products.Find(id).IsDeleted = true;

                _kelass.Remove(_kelass.Find(id));
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1 });
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 });
            }
        }
    }
}