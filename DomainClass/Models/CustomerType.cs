namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerType")]
    public partial class CustomerType
    {             

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }
        public bool? IsDeleted { get; set; }


        public virtual ICollection<Customer> Customers { get; set; }
    }
}
