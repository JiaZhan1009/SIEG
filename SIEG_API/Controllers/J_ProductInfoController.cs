using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SIEG_API.DTO;
using SIEG_API.Models;
using SIEG_API.Parameters;

namespace SIEG_API.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/J")]
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
        [HttpGet("GetProductInfo/{pID}")]
        public async Task<IEnumerable<J_ProductInfoDTO>> getProductInfo(int pID)
        {
            var pCateID = await _context.Product.Where(p => p.ProductId == pID).Select(p => p.ProductCategoryId).SingleOrDefaultAsync();
            var list = await _context.Product.Include(p => p.ProductCategory).Where(p => p.ProductCategoryId == pCateID).Select(p => new J_ProductInfoDTO
            {
                pID = pID,
                pName = p.ProductCategory.ProductName,
                pBrand = p.ProductCategory.BrandName,
                pCateID = pCateID,
                pCateName = p.ProductCategory.CategoryName,
                pImg = p.ImgFront,
                pSize = p.Size,
                pModel = p.Model
            })
            .ToListAsync();
            return list;
        }

        [HttpGet("GetSizeAndQuote/{pCateID}")]
        public async Task<IEnumerable<J_PriceListDTO>> GetSizeAndQuote(int pCateID)
        {
            var list = await _context.Procedures.GetSizeAndQuoteAsync(pCateID);
            return list.Select(x => new J_PriceListDTO {
                pID = x.pID,
                pSize = x.pSize,
                pPrice = x.quotePrice,
                sID = x.mID,
            }).OrderBy(x => float.Parse(x.pSize));
        }

        [HttpGet("GetSizeAndBid/{pCateID}")]
        public async Task<IEnumerable<Object>> GetSizeAndBid(int pCateID)
        {
            var list = await _context.Procedures.GetSizeAndBidAsync(pCateID);
            return list.Select(x => new J_PriceListDTO
            {
                pID = x.pID,
                pSize = x.pSize,
                pPrice = x.bidPrice,
                bID = x.bID,
            }).OrderBy(x => float.Parse(x.pSize));
        }

        [HttpGet("BidPriceList/{pID}")]
        public async Task<IEnumerable<J_PriceListDTO>> getBidPrice([FromRoute] PriceParameter val)
        {
            var productName = _context.Product.Include(p => p.ProductCategory).Where(p => p.ProductId == val.pID).ToList()[0].ProductCategory.ProductName;
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
            //return new List<J_PriceListDTO>();
        }

        [HttpGet("QuotePriceList/{pID}")]
        public async Task<IEnumerable<J_PriceListDTO>> getQuotePrice([FromRoute] PriceParameter val)
        {
            var productName = _context.Product.Include(p => p.ProductCategory).Where(p => p.ProductId == val.pID).ToList()[0].ProductCategory.ProductName;
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
        public async Task<IEnumerable<J_OrderListDTO>> GetOrderList([FromRoute] PriceParameter val)
        {
            var productName = _context.Product.Include(p => p.ProductCategory).Where(p => p.ProductId == val.pID).ToList()[0].ProductCategory.ProductName;
            var list = await _context.Order.Include(o => o.Product)
                .Where(o => o.Product.ProductCategory.ProductName == productName && o.DoneTime != null)//之後補上  && o.State == "已完成" )
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
            var lastPrice = 0;
            var price = await _context.Order.Where(o => o.ProductId == pID && o.DoneTime != null)//之後補上 && o.State == "已完成" )
               .OrderByDescending(o => o.DoneTime).Select(o => o.Price)
               .FirstOrDefaultAsync();
            if (price != null)
            {
                lastPrice = price;
            }
            //.MinAsync(o => o.價錢); 找最少的價格
            return lastPrice;
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

        [HttpGet("GetMemberBankInfo/{mID}")]
        public async Task<J_MemberBankInfoDTO> GetMemberBankInfo(int mID)
        {
            var Member = await _context.Member
                .Join(_context.Bank, m => m.BankCode, b => b.BankCode, (m, b) => new { member = m, bank = b })
                .Where(m => m.member.MemberId == mID)
                .Select(o => o).SingleOrDefaultAsync();
            if (Member != null)
            {
                var mDTO = new J_MemberBankInfoDTO
                {
                    mID = Member.member.MemberId,
                    mBankName = Member.bank.Name,
                    mBankCode = Member.member.BankCode,
                    mBankAccount = Member.member.BankAccount.ToString(),
                };
                return mDTO;
            }
            else
            {
                var mDTO = new J_MemberBankInfoDTO
                {
                    mID = mID,
                    mBankName = "",
                    mBankCode = "",
                    mBankAccount = ""
                };
                return mDTO;
            }
        }

        [HttpGet("GetMaxMinPrice/{pID}")]
        public async Task<object> GetMaxMinQuote(int pID)
        {
            var maxBid = 0;
            var minQuote = 0;
            var bid = await _context.BuyerBid.Where(s => s.ProductId == pID)
                .Select(s => s.Price).OrderByDescending(s => s).FirstOrDefaultAsync();
            var quote = await _context.SellerAddProduct.Where(s => s.ProductId == pID && s.Price != 0)
                .Select(s => s.Price).OrderBy(s => s).FirstOrDefaultAsync();
            if (bid != null)
            {
                maxBid = bid;
            }
            if (quote != null)
            {
                minQuote = quote;
            }
            var list = new
            {
                maxBid = maxBid,
                minQuote = minQuote
            };
            return list;
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

//[HttpGet("GetSizeAndQuote/{pCateID}")]
//public async Task<List<J_PriceListDTO>> GetSizeAndQuote(int pCateID)
//{
//    var sizeAndPrice = await _context.Product
//    .Join(_context.SellerAddProduct, product => product.ProductId, s => s.ProductId,
//    (p, s) => new { Product = p, seller = s, sID = s.MemberId })
//    .Where(p => p.seller.Price != 0 && p.Product.ProductCategoryId == pCateID)
//    .GroupBy(p => p.Product.Size)
//    .Select(g => g.OrderBy(g => g.seller.Price)
//    .First()).ToListAsync();

//    List<J_PriceListDTO> priceList = sizeAndPrice.Select(p => new J_PriceListDTO
//    {
//        sID = p.sID,
//        pID = p.Product.ProductId,
//        pPrice = p.seller.Price,
//        pSize = p.Product.Size
//    }).ToList();
//    return priceList;
//}