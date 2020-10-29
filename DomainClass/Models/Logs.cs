namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Logs
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Action { get; set; }

        [StringLength(50)]
        public string TableName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
