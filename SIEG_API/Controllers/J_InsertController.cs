using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost("InsertBuyOrder")]
        public void InsertBuyOrder([FromBody] J_AddSellrrOrder orderInfo)
        {
            var seller = _context.SellerAddProduct
                .Where(s => s.ProductId == orderInfo.pID && s.Price == orderInfo.pPrice && s.ValIdity == true)
                .OrderBy(s => s.AddTime).First();

            Order order = new Order
            {
                SellerId = seller.MemberId,
                BuyerId = orderInfo.bID,
                ProductId = orderInfo.pID,
                Price = orderInfo.pPrice,
                State = "待出貨",
                Pay = orderInfo.pay,
                Receiver = orderInfo.receiver,
                ReceivingPhone = orderInfo.receivingPhone,
                ShippingAddress = orderInfo.shippingAddress,
                UpdateTime = DateTime.Now,
                DoneTime = DateTime.Now, // 正式版要拿掉
                AddTime = DateTime.Now,
            };
            // 正式版要連同賣價一起完成
            
            _context.Order.Add(order);
            _context.SaveChanges();
        }


        [HttpPost("AddMemberQuotePrice")]
        public void AddMemberQuoteInfo([FromBody] J_AddQuotePrice quote)
        {
            SellerAddProduct sQuote = new SellerAddProduct
            {
                ProductId= quote.pID,
                MemberId = quote.mID,
                Price = quote.pPrice,
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
