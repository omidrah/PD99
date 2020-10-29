using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IProductFeaturesService
    {
        ProductFeatures Add(ProductFeatures item);
        ProductFeatures Find(int productFeaturesId);
        IList<ProductFeatures> GetAll();
        IList<ProductFeatures> GetAllByFeatureId(int featureId);
        IList<ProductFeatures> GetAllByProductId(int productId);
        bool IsExist(int productFeaturesId);
        void Remove(ProductFeatures item);
    }
}