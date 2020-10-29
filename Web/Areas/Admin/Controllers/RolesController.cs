using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models.backendVm;

namespace Web.Areas.Admin.Controllers
{
    public partial class RolesController : Controller
    {
        private readonly IApplicationRoleManager _role;
        private readonly IRuleService _rule;
        private readonly IRuleRoleService _ruleRole;
        private readonly IUnitOfWork _uow;

        public RolesController(IUnitOfWork uow, IApplicationRoleManager role, IRuleRoleService ruleRole,
            IRuleService rule)
        {
            _uow = uow;
            _role = role;
            _ruleRole = ruleRole;
            _rule = rule;
        }

        // GET: Roles
        public virtual async Task<ActionResult> Index()
        {
            var d = await _role.GetAllRolesAsync();
            return View(d);
        }

        public virtual async Task<ActionResult> Create()
        {
            ViewBag.ParentRules = _rule.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(string roleTitle, List<int> selectedRule)
        {
            if (string.IsNullOrEmpty(roleTitle))
            {
                ModelState.AddModelError("roleTitle", "عنوان نقش را وارد کنید");
            }
            if (selectedRule == null)
            {
                ModelState.AddModelError("roleTitle", "دسترسی برای نقش انتخاب نشده است");
            }
            if (await _role.FindByNameAsync(roleTitle) != null)
            {
                ModelState.AddModelError("roleTitle", "این عنوان برای نقش قبلن انتخاب شده است");
            }
            //if (_role.GetRolesForUser(_user.GetCurrentUserId()).Contains(roleTitle))
            //{
            //    ModelState.AddModelError("roleTitle", "این نقش به این کاربر انتساب داده شده است");
            //}
            if (ModelState.IsValid)
            {
                var newRole = new Role { Title = roleTitle, Name = Guid.NewGuid().ToString("N").Substring(1, 8) };
                var result = await _role.CreateAsync(newRole);
                if (result.Succeeded && selectedRule.Count > 0)
                {
                    foreach (var tmp in selectedRule)
                    {
                        _ruleRole.Add(new RuleRole { RoleId = newRole.Id, RuleId = tmp });
                    }
                }
                _uow.SaveAllChanges();
                TempData["msg"] = "<script>showNotification('عملیات با موفقیت انجام گردید','success');</script>";
                return RedirectToAction("Index");
            }
            ViewBag.ParentRules = _ruleRole.GetAll();
            return View();
        }

        // GET: Roles/Edit/5
        public virtual async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var selectedRole = await _role.FindByIdAsync((int)id);
            if (selectedRole == null)
            {
                return HttpNotFound();
            }
            var d = GetRulesOfThisRole((int)id, selectedRole.Title);
            return View(d);
        }

        /// <summary>
        ///     لیست دسترسی های منتسب به یک نقش را برمی گرداند
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private RoleRuleViewModel GetRulesOfThisRole(int id, string roleTitle)
        {
            var selectedRule = _ruleRole.GetAllByRoleId(id); //list of rule assign to this role
            var allRules = _rule.GetAll(); //list all of rules
            var retObj = new RoleRuleViewModel //object return
            {
                roleId = id,
                RoleTitle = roleTitle,
                ChildRules = allRules.Select(
                    aa =>
                        new CheckBoxItem
                        {
                            Id = aa.RuleId,
                            ParentId = aa.ParentId,
                            Title = aa.RuleTitle,
                            IsChecked = false
                        }).ToList()
            };


            foreach (var tmp in selectedRule) //check rule of this rule
            {
                retObj.ChildRules.FirstOrDefault(ll => ll.Id == tmp.RuleId).IsChecked = true;
            }
            //ViewBag.ParentRules = allRules.Where(ll => ll.ParentId == null).ToList();
            return retObj;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(RoleRuleViewModel roles, List<int> selectedRules)
        {
            if (ModelState.IsValid)
            {
                var selectedRole = await _role.FindByIdAsync(roles.roleId);
                selectedRole.Title = roles.RoleTitle;
                /*remove previos rules for this role*/
                var preRulesRole = _ruleRole.GetAllByRoleId(roles.roleId);
                foreach (var tmp in preRulesRole)
                {
                    _ruleRole.Remove(tmp.roleRuleId);
                }
                /*Add new Rules for this role*/
                foreach (var tmp in selectedRules)
                {
                    _ruleRole.Add(new RuleRole { RoleId = roles.roleId, RuleId = tmp });
                }
                TempData["msg"] = "<script>showNotification('عملیات با موفقیت انجام گردید','success');</script>";
                _uow.SaveAllChanges();
                return RedirectToAction("Index");
            }            
            var d = GetRulesOfThisRole(roles.roleId, roles.RoleTitle);
            return View(d);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        public virtual async Task<ActionResult> DeleteConfirmed(int id)
        {
            var ruleThisRole = _ruleRole.GetAllByRoleId(id);
            if (ruleThisRole.Any() || _role.GetUsersInRoleById(id).Count <= 0)
            {
                return Json(new { res = false, code = 2, msg = "این نقش سابقه داشته وقابل حذف کردن نمی باشد" });
            }
            foreach (var tmp in ruleThisRole)
            {
                /*remove relation with rules*/
                _ruleRole.Remove(tmp.RuleId);
            }
            //remove role 
            await _role.DeleteAsync(await _role.FindByIdAsync(id));
            _uow.SaveAllChanges();
            return Json(new { res = true, code = 1, msg = "نقش موردنظر حذف گردید" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
                _role.Dispose();

            }
            base.Dispose(disposing);
        }
    }
}