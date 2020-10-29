using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IDourseService
    {
        Doruses Add(Doruses item);
        IList<Doruses> GetAll();
        void Remove(Doruses item);
        bool IsExist(int drsid);
        Doruses Find(int drsId);
    }
        
}