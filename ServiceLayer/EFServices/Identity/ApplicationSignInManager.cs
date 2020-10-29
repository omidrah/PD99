using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DomainClass.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
     // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<User, int>, IApplicationSignInManager
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        public ApplicationSignInManager(IApplicationUserManager userManager,IAuthenticationManager authenticationManager) :base((ApplicationUserManager)userManager, authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            
            return _userManager.GenerateUserIdentityAsync(user);
        }

        //public async Task SignInAsync(User user, bool isPersistent)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //    identity.AddClaim(new Claim("fullname", user.Name + " " + user.Family));
        //    identity.AddClaim(new Claim("Roles", _userManager.GetRolesAsync(user.Id).ToString()));
        //    AuthenticationManager.SignIn(new AuthenticationProperties() {IsPersistent = isPersistent}, identity);
        //}


        //public override async Task<SignInStatus> PasswordSignInAsync(string userEmail, string password, bool isPersistent, bool shouldLockout)
        //{
        //    SignInStatus signInStatu;
        //    if (_userManager != null)
        //    {
        //        /// changed to use email address instead of username
        //        Task<User> userAwaiter = _userManager.FindByEmailAsync(userEmail);

        //        User tUser = await userAwaiter;
        //        if (tUser != null)
        //        {
        //            Task<bool> cultureAwaiter1 = _userManager.IsLockedOutAsync(tUser.Id);
        //            if (!await cultureAwaiter1)
        //            {
        //                Task<bool> cultureAwaiter2 = _userManager.CheckPasswordAsync(tUser, password);
        //                if (!await cultureAwaiter2)
        //                {
        //                    if (shouldLockout)
        //                    {
        //                        Task<IdentityResult> cultureAwaiter3 = _userManager.AccessFailedAsync(tUser.Id);
        //                        await cultureAwaiter3;
        //                        Task<bool> cultureAwaiter4 = _userManager.IsLockedOutAsync(tUser.Id);
        //                        if (await cultureAwaiter4)
        //                        {
        //                            signInStatu = SignInStatus.LockedOut;
        //                            return signInStatu;
        //                        }
        //                    }
        //                    signInStatu = SignInStatus.Failure;
        //                }
        //                else
        //                {
        //                    Task<IdentityResult> cultureAwaiter5 = _userManager.ResetAccessFailedCountAsync(tUser.Id);
        //                    await cultureAwaiter5;
        //                    Task<SignInStatus> cultureAwaiter6 = this.SignInOrTwoFactor(tUser, isPersistent);
        //                    signInStatu = await cultureAwaiter6;
        //                }
        //            }
        //            else
        //            {
        //                signInStatu = SignInStatus.LockedOut;
        //            }
        //        }
        //        else
        //        {
        //            signInStatu = SignInStatus.Failure;
        //        }
        //    }
        //    else
        //    {
        //        signInStatu = SignInStatus.Failure;
        //    }
        //    return signInStatu;
        //}
        //private async Task<SignInStatus> SignInOrTwoFactor(User user, bool isPersistent)
        //{
        //    SignInStatus signInStatu;
        //    string str = Convert.ToString(user.Id);
        //    Task<bool> cultureAwaiter = _userManager.GetTwoFactorEnabledAsync(user.Id);
        //    //if (await cultureAwaiter)
        //    //{
        //    //    Task<IList<string>> providerAwaiter = _userManager.GetValidTwoFactorProvidersAsync(user.Id);
        //    //    IList<string> listProviders = await providerAwaiter;
        //    //    if (listProviders.Count > 0)
        //    //    {
        //    //        Task<bool> cultureAwaiter2 = AuthenticationManagerExtensions.TwoFactorBrowserRememberedAsync(this.AuthenticationManager, str);
        //    //        if (!await cultureAwaiter2)
        //    //        {
        //    //            ClaimsIdentity claimsIdentity = new ClaimsIdentity("TwoFactorCookie");
        //    //            claimsIdentity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", str));
        //    //            this.AuthenticationManager.SignIn(new ClaimsIdentity[] { claimsIdentity });
        //    //            signInStatu = SignInStatus.RequiresVerification;
        //    //            return signInStatu;
        //    //        }
        //    //    }
        //    //}
        //    Task cultureAwaiter3 = this.SignInAsync(user, isPersistent, false);
        //    await cultureAwaiter3;
        //    signInStatu = SignInStatus.Success;
        //    return signInStatu;
        //}

        /// <summary>
        /// How to refresh authentication cookies
        /// </summary>
        public async Task RefreshSignInAsync(User user, bool isPersistent)
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            // await _userManager.UpdateSecurityStampAsync(user.Id).ConfigureAwait(false); // = used for SignOutEverywhere functionality
            var claimsIdentity = await _userManager.GenerateUserIdentityAsync(user).ConfigureAwait(false);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, claimsIdentity);
        }

    }
}
