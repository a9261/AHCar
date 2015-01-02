using AHCar.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHCar.Models.Repositiry
{
    public class ProductCategoryRepository : BaseEntities,IProductCategoryRepository
    {
        //判斷instance是否為null
        private bool isNull(Models.ProductCategory n)
        {
            if (n == null)
            {
                throw new ArgumentNullException("物件未正確指定參考");
                //return true;
            }
            else
                return false;
        }
        public void Create(Models.ProductCategory instance)
        {
            if (!isNull(instance))
            {
                db.ProductCategories.Add(instance);
                SaveChanges();
            }
        }

        public void Update(Models.ProductCategory instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Modified;
                SaveChanges();
            }
        }

        public void Delete(Models.ProductCategory instance)
        {
            if (!isNull(instance))
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Deleted;
                SaveChanges();
            }
        }

        public Models.ProductCategory Get(int CategoryID)
        {
            return db.ProductCategories.FirstOrDefault(x=> x.CategoryID==CategoryID);
        }

        public IQueryable<Models.ProductCategory> GetAll()
        {
            var result = (
                    from item in db.ProductCategories
                    where item.isUse==true
                    select item
                ).OrderBy(x=> x.CategoryID);
            return result;
        }
        /// <summary>
        /// 自行定義之儲存確認動作
        /// </summary>
        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
            System.IO.File.AppendAllText(@"C:\Bugs\Bugs.txt", "This Dispose is run by " + this.GetType().Name + Environment.NewLine);
            GC.SuppressFinalize(this);
        }
       
    }
}