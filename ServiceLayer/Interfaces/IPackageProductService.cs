using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IPackageProductService
    {
        PackageProduct Add(PackageProduct item);
        PackageProduct Find(int packageProductId);
        IList<PackageProduct> GetAll(int packageId);
        IList<PackageProduct> GetAllPackage(int productId);
        bool IsExist(int packageProductId);
        void Remove(PackageProduct item);
    }
}