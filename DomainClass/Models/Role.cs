using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DomainClass.Models
{
    public class Role : IdentityRole<int, UserRole>
    {
        public Role()
        {
        }
        public Role(string name)
        {
            Name = name;
        }
        public Role(string name, string title)
        {
            Name = name;
            Title = title;
        }

        [MaxLength(25)]
        public string Title { get; set; }

        public virtual ICollection<RuleRole> RuleRoles { get; set; }
        public virtual ICollection<RoleMenu> RuleMenus { get; set; }
    }
}