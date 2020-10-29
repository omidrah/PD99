namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Features
    {
        
        [Key]
        public int FeatureId { get; set; }

        [Required]
        [StringLength(250)]
        public string FeaturesName { get; set; }

        //[Required]
        [StringLength(250)]
        public string featuresDispaly { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifyBy { get; set; }
        public virtual ICollection<ProductFeatures> ProductFeatures { get; set; }

        public virtual ICollection<ProductGroupFeatures> ProductGroupFeatures { get; set; }


    }
}
