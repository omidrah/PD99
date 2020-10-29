using System;
using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IRoleMenuService 
    {
        IList<RoleMenu> GetAll();
        RoleMenu Add(RoleMenu item);
        RoleMenu Remove(RoleMenu item);
        IList<RoleMenu> GetAllbyRoleId(int roleId);
        IList<RoleMenu> GetAllbyRolesId(string[] rolesName);
    }
}