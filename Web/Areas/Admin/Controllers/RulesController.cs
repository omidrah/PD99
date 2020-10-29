using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Web.Mvc;
using DataLayer.Context;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.backendVm;

namespace Web.Areas.Admin.Controllers
{
    public partial class RulesController : Controller
    {
        private readonly IApplicationRoleManager _role;
        private readonly IRuleService _rule;
        private readonly IRuleRoleService _ruleRole;
        private readonly IUnitOfWork _uow;

        public RulesController(IUnitOfWork uow, IApplicationRoleManager role, IRuleRoleService ruleRole,
            IRuleService rule)
        {
            _uow = uow;
            _role = role;
            _ruleRole = ruleRole;
            _rule = rule;
        }

        public virtual ActionResult Index()
        {
            return View(_rule.GetAllParent());
        }

        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var curRule = _rule.Find((int)id);
            if (curRule == null)
            {
                return HttpNotFound();
            }
            var retobj = new ruleParent
            {
                ruleParentId = curRule.RuleId,
                ruleParentTitle = curRule.RuleTitle,
                childRule = new List<ruleChild>()
            };
            foreach (var tmp in _rule.GetChildByParentId(curRule.RuleId))
            {
                retobj.childRule.Add(
                    new ruleChild
                    {
                        ruleChildAction = tmp.RuleAction,
                        ruleChildContorller = tmp.RuleController,
                        ruleChildSequence = tmp.Sequence,
                        rulechildId = tmp.RuleId,
                        rulechildTitle = tmp.RuleTitle
                    });
            }
            return View(retobj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(ruleParent editrules)
        {
            if (ModelState.IsValid)
            {
                var dbRules = _rule;
                foreach (var tmp in editrules.childRule)
                {
                    var selectedRule = dbRules.Find(tmp.rulechildId);
                    // ReSharper disable once PossibleNullReferenceException
                    selectedRule.RuleTitle = tmp.rulechildTitle;
                    selectedRule.RuleAction = tmp.ruleChildAction;
                    selectedRule.RuleController = tmp.ruleChildContorller;

                    selectedRule.Sequence = tmp.ruleChildSequence ?? 0;
                }
                /*change parent title*/
                var parentRUle = dbRules.Find(editrules.ruleParentId);
                Debug.Assert(parentRUle != null, "parentRUle != null");
                parentRUle.RuleTitle = editrules.ruleParentTitle;

                try
                {
                    _uow.SaveAllChanges();
                    TempData["msg"] = "<script>showNotification('عملیات با موفقیت انجام گردید','success');</script>";
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                validationError.PropertyName,
                                validationError.ErrorMessage);
                        }
                    }
                }
            }
            TempData["msg"] = "<script>showNotification('خطا در عملیات','error');</script>";
            return View(editrules);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}