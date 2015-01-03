using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AHCar.Models.Original
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }
        public string UserID { get; set; }
    }
}