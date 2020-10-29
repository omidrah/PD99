namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PackageProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PackageId { get; set; }

        public int ProductId { get; set; }

        public bool? IsDeleted { get; set; }
        public virtual Packages Packages { get; set; }

        public virtual Product Products { get; set; }
    }
}
