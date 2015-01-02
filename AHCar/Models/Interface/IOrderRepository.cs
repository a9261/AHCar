using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHCar.Models.Interface
{
   public interface IOrderRepository :IDisposable
    {
       void Create(Order instance);
       void Update(Order instance);
       void Delete(Order instance);

       Order Get(int OrderID);
       IQueryable<Order> GetAll();
       void SaveChanges();
    }
}
