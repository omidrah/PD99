using System;
using System.Linq;
using System.Linq.Expressions;
using DomainClass.Models;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IProductService
    {
        bool Add(Product entity, bool autoSave = true);
        void Dispose();
        bool Exist(string productName);
        IList<Product> GetAll();
        Product Find(int id);
        int GetLastIdentity();
        IQueryable<Product> Select();
        IQueryable<TResult> Select<TResult>(Expression<Func<Product, TResult>> selector);
        bool Update(Product entity, bool autoSave = true);
        bool Delete(int id, bool autoSave = true);
        IQueryable<Product> Where(Expression<Func<Product, bool>> predicate);
        IQueryable<Product> SelectByType(int TypeId);
    }
}