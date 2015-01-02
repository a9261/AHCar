using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHCar.Models.Interface
{
    public interface IProductRepository : IDisposable
    {
        void Create(Product instance);
        void Update(Product instance);
        void Delete(Product instance);

        Product Get(int ProductID);
        IQueryable<Product> GetAll();
        IQueryable<Product> GetAllCategory(int CateogryID);
        void SaveChanges();
    }
}
