using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomainClass.Models
{
    public class Tags
    {
        [Key]
        public int TagId { get; set; }

        [StringLength(150)]
        public string tagTitle { get; set; }
        public int itemId { get; set; }
        [StringLength(150)]
        public string tagConstant { get; set; } //products,dooreh,package
        public int? CreatedBy { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public int? Modifyby { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
        //  public virtual ICollection<TagsItem> TagsItems { get; set; }
    }
}