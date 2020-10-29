

using System.ComponentModel.DataAnnotations;

namespace DomainClass.Models
{    
    public partial class PaymentUniqueNumber
    {
        [Key]
        public int PaymentUniqueID { get; set; }
        public int OrderID { get; set; }
    
        public virtual Order Orders { get; set; }
    }
}
