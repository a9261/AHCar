using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHCar.Models.ViewModels
{
    public class CARandORDERViewModel
    {
        public Models.Original.UserShopCar UserShopCar { get; set; }
        public Models.Order Order { get; set; }
    }
}