using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IMahaleBargozariService
    {
        MahaleBargozari Add(MahaleBargozari item);
        MahaleBargozari Find(int? mahalId);
        IList<MahaleBargozari> GetAll();
        bool IsExist(int drsid);
        void Remove(MahaleBargozari item);
    }
}