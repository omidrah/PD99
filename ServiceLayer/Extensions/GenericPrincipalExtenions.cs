using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Extensions
{
    public static class GenericPrincipalExtensions
    {
        public static string FullName(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
                foreach (var claim in claimsIdentity.Claims)
                {
                    if (claim.Type.ToLower() == "fullname")
                        return claim.Value;
                }
                return "";
            }
            else
                return "";
        }
        public static string[] Scope(this IPrincipal user)
        {
            
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
                foreach (var claim in claimsIdentity.Claims)
                {
                    if (claim.Type.ToLower() == "scope")
                    {
                        var items = claim.Value.Split(',');
                        return items;
                    }
                        //return claim.Value;
                }
                return null;
            }
            else
                return null;
        }
        public static string[] Roles(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
                foreach (var claim in claimsIdentity.Claims)
                {
                    if (claim.Type.ToLower() == "role")
                        return claim.Value.Split(',');
                }
                return null;
            }
            else
                return null;
        }
    }
}
