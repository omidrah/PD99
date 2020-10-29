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
   public  class CommentService : ICommentService
    {
        IUnitOfWork _uow;
        IDbSet<Comments> _comments;
        
        public CommentService(IUnitOfWork uow)
        {
            _uow = uow;
            _comments = uow.Set<Comments>();
        }

        public Comments Add(Comments item)
        {
            return _comments.Add(item);
        }

        public void Remove(Comments item)
        {
            _comments.Remove(item);
        }
        public IList<Comments> GetAll()
        {
            return _comments.Where(ll=>ll.IsDeleted == false).ToList();
        }
        public bool IsExist(int commentId)
        {
            return _comments.Any(x => x.ID == commentId);
        }
        public Comments Find(int commentId) {
            return _comments.Find(commentId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
