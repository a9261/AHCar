using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHCar.Models.Interface;
using AHCar.Models.Repositiry;
using AHCar.Models;
using System.Transactions;
namespace AHCar.Models.Original
{
    public class UserShopCar
    {
        private List<ShopItem> ShopItems = new List<ShopItem>();
        public UserInfo Userinfo { get; set; }
        public UserShopCar()
        {
            //初始化UserInfo 
            Userinfo = new UserInfo();
        }
        /// <summary>
        /// 新增商品至購物車
        /// </summary>
        /// <param name="item">商品</param>
        public void Add(ShopItem item)
        {
            ShopItems.Add(item);
        }
        /// <summary>
        /// 從購物車移除商品
        /// </summary>
        /// <param name="item">商品</param>
        public void Remove(int id)
        {
            ShopItems.Remove(ShopItems.Find(x=>x.ProductID==id));
        }
        /// <summary>
        /// 取得購物車所有商品
        /// </summary>
        /// <returns></returns>
        public List<ShopItem> GetAllItems()
        {
            return ShopItems;
        }
        public void SendOrder()
        {
            using (TransactionScope scope = new TransactionScope()) {
                //新增訂單表頭
                int Total = 0;
                Order oHead = new Order();
                oHead.Discount = 1.0;
                oHead.isPay = true;
                oHead.UserAddress = Userinfo.UserAddress;
                oHead.UserName = Userinfo.UserName;
                oHead.UserPhone = Userinfo.UserPhone;
                oHead.UserID = Userinfo.UserID;
                foreach (var item in ShopItems)
                {
                    Total += item.Price * item.Amount;
                }
                oHead.Total = Total;
                IOrderRepository oRep = new OrderRepository();
                oRep.Create(oHead);
                //新增訂單表身
                int OrderID = oHead.OrderId;
                IOrderdetailRepository oDetailRep = new OrderdetailRepository();
                foreach (var item in ShopItems) { 
                    OrderDetail oDetail = new OrderDetail();
                    oDetail.OrderID = OrderID;
                    oDetail.Price = item.Price;
                    oDetail.ProductID = item.ProductID;
                    oDetail.Amount = item.Amount;
                    oDetailRep.Create(oDetail);
                }
                scope.Complete();
            }
        }
    }
}