using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainClass.Models
{
    public class Rule
    {
        [Key]
        public int RuleId { get; set; }
        public int? ParentId { get; set; }
        [StringLength(50)]
        public string RuleTitle { get; set; }
        [StringLength(50)]
        public string RuleAction { get; set; }
        [StringLength(50)]
        public string RuleController { get; set; }
        public string RuleArea { get; set; }
        [StringLength(50)]
        public string RuleIcon { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsBackendOrFrontEnd { get; set; }
        public byte? Sequence { get; set; }
        public virtual ICollection<RuleRole> RuleRoles { get; set; }
    }
}