using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IRoleService
    {
        Role Add(Role item);
        Role Find(int roleId);
        IList<Role> GetAll();
        bool IsExist(int roleId);
        void Remove(Role item);
    }
}