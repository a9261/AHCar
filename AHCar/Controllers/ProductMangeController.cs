using AHCar.Models.Interface;
using AHCar.Models.Repositiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHCar.Controllers
{
    public class ProductMangeController : BaseController
    {
        //
        // GET: /ProductMange/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateProduct()
        {
            //IProductCategoryRepository pro = new ProductCategoryRepository();
            //return View(pro.GetAll());
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Models.Product p)
        {
            IProductRepository pro = new ProductRepository();
            pro.Create(p);

            return RedirectToAction("CreateProduct");
        }
	}
}