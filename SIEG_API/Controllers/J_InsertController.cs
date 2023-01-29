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
        public async Task InsertBuyerOrderAsync([FromBody] J_OrderInfo orderInfo)
        {
            try
            {
                // insert Order 
                Order order = new Order
                {
                    BuyerId = orderInfo.bID,
                    ProductId = orderInfo.pID,
                    BuyerPrice = orderInfo.finalPrice,                    
                    Pay = orderInfo.pay,
                    Receiver = orderInfo.receiver,
                    ReceivingPhone = orderInfo.receivingPhone,
                    ShippingAddress = orderInfo.shippingAddress,
                    UpdateTime = DateTime.Now,
                    //DoneTime = DateTime.Now, // 正式版要拿掉
                    AddTime = DateTime.Now,
                };

                if (orderInfo.info == "buy")
                {
                    order.State = "待出貨";
                    order.SellerId = orderInfo.sID;
                    order.SellerPrice = orderInfo.sellerPrice;
                    _context.Order.Add(order);
                    await _context.SaveChangesAsync();

                    // insert Bid
                    BuyerBid bid = new BuyerBid
                    {
                        OrderId = order.OrderId,
                        MemberId = orderInfo.bID,
                        ProductId = orderInfo.pID,
                        Price = (int)orderInfo.buyerPrice,
                        FinalPrice = orderInfo.finalPrice,
                        EffectiveTime = DateTime.Now
                    };
                    _context.BuyerBid.Add(bid);
                    await _context.SaveChangesAsync();

                    // update Quote
                    var quote = await _context.SellerAddProduct.FirstOrDefaultAsync(x => x.SellerAddProductId == orderInfo.quoteID);
                    quote.OrderId = order.OrderId;
                    _context.SellerAddProduct.Update(quote);
                    await _context.SaveChangesAsync();

                }
                else if (orderInfo.info == "bid")
                {
                    // insert Order 
                    order.State = "出價中";
                    _context.Order.Add(order);
                    await _context.SaveChangesAsync();

                    // insert Bid
                    BuyerBid bid = new BuyerBid
                    {
                        OrderId = order.OrderId,
                        MemberId = orderInfo.bID,
                        ProductId = orderInfo.pID,
                        Price = (int)orderInfo.buyerPrice,
                        FinalPrice = orderInfo.finalPrice,
                        EffectiveTime = DateTime.Now
                    };
                    _context.BuyerBid.Add(bid);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {     
                Console.WriteLine(ex.Message);
            }
        }


        [HttpPost("InsertSellOrder")]
        public async Task InsertSellOrder([FromBody] J_OrderInfo orderInfo)
        {
            // error check :
            if (orderInfo.bidID == 0 || orderInfo.sID == 0 || orderInfo.quoteID == 0)
                throw new Exception("無法取得訂單資訊");

            var bid = await _context.BuyerBid.FindAsync(orderInfo.bidID);
            if (bid == null)
                throw new Exception("無法取得競標資訊");

            // alter Order: 
            var order = await _context.Order.Where(o => o.OrderId == bid.OrderId).SingleOrDefaultAsync();
            if (order == null)
                throw new Exception("無法取得訂單資訊");

            order.State = "待出貨";
            order.SellerId = orderInfo.sID;
            order.SellerPrice = orderInfo.finalPrice;
            order.UpdateTime = DateTime.Now;

            _context.Order.Update(order);
            await _context.SaveChangesAsync();

            // insert Quote : 填上 OrderID
            SellerAddProduct quote = new SellerAddProduct
            {
                OrderId = order.OrderId,
                FinalPrice = orderInfo.finalPrice,
                MemberId = orderInfo.sID,
                Price = (int)orderInfo.sellerPrice,
                ProductId = orderInfo.pID,
            };

            _context.SellerAddProduct.Add(quote);
            await _context.SaveChangesAsync();
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
