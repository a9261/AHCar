using AHCar.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHCar.Models.Repositiry
{
    public class ProductRepository : BaseEntities,IProductRepository
    {
        private bool isNull(Models.Product n)
        {
            if (n == null)
            {
                throw new ArgumentNullException("物件未正確指定參考");
                //return true;
            }
            else
                return false;
        }
        public void Create(Models.Product instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Added;
                SaveChanges();
            }
        }

        public void Update(Models.Product instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Modified;
                SaveChanges();
            }
        }

        public void Delete(Models.Product instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Deleted;
                SaveChanges();
            }
        }

        public Models.Product Get(int ProductID)
        {
            return db.Products.FirstOrDefault(x => x.ProductID == ProductID);
        }

        public IQueryable<Models.Product> GetAll()
        {
            return db.Products.OrderBy(x => x.ProductID);
        }

        public IQueryable<Models.Product> GetAllCategory(int CateogryID)
        {
            var result = (
                    from item in db.Products
                    where item.CategoryID==CateogryID
                    select item
                ).OrderBy(items => items.ProductID);
            return result;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            
        }
    }
}