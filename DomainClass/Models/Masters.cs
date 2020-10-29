namespace DomainClass.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
 

    public partial class Master
    {
        [Key]        
        public int UserId { get; set; }

        [StringLength(20)]
        public string Rotbe { get; set; } //دکتر، متخصص ، فلوشیپ

        public string Takhasos { get; set; } //اندوسکوپی، قلب ، ...
        public string Bio { get; set; } //زندگینامه

        public bool? IsHeiatElmi { get; set; } //هیئت علمی
        
        public virtual ICollection<DoorehaDorouse> DoorehaDorouse { get; set; }
        public virtual User  Users { get; set; }
    }
}
