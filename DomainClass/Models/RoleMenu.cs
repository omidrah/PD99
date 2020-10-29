
namespace DomainClass.Models
{

    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class RoleMenu
    {
        [Key]
        public int RoleMenusId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public Nullable<bool> IsActive { get; set; }    
        public virtual Role Role { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
