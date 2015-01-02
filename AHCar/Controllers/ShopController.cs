using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHCar.Models.Interface;
using AHCar.Models.Repositiry;
using AHCar.Models;
using AHCar.Models.Original;
using Newtonsoft.Json;
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
        public JsonResult AddCar(ShopItem item)
        {
            //2015.1.2
            //TODO:將相關頁面 建立對應JSON物件，傳至購物車
            UserShopCar userCar = null;
            msg m = new msg();
            if (Session["Car"] == null)
            {
               userCar =  new UserShopCar();
               userCar.Userinfo.UserID = User.Identity.IsAuthenticated == true ? User.Identity.Name : "非會員";
               Session["Car"] = userCar;
            }
            else
            {
                userCar = (UserShopCar)Session["Car"];
            }
            userCar.Add(item);
            m.errorCode = msg.msgCode.sucess;
            return Json(JsonConvert.SerializeObject(m), JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult RemoveCar(int ProductId)
        {
            //2015.1.2
            //TODO:將相關頁面 對應ProductID，傳至購物車
             msg m = new msg();
            if (Session["Car"] == null)
            {
                m.errorCode=msg.msgCode.error;
                return Json(JsonConvert.SerializeObject(m), JsonRequestBehavior.DenyGet);
            }
            else
            {
                 UserShopCar userCar = (UserShopCar)Session["Car"];
                 m.BeforCount = userCar.GetAllItems().Count;
                 userCar.Remove(ProductId);
                 m.AfterCount = userCar.GetAllItems().Count;
            }
            m.errorCode = msg.msgCode.sucess;
           
            return Json(JsonConvert.SerializeObject(m), JsonRequestBehavior.DenyGet);
        }
        //顯示購物車內容
        public ActionResult ShowCar()
        {
            UserShopCar userCar = null;
            if (Session["Car"] == null)
            {
                userCar = new UserShopCar();
                userCar.Userinfo.UserID = User.Identity.IsAuthenticated == true ? User.Identity.Name : "非會員";
                Session["Car"] = userCar;
            }
            else
            {
                userCar = (UserShopCar)Session["Car"];
            }
            //模擬裡面有一筆資料
            userCar.Add(new ShopItem { ProductID = 0, ProductName = "金色大鉛筆", Price = 999, Amount = 10 });
            return View(userCar.GetAllItems());
        }

	}
}