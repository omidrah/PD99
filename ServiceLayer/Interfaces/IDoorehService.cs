using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IDoorehService
    {
        Dooreha Add(Dooreha item);
        Dooreha Find(int doorehId);
        IList<Dooreha> GetAll();
        bool IsExist(int doorehId);
        void Remove(Dooreha item);
    }
}