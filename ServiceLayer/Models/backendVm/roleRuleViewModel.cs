using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Models.backendVm
{
    public class RoleRuleViewModel
    {
        public int roleId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "نام نقش را وارد کنید")]
        public string RoleTitle { get; set; }

        // [UIHint("CheckBoxListItem")]
        //public List<Rule> ParentRules { get; set; }
        public List<CheckBoxItem> ChildRules { get; set; }
    }

   
}