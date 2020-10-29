using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
    public class RuleService : IRuleService
    {
        private readonly IDbSet<RuleRole> _ruleRoles;
        private readonly IDbSet<Rule> _rules;
        private readonly IUnitOfWork _uow;
        public RuleService(IUnitOfWork uow)
        {
            _uow = uow;
            _rules = _uow.Set<Rule>();
            _ruleRoles = uow.Set<RuleRole>();
        }

        public IList<Rule> GetAll()
        {
            return _rules.Where(x => x.IsActive == true).OrderBy(x=>x.Sequence).ToList();
        }

        public IList<Rule> GetAllParent()
        {
            return _rules.Where(x => x.ParentId == null && x.IsActive==true).OrderBy(x => x.Sequence).ToList();
        }

        public IList<Rule> GetChildByParentId(int ParentId)
        {
            return _rules.Where(x => x.ParentId == ParentId && x.IsActive == true).OrderBy(x => x.Sequence).ToList();
        }
        public Rule AddRule(Rule item)
        {
            return _rules.Add(item);
        }

        public Rule RemoRule(Rule item)
        {
            return _rules.Remove(item);
        }

        public bool IsUseRule(Rule item)
        {
            return _ruleRoles.Any(x => x.RuleId == item.RuleId);
        }

        public Rule Find(int Id)
        {
            return _rules.Find(Id);
        }
     
    }
}