using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
    public class MenuService : IMenuService
    {
        
        private readonly IDbSet<Menu> _menu;
        private readonly IDbSet<RoleMenu> _roleMenu;
        private readonly IUnitOfWork _uow;

        public MenuService(IUnitOfWork uow)
        {
            _uow = uow;
            _menu = _uow.Set<Menu>();
            _roleMenu = _uow.Set<RoleMenu>();
        }

        public IList<Menu> GetAll()
        {
            return _menu.Where(x => x.IsActive == true).ToList();
        }

        public Menu AddMenu(Menu item)
        {
            return _menu.Add(item);
        }

        public Menu RemoMenu(Menu item)
        {
            return _menu.Remove(item);
        }

        public bool IsUseMenu(Menu item)
        {
            return _roleMenu.Any(x => x.MenuId == item.MenuId);
        }
    }
}