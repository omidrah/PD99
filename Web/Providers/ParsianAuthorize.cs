using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Web.Providers
{
    public class ParsianAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (checkAccess(filterContext.HttpContext))
            {
                // base.OnAuthorization(filterContext);
                return;
            }
            HandleUnauthorizedRequest(filterContext);


        }

        private bool checkAccess(HttpContextBase httpContext)
        {
            var result = false;
            if (httpContext.Request.IsAuthenticated)
            {
                var area = httpContext.Request.RequestContext.RouteData.DataTokens["area"];
                if (area==null/* || area.ToString() != "Admin"*/) //for Customr role
                {
                    result = true;
                }
                else 
                {
                    var ctrl = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
                    var action = httpContext.Request.RequestContext.RouteData.Values["Action"].ToString();
                    var identity = (ClaimsPrincipal) Thread.CurrentPrincipal;
                    
                   
                    var Userroles = identity.Claims.Where(c => c.Type.ToLower() == "role").Select(c => c.Value).SingleOrDefault();
                    foreach (var item in Userroles.Split(','))
                    {
                        if(item == "Admin")
                        {
                            return true;
                        }
                    }

                    var scope =
                        identity.Claims.Where(c => c.Type.ToLower() == "scope").Select(c => c.Value).SingleOrDefault();
                    var acc = scope.Split('~');
                    if (!string.IsNullOrEmpty(scope))
                    {
                        foreach (var tmp in acc.ToArray())
                        {
                            //if (!string.IsNullOrEmpty(tmp))
                            //{
                                if (string.Equals(tmp.Split('!')[1], ctrl, StringComparison.CurrentCultureIgnoreCase) 
                                //we disabled action Checking in Aturize Filter . 99.07.20
                                //  &&  string.Equals(tmp.Split('!')[2], action, StringComparison.CurrentCultureIgnoreCase
                               )
                                {
                                    result = true;
                                    break;
                                }
                            //}
                        }
                    }
                }
            }
            return result;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Account/Login");
        }
    }
}