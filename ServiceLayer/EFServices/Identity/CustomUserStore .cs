using System.Data.Entity;
using DataLayer.Context;
using DomainClass.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
    public class CustomUserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>, ICustomUserStore
    {
        private readonly IUnitOfWork _context;

        public CustomUserStore(IUnitOfWork context)
            : base((ApplicationDbContext)context)
        {
            _context = context;
        }

    }
}
