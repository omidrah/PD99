namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        
        public Product()
        {
            PackageProducts = new HashSet<PackageProduct>();
            PriceList = new HashSet<PriceList>();
            Images = new HashSet<Image>();
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(150)]
        public string ProductName { get; set; }
        [StringLength(300)]
        public string ProductEnglishName { get; set; }

        public int ProductGroupID { get; set; }

        public int? NoghteSefaresh { get; set; }

        public int? ProductMojodi { get; set; }

        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }

        [StringLength(350)]
        public string PicPath { get; set; }

        public int? Price { get; set; }

        public int? Weight { get; set; }
        [Range(0,100)]
        public byte? OffPercent { get; set; }
        public string Summery { get; set; }
        [StringLength(500)]
        public string  Description { get; set; }

        public int? Createdby { get; set; }

        public int? Modifiedby { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public virtual ICollection<PackageProduct> PackageProducts { get; set; }        
        public virtual ICollection<PriceList> PriceList { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ProductFeatures> ProductFeatures { get; set; }
    }
}
