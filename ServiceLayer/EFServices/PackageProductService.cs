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
   public  class PackageProductService : IPackageProductService
    {
        IUnitOfWork _uow;
       // IDbSet<Packages> _Packages;
        IDbSet<PackageProduct> _PackageProducts;

        public PackageProductService(IUnitOfWork uow)
        {
            _uow = uow;
         //   _Packages = uow.Set<Packages>();
            _PackageProducts = uow.Set<PackageProduct>();
        }

        public PackageProduct Add(PackageProduct item)
        {
            return _PackageProducts.Add(item);
        }

        public void Remove(PackageProduct item)
        {
            _PackageProducts.Remove(item);
        }
        public IList<PackageProduct> GetAll(int packageId)
        {
            return _PackageProducts.Where(ll=>ll.PackageId ==packageId).ToList();
        }
        public bool IsExist(int packageProductId)
        {
            return _PackageProducts.Any(x => x.Id == packageProductId);
        }
        public PackageProduct Find(int packageProductId) {
            return _PackageProducts.Find(packageProductId);
        }

        public IList<PackageProduct> GetAllPackage(int productId)
        {
            return _PackageProducts.Where(p=>p.ProductId ==productId).ToList();
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
