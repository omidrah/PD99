using System.Collections.Generic;
using DomainClass.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IKelassService
    {
        Kelass Add(Kelass item);
        Kelass Find(int klassId);
        IList<Kelass> GetAll();
        bool IsExist(int KelassId);
        void Remove(Kelass item);
        IList<KelassVM> GetAllKelass();
    }
}