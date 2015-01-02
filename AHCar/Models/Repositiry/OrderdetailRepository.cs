using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHCar.Models.Interface;
namespace AHCar.Models.Repositiry
{
    public class OrderdetailRepository :BaseEntities, IOrderdetailRepository
    {
        private bool isNull(Models.OrderDetail n)
        {
            if (n == null)
            {
                throw new ArgumentNullException("物件未正確指定參考");
                //return true;
            }
            else
                return false;
        }
        public void Create(OrderDetail instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Added;
                SaveChanges();
            }
        }

        public void Update(OrderDetail instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Modified;
                SaveChanges();
            }
        }

        public void Delete(OrderDetail instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Deleted;
                SaveChanges();
            }
        }

        public OrderDetail Get(int OrderDetailID)
        {
            return db.OrderDetails.FirstOrDefault(x => x.OrderDetailID == OrderDetailID);
           
        }

        public IQueryable<OrderDetail> GetAll()
        {
            return db.OrderDetails.OrderBy(x => x.OrderDetailID);
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