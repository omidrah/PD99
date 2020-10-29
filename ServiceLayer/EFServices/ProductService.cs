using DataLayer.Context;
using DomainClass.Models;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceLayer.EFServices
{
       public class ProductService : IProductService
    {
        IUnitOfWork _uow;
        IDbSet<Product> _Product;
        IDbSet<ProductGroup> _ProductGroup;
        public ProductService(IUnitOfWork uow)
        {
           
            _uow = uow;
            _Product = uow.Set<Product>();
            _ProductGroup = uow.Set<ProductGroup>();

        }

        public bool Add(Product entity, bool autoSave = true)
        {
            try
            {
                _Product.Add(entity);
                if (autoSave)
                    return Convert.ToBoolean(_uow.SaveAllChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Exist(string productName)
        {
            try
            {
                return _Product.Where(p => p.ProductName == productName).Any();
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Product entity, bool autoSave = true)
        {
            try
            {
                _Product.Attach(entity);
          //      _Product.Entry(entity).State = EntityState.Modified;
                _uow.MarkAsChanged(entity);
                if (autoSave)
                    return Convert.ToBoolean(_uow.SaveAllChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        //public bool Delete(Product entity, bool autoSave = true)
        //{
        //    try
        //    {

        //        _Product.Attach(entity);
        //        // _uow.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //        _uow.MarkAsChanged(entity);
        //        if (autoSave)
        //            return Convert.ToBoolean(_uow.SaveAllChanges());
        //        else
        //            return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public bool Delete(int id, bool autoSave = true)
        {
            try
            {
                var entity = _Product.Find(id);
                _Product.Remove(entity);
               // _uow.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(_uow.SaveAllChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public IList<Product> GetAll()
        {
            return _Product.ToList();
        }
        public Product Find(int id)
        {
            try
            {
                return _Product.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Product> Where(System.Linq.Expressions.Expression<Func<Product, bool>> predicate)
        {
            try
            {
                return _Product.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Product> Select()
        {
            try
            {
                return  _Product.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<Product> SelectByType(int TypeId)
        {
            try
            {
                return _Product.AsQueryable().Where(ll=> ll.ProductGroupID == TypeId);
            }
            catch
            {
                return null;
            }
        }
        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Product, TResult>> selector)
        {
            try
            {
                return _Product.Select(selector);
            }
            catch
            {
                return null;
            }
        }

        public int GetLastIdentity()
        {
            try
            {
                if (_Product.Any())
                    return _Product.OrderByDescending(p => p.ProductId).First().ProductId;
                else
                    return 0;
            }
            catch
            {
                return -1;
            }
        }

        //public int Save()
        //{
        //    try
        //    {
        //        return _Product.SaveChanges();
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._uow != null)
                {
                    this._uow.Dispose();
                    this._uow = null;
                }
            }
        }

        ~ProductService()
        {
            Dispose(false);
        }
    }
}
