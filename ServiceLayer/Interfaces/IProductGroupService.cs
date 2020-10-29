using System.Collections.Generic;
using DomainClass.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IProductGroupService
    {
        ProductGroup Add(ProductGroup item);
        ProductGroup Find(int productGroupId);
        IList<ProductGroup> GetAll();
        bool IsExist(int productGroupId);
        void Remove(ProductGroup item);
        IList<ProductGroupVM> GetAllActive();
    }
}