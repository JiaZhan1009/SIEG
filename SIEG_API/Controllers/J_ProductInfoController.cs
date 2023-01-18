using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SIEG_API.DTO;
using SIEG_API.Models;
using SIEG_API.Parameters;

namespace SIEG_API.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api")]
    //[Route("api/[controller]")]
    [ApiController]
    public class J_ProductInfoController : ControllerBase
    {
        private readonly SIEGContext _context;
        public J_ProductInfoController(SIEGContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet("ProductInfo/{pID}")]
        public async Task<J_ProductInfoDTO> getProductInfo(int pID)
        {
            var list = await _context.Product.Include(p => p.ProductCategory)
            .SingleOrDefaultAsync(p => p.ProductId == pID);

            return new J_ProductInfoDTO
            {
                pID = list.ProductId,
                pCategory = list.ProductCategory.CategoryName,
                pName = list.ProductCategory.ProductName,
                pImg = list.ImgFront,
                pBrand = list.ProductCategory.BrandName,
                pSize = list.Size,
            };
        }


        [HttpGet("GetSizeAndPriceList")]
        public async Task<List<J_PriceListDTO>> GetSizeAndPriceList()
        {
            var sizeAndPrice = await _context.Product
            .Join(_context.SellerAddProduct, product => product.ProductId, s => s.ProductId,
            (p, s) => new { Product = p, seller = s }).ToListAsync();

            var bbb = sizeAndPrice.GroupBy(p => p.Product.Size);

            var aaa = bbb.Select(g => g.OrderBy(g => g.seller.Price).First());


            List<J_PriceListDTO> priceList = aaa.Select(p => new J_PriceListDTO
            {
                pID = p.Product.ProductId,
                pPrice = p.seller.Price,
                pSize = p.Product.Size
            }).ToList();
            return priceList;
        }

        [HttpGet("BidPriceList/{pID}")]
        public async Task<IEnumerable<J_PriceListDTO>> getBidPrice([FromRoute] PriceParameter val)
        {
            var productName = _context.Product.Where(p => p.ProductId == val.pID).ToList()[0].ProductCategory.ProductName;
            var BidInfo = await _context.BuyerBid
                .Include(b => b.Product)
                .Where(b => b.ValIdity == true && b.Product.ProductCategory.ProductName == productName)
                .GroupBy(b => new { b.Product.Size, b.Price, b.Product.ProductCategory.ProductName })
                .Select(x => new J_PriceListDTO
                {
                    pID = val.pID,
                    pPrice = x.Key.Price,
                    pSize = x.Key.Size,
                    pCount = x.Sum(b => b.Count)
                })
                .OrderBy(x => x.pPrice).ToListAsync();

            return BidInfo;
        }

        [HttpGet("QuotePriceList/{pID}")]
        public async Task<IEnumerable<J_PriceListDTO>> getQuotePrice([FromRoute] PriceParameter val)
        {
            var productName = _context.Product.Where(p => p.ProductId == val.pID).ToList()[0].ProductCategory.ProductName;
            var QuoteInfo = await _context.SellerAddProduct
                .Include(s => s.Product)
                .Where(s => s.ValIdity == true && s.Product.ProductCategory.ProductName == productName && s.SaleDate == null) // 待補上 s.SaleDate == null && 
                .GroupBy(s => new { s.Product.Size, s.Price, s.Product.ProductCategory.ProductName })
                .Select(x => new J_PriceListDTO
                {
                    pID = val.pID,
                    pPrice = x.Key.Price,
                    pSize = x.Key.Size,
                    pCount = x.Sum(b => b.Count)
                })
                .OrderBy(x => x.pPrice).ToListAsync();

            return QuoteInfo;
        }

        [HttpGet("GetOrderList/{pID}")]
        public async Task<IEnumerable<J_OrderListDTO>> GetOrderList(int pID)
        {
            var productName = _context.Product.Where(p => p.ProductId == pID).ToList()[0].ProductCategory.ProductName;
            var list = await _context.Order.Include(o => o.Product)
                .Where(o => o.Product.ProductCategory.ProductName == productName && o.State == "已完成" && o.DoneTime != null)
                .OrderByDescending(o => o.DoneTime)
                .Select(x => new J_OrderListDTO
                {
                    oID = x.Product.ProductId,
                    oPrice = x.Price,
                    oTime = x.DoneTime,
                    oSize = x.Product.Size,
                }).ToListAsync();

            return list;
        }

        [HttpGet("GetLastDealPrice/{pID}")]
        public async Task<int?> GetLastDealPrice(int pID)
        {
            var lsit = await _context.Order.Where(o => o.ProductId == pID && o.State == "已完成") // 先拿掉 && o.State == "已完成" 有中文會錯誤
               .OrderByDescending(o => o.DoneTime).Select(o => o.Price)
               .FirstOrDefaultAsync();
            //.MinAsync(o => o.價錢); 找最少的價格
            return lsit;
        }

        [HttpGet("GetMemberInfo/{mID}")]
        public async Task<J_MenberInfo> GetMemberInfo(int mID)
        {
            var Member = await _context.Member.Where(o => o.MemberId == mID).FirstOrDefaultAsync();
            var mDTO = new J_MenberInfo
            {
                mID = Member.MemberId,
                mName = Member.Name,
                mAddress = Member.Address,
                mPhone = Member.Phone
            };

            return mDTO;
        }
    }
}




//var BrandName = "Air Jordan";
//var CategoryName = "高檔鞋履";

//var SaleInfo = await _context.Order.Include(o => o.Product).Include(o => o.Product.SellerAddProduct)
//    .Include(o => o.Product.ProductCategory).Where(x => x.Product.ProductCategory.CategoryName
//    .Contains(CategoryName) && x.Product.ProductCategory.BrandName.Contains(BrandName))
//    .GroupBy(x => new { x.Product.Name, x.Product.ImgFront })
//    .Select(g => new J_SaleCount
//    {
//        SaleCount = g.Count(),
//        Name = g.Key.Name,
//        Img = g.Key.ImgFront,
//        minPrice = g.Min(x => x.Price)
//    }).ToListAsync();

//return SaleInfo;

//var saleCount = from o in _context.Order
//                join p in _context.Product on o.ProductId equals p.ProductId
//                join s in _context.SellerAddProduct on p.ProductId equals s.ProductId
//                join pc in _context.ProductCategory on p.ProductCategoryId equals pc.ProductCategoryId
//                //where pc.CategoryName.Contains("高檔鞋履")
//                group new { p, s } by p.Name into g
//                select new
//                {
//                    Img = g,
//                    銷售數量 = g.Count(),
//                    g.Key,
//                    最低價 = g.Min(x => x.s.Price)
//                };