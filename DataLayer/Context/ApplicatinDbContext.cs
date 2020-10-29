using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DomainClass.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>, IUnitOfWork
    {
        public ApplicationDbContext(): base("ParsianDanesh")
        {

        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<CustomerType> CustomerType { get; set; }
        public virtual DbSet<Dooreha> Dooreha { get; set; }
        public virtual DbSet<DoorehaDorouse> DoorehaDorouse { get; set; }
        public virtual DbSet<Doruses> Doruses { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<MahaleBargozari> MahaleBargozari { get; set; }
        public virtual DbSet<Master> Masters { get; set; }
        public virtual DbSet<PackageDooreha> PackageDooreha { get; set; }
        public virtual DbSet<PackageProduct> PackageProducts { get; set; }
        public virtual DbSet<Packages> Packages { get; set; }
        public virtual DbSet<Personel> Personels { get; set; }
        public virtual DbSet<PriceList> PriceList { get; set; }
        public virtual DbSet<ProductGroup> ProductGroup { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RuleRole> RuleRoles { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<RoleMenu> RoleMenus { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<Gallery> Gallery { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<TagsItem> TagsItem { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<PaymentLog> PaymentLogs { get; set; }
        public virtual DbSet<PaymentUniqueNumber> PaymentUniqueNumbers { get; set; }
        public virtual DbSet<Kelass> Kelasses { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User").Property(x => x.Id).HasColumnName("UserId");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim").Property(x => x.Id).HasColumnName("UserclaimId");
            modelBuilder.Entity<Role>().ToTable("Role").Property(x => x.Id).HasColumnName("RoleId");

            modelBuilder.Entity<Customer>().HasKey(x => x.UserId);
            modelBuilder.Entity<Master>().HasKey(x => x.UserId);
            modelBuilder.Entity<Personel>().HasKey(x => x.UserId);
        //   modelBuilder.Entity<Category>().HasKey(x => x.CategoryId);

            modelBuilder.Entity<Customer>().HasRequired(x => x.Users).WithOptional(x => x.Customers);
            modelBuilder.Entity<Master>().HasRequired(x => x.Users).WithOptional(x => x.Masters);
            modelBuilder.Entity<Personel>().HasRequired(x => x.Users).WithOptional(x => x.Personels);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void ForceDatabaseInitialize()
        {
           this.Database.Initialize(force: true);
        }

        public System.Data.Entity.DbSet<DomainClass.Models.ProductFeatures> ProductFeatures { get; set; }

        public System.Data.Entity.DbSet<DomainClass.Models.ProductGroupFeatures> ProductGroupFeatures { get; set; }
    }
}
