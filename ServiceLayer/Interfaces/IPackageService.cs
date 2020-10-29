using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IPackageService
    {
        Packages Add(Packages item);
        Packages Find(int packageId);
        IList<Packages> GetAll();
        bool IsExist(int packageId);
        void Remove(Packages item);
        IList<Packages> GetAllChild(int? pakcageId);
    }
}