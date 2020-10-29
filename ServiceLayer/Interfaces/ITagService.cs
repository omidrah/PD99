using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface ITagService
    {
        Tags Add(Tags item);
        Tags Find(int tagId);
        IList<Tags> GetAllByItemId(int itemId, string constant);
        IList<Tags> GetAllByTagTitle(string tagTitle);
        bool IsExist(int tagId);
        void Remove(Tags item);
    }
}