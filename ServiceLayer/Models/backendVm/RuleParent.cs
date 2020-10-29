using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.backendVm
{
    public class ruleParent
    {
        public int ruleParentId { get; set; }

        [Required(ErrorMessage = @"عنوان گروه را وارد کنید")]
        public string ruleParentTitle { get; set; }

        public List<ruleChild> childRule { get; set; }
    }

    public class ruleChild
    {
        public int rulechildId { get; set; }

        [Required(ErrorMessage = @"عنوان  را وارد کنید")]
        public string rulechildTitle { get; set; }

        [Required(ErrorMessage = @"عنوان اکشن را وارد کنید")]
        public string ruleChildAction { get; set; }

        [Required(ErrorMessage = @"عنوان کنترلر را وارد کنید")]
        public string ruleChildContorller { get; set; }

        public byte? ruleChildSequence { get; set; }
    }
}
