namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceList")]
    public partial class PriceList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public byte? Discount { get; set; }

        public int? OrginPrice { get; set; }

        public int? DiscountPrice { get; set; }

        public byte? Status { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Product Products { get; set; }
    }
}
