using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DomainClass.Models
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Family { get; set; }

        [StringLength(10)]
        public string NationCode { get; set; }
        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(15)]
        public string Tel { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        [StringLength(650)]
        public string Address { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public byte? UserStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsLocked { get; set; }

        public DateTime? LastDateLogin { get; set; }
		public DateTime? BirthDate { get; set; }

        public string ImgPath { get; set; }

        public virtual Customer Customers { get; set; }

        public virtual Master Masters { get; set; }

        public virtual Personel Personels { get; set; }
        public string confirmCode { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User,int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here            
            return userIdentity;
        }
    }
}
  