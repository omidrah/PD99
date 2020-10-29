namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

   
    public partial class ProductGroupFeatures
    {
        
       
        [Key]
        public int ProductGroupFeatuesId { get; set; }

        public int ProductGroupId { get; set; }

        public int FeaturesId { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public virtual Features Features { get; set; }
        public virtual ProductGroup ProductGroups { get; set; }
    }
}
