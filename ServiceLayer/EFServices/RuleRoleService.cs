using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.backendVm;

namespace ServiceLayer.EFServices
{
    public class RuleRoleService : IRuleRoleService
    {
        private readonly IDbSet<RuleRole> _ruleRoles;
        private readonly IUnitOfWork _uow;

        public RuleRoleService(IUnitOfWork uow)
        {
            _uow = uow;
            _ruleRoles = _uow.Set<RuleRole>();
        }

        public RuleRole Add(RuleRole item)
        {
            return _ruleRoles.Add(item);
        }

        public void Remove(RuleRole item)
        {
            _ruleRoles.Remove(item);
        }

        public void Remove(int Id)
        {
            _ruleRoles.Remove(_ruleRoles.Find(Id));
        }

        public IList<RuleRole> GetAll()
        {
            return  _ruleRoles.ToList();
        }

        public IList<Rrvm> GetAllByRoleId(int roleId)
        {
            return _ruleRoles.Where(x => x.RoleId == roleId).
                Select(x => new Rrvm
                {
                    RuleId = (int)x.RuleId,
                    roleRuleId =x.RuleRoleId,
                    Icon = x.Rules.RuleIcon,
                    Action = x.Rules.RuleAction,
                    Controller = x.Rules.RuleController,
                    Area = x.Rules.RuleArea,
                    Title = x.Rules.RuleTitle,
                    seq= x.Rules.Sequence
                }).OrderBy(x=>x.seq).ToList();
        }
    }
}