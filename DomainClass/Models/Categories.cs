using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.Models
{
    [Table("Categories")]
     public partial class Category
    {
        public Category()
        {
            //      this.NewsCategory = new HashSet<NewsCategory>();

        }
        [Key]
        public int CategoryId { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public int? ParentID { get; set; }
        [StringLength(100)]
        public string Image { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        // public virtual ICollection<NewsCategory> NewsCategory { get; set; }

    }
}
