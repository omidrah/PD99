using DataLayer.Context;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{

    public partial class ProfileController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IApplicationUserManager _user;
        public ProfileController(IUnitOfWork uow, IApplicationUserManager user)
        {
            _uow = uow;
            _user = user;
        }
        public virtual async Task<ActionResult> Index()
        {
            var curUser = _user.GetCurrentUser();
            var userRoleTitlt = await _user.GetRolesAsync(curUser.Id);
            return View(new Models.UserProfile
            {
                Address = curUser.Address,
                family = curUser.Family,
                name = curUser.Name,
                codeMelli = curUser.NationCode,
                Tel = curUser.Tel,
                Mobile = curUser.PhoneNumber,
                username = curUser.Email,
                RoleId = curUser.Roles.First().RoleId,
                RoleTitle = userRoleTitlt.First(),
                userId = curUser.Id,
            });

        }

        public virtual ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result =
                await
                    _user.ChangePasswordAsync(_user.GetCurrentUser().Id, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult Edit(Models.UserProfile CurUser)
        {
            if (ModelState.IsValid)
            {
                var dbUser = _user.GetCurrentUser();
                dbUser.UserName = CurUser.username;
                dbUser.Name = CurUser.name;
                dbUser.Family = CurUser.family;
                dbUser.PhoneNumber = CurUser.Mobile;
                dbUser.Tel = CurUser.Tel;
                dbUser.Address = CurUser.Address;
                dbUser.NationCode = CurUser.codeMelli;
                try
                {
                    _uow.MarkAsChanged(dbUser);
                    _uow.SaveAllChanges();
                    TempData["res"] = "<script>showNotification('پروفایل شما بروزرسانی گردید', 'success')</script>";
                    return View("Index");
                    //return View(MVC.Admin.Home.Index,MVC.Admin.Home.Name,new { Areas="admin"});
                }
                catch (Exception ex)
                {
                    return Json(false);
                }
            }
            return View(CurUser);
        }

    }


}