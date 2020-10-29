namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KelassDooreh")]
    public partial class KelassDooreh
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KelassDoorehId { get; set; }
        public int KelassId { get; set; }
        public int DoorehaId { get; set; }
        public int? Cost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? HasVideo { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public virtual Kelass Kelass { get; set; }
        public virtual Dooreha Dooreha { get; set; }
    }
}
