using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DomainClass.Models
{
    public class TagsItem
    {
        [Key]
        public int TagItemId { get; set; }
        public int tagId { get; set; }
        public int itemId { get; set; }
        public string itemType { get; set; } 
        public int? CreatedBy { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public int? Modifyby { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
       // public virtual Tags Tags { get; set; }
    }
}