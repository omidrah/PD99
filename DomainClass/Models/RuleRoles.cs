using System.ComponentModel.DataAnnotations;
namespace DomainClass.Models
{
    public class RuleRole
    {
        [Key]
        public int RuleRoleId { get; set; }
        public int RuleId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Roles { get; set; }
        public virtual Rule Rules { get; set; }
    }
}