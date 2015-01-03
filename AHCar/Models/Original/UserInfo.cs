using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AHCar.Models.Original
{
    public class UserInfo
    {
        [Required]
        [Display(Name = "姓名")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "住址")]
        public string UserAddress { get; set; }
        [Required]
        [Display(Name = "聯絡電話")]
        public string UserPhone { get; set; }
        public string UserID { get; set; }
    }
}