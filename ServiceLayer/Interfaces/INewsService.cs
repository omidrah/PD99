using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface INewsService
    {
        News Add(News item);
        News Find(int newsId);
        IList<News> GetAll();
        bool IsExist(int newsId);
        void Remove(News item);
    }
}