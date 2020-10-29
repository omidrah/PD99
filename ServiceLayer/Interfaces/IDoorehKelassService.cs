using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IDoorehKelassService
    {
        KelassDooreh Add(KelassDooreh item);
        KelassDooreh Find(int doorehKelassId);
        IList<KelassDooreh> GetAllDooreh(int KelassId);
        IList<KelassDooreh> GetAllKelass(int doorehId);
        bool IsExist(int doorehKelassId);
        void Remove(KelassDooreh item);
    }
}