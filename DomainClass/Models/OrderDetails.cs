namespace DomainClass.Models
{
    
    public partial class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int? OrderDetailParenID { get; set; }
        public int OrderID { get; set; }
        public int ItemId { get; set; }

        public string ItemType { get; set; }

        public bool IsTransfered { get; set; }

        public bool ShowForCusomer { get; set; }



        public int Price { get; set; }
        public int OrderedCount { get; set; }
    
        public virtual Order Orders { get; set; }
       // public virtual Product Products { get; set; }
    }
}
