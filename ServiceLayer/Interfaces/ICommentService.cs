using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface ICommentService
    {
        Comments Add(Comments item);
        Comments Find(int commentId);
        IList<Comments> GetAll();
        bool IsExist(int commentId);
        void Remove(Comments item);
    }
}