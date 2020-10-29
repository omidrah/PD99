using System.Collections.Generic;
using DomainClass.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IGalleryService
    {
        Gallery Add(Gallery newItem);
        IList<Gallery> GetAll();
        Gallery Delete(Gallery Item);
        IList<SliderVm> GetAllActive();
        IList<BSliderVm> BGetAllActive();
        Gallery FindById(int itemId);
    }
}