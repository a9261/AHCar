using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AHCar.Models
{
    public class OrderMetadata
    {
        [Required]
        [Display(Name = "姓名")]
        public string UserName;
        [Required]
        [Display(Name = "住址")]
        public string UserAddress;
        [Required]
        [Display(Name = "電話")]
        public string UserPhone;
    }
}