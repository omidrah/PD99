namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductGroup")]
    public partial class ProductGroup
    {
        
        public ProductGroup()
        {
            Products = new HashSet<Product>();
        }

        public int ProductGroupId { get; set; }

        [StringLength(50)]
        public string ProductGroupTitle { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        [StringLength(350)]
        public string PicPath { get; set; }

        [StringLength(350)]
        public string PicPathThumbnail { get; set; }        
        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ProductGroupFeatures> ProductGroupFeatures { get; set; }
    }
}
