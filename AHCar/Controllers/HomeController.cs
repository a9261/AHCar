using AHCar.Models.Repositiry;
using AHCar.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHCar.Models;
namespace AHCar.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //取得前六筆商品資料
            using (AHShoppingEntities db = new AHShoppingEntities())
            {
                var result = (
                    from item in db.Products
                    orderby item.ProductID
                    select item
                    ).Take(6).ToList();
               
                return View(result);
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}