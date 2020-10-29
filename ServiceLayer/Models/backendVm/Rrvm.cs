using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.backendVm
{
    public class Rrvm
    {
        public int RuleId { get; set; }
        public int roleRuleId { get; set; }
        public byte? seq { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string  Action { get; set; }
        public string Controller { get; set; }
        public string  Area { get; set; }
    }
    
}
