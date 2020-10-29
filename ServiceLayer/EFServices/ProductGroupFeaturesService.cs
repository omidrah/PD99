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
    public class ProductGroupFeaturesService : IProductGroupFeaturesService
    {
        IUnitOfWork _uow;
        IDbSet<ProductGroupFeatures> _productGroupFeatures;

        public ProductGroupFeaturesService(IUnitOfWork uow)
        {
            _uow = uow;
            _productGroupFeatures = uow.Set<ProductGroupFeatures>();
        }

        public ProductGroupFeatures Add(ProductGroupFeatures item)
        {
            return _productGroupFeatures.Add(item);
        }

        public void Remove(ProductGroupFeatures item)
        {
            _productGroupFeatures.Remove(item);
        }
        public IList<ProductGroupFeatures> GetAll()
        {
            return _productGroupFeatures.ToList();
        }

        public IList<ProductGroupFeatures> GetAllByProductGroupId(int productGroupId)
        {
            return _productGroupFeatures.Where(ll => ll.ProductGroupId == productGroupId).ToList();
        }

        public IList<ProductGroupFeatures> GetAllByFeatureId(int featureId)
        {
            return _productGroupFeatures.Where(ll => ll.FeaturesId == featureId).ToList();
        }
        public bool IsExist(int productGroupFeaturesId)
        {
            return _productGroupFeatures.Any(x => x.ProductGroupFeatuesId == productGroupFeaturesId);
        }
        public ProductGroupFeatures Find(int productGroupFeaturesId)
        {
            return _productGroupFeatures.Find(productGroupFeaturesId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
