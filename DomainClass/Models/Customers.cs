namespace DomainClass.Models
{
    public partial class Customer 
    {
        public int UserId { get; set; }
        public string PanvandeNo { get; set; }

        public byte? CustomerTypeId { get; set; }

        public bool IsMadrakTaeed { get; set; }
        public string ImgDocPath { get; set; }

        public bool IsMadrakChecked { get; set; }
        public virtual CustomerType CustomerType { get; set; }

        public virtual User Users { get; set; }
    }
}
