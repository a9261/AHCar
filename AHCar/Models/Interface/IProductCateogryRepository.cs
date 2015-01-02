using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHCar.Models.Interface
{
    public interface IProductCategoryRepository : IDisposable
    {
        void Create(ProductCategory instance);
        void Update(ProductCategory instance);
        void Delete(ProductCategory instance);

        ProductCategory Get(int CategoryID);
        IQueryable<ProductCategory> GetAll();
        void SaveChanges();
    }
}
