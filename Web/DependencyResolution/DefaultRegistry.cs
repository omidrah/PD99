// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.DependencyResolution {
    using DataLayer.Context;
    using DomainClass.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataProtection;
    using Providers;
    using ServiceLayer;
    using ServiceLayer.EFServices;
    using ServiceLayer.Interfaces;
    using StructureMap;
    using StructureMap.Web;
    using System;
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;

    public class DefaultRegistry : Registry {
        #region Constructors and Destructors
               
        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });

            For<Microsoft.AspNet.SignalR.IDependencyResolver>()
                  .Singleton()
                  .Add<StructureMapSignalRDependencyResolver>();

            For<IIdentity>()
               .HybridHttpOrThreadLocalScoped()
               .Use(() => getIdentity());

            For<IUnitOfWork>()
                .HybridHttpOrThreadLocalScoped()
                .Use<ApplicationDbContext>();
            // Remove these 2 lines if you want to use a connection string named connectionString1, defined in the web.config file.
            //.Ctor<string>("connectionString")
            //.Is("Data Source=(local);Initial Catalog=TestDbIdentity;Integrated Security = true");


            For<ApplicationDbContext>()
               .HybridHttpOrThreadLocalScoped()
               .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());
            For<DbContext>()
               .HybridHttpOrThreadLocalScoped()
               .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());

            For<IUserStore<User, int>>()
                .HybridHttpOrThreadLocalScoped()
                .Use<CustomUserStore>();            

            For<IRoleStore<Role, int>>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<CustomRoleStore>();
            
            For<IAuthenticationManager>()
               .HybridHttpOrThreadLocalScoped()
               .Use(() => HttpContext.Current.GetOwinContext().Authentication);

            For<IApplicationSignInManager>()
                  .HybridHttpOrThreadLocalScoped()
                  .Use<ApplicationSignInManager>();


            For<IApplicationRoleManager>()
                  .HybridHttpOrThreadLocalScoped()
                  .Use<ApplicationRoleManager>();
            
            // map same interface to different concrete classes
            For<IIdentityMessageService>().Use<SmsService>();
            For<IIdentityMessageService>().Use<EmailService>();

            For<IApplicationUserManager>()
               .HybridHttpOrThreadLocalScoped()
               .Use<ApplicationUserManager>()
               .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
               .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
               .Setter(userManager => userManager.SmsService).Is<SmsService>()
               .Setter(userManager => userManager.EmailService).Is<EmailService>();

            For<ApplicationUserManager>()
               .HybridHttpOrThreadLocalScoped()
               .Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());

            For<ICustomRoleStore>()
                  .HybridHttpOrThreadLocalScoped()
                  .Use<CustomRoleStore>();

            For<ICustomUserStore>()
                  .HybridHttpOrThreadLocalScoped()
                  .Use<CustomUserStore>();

            //config.For<IDataProtectionProvider>().Use(()=> app.GetDataProtectionProvider()); // In Startup class
            //خطا داد این خط رو اضافه کردم
            For<IDataProtectionProvider>().HybridHttpOrThreadLocalScoped().Use<DpapiDataProtectionProvider>();
            

            For<IGalleryService>().HybridHttpOrThreadLocalScoped().Use<GalleryService>();
            For<IMasterService>().HybridHttpOrThreadLocalScoped().Use<MasterService>();
            For<ICustomerService>().HybridHttpOrThreadLocalScoped().Use<CustomerService>();
            For<IPersonelService>().HybridHttpOrThreadLocalScoped().Use<PersonelService>();
            For<IDourseService>().HybridHttpOrThreadLocalScoped().Use<DourseService>();
            For<IProductService>().HybridHttpOrThreadLocalScoped().Use<ProductService>();
            For<IProductGroupService>().HybridHttpOrThreadLocalScoped().Use<ProductGroupService>();
            For<IMahaleBargozariService>().HybridHttpOrThreadLocalScoped().Use<MahaleBargozariService>();
            For<IDoorehService>().HybridHttpOrThreadLocalScoped().Use<DoorehService>();
            For<IPackageService>().HybridHttpOrThreadLocalScoped().Use<PackageService>();
            For<IPackageProductService>().HybridHttpOrThreadLocalScoped().Use<PackageProductService>();
            For<IDoorehDorusService>().HybridHttpOrThreadLocalScoped().Use<DoorehDorusService>();
            For<ICustomerTypeServic>().HybridHttpOrThreadLocalScoped().Use<CustomerTypeServic>();
            For<IRuleRoleService>().HybridHttpOrThreadLocalScoped().Use<RuleRoleService>();
            For<IRoleMenuService>().HybridHttpOrThreadLocalScoped().Use<RoleMenuService>();
            For<IRuleService>().HybridHttpOrThreadLocalScoped().Use<RuleService>();
            For<IMenuService>().HybridHttpOrThreadLocalScoped().Use<MenuService>();
            For<IImageService>().HybridHttpOrThreadLocalScoped().Use<ImageService>();
            For<IRoleService>().HybridHttpOrThreadLocalScoped().Use<RoleService>();
            For<INewsService>().HybridHttpOrThreadLocalScoped().Use<NewsService>();
            For<ICommentService>().HybridHttpOrThreadLocalScoped().Use<CommentService>();
            For<ITagService>().HybridHttpOrThreadLocalScoped().Use<TagsService>();
            For<IFeaturesService>().HybridHttpOrThreadLocalScoped().Use<FeaturesService>();
            For<IProductGroupFeaturesService>().HybridHttpOrThreadLocalScoped().Use<ProductGroupFeaturesService>();
            For<IProductFeaturesService>().HybridHttpOrThreadLocalScoped().Use<ProductFeaturesService>();
            For<IOrderService>().HybridHttpOrThreadLocalScoped().Use<OrderService>();
            For<IOrderStatusService>().HybridHttpOrThreadLocalScoped().Use<OrderStatusService>();
            For<IOrderDetailsService>().HybridHttpOrThreadLocalScoped().Use<OrderDetailsService>();
            For<IKelassService>().HybridHttpOrThreadLocalScoped().Use<KelassService>();
            For<IDoorehKelassService>().HybridHttpOrThreadLocalScoped().Use<DoorehKelassService>();
            For<IPaymentUniqueNumberService>().HybridHttpOrThreadLocalScoped().Use<PaymentUniqueNumberService>();
            For<IPaymentLogService>().HybridHttpOrThreadLocalScoped().Use<PaymentLogService>();
            For<ISettingsService>().HybridHttpOrThreadLocalScoped().Use<SettingsService>();

        }
        private static IIdentity getIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity;
            }

            return ClaimsPrincipal.Current != null ? ClaimsPrincipal.Current.Identity : null;
        }
        #endregion
    }
}