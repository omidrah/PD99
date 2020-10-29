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
using System.Web;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ServiceLayer.Models.backendVm;

namespace Web.Areas.Admin.Controllers
{
    public partial class MastersController : Controller

    {
        IUnitOfWork _uow;//db
        IMasterService _masters;
        IApplicationUserManager _user;
        public MastersController(IUnitOfWork uow, IMasterService masters, IApplicationUserManager user)
        {
            _uow = uow;
            _masters = masters;
            _user = user;
        }

        // GET: doruses
        public virtual ActionResult Index()
        {
            return View(_masters.GetAll().Select(item => new MasterVm
            {

                Name = _user.FindById(item.UserId).Name + " " + _user.FindById(item.UserId).Family,
                Rotbe = item.Rotbe,
                UserId = item.UserId,
                Bio = item.Bio,
                Takhasos = item.Takhasos,
                IsHeiatElmi = item.IsHeiatElmi
                // = item.IsHeiatElmi
            }));
        }

        public virtual ActionResult Create()
        {
            // PopulateRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(MasterVm newMaster, HttpPostedFileBase PicPath)
        {

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = newMaster.Email,
                    Email = newMaster.Email,
                    Name = newMaster.Name,
                    Family = newMaster.Family,
                    NationCode = newMaster.NationCode,
                    Address = newMaster.Address,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    PhoneNumber = newMaster.Mobile,
                    IsDeleted = false,
                    BirthDate = newMaster.BirthDate,
                    Mobile= newMaster.Mobile,
                    Tel = newMaster.Tel
                };

                if (PicPath != null)
                {
                    var filename = Path.GetFileName(PicPath.FileName);
                    var newFilename = Guid.NewGuid().ToString("N") + Path.GetExtension(filename);

                    var newFilenameUrl = "/Uploads/ImgMaster/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    PicPath.SaveAs(physicalFilename);
                    var thumbnailUrl = "/Uploads/ImgMaster/" + Utils.CreateThumbnail(physicalFilename);
                    user.ImgPath = newFilenameUrl;
                }
                user.Masters = new Master
                {
                    Users = user,
                    Rotbe = newMaster.Rotbe,
                    Bio = newMaster.Bio,
                    Takhasos = newMaster.Takhasos,
                    IsHeiatElmi = newMaster.IsHeiatElmi
                };
                var result = await _user.CreateAsync(user, "1@3$5^7*");
                if (result.Succeeded)
                {
                    // Add user to Role Customer if not already added
                    var rolesForUser = await _user.GetRolesAsync(user.Id);
                    if (!rolesForUser.Contains("Master"))
                    {
                        var addToRoleResult = await _user.AddToRoleAsync(user.Id, "Master");
                        if (!addToRoleResult.Succeeded)
                        {
                            AddErrors(addToRoleResult);
                        }
                    }
                    TempData["res"] = "<script>showNotification('رکورد اضافه شد', 'success', 'center')</script>";
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            TempData["res"] = "<script>showNotification('داده ها ی ورودی نادرست است', 'error', 'center')</script>";
            return View(newMaster);
        }

        public virtual ActionResult Edit(int id)
        {
            var curUser = _user.FindById(id);
            var curMaster = _masters.Find(id);
            //var curMaster = _masters.Find(id);
            MasterVm curMasterVM = new MasterVm()
            {
                Name = curUser.Name,
                Family = curUser.Family,
                UserId = curUser.Id,
                NationCode = curUser.NationCode,
                Rotbe = curMaster.Rotbe,
                Takhasos = curMaster.Takhasos,
                IsHeiatElmi = curMaster.IsHeiatElmi,
                Bio = curMaster.Bio,
                Tel = curUser.Tel,
                Email = curUser.Email,
                Address = curUser.Address,
                BirthDate = curUser.BirthDate,
                ImgThumbPath = curUser.ImgPath,
                PicPath = curUser.ImgPath
            };
            return View(curMasterVM);
        }

        [HttpPost]
        public virtual ActionResult Edit(MasterVm vmMaster, HttpPostedFileBase PicPath)
        {
            if (ModelState.IsValid == true)
            {
                #region Save Uploaded Image 
                string newFilenameUrl;
                //string thumbnailUrl;
                if (PicPath != null)
                {
                    var filename = Path.GetFileName(PicPath.FileName);
                    var newFilename = Guid.NewGuid().ToString()
                                             .Replace("-", string.Empty)
                                             + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/ImgMaster/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    PicPath.SaveAs(physicalFilename);
                    var thumbnailUrl = "/Uploads/ImgMaster/" + Utils.CreateThumbnail(physicalFilename);
                    vmMaster.PicPath = newFilenameUrl;

                }
                else
                {
                    vmMaster.PicPath = _user.FindById(vmMaster.UserId).ImgPath;
                }
                #endregion
                try
                {

                    var curUser = _user.FindById(vmMaster.UserId);
                    curUser.Name = vmMaster.Name;
                    curUser.Family = vmMaster.Family;
                    curUser.NationCode = vmMaster.NationCode;
                    curUser.Tel = vmMaster.Tel;
                    curUser.Address = vmMaster.Address;
                    curUser.Email = vmMaster.Email;
                    curUser.ImgPath = vmMaster.PicPath;
                    curUser.BirthDate = vmMaster.BirthDate;
                    //curUser.Mobile = vmMaster.Mobile;
                    //var curMaster = _masters.Find(vmMaster.UserId);
                    //curMaster.Rotbe = vmMaster.Rotbe;
                    //curMaster.IsHeiatElmi = vmMaster.IsHeiatElmi;
                    //curMaster.Takhasos = vmMaster.Takhasos;
                    //curMaster.Bio = vmMaster.Bio;


                    _uow.SaveAllChanges();
                    TempData["res"] = "<script>showNotification('رکورد بروز شد', 'success','center')</script>";
                    return RedirectToAction("Index");
                }
                catch
                {

                    TempData["res"] = "<script>showNotification('رکورد جدید افزوده نشد', 'success','center')</script>";
                    return View(vmMaster);
                }

            }
            else
            {
                TempData["res"] = "<script>showNotification('مقادیر را درست وارد کنید', 'success','center')</script>";
            }
            return View(vmMaster);
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
                _user.FindById(id).IsDeleted = true;
                //_products.Delete(id);
                _uow.SaveAllChanges();
                return Json(new { ret = "true", codeid = 1 });
            }
            catch
            {
                return Json(new { ret = "false", codeid = 2 });
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
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