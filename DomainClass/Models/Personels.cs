namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Personel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [StringLength(10)]
        public string PersonliNo { get; set; }

        public int? Age { get; set; }

        public bool? HasInsurance { get; set; }

        public bool? IsRasmi { get; set; }

        public DateTime? SalaryDate { get; set; }

        [StringLength(150)]
        public string MadrakTashili { get; set; }

        public virtual User Users { get; set; }
    }
}
