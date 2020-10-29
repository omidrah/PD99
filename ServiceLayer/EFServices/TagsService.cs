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
   public  class TagsService : ITagService
    {
        IUnitOfWork _uow;
        IDbSet<Tags> _tags;
        
        public TagsService(IUnitOfWork uow)
        {
            _uow = uow;
            _tags = uow.Set<Tags>();
        }

        public Tags Add(Tags item)
        {
            return _tags.Add(item);
        }

        public void Remove(Tags item)
        {
            _tags.Remove(item);
        }
        public IList<Tags> GetAllByItemId(int itemId,string constant)
        {
            return _tags.Where(ll=>ll.itemId== itemId && ll.tagConstant == constant).ToList();
        }

        public IList<Tags> GetAllByTagTitle(string tagTitle)
        {
            return _tags.Where(ll => ll.tagTitle == tagTitle).ToList();
        }
        public bool IsExist(int tagId)
        {
            return _tags.Any(x => x.TagId == tagId);
        }
        public Tags Find(int tagId) {
            return _tags.Find(tagId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
