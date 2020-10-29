using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.EFServices
{
    public  class GalleryService : IGalleryService
    {
        private IUnitOfWork _uow;
        private readonly IDbSet<Gallery> _Galleries;
        public GalleryService(IUnitOfWork uow)
        {
            _uow = uow;
            _Galleries = _uow.Set<Gallery>();
        }
        public IList<Gallery> GetAll()
        {
            return _Galleries.ToList();
            
        }
        public IList<SliderVm> GetAllActive()
        {
            return _Galleries.Where(x => x.imgIsActive == true).OrderBy(x=>x.imgPriority).Select(a => new SliderVm { Id = a.Id, ImgName = a.imgName, ImgPath = a.imgPath , Url = a.NavigateUrl}).ToList();
        }
        public IList<BSliderVm> BGetAllActive()
        {
            return _Galleries.Where(x => x.imgIsActive == true).OrderBy(x => x.imgPriority).Select(a => new BSliderVm { Id = a.Id, imgTitle = a.imgName, ImgPath = a.imgPath,imgIsActive= a.imgIsActive==true?true:false ,imgPriority= a.imgPriority.ToString()}).ToList();
        }
        public Gallery Add(Gallery newItem)
        {
          return  _Galleries.Add(newItem);
        }
        public Gallery Delete(Gallery Item)
        {
            if (this.FindById(Item.Id) != null) { 
                    _Galleries.Remove(Item);
                return Item;
                }
            return null;
        }
        public Gallery FindById(int  itemId)
        {
            return _Galleries.Find(itemId);
        }
    }
}
