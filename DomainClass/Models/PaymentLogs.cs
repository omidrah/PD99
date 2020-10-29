
namespace DomainClass.Models
{
    public partial class PaymentLog
    {
        public int PaymentLogID { get; set; }
        public int OrderID { get; set; }
        public string TrackingCode { get; set; }
        public string PaymentResponseCode { get; set; }
        public string PaymentResponseMessage { get; set; }
        public bool IsSuccessful { get; set; }
        public bool? IsDeleted { get; set; }
        public System.DateTime PaymentDate { get; set; }    
        public virtual Order Orders { get; set; }
    }
}
