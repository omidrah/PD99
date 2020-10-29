namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dooreha")]
    public partial class Dooreha
    {
        public Dooreha()
        {
            Images = new HashSet<Image>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? Pid { get; set; }

        [StringLength(250)]
        // عنوان دوره
        public string Title { get; set; }

        [StringLength(500)]
        // محتوای آموزشی دوره
        public string Content { get; set; }

        [StringLength(1000)]
        // هدف از برکزاری دوره
        public string Goal { get; set; }

        [StringLength(1000)]
        // پیش نیازهای دوره
        public string Pishniaz { get; set; }

        [StringLength(2000)]
        // فهرست دوره
        public string Appendix { get; set; }

        [StringLength(1000)]
        // روزهای برگزاری دوره: شنبه و دوشنبه
        public string BargozariDay { get; set; }
        // قیمت دوره
        public int? Cost { get; set; }
        
        [Range(0, 100)]
        public byte? OffPercent { get; set; }

        [StringLength(250)]
        // ساعت برگزاری دوره
        public string BargozariTime { get; set; }
        [StringLength(300)]
        public string KeyWord { get; set; }
        public DateTime? DoorehStartDate { get; set; }

        public DateTime? DoorehEndDate { get; set; }

        public bool? IsOnline { get; set; }
        public bool? IsHozori { get; set; }

        public bool? IsActive { get; set; }
        [StringLength(350)]
        public string ImageName { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }
        public virtual ICollection<DoorehaDorouse> DoorehaDorouse { get; set; }
        public virtual ICollection<PackageDooreha> PackageDooreha { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<KelassDooreh> KelassDooreh { get; set; }
    }
}
