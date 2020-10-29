namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Settings")]
    public partial class Settings
    {             

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        //[StringLength(50)]
        public int PostBasePrice { get; set; }

        public int PostPricePerUnit { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }



    }
}
