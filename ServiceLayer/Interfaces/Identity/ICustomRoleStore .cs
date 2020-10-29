using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainClass.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

namespace ServiceLayer.Interfaces
{
    public interface ICustomRoleStore : IDisposable
    {
        Task<Role> FindByIdAsync(int roleId);
        Task<Role> FindByNameAsync(string roleName);
        Task CreateAsync(Role role);
        Task DeleteAsync(Role role);
        Task UpdateAsync(Role role);
        DbContext Context { get; }
        bool DisposeContext { get; set; }
        IQueryable<Role> Roles { get; }
    }
}
