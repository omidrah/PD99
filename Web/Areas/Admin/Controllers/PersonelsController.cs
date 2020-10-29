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
using System.Collections.Generic;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    public partial class PersonelsController : Controller

    {
        IUnitOfWork _uow;//db
        IPersonelService _personels;
        IApplicationUserManager _user;
        IRoleService _role;
        public PersonelsController(IUnitOfWork uow, IPersonelService personels, IApplicationUserManager user, IRoleService role)
        {
            _uow = uow;
            _personels = personels;
            _user = user;
            _role = role;
        }

        // GET: doruses
        public virtual ActionResult Index()
        {
            return View(_personels.GetAll().Where(ll => ll.Users.IsDeleted == false).Select(item => new PersonelVM
            {

                Name = _user.FindById(item.UserId).Name + " " + _user.FindById(item.UserId).Family,
                PersonliNo = item.PersonliNo,
                UserId = item.UserId,
                HasInsurance = item.HasInsurance,
                IsRasmi = item.IsRasmi,
                MadrakTashili = item.MadrakTashili,
                Username = _user.FindById(item.UserId).Email

                // = item.IsHeiatElmi
            }));
        }

        public virtual ActionResult _Search(string keyword)
        {
            return View(_personels.GetAll().Where(ll => ll.Users.IsDeleted == false && ll.Users.Name.Contains(keyword)).Select(item => new PersonelVM
            {

                Name = _user.FindById(item.UserId).Name + " " + _user.FindById(item.UserId).Family,
                PersonliNo = item.PersonliNo,
                UserId = item.UserId,
                HasInsurance = item.HasInsurance,
                IsRasmi = item.IsRasmi,
                MadrakTashili = item.MadrakTashili,
                Username = _user.FindById(item.UserId).Email

                // = item.IsHeiatElmi
            }));
        }
        public virtual ActionResult Create()
        {
            // PopulateRoles();
            ViewBag.RoleId = new SelectList(_role.GetAll().ToList(), "Name", "Title");
            return View();
        }
        private void PopulateRoles()
        {
            var dd = new List<SelectListItem>(); //for show radio button list in create view
            foreach (var item in _role.GetAll())
            {
                dd.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Title });
            }
            //ViewBag.RoleId = new SelectList ( _db.Roles, "RoleID", "RoleTitle");            
            ViewBag.RoleId = dd;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(PersonelVM newPersonel, HttpPostedFileBase PicPath, params string[] selectedRoles)
        {

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = newPersonel.Email,
                    Email = newPersonel.Email,
                    Name = newPersonel.Name,
                    Family = newPersonel.Family,
                    NationCode = newPersonel.NationCode,
                    Address = newPersonel.Address,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    PhoneNumber = newPersonel.Mobile,
                    IsDeleted = false,
                    BirthDate = newPersonel.BirthDate,
                    Mobile = newPersonel.Mobile,
                    Tel = newPersonel.Tel
                };

                if (PicPath != null)
                {
                    var filename = Path.GetFileName(PicPath.FileName);
                    var newFilename = Guid.NewGuid().ToString("N") + Path.GetExtension(filename);

                    var newFilenameUrl = "/Uploads/ImgPerson/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    PicPath.SaveAs(physicalFilename);
                    var thumbnailUrl = "/Uploads/ImgPerson/" + Utils.CreateThumbnail(physicalFilename);
                    user.ImgPath = newFilenameUrl;
                }
                user.Personels = new Personel
                {
                    Users = user,
                    HasInsurance = newPersonel.HasInsurance,
                    Age = newPersonel.Age,
                    MadrakTashili = newPersonel.MadrakTashili,
                    IsRasmi = newPersonel.IsRasmi,
                    PersonliNo = newPersonel.PersonliNo,
                    SalaryDate = newPersonel.SalaryDate
                };
                var result = await _user.CreateAsync(user, newPersonel.Password);
                if (result.Succeeded)
                {
                    //// Add user to Role Customer if not already added
                    //var roleName = _role.Find(newPersonel.RoleId).Name;
                    //var rolesForUser = await _user.GetRolesAsync(user.Id);
                    //if (!rolesForUser.Contains(roleName))
                    //{
                    //    var addToRoleResult = await _user.AddToRoleAsync(user.Id, roleName);
                    //    if (!addToRoleResult.Succeeded)
                    //    {
                    //        AddErrors(addToRoleResult);
                    //    }
                    if (selectedRoles != null)
                    {
                        var roleResult = await _user.AddToRolesAsync(user.Id, selectedRoles);
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(_role.GetAll().ToList(), "Name", "Title");
                            return View();
                        }

                    }
                    TempData["res"] = "<script>showNotification('رکورد اضافه شد', 'success', 'center')</script>";
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            TempData["res"] = "<script>showNotification('داده ها ی ورودی نادرست است', 'error', 'center')</script>";
            //PopulateRoles();
            ViewBag.RoleId = new SelectList(_role.GetAll().ToList(), "Name", "Title");
            return View(newPersonel);
        }

        public virtual async Task<ActionResult> Edit(int id)
        {
            var curUser = _user.FindById(id);
            var curPersonel = _personels.Find(id);
            //var curMaster = _masters.Find(id);
            EditPersonelVMWithoutPass curPersonelVM = new EditPersonelVMWithoutPass()
            {
                Name = curUser.Name,
                Family = curUser.Family,
                UserId = curUser.Id,
                NationCode = curUser.NationCode,
                Mobile = curUser.PhoneNumber,
                Tel = curUser.Tel,
                Email = curUser.Email,
                Address = curUser.Address,
                BirthDate = curUser.BirthDate,
                ImgThumbPath = curUser.ImgPath,
                PicPath = curUser.ImgPath,
                PersonliNo = curPersonel.PersonliNo,
                SalaryDate = curPersonel.SalaryDate,
                IsRasmi = curPersonel.IsRasmi,
                HasInsurance = curPersonel.HasInsurance,
                MadrakTashili = curPersonel.MadrakTashili,
                Age = curPersonel.Age
                //RoleId = 
                //Password = curUser.PasswordHash,
            };
            // PopulateRoles();
            var userRoles = await _user.GetRolesAsync(curPersonel.UserId);
            curPersonelVM.RolesList = _role.GetAll().ToList().Select(x => new SelectListItem()
            {
                Selected = userRoles.Contains(x.Name),
                Text = x.Title,

                Value = x.Name
            });
            return View(curPersonelVM);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Edit(EditPersonelVMWithoutPass vmPersonel, HttpPostedFileBase PicPath, params string[] selectedRoles)
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
                    vmPersonel.PicPath = newFilenameUrl;

                }
                else
                {
                    vmPersonel.PicPath = _user.FindById(vmPersonel.UserId).ImgPath;
                }
                #endregion
                try
                {

                    var curUser = _user.FindById(vmPersonel.UserId);
                    curUser.Name = vmPersonel.Name;
                    curUser.Family = vmPersonel.Family;
                    curUser.NationCode = vmPersonel.NationCode;
                    curUser.Tel = vmPersonel.Tel;
                    curUser.Address = vmPersonel.Address;
                    curUser.Email = vmPersonel.Email;
                    curUser.ImgPath = vmPersonel.PicPath;
                    curUser.BirthDate = vmPersonel.BirthDate;
                    curUser.PhoneNumber = vmPersonel.Mobile;
                    var curPersonel = _personels.Find(vmPersonel.UserId);
                    curPersonel.Age = vmPersonel.Age;
                    curPersonel.IsRasmi = vmPersonel.IsRasmi;
                    curPersonel.MadrakTashili = vmPersonel.MadrakTashili;
                    curPersonel.SalaryDate = vmPersonel.SalaryDate;
                    curPersonel.PersonliNo = vmPersonel.PersonliNo;
                    curPersonel.HasInsurance = curPersonel.HasInsurance;

                    //var result = await _user.CreateAsync(user, "1@3$5^7*");
                    //if (result.Succeeded)
                    //{
                    // Add user to Role Customer if not already added
                    //var roleName = _role.Find(vmPersonel.RoleId).Name;
                    //var rolesForUser = await _user.GetRolesAsync(curPersonel.UserId);
                    //if (!rolesForUser.Contains(roleName))
                    //{
                    //    var addToRoleResult = await _user.AddToRoleAsync(vmPersonel.UserId, roleName);
                    //    if (!addToRoleResult.Succeeded)
                    //    {
                    //        AddErrors(addToRoleResult);
                    //    }
                    //}
                    var userRoles = await _user.GetRolesAsync(curPersonel.UserId);

                    selectedRoles = selectedRoles ?? new string[] { };

                    var result = await _user.AddToRolesAsync(curPersonel.UserId, selectedRoles.Except(userRoles).ToArray<string>());

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                    result = await _user.RemoveFromRolesAsync(curPersonel.UserId, userRoles.Except(selectedRoles).ToArray<string>());

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                    _uow.SaveAllChanges();
                    TempData["res"] = "<script>showNotification('تغییرات انجام شد', 'success', 'center')</script>";
                    return RedirectToAction("Index");
                    //}


                    //TempData["res"] = "<script>showNotification('رکورد بروز شد', 'success','center')</script>";
                    //return RedirectToAction("Index");
                }
                catch
                {

                    TempData["res"] = "<script>showNotification('رکورد جدید افزوده نشد', 'success','center')</script>";
                    return View(vmPersonel);
                }

            }
            else
            {
                TempData["res"] = "<script>showNotification('مقادیر را درست وارد کنید', 'success','center')</script>";
            }
            PopulateRoles();
            return View(vmPersonel);
        }

        public virtual ActionResult ChangePassword(int id)
        {
            var curUser = _user.FindById(id);
            var curPersonel = _personels.Find(id);
            //var curMaster = _masters.Find(id);
            ChangePersonelPasswordViewModel curPersonelVM = new ChangePersonelPasswordViewModel()
            {
                Name = curUser.Name,
                Family = curUser.Family,
                UserId = curUser.Id
            };
            return View(curPersonelVM);
        }

        [HttpPost]
        public virtual async Task<ActionResult> ChangePassword(ChangePersonelPasswordViewModel vmPersonel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Password","مقادیر را درست وارد کنید");
                return View(vmPersonel);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _user.RemovePasswordAsync(vmPersonel.UserId);

                    var result = await _user.AddPasswordAsync(vmPersonel.UserId, vmPersonel.Password);
                    _uow.SaveAllChanges();
                    TempData["res"] = "<script>showNotification('تغییرات انجام شد', 'success', 'center')</script>";
                    return RedirectToAction("Index");
                }
                catch
                {

                    TempData["res"] = "<script>showNotification('رکورد جدید افزوده نشد', 'success','center')</script>";
                    return View(vmPersonel);
                }

            }
            else
            {
                TempData["res"] = "<script>showNotification('مقادیر را درست وارد کنید', 'success','center')</script>";
            }

            return View(vmPersonel);
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