using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHCar.Models.ViewModels
{
    public class PandCViewModel
    {
        public IQueryable<Models.Product> Producs { get; set; }
        public IQueryable<Models.ProductCategory> Cateogrys { get; set; }
    }
}