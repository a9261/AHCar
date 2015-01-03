using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHCar.Models.Interface;
using AHCar.Models.Repositiry;
using AHCar.Models;
using System.Transactions;
using System.Text;
namespace AHCar.Models.Original
{
    public class UserShopCar
    {
        private List<ShopItem> ShopItems = new List<ShopItem>();
        public UserInfo Userinfo { get; set; }

        public int Total { get; set; }
        public UserShopCar()
        {
            //初始化UserInfo 
            Userinfo = new UserInfo();
            Total = 0;
        }
        /// <summary>
        /// 新增商品至購物車
        /// </summary>
        /// <param name="item">商品</param>
        public void Add(ShopItem item)
        {
            //判斷有這項商品
            IProductRepository pRep = new ProductRepository();
            if (pRep.Get(item.ProductID) != default(Product)) {
                //防止數量被前端修改小於0
                if (item.Amount <= 0)
                {
                    item.Amount = 1;
                }
                //防止金額被前端修改
                item.Price = pRep.Get(item.ProductID).Price;

                ShopItems.Add(item);
                UpdateTotal();
            }
        }
        /// <summary>
        /// 從購物車移除商品
        /// </summary>
        /// <param name="item">商品</param>
        public void Remove(int id)
        {
            ShopItems.Remove(ShopItems.Find(x=>x.ProductID==id));
            UpdateTotal();
        }
        /// <summary>
        /// 取得購物車所有商品
        /// </summary>
        /// <returns></returns>
        public List<ShopItem> GetAllItems()
        {
            return ShopItems;
        }
        /// <summary>
        /// 更新購物車總金額
        /// </summary>
        public void UpdateTotal()
        {
            Total = 0;
            foreach (var item in ShopItems)
            {
                Total += item.Price * item.Amount;
            }
        }
        public void SendOrder()
        {
            using (TransactionScope scope = new TransactionScope()) {
                //新增訂單表頭
                Order oHead = new Order();
                oHead.Discount = 1.0;
                oHead.isPay = true;
                oHead.UserAddress = Userinfo.UserAddress;
                oHead.UserName = Userinfo.UserName;
                oHead.UserPhone = Userinfo.UserPhone;
                oHead.UserID = Userinfo.UserID;
                oHead.Total = Total;
                oHead.OrderDate = DateTime.Now;
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