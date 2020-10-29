using System;
using System.Collections.Generic;
using DomainClass.Models;

namespace ServiceLayer.Interfaces
{
    public interface IMenuService 
    {
        IList<Menu> GetAll();
        Menu AddMenu(Menu item);
        Menu RemoMenu(Menu item);
        bool IsUseMenu(Menu item);
    }
}