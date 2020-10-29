using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IProductGroupFeaturesService
    {
        ProductGroupFeatures Add(ProductGroupFeatures item);
        ProductGroupFeatures Find(int productGroupFeaturesId);
        IList<ProductGroupFeatures> GetAll();
        IList<ProductGroupFeatures> GetAllByFeatureId(int featureId);
        IList<ProductGroupFeatures> GetAllByProductGroupId(int productGroupId);
        bool IsExist(int productGroupFeaturesId);
        void Remove(ProductGroupFeatures item);
    }
}