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
   public  class PackageService : IPackageService
    {
        IUnitOfWork _uow;
        IDbSet<Packages> _Packages;
        
        public PackageService(IUnitOfWork uow)
        {
            _uow = uow;
            _Packages = uow.Set<Packages>();
        }

        public Packages Add(Packages item)
        {
            return _Packages.Add(item);
        }

        public void Remove(Packages item)
        {
            _Packages.Remove(item);
        }
        public IList<Packages> GetAll()
        {
            return _Packages.ToList();
        }

        public IList<Packages> GetAllChild(int? pakcageId)
        {
            return _Packages.Where(ll=>ll.ParentId == pakcageId).ToList();
        }
        public bool IsExist(int packageId)
        {
            return _Packages.Any(x => x.Id == packageId);
        }
        public Packages Find(int packageId) {
            return _Packages.Find(packageId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
