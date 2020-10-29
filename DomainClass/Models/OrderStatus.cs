namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderStatus")]
    public partial class OrderStatus
    {             

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Order> Orders { get; set; }


    }
}
