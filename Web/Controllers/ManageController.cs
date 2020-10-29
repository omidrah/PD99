using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ServiceLayer.Interfaces;
using Web.Models;
using ServiceLayer.Models;
using System.Collections.Generic;

namespace Web.Controllers
{
    [Authorize]
    public partial class ManageController : Controller
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IOrderService _order;
        private readonly IOrderDetailsService _orderDetail;
        private readonly IDoorehService _dooreh;
        private readonly IPackageService _packages;
        private readonly IProductService _product;
        private readonly IProductGroupService _productGroup;
        private readonly IKelassService _kelass;
        private readonly IPackageProductService _packageProduct;
        private readonly IDoorehKelassService _doorehKelass;
        private readonly IDourseService _dourse;
        private readonly IUnitOfWork _uow;
        public ManageController(
            IUnitOfWork uow,
            IApplicationUserManager userManager,
            IAuthenticationManager authenticationManager,
            IApplicationSignInManager applicationSignInManager,
            IOrderService order,
            IOrderDetailsService orderDetail,
            IDoorehService dooreh,
            IPackageService packages,
            IProductService product,
            IProductGroupService productGroup,
            IKelassService kelass,
            IPackageProductService packageProduct,
            IDoorehKelassService doorehKelass,
            IDourseService dourse)
        {
            _uow = uow;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _signInManager = applicationSignInManager;
            _order = order;
            _orderDetail = orderDetail;
            _product = product;
            _productGroup = productGroup;
            _kelass = kelass;
            _packageProduct = packageProduct;
            _packages = packages;
            _doorehKelass = doorehKelass;
            _dooreh = dooreh;
            _dourse = dourse;

        }

        //
        // GET: /Manage/Index
        public virtual async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess
                        ? "Your password has been set."
                        : message == ManageMessageId.SetTwoFactorSuccess
                            ? "Your two-factor authentication provider has been set."
                            : message == ManageMessageId.Error
                                ? "An error has occurred."
                                : message == ManageMessageId.AddPhoneSuccess
                                    ? "Your phone number was added."
                                    : message == ManageMessageId.RemovePhoneSuccess
                                        ? "Your phone number was removed."
                                        : "";

            var userId = User.Identity.GetUserId<int>();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(userId),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(userId),
                Logins = await _userManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId.ToString())
            };
            return View(model);
        }
        public virtual async Task<ActionResult> Profiles()
        {
            var curUser = _userManager.GetCurrentUser();
            var userRoleTitlt = await _userManager.GetRolesAsync(curUser.Id);
            return View(new Web.Areas.Admin.Models.UserProfile
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
                PostalCode = curUser.PostalCode
            });

        }
        //
        [HttpPost]
        public virtual ActionResult Profiles(Web.Areas.Admin.Models.UserProfile CurUser)
        {
            if (ModelState.IsValid)
            {
                var dbUser = _userManager.GetCurrentUser();
                dbUser.UserName = CurUser.username;
                dbUser.Name = CurUser.name;
                dbUser.Family = CurUser.family;
                dbUser.PhoneNumber = CurUser.Mobile;
                dbUser.Tel = CurUser.Tel;
                dbUser.Address = CurUser.Address;
                dbUser.NationCode = CurUser.codeMelli;
                dbUser.PostalCode = CurUser.PostalCode;
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

        public virtual ActionResult Orders()
        {
            var curUser = _userManager.GetCurrentUser();
            var result = new List<OrderViewModel>();
            foreach (var item in _order.GetAll().Where(o => o.UserID == curUser.Id))
            {
                result.Add(
                     new OrderViewModel
                     {
                         OrderID = item.OrderID,
                         CustomersName = _userManager.FindById(item.UserID).Name + " " + _userManager.FindById(item.UserID).Family,
                         OrderDate = (DateTime)item.OrderDate,
                         UserID = item.UserID,
                         OrderSum = item.Sum// _orderDetail.GetAll().Where(LL => LL.OrderID == item.OrderID).Sum(ll => ll.OrderedCount * ll.Price),
                       //  StatusDesc = "منتظر تهیه موجودی"
                         // StatusDesc = _db.Condition.Find(item.OrderStatusID).ConditionDescription,
                         // ExporterName = alluser.FirstOrDefault(aa => aa.UserId == item.CreatedBy).Persons.name + " " + alluser.FirstOrDefault(aa => aa.UserId == item.CreatedBy).Persons.family,
                         //  Authenticated = item.IsAuthenticated ?? false
                     }
                    );
            }
            return View(result);
        }

        public virtual ActionResult OrderDetails(int id)
        {
            ViewBag.OrderId = id;
            var order = _order.Find(id);
            // ViewBag.CustomerName = _user.FindById(order.UserID).Name + " " + _user.FindById(order.UserID).Family;
            ViewBag.OrderDate = order.OrderDate;
            ViewBag.Percent = 0;
            var orderDetailList = _orderDetail.GetAll().Where(LL => LL.OrderID == id).ToList();
            int sum = orderDetailList.Where(p => p.ShowForCusomer == true).Sum(ll => ll.OrderedCount * ll.Price);
            ViewBag.Sum = order.Sum;
            ViewBag.TransferCost = order.Sum - sum;
            List<OrderDetailViewModel> list = new List<OrderDetailViewModel>();
            foreach (var item in orderDetailList.Where(o => o.ShowForCusomer == true))
            {
                if (item.ItemType == "Product")
                {
                    list.Add(new OrderDetailViewModel()
                    {
                        OrderID = item.OrderID,
                        OrderDetailID = item.OrderDetailID,
                        Price = item.Price,
                        ItemType = item.ItemType,
                        IsTransfered = item.IsTransfered,
                        ItemId = item.ItemId,
                        ItemName = _productGroup.Find(_product.Find(item.ItemId).ProductGroupID).ProductGroupTitle + " " + _product.Find(item.ItemId).ProductName,
                        OrderedCount = item.OrderedCount
                    });
                }
                if (item.ItemType == "Dooreh")
                {
                    list.Add(new OrderDetailViewModel()
                    {
                        OrderID = item.OrderID,
                        OrderDetailID = item.OrderDetailID,
                        Price = item.Price,
                        ItemType = item.ItemType,
                        IsTransfered = item.IsTransfered,
                        ItemId = item.ItemId,
                        ItemName = "دوره " + _dooreh.Find(item.ItemId).Title,
                        OrderedCount = item.OrderedCount
                    });
                }

                if (item.ItemType == "Package")
                {
                    list.Add(new OrderDetailViewModel()
                    {
                        OrderID = item.OrderID,
                        OrderDetailID = item.OrderDetailID,
                        Price = item.Price,
                        ItemType = item.ItemType,
                        IsTransfered = item.IsTransfered,
                        ItemId = item.ItemId,
                        ItemName = "بسته " + _packages.Find(item.ItemId).PackageTitle,
                        OrderedCount = item.OrderedCount
                    });
                }
            }

            return View(list);
        }


        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result =
                await
                    _userManager.RemoveLoginAsync(User.Identity.GetUserId<int>(),
                        new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, false, false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction(MVC.Manage.ActionNames.ManageLogins, new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public virtual ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code =
                await _userManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId<int>(), model.Number);
            if (_userManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await _userManager.SmsService.SendAsync(message);
            }
            return RedirectToAction(MVC.Manage.ActionNames.VerifyPhoneNumber, new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await _userManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId<int>(), true);
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (user != null)
            {
                await _signInManager.SignInAsync(user, false, false);
            }
            return RedirectToAction(MVC.Manage.Views.Index, MVC.Manage.Name);
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await _userManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId<int>(), false);
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (user != null)
            {
                await _signInManager.SignInAsync(user, false, false);
            }

            return RedirectToAction(MVC.Manage.Views.Index, MVC.Manage.Name);
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public virtual async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code =
                await _userManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId<int>(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null
                ? View("Error")
                : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result =
                await _userManager.ChangePhoneNumberAsync(User.Identity.GetUserId<int>(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, false, false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public virtual async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await _userManager.SetPhoneNumberAsync(User.Identity.GetUserId<int>(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (user != null)
            {
                await _signInManager.SignInAsync(user, false, false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
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
                    _userManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword,
                        model.NewPassword);
            if (result.Succeeded)
            {
                //var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                //if (user != null)
                //{
                //    //await _signInManager.SignInAsync(user, false, false);
                //    return RedirectToAction(MVC.Manage.ActionNames.Index, MVC.Manage.Name, new { area = "", Message = ManageMessageId.ChangePasswordSuccess });
                //}
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name, new { area = "" });

            }

            ModelState.AddModelError(string.Empty, "رمز فعلی وارد شده نادرست است");

            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public virtual ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.AddPasswordAsync(User.Identity.GetUserId<int>(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, false, false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public virtual async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess
                    ? "The external login was removed."
                    : message == ManageMessageId.Error
                        ? "An error has occurred."
                        : "";
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await _userManager.GetLoginsAsync(User.Identity.GetUserId<int>());
            var otherLogins =
                AuthenticationManager.GetExternalAuthenticationTypes()
                    .Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider))
                    .ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"),
                User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public virtual async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(User.Identity.GetUserId<int>(), loginInfo.Login);
            return result.Succeeded
                ? RedirectToAction("ManageLogins")
                : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }


        //public async Task<ActionResult> getInfo()
        //{

        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
            }

            if (disposing && _signInManager != null)
            {
                _signInManager.Dispose();
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

        private bool HasPassword()
        {
            var user = _userManager.FindById(User.Identity.GetUserId<int>());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = _userManager.FindById(User.Identity.GetUserId<int>());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}