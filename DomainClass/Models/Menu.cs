
namespace DomainClass.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    
    
    public partial class Menu
    {
        [Key]
        public int MenuId { get; set; }
        public Nullable<int> MenuParentID { get; set; }
        public string MenuTitle { get; set; }
        public string MenuAction { get; set; }
        public string MenuController { get; set; }
        public string MenuUrl { get; set; }
        public string MenuDesc { get; set; }
        public string MenuPic { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> Sequence { get; set; }
        public Nullable<int> Createdby { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string MenuPosition { get; set; }
        public Nullable<int> PageId { get; set; }        
        public virtual ICollection<Menu> Menus1 { get; set; }
        public virtual Menu Menu1 { get; set; }        
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
