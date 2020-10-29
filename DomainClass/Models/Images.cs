namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        
        //public ProductImages()
        //{
        //    PackageProducts = new HashSet<PackageProduct>();
        //    PriceList = new HashSet<PriceList>();
        //}
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ImageID { get; set; }
        public int? ItemId { get; set; }
        public string ImagePath { get; set; }
        /// <summary>
        /// کالا= product
        /// دوره= Dooreh
        /// بسته = Package
        /// کاربر= User
        /// 
        /// </summary>
        public string Constant { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public string ImageThumbPath { get; set; }

        public bool? IsActive { get; set; }
        public string ImagePostfix { get; set; }

        public string ImageName { get; set; }
        public int? Sequence { get; set; }
        public int? ImageSize { get; set; }

        public bool? IsDeleted { get; set; }
        public int? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual Product Products { get; set; }
        public virtual Packages Packages { get; set; }
        public virtual Dooreha Dooreha { get; set; }
        //public virtual User  { get; set; }
    }

}
