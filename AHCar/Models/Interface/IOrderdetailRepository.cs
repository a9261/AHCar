using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHCar.Models.Interface
{
   public interface IOrderdetailRepository :IDisposable
    {
        void Create(OrderDetail instance);
        void Update(OrderDetail instance);
        void Delete(OrderDetail instance);

        OrderDetail Get(int OrderID);
        IQueryable<OrderDetail> GetAll();
        void SaveChanges();
    }
}
