namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoorehaDorouse")]
    public partial class DoorehaDorouse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DourseId { get; set; }
        public int DoorehaId { get; set; }
        public int MasterId { get; set; }
        public int? MahaleBargozariId { get; set; }
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

        public virtual Dooreha Dooreha { get; set; }

        public virtual Doruses Doruses { get; set; }

        public virtual MahaleBargozari MahaleBargozari { get; set; }

        public virtual Master Masters { get; set; }
    }
}
