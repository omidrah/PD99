using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IDoorehDorusService
    {
        DoorehaDorouse Add(DoorehaDorouse item);
        DoorehaDorouse Find(int doorehDorusId);
        IList<DoorehaDorouse> GetAll(int doorehId);
        bool IsExist(int doorehDorusId);
        void Remove(DoorehaDorouse item);
    }
}