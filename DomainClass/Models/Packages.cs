namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Packages
    {
    
        public Packages()
        {
            PackageDooreha = new HashSet<PackageDooreha>();
            PackageProducts = new HashSet<PackageProduct>();
            Images = new HashSet<Image>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [StringLength(150)]
        public string PackageTitle { get; set; }

        [StringLength(300)]

        public string PackageDescription { get; set; }

       // [StringLength(300)]

        public string Summery { get; set; }

        [StringLength(300)]

        public string KeyWord { get; set; }

        [StringLength(300)]

        public string ImageName { get; set; }

        public int? Cost { get; set; }
        public int? Weight { get; set; }
        [Range(0, 100)]
        public byte? OffPercent { get; set; }
        public byte? Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifyBy { get; set; }

        public bool PackageType { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<PackageDooreha> PackageDooreha { get; set; }        
        public virtual ICollection<PackageProduct> PackageProducts { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
