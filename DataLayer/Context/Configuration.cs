using System.Data.Entity.Migrations;
using DomainClass.Models;
using System.Linq;

namespace DataLayer.Context
{    
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(ApplicationDbContext context)
        {
        //    context.CustomerType.AddOrUpdate( x=> new {x.Title},
        //        new CustomerType { Title = "آزاد"},
        //        new CustomerType { Title = "بسیج" },
        //        new CustomerType { Title = "شاهد" },
        //        new CustomerType { Title = "دانشجو" }
        //        );
        //    context.Roles.AddOrUpdate(x=>new {x.Title,x.Name},
        //        new Role { Title ="مدیر",Name = "Admin"},
        //        new Role { Title = "گروه آموزشی ",Name = "TrainGroup"},
        //        new Role { Title = "گروه اجرایی - انبار",Name="AnbarGroup" },
        //        new Role { Title = "اداری" ,Name="Office"},
        //        new Role { Title = "مشتری" ,Name="Customer"}
        //        );
        //    context.Rules.AddOrUpdate(x => new {/* x.IsActive,x.IsBackendOrFrontEnd,*/ x.ParentId, x.RuleIcon, x.RuleArea, x.RuleTitle, x.RuleController, x.RuleAction/*, x.Sequence*/ },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-home,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "داشبورد",
        //            RuleController = "Home",
        //            RuleAction = "Index",
        //            Sequence = 1
        //        },
                
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-address-book,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "اطلاعات کاربر",
        //            RuleController = "Profile",
        //            RuleAction = "Index",
        //            Sequence = 1
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-group,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "گروه منابع",
        //            RuleController = "ProductGroups",
        //            RuleAction = "Index",
        //            Sequence = 1
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-picture-o,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "گالری",
        //            RuleController = "Galleries",
        //            RuleAction = "Index",
        //            Sequence = 2
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-archive,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "منابع",
        //            RuleController = "Products",
        //            RuleAction = "Index",
        //            Sequence = 1
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-newspaper-o,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "گروه خبر",
        //            RuleController = "NewsGroup",
        //            RuleAction = "Index",
        //            Sequence = 3
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-newspaper-o,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "خبر",
        //            RuleController = "News",
        //            RuleAction = "Index",
        //            Sequence = 3
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-user,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "مدیریت پرسنل",
        //            RuleController = "Users",
        //            RuleAction = "Index",
        //            Sequence = 4
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-blackberry,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "مدیریت مشتریان",
        //            RuleController = "Customers",
        //            RuleAction = "Index",
        //            Sequence = 4
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-eye-slash,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "مدیریت اساتید",
        //            RuleController = "Masters",
        //            RuleAction = "Index",
        //            Sequence = 4
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-file-text,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "صفحات",
        //            RuleController = "pages",
        //            RuleAction = "Index",
        //            Sequence = 5
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-table,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "گروه صفحات",
        //            RuleController = "pageGroups",
        //            RuleAction = "Index",
        //            Sequence = 5
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-bookmark,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "دوره های حضوری",
        //            RuleController = "Dooreh",
        //            RuleAction = "Index",
        //            Sequence = 6
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-building,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "پکیج های مکاتبه ای",
        //            RuleController = "Packages",
        //            RuleAction = "Index",
        //            Sequence = 7
        //        },
        //        new Rule
        //        {
        //            IsActive = true,
        //            IsBackendOrFrontEnd = true,
        //            ParentId = null,
        //            RuleIcon = "fa-book,bg-olive",
        //            RuleArea = "Admin",
        //            RuleTitle = "دروس",
        //            RuleController = "Doruses",
        //            RuleAction = "Index",
        //            Sequence = 8
        //        }
        //        );
            
        //    context.SaveChanges();
        }
    }
}
