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
    public class ProductFeaturesService : IProductFeaturesService
    {
        IUnitOfWork _uow;
        IDbSet<ProductFeatures> _productFeatures;

        public ProductFeaturesService(IUnitOfWork uow)
        {
            _uow = uow;
            _productFeatures = uow.Set<ProductFeatures>();
        }

        public ProductFeatures Add(ProductFeatures item)
        {
            return _productFeatures.Add(item);
        }

        public void Remove(ProductFeatures item)
        {
            _productFeatures.Remove(item);
        }
        public IList<ProductFeatures> GetAll()
        {
            return _productFeatures.ToList();
        }

        public IList<ProductFeatures> GetAllByProductId(int productId)
        {
            return _productFeatures.Where(ll => ll.ProductId == productId).ToList();
        }

        public IList<ProductFeatures> GetAllByFeatureId(int featureId)
        {
            return _productFeatures.Where(ll => ll.FeatureId == featureId).ToList();
        }
        public bool IsExist(int productFeaturesId)
        {
            return _productFeatures.Any(x => x.ProductFeatureValueID == productFeaturesId);
        }
        public ProductFeatures Find(int productFeaturesId)
        {
            return _productFeatures.Find(productFeaturesId);
        }
    }
}
