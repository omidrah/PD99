using System;
using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IRuleService 
    {
        IList<Rule> GetAll();
        IList<Rule> GetAllParent();
        IList<Rule> GetChildByParentId(int ParentId);
        Rule AddRule(Rule item);
        Rule RemoRule(Rule item);
        bool IsUseRule(Rule item);
        Rule Find(int Id);
    }
}