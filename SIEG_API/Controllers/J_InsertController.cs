using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SIEG_API.DTO;
using SIEG_API.Models;

namespace SIEG_API.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/J")]
    [ApiController]
    public class J_InsertController : ControllerBase
    {
        private readonly SIEGContext _context;

        public J_InsertController(SIEGContext context)
        {
            _context = context;
        }

        [HttpPost("InsertBuyerBid")]
        public void InsertBuyerBid([FromBody] J_AddBidPrice orderInfo)
        {
            BuyerBid bid = new BuyerBid
            {
                MemberId = orderInfo.mID,
                ProductId = orderInfo.pID,
                Price = orderInfo.pPrice,
                EffectiveTime = DateTime.Now
            };
            _context.BuyerBid.Add(bid);
            _context.SaveChanges();
        }

        [HttpPost("InsertBuyerOrder")]
        public void InsertBuyerOrder([FromBody] J_OrderInfo orderInfo)
        {
            // 先判斷是 buy 還是 bid
            // 先新增 Order 再新增 bid 最後修改 seller           
            
            if (orderInfo.info == "buy")
            {
                Order order = new Order
                {
                    SellerId = orderInfo.sID,
                    BuyerId = orderInfo.bID,
                    ProductId = orderInfo.pID,
                    Price = orderInfo.finalPrice,
                    State = "待出貨",
                    Pay = orderInfo.pay,
                    Receiver = orderInfo.receiver,
                    ReceivingPhone = orderInfo.receivingPhone,
                    ShippingAddress = orderInfo.shippingAddress,
                    UpdateTime = DateTime.Now,
                    //DoneTime = DateTime.Now, // 正式版要拿掉
                    AddTime = DateTime.Now,
                };
                //var seller = _context.SellerAddProduct.Where(s => s.ProductId == list.pID && s.Price == list.pPrice && s.ValIdity == true)
                //    .OrderBy(s => s.AddTime).First();
                //order.SellerId = seller.MemberId;

                _context.Order.Add(order);
                _context.SaveChanges();

                BuyerBid bid = new BuyerBid
                {
                    MemberId = orderInfo.bID,
                    ProductId = orderInfo.pID,
                    Price = orderInfo.pPrice,
                    FinalPrice = orderInfo.finalPrice,
                    EffectiveTime = DateTime.Now
                };
                _context.BuyerBid.Add(bid);
                _context.SaveChanges();
            }
            else if (orderInfo.info == "bid")
            {
                Order order = new Order
                {
                    //SellerId = seller.MemberId,
                    BuyerId = orderInfo.bID,
                    ProductId = orderInfo.pID,
                    Price = orderInfo.finalPrice,
                    State = "出價中",
                    Pay = orderInfo.pay,
                    Receiver = orderInfo.receiver,
                    ReceivingPhone = orderInfo.receivingPhone,
                    ShippingAddress = orderInfo.shippingAddress,
                    UpdateTime = DateTime.Now,
                    //DoneTime = DateTime.Now, // 正式版要拿掉
                    AddTime = DateTime.Now,
                };
                _context.Order.Add(order);
                _context.SaveChanges();

                BuyerBid bid = new BuyerBid
                {
                    MemberId = orderInfo.bID,
                    ProductId = orderInfo.pID,
                    Price = orderInfo.pPrice,
                    FinalPrice = orderInfo.finalPrice,
                    EffectiveTime = DateTime.Now
                };
                _context.BuyerBid.Add(bid);
                _context.SaveChanges();
            }       
            // 正式版要連同賣價一起完成
        }

        [HttpPost("InsertSellOrder")]
        public void InsertSellOrder([FromBody] J_OrderInfo list)
        {
            // 新增 Order 資料表: 



            // 修改 Quote 資料表: 填上 OrderID,



            // 修改 Bid 資料表: 根據 BuyerBidID 找到該筆資料, 填上 OrderID





            Order order = new Order
            {
                //SellerId = seller.MemberId,
                BuyerId = list.bID,
                ProductId = list.pID,
                Price = list.pPrice,
                State = "待出貨",
                Pay = list.pay,
                Receiver = list.receiver,
                ReceivingPhone = list.receivingPhone,
                ShippingAddress = list.shippingAddress,
                UpdateTime = DateTime.Now,
                DoneTime = DateTime.Now, // 正式版要拿掉
                AddTime = DateTime.Now,
            };

            if (list.info == "buy")
            {
                var seller = _context.SellerAddProduct.Where(s => s.ProductId == list.pID && s.Price == list.pPrice && s.ValIdity == true)
                    .OrderBy(s => s.AddTime).First();
                order.SellerId = seller.MemberId;
            }
            else if (list.info == "sell")
            {
                order.SellerId = list.sID;
            }
            // 正式版要連同賣價一起完成

            _context.Order.Add(order);
            _context.SaveChanges();

        }




        [HttpPost("InsertSellerQuote")]
        public void InsertSellerQuote([FromBody] J_AddQuotePrice quote)
        {
            SellerAddProduct sQuote = new SellerAddProduct
            {
                ProductId = quote.pID,
                MemberId = quote.sID,
                Price = quote.pPrice,
                FinalPrice = quote.finalPrice
            };
            _context.SellerAddProduct.Add(sQuote);
            _context.SaveChanges();
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.MemberId == id);
        }
    }
}
