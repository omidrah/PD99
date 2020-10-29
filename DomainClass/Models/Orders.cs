namespace DomainClass.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Order
    {    
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public bool IsFinalized { get; set; }

        public bool IsTransfered { get; set; }

        public int  Sum { get; set; }
        
        [Range(0, 100)]
        public byte? OffPercent { get; set; }
        public int OrderStatusId { get; set; }
        public bool? IsDeleted { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual User Users { get; set; }
        public virtual ICollection<PaymentLog> PaymentLogs { get; set; }
        public virtual ICollection<PaymentUniqueNumber> PaymentUniqueNumbers { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }
}
