
namespace ServiceLayer.Models.backendVm
{
        public class CheckBoxItem
        {
            public int Id { get; set; }
            public int? ParentId { get; set; }
            public string Title { get; set; }
            public bool IsChecked { get; set; }
            //public byte? Sequence { get; set; }
        }
   
}