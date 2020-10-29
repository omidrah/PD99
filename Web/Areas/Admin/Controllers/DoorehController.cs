
using DataLayer.Context;
using System;
using System.Linq;
using System.Net;
using DomainClass.Models;
using System.Web.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Web.Models;
using KooyWebApp_MVC.Classes;
using InsertShowImage;

namespace Web.Areas.Admin.Controllers
{
    [Authorize]

    public partial class DoorehController : Controller
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
        IDoorehKelassService _doorehKelass;
        ITagService _tags;
        public DoorehController(IUnitOfWork uow, IDoorehService dooreh, IImageService images, IDourseService doruse, IMasterService master,
        IDoorehDorusService doorehDorus, IMahaleBargozariService mahaleBargozari, IApplicationUserManager user
            , IKelassService kelass, IDoorehKelassService doorehKelass, ITagService tags)
        {
            _uow = uow;
            _dooreh = dooreh;
            _user = user;
            _Images = images;
            _doorehDorus = doorehDorus;
            _mahaleBargozari = mahaleBargozari;
            _doruse = doruse;
            _master = master;
            _doorehKelass = doorehKelass;
            _kelass = kelass;
            _tags = tags;
        }

        #region Dooreh
        public virtual ActionResult Index()
        {
            return View(_dooreh.GetAll().Select(item => new DoorehVM
            {
                Id = item.Id,
                DoorehTitle = item.Title,
                DoorehStartDate = item.DoorehStartDate,
                DoorehEndDate = item.DoorehEndDate,
                Cost = item.Cost,
                OffPercent = item.OffPercent,
                IsHozori = item.IsHozori,
                IsOnline = item.IsOnline,
                IsActive = item.IsActive
            }).OrderBy(d => d.DoorehTitle));
        }


        public virtual JsonResult Index1([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var result = (_dooreh.GetAll().ToTreeDataSourceResult(request,
                e => e.Id,
                e => e.Pid,
                e => id.HasValue ? e.Pid == id : e.Pid == null,
                e => e
            ));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(DoorehVM doorehVM, HttpPostedFileBase ImgDooreh)
        {
            if (ModelState.IsValid && doorehVM != null)
            {
                doorehVM.IsHozori = false;
                doorehVM.IsOnline = false;
                if (doorehVM.DoorehType == "Online")
                {
                    doorehVM.IsOnline = true;
                    doorehVM.IsHozori = false;

                }
                else
                {
                    if (doorehVM.DoorehType == "Hozori")
                    {
                        doorehVM.IsOnline = false;
                        doorehVM.IsHozori = true;

                    }
                }

                var newItem = new Dooreha()
                {
                    Title = doorehVM.DoorehTitle,
                    Content = doorehVM.Content,
                    Appendix = doorehVM.Appendix,
                    Goal = doorehVM.Goal,
                    Pishniaz = doorehVM.Pishniaz,
                    KeyWord = doorehVM.KeyWord,
                    BargozariDay = doorehVM.BargozariDay,
                    BargozariTime = doorehVM.BargozariTime,
                    IsDeleted = false,
                    IsActive = doorehVM.IsActive,
                    IsHozori = doorehVM.IsHozori,
                    IsOnline = doorehVM.IsOnline,
                    Cost = doorehVM.Cost,
                    OffPercent = doorehVM.OffPercent,
                    CreateDate = DateTime.Now,
                    CreatedBy = _user.GetCurrentUserId()
                };

                #region Save Uploaded Image 
                newItem.ImageName = "images.jpg";
                if (ImgDooreh != null && ImgDooreh.IsImage())
                {
                    newItem.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgDooreh.FileName);
                    ImgDooreh.SaveAs(Server.MapPath("/Uploads/ImgProducts/" + newItem.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Uploads/ImgProducts/" + newItem.ImageName),
                        Server.MapPath("/Uploads/ImgProducts/Thumb/" + newItem.ImageName));
                }
                #endregion
                _dooreh.Add(newItem);
                _uow.SaveAllChanges();
                if (!string.IsNullOrEmpty(doorehVM.tags))
                {
                    string[] tag = doorehVM.tags.Split('-');
                    foreach (string t in tag)
                    {
                        _tags.Add(new Tags()
                        {
                            itemId = newItem.Id,
                            tagTitle = t.Trim(),
                            tagConstant = "Dooreh"
                        });
                    }
                }
                _uow.SaveAllChanges();
                TempData["res"] = "<script>showNotification('رکورد جدید افزوده شد', 'success','center')</script>";
                return RedirectToAction("Index");
            }

            return View(doorehVM);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            ViewBag.Tags = string.Join("-", _tags.GetAllByItemId(id, "Dooreh").Select(t => t.tagTitle).ToList());
            var curRec = _dooreh.Find(id);
            string doorehType = "";
            if (curRec.IsOnline == true)
            {
                doorehType = "Online";
            }
            else if (curRec.IsHozori == true)
            {
                doorehType = "Hozori";
            }


            ViewBag.Tags = string.Join("-", _tags.GetAllByItemId(id, "Dooreh").Select(t => t.tagTitle).ToList());
            return View(new DoorehVM
            {
                Id = curRec.Id,
                DoorehTitle = curRec.Title,
                KeyWord = curRec.KeyWord,
                Content = curRec.Content,
                Goal = curRec.Goal,
                Appendix = curRec.Appendix,
                Pishniaz = curRec.Pishniaz,
                BargozariDay = curRec.BargozariDay,
                BargozariTime = curRec.BargozariTime,
                DoorehStartDate = curRec.DoorehStartDate,
                DoorehEndDate = curRec.DoorehEndDate,
                Cost = curRec.Cost,
                OffPercent = curRec.OffPercent,
                IsOnline = curRec.IsOnline,
                IsHozori = curRec.IsHozori,
                DoorehType = doorehType,
                ImageName = curRec.ImageName,

                IsActive = curRec.IsActive
            }
                );
        }
        [HttpPost]
        public virtual ActionResult Edit(DoorehVM doorehVM, HttpPostedFileBase ImgDooreh)
        {
            var curDooreh = _dooreh.Find(doorehVM.Id);
            string doorehType = "";
            if (curDooreh.IsOnline == true)
            {
                doorehType = "Online";
            }
            else if (curDooreh.IsHozori == true)
            {
                doorehType = "Hozori";
            }
            if (ModelState.IsValid && doorehVM != null)
            {

                if (doorehVM.DoorehType == "Online")
                {
                    doorehVM.IsOnline = true;
                    doorehVM.IsHozori = false;

                }
                else
                {
                    if (doorehVM.DoorehType == "Hozori")
                    {
                        doorehVM.IsOnline = false;
                        doorehVM.IsHozori = true;

                    }
                }
                curDooreh.Title = doorehVM.DoorehTitle;
                curDooreh.IsActive = doorehVM.IsActive;
                curDooreh.KeyWord = doorehVM.KeyWord;
                curDooreh.Content = doorehVM.Content;
                curDooreh.BargozariDay = doorehVM.BargozariDay;
                curDooreh.BargozariTime = doorehVM.BargozariTime;
                curDooreh.DoorehStartDate = doorehVM.DoorehStartDate;
                curDooreh.DoorehEndDate = doorehVM.DoorehEndDate;
                curDooreh.Cost = doorehVM.Cost;
                curDooreh.OffPercent = doorehVM.OffPercent;
                curDooreh.IsHozori = doorehVM.IsHozori;
                curDooreh.IsOnline = doorehVM.IsOnline;
                //curPackage.ModifyBy = _user.GetCurrentUserId();
                //curPackage.ModifyDate = DateTime.Now;
                #region Save Uploaded Image 
                if (ImgDooreh != null && ImgDooreh.IsImage())
                {
                    if (curDooreh.ImageName != "images.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/Uploads/ImgProducts/Thumb/" + curDooreh.ImageName));
                        System.IO.File.Delete(Server.MapPath("/Uploads/ImgProducts/" + curDooreh.ImageName));
                    }

                    curDooreh.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgDooreh.FileName);
                    ImgDooreh.SaveAs(Server.MapPath("/Uploads/ImgProducts/" + curDooreh.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Uploads/ImgProducts/" + curDooreh.ImageName),
                        Server.MapPath("/Uploads/ImgProducts/Thumb/" + curDooreh.ImageName));
                }
                #endregion

                var tags = _tags.GetAllByItemId(doorehVM.Id, "Dooreh");
                foreach (var tag in tags)
                {
                    _tags.Remove(tag);
                }
                if (!string.IsNullOrEmpty(doorehVM.tags))
                {
                    string[] tag = doorehVM.tags.Split('-');
                    foreach (string t in tag)
                    {
                        _tags.Add(new Tags()
                        {
                            itemId = doorehVM.Id,
                            tagTitle = t.Trim(),
                            tagConstant = "Dooreh"
                        });
                    }
                }
                _uow.SaveAllChanges();
                TempData["res"] = "<script>showNotification('رکورد جدید بروز شد', 'success','center')</script>";
                return RedirectToAction("Index");
            }

            TempData["res"] = "<script>showNotification('مقادیر را درست وارد کنید', 'success','center')</script>";
            ViewBag.Tags = string.Join("-", _tags.GetAllByItemId(doorehVM.Id, "Dooreh").Select(t => t.tagTitle).ToList());
            return View(doorehVM);
        }

        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            //if (db.PackageProducts.Any(ll => ll.ProductId == id))

            //{
            //    return Json(new { ret = "false", codeid = 3 });
            //}
            //db.Products.Remove(products);            
            try
            {
                _dooreh.Remove(_dooreh.Find(id));
                // var curDorus = _packages.Find(packageVM.Id);
                // curDorus.IsDeleted = true;
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1 });
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 });
            }
        }
        #endregion


        #region Dooreh Detailes

        public virtual ActionResult ShowKelass(int id)
        {
            var curDooreh = _dooreh.Find(id);
            ViewBag.DoorehId = id;
            ViewBag.DoorehTitle = curDooreh.Title;
            ViewBag.DoorehStartDate = curDooreh.DoorehStartDate;
            ViewBag.DoorehEndDate = curDooreh.DoorehEndDate;
            ViewBag.IsHozori = curDooreh.IsHozori;
            ViewBag.IsOnLine = curDooreh.IsOnline;
            if (curDooreh.IsHozori == true)
            {
                ViewBag.DoorehType = "حضوری";
            }
            if (curDooreh.IsOnline == true)
            {
                ViewBag.DoorehType = "آنلاین";
            }
            return View(_doorehKelass.GetAllKelass(id).Select(item => new DoorehVM
            {
                KelassDoorehId = item.KelassDoorehId,
                KelassId = item.KelassId,
                DourseId = item.Kelass.DourseId,
                MasterName = _user.FindById(item.Kelass.MasterId).Name + " " + _user.FindById(item.Kelass.MasterId).Family,
                dorusTitle = _doruse.Find(item.Kelass.DourseId).Title,
                StartDate = item.Kelass.StartDate,
                EndDate = item.Kelass.EndDate,
                Cost = item.Kelass.Cost,
                OffPercent = item.Dooreha.OffPercent,
                MahaleBargozari = _mahaleBargozari.Find(item.Kelass.MahaleBargozariId).MahalTitle

            }));
        }

        public virtual ActionResult AddKelass(int doorehId)
        {
            var curDooreh = _dooreh.Find(doorehId);
            var klases = _kelass.GetAll().Where(k => k.IsActive == true && k.IsOnline == curDooreh.IsOnline && k.IsHozori == curDooreh.IsHozori).Select(a => new Kelas
            {
                KelassId = a.KelassId,
                KlassName = _doruse.Find(a.DourseId).Title + "- استاد " + _user.FindById(a.MasterId).Name + " " + _user.FindById(a.MasterId).Family + " " + a.StartDate.Value.ToLongDateString() + "  الی  " + a.EndDate.Value.ToLongDateString()
            });
            ViewBag.KelassId = new SelectList(klases, "KelassId", "KlassName");
            ViewBag.DoorehId = doorehId;
            // var curDooreh = _dooreh.Find(doorehId);
            // ViewBag.DoorehId = id;
            if (curDooreh.IsHozori == true)
            {
                ViewBag.DoorehType = "حضوری";
            }
            if (curDooreh.IsOnline == true)
            {
                ViewBag.DoorehType = "آنلاین";
            }
            ViewBag.DoorehTitle = curDooreh.Title;
            ViewBag.DoorehStartDate = curDooreh.DoorehStartDate;
            ViewBag.DoorehEndDate = curDooreh.DoorehEndDate;
            return View();
        }

        [HttpPost]
        public virtual ActionResult AddKelass(DoorehVM doorehVM)
        {
            var curDooreh = _dooreh.Find(doorehVM.Id);
            var dooreh = _dooreh.Find(doorehVM.Id);
            var kelass = _kelass.Find(doorehVM.KelassId);
            if (ModelState.IsValid && doorehVM != null)
            {
                var doorehKelass = _doorehKelass.GetAllKelass(doorehVM.Id);
                if (_doorehKelass.GetAllKelass(doorehVM.Id).Where(ll => ll.KelassId == doorehVM.KelassId).Any())
                {
                    ModelState.AddModelError("KelassId", "کلاس قبلا به دوره اضافه شده است");
                    var klases = _kelass.GetAll().Where(k => k.IsActive == true).Select(a => new Kelas
                    {
                        KelassId = a.KelassId,
                        KlassName = _doruse.Find(a.DourseId).Title + "- استاد " + _user.FindById(a.MasterId).Name + " " + _user.FindById(a.MasterId).Family + " " + a.StartDate.Value.ToLongDateString() + "  الی  " + a.EndDate.Value.ToLongDateString()
                    });
                    ViewBag.KelassId = new SelectList(klases, "KelassId", "KlassName");
                    ViewBag.DoorehId = doorehVM.Id;
                    if (curDooreh.IsHozori == true)
                    {
                        ViewBag.DoorehType = "حضوری";
                    }
                    if (curDooreh.IsOnline == true)
                    {
                        ViewBag.DoorehType = "آنلاین";
                    }
                    ViewBag.DoorehTitle = curDooreh.Title;
                    //ViewBag.DoorehStartDate = curDooreh.DoorehStartDate;
                    //ViewBag.DoorehEndDate = curDooreh.DoorehEndDate;
                    return View();


                }
                var newDoorehKelass = new KelassDooreh()
                {
                    KelassId = doorehVM.KelassId,
                    DoorehaId = doorehVM.Id,
                    IsActive = true,
                    IsDeleted = false

                };
                _doorehKelass.Add(newDoorehKelass);
                _uow.SaveAllChanges();
                TempData["res"] = "<script>showNotification('رکورد جدید افزوده شد', 'success','center')</script>";
                return RedirectToAction("ShowKelass", new { id = (int)doorehVM.Id });


            }

            TempData["res"] = "<script>showNotification('مقادیر را درست وارد کنید', 'error','center')</script>";
            //var klases = _kelass.GetAll().Where(k => k.IsActive == true).Select(a => new Kelas
            //{
            //    KelassId = a.KelassId,
            //    KlassName = _doruse.Find(a.DourseId).Title + "- استاد " + _user.FindById(a.MasterId).Name + " " + _user.FindById(a.MasterId).Family + " " + a.StartDate.Value.ToLongDateString() + "  الی  " + a.EndDate.Value.ToLongDateString()
            //});
            //ViewBag.KelassId = new SelectList(klases, "KelassId", "KlassName");
            //ViewBag.DoorehId = doorehVM.Id;
            // var curDooreh = _dooreh.Find(doorehVM.Id);
            // ViewBag.DoorehId = id;
            ViewBag.DoorehTitle = curDooreh.Title;
            //ViewBag.DoorehStartDate = curDooreh.DoorehStartDate;
            //ViewBag.DoorehEndDate = curDooreh.DoorehEndDate;
            return View(doorehVM);
        }

        [HttpPost]
        public virtual ActionResult DeleteKelass(int id)
        {
            try
            {
                var item = _doorehKelass.Find(id);
                _doorehKelass.Remove(item);
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1, doorehId = item.DoorehaId });
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 });
            }

        }
        public virtual ActionResult ShowProp(int id)
        {
            var curDooreh = _dooreh.Find(id);
            ViewBag.DoorehId = id;
            ViewBag.DoorehTitle = curDooreh.Title;
            ViewBag.DoorehStartDate = curDooreh.DoorehStartDate;
            ViewBag.DoorehEndDate = curDooreh.DoorehEndDate;
            return View(_doorehDorus.GetAll(id).Select(item => new DoorehVM
            {
                Id = item.Id,
                MasterName = _user.FindById(item.MasterId).Name + " " + _user.FindById(item.MasterId).Family,
                dorusTitle = _doruse.Find(item.DourseId).Title,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Cost = item.Cost,
                OffPercent = item.Dooreha.OffPercent,
                MahaleBargozari = _mahaleBargozari.Find(item.MahaleBargozariId).MahalTitle

            }));
        }

        public virtual ActionResult AddDetiales(int doorehId)
        {

            ViewBag.DoorehDorusId = new SelectList(_doruse.GetAll().Where(ll => ll.IsActive == true), "Id", "Title");
            ViewBag.MasterId = new SelectList(_master.GetInfoAll(), "UserId", "Name");
            ViewBag.MahaleBargozariId = new SelectList(_mahaleBargozari.GetAll(), "Id", "MahalTitle");
            ViewBag.DoorehId = doorehId;
            return View();
        }
        [HttpPost]
        public virtual ActionResult AddDetiales(DoorehVM doorehVM)
        {
            if (ModelState.IsValid && doorehVM != null)
            {
                var newDoorehDorus = new DoorehaDorouse()
                {
                    DourseId = doorehVM.DoorehDorusId,
                    DoorehaId = doorehVM.Id,
                    MahaleBargozariId = doorehVM.MahaleBargozariId,
                    MasterId = doorehVM.MasterId,
                    StartDate = doorehVM.StartDate,
                    EndDate = doorehVM.EndDate,
                    Cost = doorehVM.Cost,
                    IsActive = true,
                    IsDeleted = false

                };
                _doorehDorus.Add(newDoorehDorus);
                _uow.SaveAllChanges();
                TempData["res"] = "<script>showNotification('رکورد جدید افزوده شد', 'success','center')</script>";
                return RedirectToAction("ShowProp", new { id = (int)doorehVM.Id });


            }

            TempData["res"] = "<script>showNotification('مقادیر را درست وارد کنید', 'error','center')</script>";
            ViewBag.DoorehDorusId = new SelectList(_doruse.GetAll().Where(ll => ll.IsActive == true), "Id", "Title");
            ViewBag.MasterId = new SelectList(_master.GetInfoAll(), "UserId", "Name");
            ViewBag.MahaleBargozariId = new SelectList(_mahaleBargozari.GetAll(), "Id", "MahalTitle");
            ViewBag.DoorehId = doorehVM.Id;
            return View(doorehVM);
        }

        [HttpPost]
        public virtual ActionResult DeleteDooreh(int id)
        {
            try
            {
                var item = _doorehDorus.Find(id);
                _doorehDorus.Remove(item);
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1, doorehId = item.DoorehaId });
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 });
            }

        }


        #endregion



        #region Picture

        public virtual ActionResult Gallery(int id)
        {
            ViewBag.Galleries = _Images.GetAll().Where(p => p.ItemId == id && p.Constant == "Dooreh").ToList();
            return View(new Image()
            {
                ItemId = id,
                Constant = "Dooreh"
            });
        }

        [HttpPost]
        public virtual ActionResult Gallery(Image galleries, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null && imgUp.IsImage())
                {
                    galleries.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/Uploads/ImgProducts/" + galleries.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Uploads/ImgProducts/" + galleries.ImageName),
                        Server.MapPath("/Uploads/ImgProducts/Thumb/" + galleries.ImageName));
                    galleries.Constant = "Dooreh";
                    _Images.Add(galleries);
                    _uow.SaveAllChanges();
                }
            }

            return RedirectToAction("Gallery", new { id = galleries.ItemId });
        }

        public virtual ActionResult DeleteGallery(int id)
        {
            var gallery = _Images.Find(id);

            System.IO.File.Delete(Server.MapPath("/Uploads/ImgProducts/" + gallery.ImageName));
            System.IO.File.Delete(Server.MapPath("/Uploads/ImgProducts/Thumb/" + gallery.ImageName));

            _Images.Remove(gallery);
            _uow.SaveAllChanges();
            return RedirectToAction("Gallery", new { id = gallery.ItemId });
        }

        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);

        }

    }
}

