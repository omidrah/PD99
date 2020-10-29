using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainClass.Models;
using ServiceLayer.Models.backendVm;
using ServiceLayer.Models.Enum;

namespace ServiceLayer.Interfaces
{
    public interface IRuleRoleService 
    {
        RuleRole Add(RuleRole item);
        void Remove(RuleRole item);
        void Remove(int Id);
         IList<RuleRole> GetAll();
        IList<Rrvm> GetAllByRoleId(int roleId);
    }
}