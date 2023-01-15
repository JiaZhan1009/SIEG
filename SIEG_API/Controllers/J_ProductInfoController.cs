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
                pName = list.Name,
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
            (p, s) => new { Product = p, seller = s })
            .GroupBy(p => p.Product.Size).AsNoTracking()
            .Select(g => g.OrderBy(g => g.seller.Price).First())
            .ToListAsync();

            List<J_PriceListDTO> priceList = sizeAndPrice.Select(p => new J_PriceListDTO
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
            var productName = _context.Product.Where(p => p.ProductId == val.pID).ToList()[0].Name;
            var BidInfo = await _context.BuyerBid
                .Include(b => b.Product)
                .Where(b => b.ValIdity == true && b.Product.Name == productName)
                .GroupBy(b => new { b.Product.Size, b.Price, b.Product.Name })
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
            var productName = _context.Product.Where(p => p.ProductId == val.pID).ToList()[0].Name;
            var QuoteInfo = await _context.SellerAddProduct
                .Include(s => s.Product)
                .Where(s => s.ValIdity == true && s.Product.Name == productName) // 待補上 s.SaleDate == null && 
                .GroupBy(s => new { s.Product.Size, s.Price, s.Product.Name })
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
            var productName = _context.Product.Where(p => p.ProductId == pID).ToList()[0].Name;
            var list = await _context.Order.Include(o => o.Product)
                .Where(o => o.Product.Name == productName && o.State == "已完成")
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
            var lsit = await _context.Order.Where(o => o.ProductId == pID) // 先拿掉 && o.State == "已完成" 有中文會錯誤
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

        //[HttpPut("{mId}")]
        //public async Task<string> PutEmployees(int id, J_PutBankDTO bankDTO)
        //{
            //if (id != empDTO.EmployeeId)
            //{
            //    return "ID不正確!";
            //}
        //    Member mb = await _context.Member.FindAsync(bankDTO.mID);
        //    mb. = bankDTO.FirstName;
        //    mb.LastName = bankDTO.LastName;
        //    mb.BankAccount = bankDTO.Title;
        //    _context.Entry(emp).State = EntityState.Modified;
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeesExists(id))
        //        {
        //            return "找不到欲修改的記錄!";
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return "修改成功!";
        //}

    }
}
