using System.Data.Entity;
using DataLayer.Context;
using DomainClass.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
    public class CustomRoleStore :RoleStore<Role, int, UserRole>,        ICustomRoleStore
    {
        private readonly IUnitOfWork _context;

        public CustomRoleStore(IUnitOfWork context)
            : base((DbContext)context)
        {
            _context = context;
        }
    }
}
