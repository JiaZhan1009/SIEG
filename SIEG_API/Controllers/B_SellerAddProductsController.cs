using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    [Route("api/[controller]")]
    [ApiController]
    public class B_SellerAddProductsController : ControllerBase
    {
        private readonly SIEGContext _context;

        public B_SellerAddProductsController(SIEGContext context)
        {
            _context = context;
        }

        // GET: api/B_SellerAddProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerAddProduct>>> GetSellerAddProduct()
        {
            return await _context.SellerAddProduct.ToListAsync();
        }

        // GET: api/B_SellerAddProducts/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<B_SellerAddProductsDTO>> GetSellerAddProduct(int MemberId)
        {
            var sellproducts = _context.SellerAddProduct.Where(bb => bb.MemberId == MemberId && bb.ValIdity == true).Select(SdId => SdId.SellerAddProductId).ToArray();
            var allmessageslist = new List<B_SellerAddProductsDTO>();
            foreach (var SellerAddId in sellproducts)
            {
                var ProductId = _context.BuyerBid.Where(bb => bb.MemberId == MemberId && bb.BuyerBidId == SellerAddId).Select(pdId => pdId.ProductId).First();
                //var ID = _context.SellerAddProduct.Where(bb => bb.MemberId == MemberId && bb.ProductId == ProductId).Select(pdId => pdId.ProductId).First();
                var datetime = _context.SellerAddProduct.Where(bb => bb.MemberId == MemberId && bb.ProductId == ProductId).Select(pdId => pdId.AddTime).First();
                var BuylowPrice = await _context.BuyerBid.Where(pdId => pdId.ProductId == ProductId && pdId.ValIdity == true).OrderBy(lp => lp.Price).Select(lp => lp.Price).FirstOrDefaultAsync();
                var BuyhighPrice = await _context.BuyerBid.Where(pdId => pdId.ProductId == ProductId && pdId.ValIdity == true).OrderBy(lp => lp.Price).Select(lp => lp.Price).LastOrDefaultAsync();
                var sellPrice = _context.SellerAddProduct.Where(bb => bb.MemberId == MemberId && bb.ProductId == ProductId).Select(pdId => pdId.Price).First();
                var allmessages = _context.Product.Where(pn => pn.ProductId == ProductId).Select(y => new B_SellerAddProductsDTO
                {
                    SellerAddProductID = SellerAddId,
                    MemberId = MemberId,
                    ProductId = ProductId,
                    ProductName = y.Name,
                    ImgFront = y.ImgFront,
                    Price = (int)sellPrice,
                    lowPrice = BuylowPrice,
                    hightPrice = BuyhighPrice,
                    Size = y.Size,
                    Shelfdate = datetime
                }).First();
                allmessageslist.Add(allmessages);
            }
            return allmessageslist.OrderByDescending(a => a.Shelfdate);
        }

        // PUT: api/B_SellerAddProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSellerAddProduct(int id, SellerAddProduct sellerAddProduct)
        {
            if (id != sellerAddProduct.SellerAddProductId)
            {
                return BadRequest();
            }

            _context.Entry(sellerAddProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellerAddProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/B_SellerAddProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SellerAddProduct>> PostSellerAddProduct(SellerAddProduct sellerAddProduct)
        {
            _context.SellerAddProduct.Add(sellerAddProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSellerAddProduct", new { id = sellerAddProduct.SellerAddProductId }, sellerAddProduct);
        }

        // DELETE: api/B_SellerAddProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSellerAddProduct(int id)
        {
            var sellerAddProduct = await _context.SellerAddProduct.FindAsync(id);
            if (sellerAddProduct == null)
            {
                return NotFound();
            }

            _context.SellerAddProduct.Remove(sellerAddProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SellerAddProductExists(int id)
        {
            return _context.SellerAddProduct.Any(e => e.SellerAddProductId == id);
        }
    }
}
