using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClass.Models;
using Microsoft.AspNet.Identity;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class ApplicationRoleManager : RoleManager<Role, int>, IApplicationRoleManager
    {
        private readonly ICustomRoleStore _roleStore;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<User> _users;

        public ApplicationRoleManager(IUnitOfWork uow, ICustomRoleStore roleStore)
            : base((IRoleStore<Role, int>) roleStore)
        {
            _uow = uow;
            _roleStore = roleStore;
            _users = _uow.Set<User>();
        }
        //یافتن باعنوان انگلیسی
        public Role FindRoleByName(string roleName)
        {
            return this.FindByName(roleName); // RoleManagerExtensions
        }
        //یافتن با عنوان فارسی
        public Role FindRoleByTitle(string roleTilte)
        {
            return this.Roles.FirstOrDefault(x => x.Title == roleTilte);

        }
        public IdentityResult CreateRole(Role role)
        {
            return this.Create(role); // RoleManagerExtensions
        }

        public IList<UserRole> GetUsersInRoleByTitle(string roleName)
        {
            return Roles.Where(role => role.Title == roleName)
                .SelectMany(role => role.Users)
                .ToList();
            // = this.FindByName(roleName).Users
        }

        public IList<UserRole> GetUsersInRoleById(int roleId)
        {
            return Roles.Where(role => role.Id == roleId)
                .SelectMany(role => role.Users)
                .ToList();
        }

        public IList<User> GetApplicationUsersInRole(string roleName)
        {
            var roleUserIdsQuery = from role in Roles
                where role.Name == roleName
                from user in role.Users
                select user.UserId;
            return _users.Where(applicationUser => roleUserIdsQuery.Contains(applicationUser.Id))
                .ToList();
        }

        public IList<Role> FindUserRoles(int userId)
        {
            var userRolesQuery = from role in Roles
                from user in role.Users
                where user.UserId == userId
                select role;

            return userRolesQuery.OrderBy(x => x.Name).ToList();
        }

        public string[] GetRolesForUser(int userId)
        {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
            {
                return new string[] {};
            }

            return roles.Select(x => x.Name).ToArray();
        }

        public bool IsUserInRole(int userId, string roleName)
        {
            var userRolesQuery = from role in Roles
                where role.Name == roleName
                from user in role.Users
                where user.UserId == userId
                select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
        }

        public Task<List<Role>> GetAllRolesAsync()
        {
            return _roleStore.Roles.ToListAsync();
        }

        public Task<List<Role>> GetAllCustomRolesAsync()
        {
            return Roles.ToListAsync();
        }
    }
}