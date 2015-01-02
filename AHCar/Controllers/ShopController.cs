using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHCar.Models.Interface;
using AHCar.Models.Repositiry;
using AHCar.Models;
namespace AHCar.Controllers
{
    public class ShopController : BaseController
    {

        //依類別顯示商品列表,0顯示所有商品
        //GET : /Shop/1
        //2015.1.2 Done
        public ActionResult Index(int CategoryID=0)
        {
            
            //設定所有類別與特定類別商品資訊
            IProductRepository pRep = new ProductRepository();
            IProductCategoryRepository cRep = new ProductCategoryRepository();
            PandCViewModel ViewModel = new PandCViewModel();
            if (CategoryID != 0)
            {
                ViewModel.Producs=pRep.GetAllCategory(CategoryID);
            }
            else
            {
                ViewModel.Producs = pRep.GetAll();
            }
            ViewModel.Cateogrys = cRep.GetAll();
            TempData["Category"] = CategoryID == 0 ? "所有" : ViewModel.Producs.FirstOrDefault().ProductCategory.CategoryName;
            return View(ViewModel);
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
            //設定特定商品資訊與所有商品類別
            IProductRepository pRep = new ProductRepository();
            IProductCategoryRepository cRep = new ProductCategoryRepository();
            PandCViewModel ViewModel = new PandCViewModel();
            if (pRep.Get(ProductID) == default(Product))
            {
                TempData["Message"] = "操作錯誤，請點選正確的商品";
                return RedirectToAction("Index");
            }
            List<Product> P = new List<Product>();
            P.Add(pRep.Get(ProductID));
            ViewModel.Producs = P.AsQueryable();
            ViewModel.Cateogrys = cRep.GetAll();
            return View(ViewModel);
            
        }
        //加入購物車
         
        [HttpPost]
        public JsonResult AddCar()
        {
            return Json("",JsonRequestBehavior.DenyGet);
        }
        //顯示購物車內容
        public ActionResult ShowCar()
        {
            return View();
        }

	}
}