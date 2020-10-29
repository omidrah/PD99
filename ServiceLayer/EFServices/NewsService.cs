using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClass.Models;
using System.Data.Entity;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
   public  class NewsService : INewsService
    {
        IUnitOfWork _uow;
        IDbSet<News> _News;
        
        public NewsService(IUnitOfWork uow)
        {
            _uow = uow;
            _News = uow.Set<News>();
        }

        public News Add(News item)
        {
            return _News.Add(item);
        }

        public void Remove(News item)
        {
            _News.Remove(item);
        }
        public IList<News> GetAll()
        {
            return _News.Where(ll=>ll.IsDeleted == false).ToList();
        }
        public bool IsExist(int newsId)
        {
            return _News.Any(x => x.NewsId == newsId);
        }
        public News Find(int newsId) {
            return _News.Find(newsId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
