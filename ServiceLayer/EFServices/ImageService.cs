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
   public  class ImageService : IImageService
    {
        IUnitOfWork _uow;
        IDbSet<Image> _Images;
        
        public ImageService(IUnitOfWork uow)
        {
            _uow = uow;
            _Images = uow.Set<Image>();
        }

        public Image Add(Image item)
        {
            return _Images.Add(item);
        }

        public void Remove(Image item)
        {
            _Images.Remove(item);
        }
        public IList<Image> GetAll()
        {
            return _Images.ToList();
        }
        public bool IsExist(int ImageId)
        {
            return _Images.Any(x => x.ItemId == ImageId);
        }
        public Image Find(int ImageId) {
            return _Images.Find(ImageId);
        }
        public Image FindByItemId(int itemId)
        {
            return _Images.FirstOrDefault(ll=> ll.ItemId == itemId);
        }

        //public Task<IQueryable<Doruse>> GetAllAsync()
        //{
        //    return _Drses.ToListAsync();
        //}
    }
}
