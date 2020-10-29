using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IImageService
    {
        Image Add(Image item);
        Image Find(int ImageId);
        IList<Image> GetAll();
        bool IsExist(int ImageId);
        void Remove(Image item);
        Image FindByItemId(int itemId);
    }
}