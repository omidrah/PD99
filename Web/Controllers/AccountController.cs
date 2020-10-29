using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DomainClass.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ServiceLayer.Interfaces;
using Web.Models;

using System.Threading;

namespace Web.Controllers
{
    [OverrideAuthorization]
    public partial class AccountController : Controller
    {
        
        private readonly ICustomerTypeServic _cType;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;

        public AccountController(IApplicationUserManager userManager,
            IApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager, ICustomerTypeServic cType)
        {
            _userManager = userManager;
            _signInManager = signInManager;        
            _cType = cType;
        }

        //[DefaultConstructor]
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                ViewBag.HasErrors = true;
                return View(model);
            }
            var curuser = await _userManager.FindByEmailAsync(model.Email);
            if (curuser.IsDeleted==false)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                switch (result)
                {
                    case SignInStatus.Success:
                        //update last login date                                     
                        curuser.LastDateLogin = DateTime.Now;
                        await _userManager.UpdateAsync(curuser);
                        //if (string.IsNullOrEmpty(returnUrl))
                        //{
                        //    var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                        //    //var identity = User.Identity as ClaimsPrincipal; 
                        //    if(!identity.HasClaim(x=>x.Type== ClaimTypes.Role))
                        //    {
                        //        ViewBag.Message = "شما کاربر غیر فعال بوده و سطح دسترسی مشخصی ندارید";
                        //        return View(model);
                        //    }
                        //var wichRole = identity.Claims.Where(c => c.Type == "Role").Select(c => c.Value).SingleOrDefault();
                        //if (wichRole.ToLower().Equals("customer"))
                        //{
                        //    RedirectToAction(MVC.Manage.ActionNames.Index, MVC.Manage.Name);
                        //}
                        //else
                        //{
                        //    RedirectToAction(MVC.Admin.Home.ActionNames.Index, MVC.Admin.Home.Name);
                        //}
                        //var UserRoles = identity.FindFirst(x => x.Type == ClaimTypes.Role).Value.Split(',');
                        //if(UserRoles.Contains("customer")){
                        //    RedirectToAction(MVC.Manage.ActionNames.Index, MVC.Manage.Name);
                        //}
                        //else{
                        //    RedirectToAction(MVC.Admin.Home.ActionNames.Index, MVC.Admin.Home.Name);
                        //}

                        // }
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        User cuserr = await _userManager.FindByEmailAsync(model.Email);
                        TempData["userId"] = cuserr.Id;
                        return RedirectToAction(MVC.Account.ActionNames.SendCode, new { ReturnUrl = returnUrl, model.RememberMe });
                    case SignInStatus.Failure:
                        ModelState.AddModelError("", "نام کاربری یا کلمه عبور اشتباه است.");
                        return View(model);
                    default:
                        ModelState.AddModelError("", "در ورود شما خطایی رخ داده است.");
                        return View(model);
                }
            }
            ModelState.AddModelError("", "کاربری شما غیرفعال شده است با مدیریت سایت تمایش بگیرید");
            return View(model);
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public virtual async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await _signInManager.HasBeenVerifiedAsync())
            {
                return View(MVC.Shared.Views.Error);
            }
            TempData["userId"] = TempData["userId"];
            return View(new VerifyCodeViewModel {Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            
            if (TempData["userId"] != null)
            {
                 var curUser =await _userManager.FindByIdAsync(Convert.ToInt32(TempData["userId"]));            
                if(curUser.confirmCode == model.Code)
                {
                    //if (await _userManager.VerifyChangePhoneNumberTokenAsync(curUser.Id, model.Provider, model.Code))
                    //{

                        await _userManager.ResetAccessFailedCountAsync(curUser.Id);
                        curUser.PhoneNumberConfirmed = false;
                        curUser.EmailConfirmed = false;
                        curUser.TwoFactorEnabled = false;
                        await _userManager.UpdateAsync(curUser);
                        await _signInManager.SignInAsync(curUser, model.RememberMe, model.RememberBrowser);
                        return RedirectToLocal(model.ReturnUrl);
                    //}
                    //else
                    //{
                    //    await _userManager.AccessFailedAsync(curUser.Id);
                    //    ModelState.AddModelError("", "کاربری شما قفل گردید با مدیر تماس بگیرید");
                    //    return View(model);
                    //}
                }
                else
                {
                    TempData["userId"] = TempData["userId"];
                    ModelState.AddModelError("", "کد احراز هویت نادرست است");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "این کاربری نامعتبر است");
                return View(model);
            }
            //var result =
            //    await
            //        _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe,
            //            model.RememberBrowser);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(model.ReturnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "کد احراز هویت نادرست است");
            //        return View(model);
            //}
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            ViewBag.CustomerTypeId = new SelectList(_cType.GetAll().Where(ct =>ct.IsDeleted !=true), "Id", "Title");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model,HttpPostedFileBase PicDoc, HttpPostedFileBase PicPath)
        {
            if(model.CustomerTypeId ==4 )
            {
                if (PicDoc == null)
                {
                    ModelState.AddModelError("PicDoc", "لططفا عکس مدرک را ارسال کنید");
                    ViewBag.CustomerTypeId = new SelectList(_cType.GetAll().Where(ct => ct.IsDeleted != true), "Id", "Title", model.CustomerTypeId);
                    return View(model);
                }
                
            }
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Family = model.Family,
                    NationCode = model.NationCode,
                    Address = model.Address,
                    PostalCode = model.PostalCode,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted =false,
                    PhoneNumber = model.Mobile,
                    Tel = model.Tel,
                    PhoneNumberConfirmed=false,
                    EmailConfirmed= false,
                    TwoFactorEnabled= false,
                    confirmCode =  Guid.NewGuid().ToString("N").Substring(1, 6)
                };
                user.Customers = new Customer
                {
                    Users = user,
                    CustomerTypeId = 1 /*آزاد*/,
                    IsMadrakTaeed= false,
                    IsMadrakChecked= false,
                    PanvandeNo = "A9610" + DateTime.Now.Day
                };                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //save user image
                    if (PicPath != null)
                    {
                        var filename = Path.GetFileName(PicPath.FileName);
                        var newFilename = Guid.NewGuid().ToString("N") + Path.GetExtension(filename);

                        var newFilenameUrl = "~/Uploads/ImgCustomer/" + newFilename;
                        //string _path = Path.Combine(Server.MapPath("~/Uploads/ImgCustomer/"), newFilename);
                        var physicalFilename = Server.MapPath(newFilenameUrl);
                        PicPath.SaveAs(physicalFilename);
                        //var thumbnailUrl = Utils.CreateThumbnail(physicalFilename);
                        user.ImgPath = newFilenameUrl;
                    }
                    //save Doc images بسیج ، آزاد، شاهد ، ایثارگر
                    if (PicDoc != null)
                    {
                        var filename = Path.GetFileName(PicDoc.FileName);
                        var newFilename = Guid.NewGuid().ToString("N") + Path.GetExtension(filename);
                      //  string _path = Path.Combine(Server.MapPath("~/Uploads/MadarekCustomer/"), newFilename);
                        var newFilenameUrl = "~/Uploads/MadarekCustomer/" + newFilename;
                        var physicalFilename = Server.MapPath(newFilenameUrl);
                        PicDoc.SaveAs(physicalFilename);
                        user.Customers.ImgDocPath = newFilenameUrl;
                    }

                    // Add user to Role Customer if not already added
                    var rolesForUser = await _userManager.GetRolesAsync(user.Id);
                    if (!rolesForUser.Contains("Customer"))
                    {
                        var addToRoleResult = await _userManager.AddToRoleAsync(user.Id, "Customer");
                        if (!addToRoleResult.Succeeded)
                        {
                            AddErrors(addToRoleResult);
                        }
                    }
                    await _signInManager.SignInAsync(user, false, false);

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action(
                    //    MVC.Account.ActionNames.ConfirmEmail,
                    //    MVC.Account.Name,
                    //    new { userId = user.Id, code = code }, 
                    //    protocol: Request.Url.Scheme);

                    //await _userManager.SendEmailAsync(
                    //    user.Id,
                    //    "تصدیق هویت ایمیل در پارسیان دانش",
                    //    "لطفا لینک زیر را کلیک کنید: <a href=\""
                    //    + callbackUrl + "\">link</a>");

                    //ViewBag.Link = callbackUrl;
                    //return View("DisplayEmail");
                    return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.CustomerTypeId = new SelectList(_cType.GetAll().Where(ct => ct.IsDeleted != true), "Id", "Title", model.CustomerTypeId);
            return View(model);
        }

        public virtual async Task<JsonResult> IsEmailAvailable(string Email)
        {
            var usrname = Request.QueryString.GetValues("username")[0];
            var checkParam = string.IsNullOrEmpty(Email) ? usrname : Email;
            var hasRecord = await _userManager.GetAllUsersAsync();
            return Json(!hasRecord.Any(ll => ll.Email == checkParam), JsonRequestBehavior.AllowGet);
        }

        public virtual async Task<JsonResult> IsNationalCodeAvailable(string NationCode)
        {
            var codeMelli = Request.QueryString.GetValues("codeMelli")[0];
            var checkParam = string.IsNullOrEmpty(NationCode) ? codeMelli : NationCode;
            var hasRecord = await _userManager.GetAllUsersAsync();
            return Json(!hasRecord.Any(ll => ll.NationCode == checkParam), JsonRequestBehavior.AllowGet);
        }
        
        public virtual async Task<JsonResult> IsMobileAvailable(string Mobile)
        {
            var hasRecord = await _userManager.GetAllUsersAsync();
            return Json(!hasRecord.Any(ll => ll.Mobile == Mobile), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public virtual async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    //return View("ForgotPasswordConfirmation");
                    return View(MVC.Account.Views.ForgotPasswordConfirmation);
                }


                //Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new {userId = user.Id, code},
                    Request.Url.Scheme);
                await
                    _userManager.SendEmailAsync(user.Id, "رمز جدید",
                        "برای تعیین رمز جدید بر روی لینک کلیک کنید <a href=\"" + callbackUrl + "\">اینجا</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public virtual ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public virtual ActionResult ResetPassword(string code)
        {
            return code == null ? View(MVC.Shared.Views.Error) : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                //return RedirectToAction("ResetPasswordConfirmation", "Account");
                return RedirectToAction(MVC.Account.ActionNames.ResetPasswordConfirmation, MVC.Account.Name);
            }
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                //return RedirectToAction("ResetPasswordConfirmation", "Account");
                return RedirectToAction(MVC.Account.ActionNames.ResetPasswordConfirmation, MVC.Account.Name);
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public virtual ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            //return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
            return new ChallengeResult(provider,
                Url.Action(MVC.Account.ActionNames.ExternalLoginCallback, MVC.Account.Name, new {ReturnUrl = returnUrl}));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public virtual async Task<ActionResult> SendCode(string returnUrl="/home/index", bool rememberMe=true)
        {
            int userId;
            if (TempData["userId"] != null)
            {
                userId = Convert.ToInt32(TempData["userId"]);
                TempData["userId"] = TempData["userId"];
            }
            else
            {
                userId = _userManager.GetCurrentUser().Id;
            }
            if (userId == null)
            {
                //return View("Error");
                return View(MVC.Shared.Views.Error);
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions =
                userFactors.Select(purpose => new SelectListItem {Text = purpose, Value = purpose}).ToList();
            return
                View(new SendCodeViewModel {Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }       
        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // Generate the token and send it
            if (!await _signInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View(MVC.Shared.Views.Error);
                //return View("Error");
            }
            //TempData["userId"] = TempData["userId"];
            User curuser=null;
            if (TempData["userId"] != null)
            {
                var userId = Convert.ToInt32(TempData["userId"]);
                curuser =await _userManager.FindByIdAsync(userId);
                TempData["userId"] = TempData["userId"];
            }
            else
            {
                curuser = _userManager.GetCurrentUser();
            }            
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(curuser.Id, curuser.Mobile);
            curuser.confirmCode = code;
            await _userManager.UpdateAsync(curuser);
            if (_userManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = curuser.Mobile,
                    Body = "کد تایید شما در پارسیان دانش: " + code
                };
                await _userManager.SmsService.SendAsync(message);
            }
            TempData["userId"] = curuser.Id;
            return RedirectToAction(MVC.Account.ActionNames.VerifyCode,
                new {Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe});
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                //return RedirectToAction("Login");
                return RedirectToAction(MVC.Account.Views.Login);
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await _signInManager.ExternalSignInAsync(loginInfo, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                //return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction(MVC.Account.ActionNames.SendCode,
                        new {ReturnUrl = returnUrl, RememberMe = false});
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View(MVC.Account.Views.ExternalLoginConfirmation,
                        new ExternalLoginConfirmationViewModel {Email = loginInfo.Email});
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                //return RedirectToAction("Index", "Manage");
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View(MVC.Account.Views.ExternalLoginFailure);
                    //return View("ExternalLoginFailure");
                }
                var user = new User {UserName = model.Email, Email = model.Email};
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false, false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public virtual async Task<ActionResult> LogOff()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            await _userManager.UpdateSecurityStampAsync(user.Id);
            //جهت بلا استفاده شدن کوکی  ها در سایر جاهایی که لاگین کرده ایم SecurityStamp بروزرسانی مجدد
            //return RedirectToAction("Index", "Home");
            //حذف سبد خرید کاربر
            Session["ShopCart"] = null;
            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public virtual ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            //return RedirectToAction("Index", "Home");
            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}