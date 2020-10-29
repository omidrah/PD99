using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClass.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using ServiceLayer.EFServices;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<User, int>, IApplicationUserManager
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IApplicationRoleManager _roleManager;
        private readonly ICustomUserStore _store;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<User> _users;
        private readonly Lazy<Func<IIdentity>> _identity;
        private readonly IRuleRoleService _ruleRole;
        private readonly IRuleService _rule;
        private User _user;
        public ApplicationUserManager(
           ICustomUserStore store,
           IUnitOfWork uow,
           Lazy<Func<IIdentity>> identity, // For lazy loading -> Controller gets constructed before the HttpContext has been set by ASP.NET.
           IApplicationRoleManager roleManager,
           IDataProtectionProvider dataProtectionProvider,
           IIdentityMessageService smsService,
           IRuleRoleService ruleRole,
           IRuleService Rule,
           IIdentityMessageService emailService)
           : base((IUserStore<User, int>)store)
        {
            _store = store;
            _uow = uow;
            _identity = identity;
            _users = _uow.Set<User>();
            _roleManager = roleManager;
            _ruleRole = ruleRole;
            _rule = Rule;
            _dataProtectionProvider = dataProtectionProvider;
            this.SmsService = smsService;
            this.EmailService = emailService;
            createApplicationUserManager();
        }
        public User FindById(int userId)
        {
            return _users.Find(userId);
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("user-email", applicationUser.Email));
            userIdentity.AddClaim(new Claim("fullname", applicationUser.Name + " " + applicationUser.Family));
            userIdentity.AddClaim(new Claim("lastlogindate", applicationUser.LastDateLogin == null ? DateTime.Now.ToShortDateString() : ((DateTime)applicationUser.LastDateLogin).ToShortDateString()));
            userIdentity.AddClaim(new Claim("userimg", applicationUser.ImgPath == null ? "~/Content/img/ProfileDefault.png" : applicationUser.ImgPath));
            string scope = string.Empty;
            var rolesIdForCurrentUser = applicationUser.Roles.Select(x => x.RoleId);
            foreach (var id in rolesIdForCurrentUser)
            {
                foreach (var item in _ruleRole.GetAllByRoleId(id))
                {
                    //read roles of user and add to scop if not repeated
                    string[] scopes = scope.Split('~');
                    var thisScope = item.Area + "!" + item.Controller + "!" + item.Action + "!" + item.Icon + "!" + item.Title;
                    if(!scopes.Contains(thisScope))
                    {
                        scope += thisScope + "~";
                    }
                }
            }

            userIdentity.AddClaim(new Claim("scope", scope));
            var userROles = _roleManager.FindUserRoles(applicationUser.Id).ToList();
            string rolestring = string.Empty;
            foreach (var tmp in userROles)
            {
                rolestring += tmp.Name + ",";
            }
            userIdentity.AddClaim(new Claim("role", rolestring));
            //userIdentity.AddClaim(new Claim(ClaimTypes.Role,rolestring));
            return userIdentity;
        }
        public Task<List<User>> GetAllUsersAsync()
        {
            return this.Users.ToListAsync();
        }
        public User GetCurrentUser()
        {
            return _user ?? (_user = this.FindById(GetCurrentUserId()));
        }
        public async Task<User> GetCurrentUserAsync()
        {
            return _user ?? (_user = await this.FindByIdAsync(GetCurrentUserId()).ConfigureAwait(false));
        }
        public int GetCurrentUserId()
        {
            return _identity.Value().GetUserId<int>();
        }
        public async Task<bool> HasPassword(int userId)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            return user != null && user.PasswordHash != null;
        }
        public async Task<bool> HasPhoneNumber(int userId)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            return user != null && user.PhoneNumber != null;
        }
        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User, int>(
                         validateInterval: TimeSpan.FromSeconds(0),
                         regenerateIdentityCallback: (manager, user) => manager.GenerateUserIdentityAsync(user),
                         getUserIdCallback: claimsIdentity => claimsIdentity.GetUserId<int>());
        }
        public void SeedDatabase()
        {
            if (_roleManager.GetAllRolesAsync().Result.Count <= 0)
            {
                InitRole();
            }
            if (_ruleRole.GetAll().Count <= 0)
            {
                InitRule(_roleManager.FindRoleByName("Admin").Id);
            }
            var user = this.FindByEmail("omid_rhm@yahoo.com");
            if (user == null)
            {
                user = new User
                {
                    UserName = "omid_rhm@yahoo.com",
                    Email = "omid_rhm@yahoo.com",
                    CreatedDate = DateTime.Now,
                    Family = "رحیمی",
                    Name = "امید",
                    Address = "همین ورا",
                    IsActive = true,
                    NationCode = "0010020030",
                    PhoneNumber = "09123715763",
                    Tel = "02133888890"
                };

                var createResult = this.Create(user, "123456");
                if (!createResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Join(", ", createResult.Errors));
                }

                // Add user admin to Role Admin
                this.AddToRole(user.Id, "Admin");
                var setLockoutResult = this.SetLockoutEnabled(user.Id, false);
                if (!setLockoutResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Join(", ", setLockoutResult.Errors));
                }



            }

        }
        private void InitRole()
        {
            //add roles to db
            var rNew = new Role("Admin", "مدیر سیستم");
            _roleManager.CreateRole(rNew);
            InitRule(rNew.Id);
            rNew = new Role("Operator", "کاربر");
            _roleManager.CreateRole(rNew);
            rNew = new Role("Customer", "مشتری");
            _roleManager.CreateRole(rNew);
            rNew = new Role("Master", "استاد");
            _roleManager.CreateRole(rNew);
            rNew = new Role("Crm", "مدیریت مشتریان");//منظور مدیریت اساتید و مشتریان
            _roleManager.CreateRole(rNew);
            rNew = new Role("Stockman", "انبار دار");//
            _roleManager.CreateRole(rNew);
            rNew = new Role("SiteAdmin", "مسئول سایت");//منظور مدیریت صفحات وب و ثبت و ضبط اطلاعات  کالاها و اخبار
            _roleManager.CreateRole(rNew);
        }

        private void InitRule(int RoleAdminId)
        {
            foreach (var item in _rule.GetAll())
            {
                _ruleRole.Add(new RuleRole { RoleId = RoleAdminId, RuleId = item.RuleId });
            }
            _uow.SaveAllChanges();
        }

        private void createApplicationUserManager()
        {

            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                //RequireNonLetterOrDigit = true,
                //RequireDigit = true,
                //RequireLowercase = true,
                //RequireUppercase = true,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            this.RegisterTwoFactorProvider("تلفن همراه", new PhoneNumberTokenProvider<User, int>
            {
                MessageFormat = "کد تایید شما در پارسیان دانش {0}"
            });
            this.RegisterTwoFactorProvider("رایانامه", new EmailTokenProvider<User, int>
            {
                Subject = "کد  احراز هویت",
                BodyFormat = "کد تایید شما در پارسیان دانش {0}"
            });
            this.EmailService = new EmailService();
            this.SmsService = new SmsService();
            if (_dataProtectionProvider != null)
            {
                var dataProtector = _dataProtectionProvider.Create("ParsianDanesh@9610");
                this.UserTokenProvider =
                    new DataProtectorTokenProvider<User, int>(dataProtector);
            }
        }
    }

}
