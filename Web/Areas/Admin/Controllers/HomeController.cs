using System.Linq;
using System.Web.Mvc;
using DataLayer.Context;
using ServiceLayer.Interfaces;
using System.Security.Claims;
using System.Collections.Generic;
using ServiceLayer.Models.FrontEnd;
using System;

namespace Web.Areas.Admin.Controllers
{
    [Authorize]
    public partial class HomeController : Controller
    {
        private readonly IRoleMenuService _roleMenu;
        private readonly IApplicationUserManager _user;
        private IUnitOfWork _uow;

        public HomeController(IUnitOfWork uow, IApplicationUserManager user, IRoleMenuService roleMenu)
        {
            _uow = uow;
            _user = user;
            _roleMenu = roleMenu;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public virtual ActionResult fillPartial(string partialViewName)
        {
            if (User.Identity.IsAuthenticated)
            {
                    var identity =  new ClaimsPrincipal(User.Identity);
                    var scope = identity.Claims.Where(c => c.Type.ToLower() == "scope").Select(c => c.Value).SingleOrDefault();
                    var result = new List<HomeRuleVm>();
                    foreach (var tmp in scope.Split('~'))
                    {
                       if(!string.IsNullOrEmpty(tmp))
                        result.Add(new HomeRuleVm
                        {
                            url = tmp.Split('!')[0] + "/" + tmp.Split('!')[1]+"/"+ tmp.Split('!')[2],
                            icon =tmp.Split('!')[3],
                            title =tmp.Split('!')[4]
                        });
                        
                    }                          
                return PartialView(partialViewName, result);
            }
            return null;
        }
        [ChildActionOnly]
        public virtual ActionResult fillMenu(string partialViewName)
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = new ClaimsPrincipal(User.Identity);
                var MenusByRole = identity.Claims.Where(c => c.Type.ToLower() == "scope").Select(c => c.Value).SingleOrDefault();
                var result = new List<ServiceLayer.Models.backendVm.RoleMenuVm>();                
                foreach (var tmp in MenusByRole.Split('~'))
                {
                    if (!string.IsNullOrEmpty(tmp) &&( tmp.Split('!')[2] == "Index"  || tmp.Split('!')[2] == "index"))
                        result.Add(new ServiceLayer.Models.backendVm.RoleMenuVm {
                           // Action ="/",
                            Action = tmp.Split('!')[2],
                            Area = tmp.Split('!')[0],
                            Controller = tmp.Split('!')[1],
                             Title = tmp.Split('!')[4],
                             Icon= tmp.Split('!')[3]
                        });
                }
                return PartialView(partialViewName, result);
            }
            else
                return null;
        }
        [ChildActionOnly]
        public virtual ActionResult UserInfo()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = new ClaimsPrincipal(User.Identity);
                var result = new Models.UserInfo();
                
                var fullname = identity.Claims.Where(c => c.Type.ToLower() == "fullname").Select(c => c.Value).First();
                if(!string.IsNullOrEmpty(fullname))
                {
                    result.fullName = fullname;
                }
                else
                {
                    result.fullName = "کاربر پارسیان دانش";
                }
                var showDate = identity.Claims.Where(c => c.Type.ToLower() == "lastlogindate").Select(c => c.Value).SingleOrDefault();
                result.lastLoginDate = showDate; //showDate[2] + "/" + showDate[1] + "/" + showDate[0];
                
                result.UserImg = "../../../"+identity.Claims.Where(c => c.Type.ToLower() == "userimg").Select(c => c.Value).SingleOrDefault().Remove(0,1);
                
                return PartialView("_TopPartial", result);
            }
            return null;
        }
    }
   
}