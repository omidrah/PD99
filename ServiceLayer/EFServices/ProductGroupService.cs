using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClass.Models;
using System.Data.Entity;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace ServiceLayer.EFServices
{
    public class ProductGroupService : IProductGroupService
    {
        IUnitOfWork _uow;
        IDbSet<ProductGroup> _productGroups;

        public ProductGroupService(IUnitOfWork uow)
        {
            _uow = uow;
            _productGroups = uow.Set<ProductGroup>();
        }

        public ProductGroup Add(ProductGroup item)
        {
            return _productGroups.Add(item);
        }

        public void Remove(ProductGroup item)
        {
            _productGroups.Remove(item);
        }
        public IList<ProductGroup> GetAll()
        {
            return _productGroups.ToList();
        }

        public IList<ProductGroupVM> GetAllActive()
        {
            return _productGroups.Where(ll => ll.IsActive == true).Select(a => new ProductGroupVM
            {
                ProductGroupId = a.ProductGroupId,
                ProductGroupTitle = a.ProductGroupTitle
            }).ToList();
        }
        public bool IsExist(int productGroupId)
        {
            return _productGroups.Any(x => x.ProductGroupId == productGroupId);
        }
        public ProductGroup Find(int productGroupId)
        {
            return _productGroups.Find(productGroupId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
