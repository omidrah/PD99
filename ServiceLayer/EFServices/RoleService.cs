using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClass.Models;
using System.Data.Entity;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
   public  class RoleService : IRoleService
    {
        IUnitOfWork _uow;
        IDbSet<Role> _Role;
        
        public RoleService(IUnitOfWork uow)
        {
            _uow = uow;
            _Role = uow.Set<Role>();
        }

        public Role Add(Role item)
        {
            return _Role.Add(item);
        }

        public void Remove(Role item)
        {
            _Role.Remove(item);
        }
        public IList<Role> GetAll()
        {
            return _Role.ToList();
        }
        public bool IsExist(int roleId)
        {
            return _Role.Any(x => x.Id == roleId);
        }
        public Role Find(int roleId)
        {
            return _Role.Find(roleId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
