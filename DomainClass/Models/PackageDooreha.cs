namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PackageDooreha")]
    public partial class PackageDooreha
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }

        public int DoorehaId { get; set; }

        public int PackageId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Dooreha Dooreha { get; set; }

        public virtual Packages Packages { get; set; }
    }
}
