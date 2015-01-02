using AHCar.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHCar.Models.Repositiry
{
    public class OrderRepository : BaseEntities,IOrderRepository
    {
        private bool isNull(Models.Order n)
        {
            if (n == null)
            {
                throw new ArgumentNullException("物件未正確指定參考");
                //return true;
            }
            else
                return false;
        }
        public void Create(Models.Order instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Added;
                SaveChanges();
            }
        }

        public void Update(Models.Order instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Modified;
                SaveChanges();
            }
        }

        public void Delete(Models.Order instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Deleted;
                SaveChanges();
            }
        }

        public Models.Order Get(int OrderID)
        {
            return db.Orders.FirstOrDefault(x => x.OrderId == OrderID);
        }

        public IQueryable<Models.Order> GetAll()
        {
            return db.Orders.OrderBy(x => x.OrderId);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}