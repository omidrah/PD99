namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kelass")]
    public partial class Kelass
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KelassId { get; set; }
        public int DourseId { get; set; }
    
        public int MasterId { get; set; }
        public int? MahaleBargozariId { get; set; }
        public int? Cost { get; set; }
       
        [Range(0, 100)]
        public byte? OffPercent { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? HasVideo { get; set; }

        public bool? IsOnline { get; set; }
        public bool? IsHozori { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<KelassDooreh> KelassDooreh { get; set; }

        public virtual Doruses Doruses { get; set; }

        public virtual MahaleBargozari MahaleBargozari { get; set; }

        public virtual Master Masters { get; set; }
    }
}
