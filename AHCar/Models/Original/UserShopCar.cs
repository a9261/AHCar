using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHCar.Models.Interface;
using AHCar.Models.Repositiry;
using AHCar.Models;
namespace AHCar.Models.Original
{
    public class UserShopCar
    {
        private List<ShopItem> ShopItems = new List<ShopItem>();
        
        public void Add(ShopItem item)
        {
            ShopItems.Add(item);
        }
        public void Remove(ShopItem item)
        {
            ShopItems.Remove(item);
        }
        public void SendOrder(UserInfo info)
        {
            Order orderhead = new Order();
            orderhead.Discount = 1.0;
            orderhead.isPay = true;
            orderhead.UserAddress=info.UserAddress;
            orderhead.UserName = info.UserName;
            orderhead.UserPhone = info.UserPhone;

        }
    }
}