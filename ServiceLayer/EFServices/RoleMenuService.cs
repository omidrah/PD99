using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
    public class RoleMenuService : IRoleMenuService
    {
        private readonly IDbSet<RoleMenu> _roleMenus;
        private IUnitOfWork _uow;

        public RoleMenuService(IUnitOfWork uow)
        {
            _uow = uow;
            _roleMenus = uow.Set<RoleMenu>();
        }
        public IList<RoleMenu> GetAll()
        {
            return _roleMenus.ToList();
        }
        public IList<RoleMenu> GetAllbyRoleId(int roleId)
        {
            return _roleMenus.Include(x=>x.Menu).Include(x=>x.Role).Where(x=>x.RoleId ==roleId && x.IsActive==true).OrderBy(x=>x.Menu.Sequence).ToList();
        }
        public IList<RoleMenu> GetAllbyRolesId(string[] rolesName)
        {
            return _roleMenus.Include(x => x.Menu).Include(x => x.Role).Where(x => rolesName.Contains(x.Role.Name) && x.IsActive==true).OrderBy(x=>x.Menu.Sequence).ToList();
        }

        public RoleMenu Add(RoleMenu item)
        {
            return _roleMenus.Add(item);
        }

        public RoleMenu Remove(RoleMenu item)
        {
            return _roleMenus.Remove(item);
        }
        
    }
}