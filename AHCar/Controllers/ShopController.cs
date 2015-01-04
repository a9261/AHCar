using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHCar.Models.Interface;
using AHCar.Models.Repositiry;
using AHCar.Models.ViewModels;
using AHCar.Models.Original;
using Newtonsoft.Json;
using AHCar.Models;
using Microsoft.AspNet.Identity;
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
        //取得目前商品品項數
        [HttpPost]
        public JsonResult getProductNum()
        {
            msg m = new msg();
            if (Session["Car"] != null)
            {
                UserShopCar userCar = (UserShopCar)Session["Car"];
                m.errorCode = msg.msgCode.sucess;
                m.AfterCount = userCar.GetAllItems().Count;
                return Json(JsonConvert.SerializeObject(m), JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(JsonConvert.SerializeObject(m), JsonRequestBehavior.DenyGet);
            }
        }
        //加入購物車
        [HttpPost]
        public JsonResult AddCar(ShopItem item)
        {
            //2015.1.2
            //Done 
            UserShopCar userCar = null;
            msg m = new msg();
            if (Session["Car"] == null)
            {
               userCar =  new UserShopCar();
               userCar.Userinfo.UserID = "非會員";
               Session["Car"] = userCar;
            }
            else
            {
                userCar = (UserShopCar)Session["Car"];
            }
            //如果有該商品移除再重新加入
            userCar.Remove(item.ProductID);

            userCar.Add(item);
            m.errorCode = msg.msgCode.sucess;
            return Json(JsonConvert.SerializeObject(m), JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult RemoveCar(int ProductId)
        {
            //2015.1.2
            //Done
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
            //模擬裡面有一筆測試資料
            // userCar.Add(new ShopItem { ProductID = 0, ProductName = "金色大鉛筆", Price = 999, Amount = 10 });
            return View(userCar);
        }
        //顯示結帳頁面
        public ActionResult ShowPay(int index=0)
        {
            if(Session["Car"] != null){
                return View();
            }else{
                return RedirectToAction("Show404");
            }
        }
        [HttpPost]
        //結帳付款
        public ActionResult ShowPay(Order orders)
        {
            if (Session["Car"] != null)
            {
                //設定完整的訂購人資訊
                UserShopCar userCar = (UserShopCar)Session["Car"];
                if (User.Identity.IsAuthenticated){
                   userCar.Userinfo.UserID = User.Identity.GetUserId();
                }
                userCar.Userinfo.UserName = orders.UserName;
                userCar.Userinfo.UserPhone = orders.UserPhone;
                userCar.Userinfo.UserAddress = orders.UserAddress;
                //確認有商品資料再寫入訂單
                if(userCar.GetAllItems().Count>0)
                    userCar.SendOrder();    
                return RedirectToAction("ShowEnd");
            }
            else
            {
                return RedirectToAction("Show404");
            }
        }
        public ActionResult Show404()
        {
            return View();
        }
        public ActionResult ShowEnd()
        {
            return View();
        }

	}
}