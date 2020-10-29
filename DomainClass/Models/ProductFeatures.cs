namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductFeatures
    {

        [Key]

        public int ProductFeatureValueID { get; set; }
        public int FeatureId { get; set; }
        public int ProductId { get; set; }
        public string FeatureValue { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual Features Features { get; set; }

        public virtual Product Products { get; set; }
    }
}
