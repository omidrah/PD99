using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IFeaturesService
    {
        Features Add(Features item);
        IList<Features> GetAll();
        void Remove(Features item);
        bool IsExist(int FeaturesId);
        Features Find(int FeaturesId);
    }
        
}