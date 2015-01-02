using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHCar.Models.Interface;
using AHCar.Models.Repositiry;
namespace AHCar.Controllers
{
    public class ShopController : BaseController
    {

        //依類別顯示商品列表,0顯示所有商品
        //GET : /Shop/1
        public ActionResult Index(int CategoryID=0)
        {
            IQueryable<Models.Product> productItems;
            IProductRepository rep = new ProductRepository();
            if (CategoryID != 0)
            {
                productItems=rep.GetAllCategory(CategoryID);
            }
            else
            {
                productItems = rep.GetAll();
            }
            return View(productItems);
        }
        //顯示所有商品類別
        public ActionResult ShowCategory()
        {
            IProductCategoryRepository cateory = new ProductCategoryRepository();
            return View(cateory.GetAll());
        }
       
        //顯示商品細項
        public ActionResult ShowDetail(int ProductID=0)
        {
            if (ProductID != 0)
            {

            }
            return View();
        }
        //顯示購物車內容
        public ActionResult ShowCar()
        {
            return View();
        }

	}
}