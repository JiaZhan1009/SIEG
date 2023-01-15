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
    [Route("api/[controller]")]
    [ApiController]
    public class E_ProductListController : ControllerBase
    {
        private readonly SIEGContext _context;

        public E_ProductListController(SIEGContext context)
        {
            _context = context;
        }

        // GET: api/E_ProductList
        [HttpGet]
        public async Task<IEnumerable<E_ProductListDTO>> GetProduct()
        {
            //pl = Product / ps = ProductCategory / pb = Product + ProductCategory / po = 訂單
            return await _context.Product
                .Join(_context.ProductCategory, pl => pl.ProductCategoryId, ps => ps.ProductCategoryId, (pl, ps) => new { pl, ps })
                .Where(x => x.pl.ValIdity == true)
                .OrderByDescending(x => x.pl.AddTime)
                .Select(x => new E_ProductListDTO
                {
                    productlistId = x.pl.ProductId,
                    productlistImg = x.pl.ImgFront,
                    productlistName = x.pl.Name,
                    productlistPrice = x.pl.Price,
                    productlistSort = x.ps.CategoryName,
                    productlistBrand = x.ps.BrandName,

                }).ToListAsync();

            ////pl = Product / ps = ProductCategory / pb = Product + ProductCategory / po = 訂單
            //return _context.商品
            //    .Join(_context.品牌, pl => pl.品牌Id, ps => ps.Id, (pl, ps) => new { pl, ps })

            //    .Join(_context.訂單, pb => pb.pl.品牌Id, po => po.商品Id, (pb, po) => new { pb, po })
            //    // x = pb (商品 + 品牌) + po (訂單)
            //    .Where(x => x.pb.pl.有效性 == true && x.pb.pl.品牌Id == x.po.商品Id && x.po.狀態 == "已完成")
            //    .OrderByDescending(x => x.pb.pl.上架日期)
            //    .Select(x => new E_ProductListDTO
            //{
            //    productlistId = x.pb.pl.Id,
            //    productlistImg = x.pb.pl.圖檔_正面,
            //    productlistName = x.pb.pl.商品名稱,
            //    productlistPrice = x.pb.pl.價格,
            //    productlistSort = x.pb.ps.類別名稱,
            //    productlistBrand = x.pb.ps.品牌名稱,

            //});
        }

        // Below500: api/E_ProductListDTO/Below500
        [HttpGet("Below500")]
        public async Task<IEnumerable<E_ProductListDTO>> Below500()
        {
            //pl = Product
            return await _context.Product
                .Where(pl => pl.Price <= 500)
                .Select(pl => new E_ProductListDTO
                {
                    productlistId = pl.ProductCategoryId,
                    productlistImg = pl.ImgFront,
                    productlistName = pl.Name,
                    productlistPrice = pl.Price,
                }).ToListAsync();
        }

        // Below1000: api/E_ProductListDTO/Below1000
        [HttpGet("Below1000")]
        public async Task<IEnumerable<E_ProductListDTO>> Below1000()
        {
            //pl = Product
            return await _context.Product
                .Where(pl => pl.Price <= 1000 && pl.Price >= 500)
                .Select(pl => new E_ProductListDTO
                {
                    productlistId = pl.ProductCategoryId,
                    productlistImg = pl.ImgFront,
                    productlistName = pl.Name,
                    productlistPrice = pl.Price,
                }).ToListAsync();
        }

        // Below1500: api/E_ProductListDTO/Below1500
        [HttpGet("Below1500")]
        public async Task<IEnumerable<E_ProductListDTO>> Below1500()
        {
            //pl = Product
            return await _context.Product
                .Where(pl => pl.Price <= 1500 && pl.Price >= 1000)
                .Select(pl => new E_ProductListDTO
                {
                    productlistId = pl.ProductCategoryId,
                    productlistImg = pl.ImgFront,
                    productlistName = pl.Name,
                    productlistPrice = pl.Price,
                }).ToListAsync();
        }

        // Below2000: api/E_ProductListDTO/Below2000
        [HttpGet("Below2000")]
        public async Task<IEnumerable<E_ProductListDTO>> Below2000()
        {
            //pl = Product
            return await _context.Product
                .Where(pl => pl.Price <= 2000 && pl.Price >= 1500)
                .Select(pl => new E_ProductListDTO
                {
                    productlistId = pl.ProductCategoryId,
                    productlistImg = pl.ImgFront,
                    productlistName = pl.Name,
                    productlistPrice = pl.Price,
                }).ToListAsync();
        }

        // More2000: api/E_ProductListDTO/More2000
        [HttpGet("More2000")]
        public async Task<IEnumerable<E_ProductListDTO>> More2000()
        {
            //pl = Product
            return await _context.Product
                .Where(pl => pl.Price >= 2000)
                .Select(pl => new E_ProductListDTO
                {
                    productlistId = pl.ProductCategoryId,
                    productlistImg = pl.ImgFront,
                    productlistName = pl.Name,
                    productlistPrice = pl.Price,
                }).ToListAsync();
        }

        // GET: api/E_ProductList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/E_ProductList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/E_ProductList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/E_ProductList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Filter: api/E_ProductList/Filter
        [HttpPost("Filter")]
        public async Task<IEnumerable<E_ProductListDTO>> FilterProduct([FromBody] E_ProductListDTO product)
        {
            return await _context.Product
                .Where(
                    pl => pl.Name.Contains(product.productlistName) &&
                            pl.ValIdity == true
                )
                .OrderByDescending(pl => pl.AddTime)
                .Select(pl => new E_ProductListDTO
                {
                    productlistId = pl.ProductCategoryId,
                    productlistImg = pl.ImgFront,
                    productlistName = pl.Name,
                    productlistPrice = pl.Price,
                }).ToListAsync();
        }

        // FilterSort: api/E_ProductList/FilterSort
        [HttpPost("FilterSort")]
        public async Task<IEnumerable<E_ProductListDTO>> FilterSort([FromBody] E_ProductListDTO product)
        {
            //pl = Product / ps = ProductCategory / pb = Product + ProductCategory
            return await _context.Product
                .Join(_context.ProductCategory, pl => pl.ProductCategoryId, ps => ps.ProductCategoryId, (pl, ps) => new { pl, ps })
                .Where(pb => pb.ps.CategoryName.Contains(product.productlistSort) && pb.pl.ValIdity == true)
                .OrderByDescending(pb => pb.pl.AddTime).Select(x => new E_ProductListDTO
                {
                    productlistId = x.pl.ProductCategoryId,
                    productlistImg = x.pl.ImgFront,
                    productlistName = x.pl.Name,
                    productlistPrice = x.pl.Price,
                    productlistSort = x.ps.CategoryName,
                    productlistBrand = x.ps.BrandName,
                }).ToListAsync();
        }

        // FilterBrand: api/E_ProductList/FilterBrand
        // Contains=包含
        [HttpPost("FilterBrand")]
        public async Task<IEnumerable<E_ProductListDTO>> FilterBrand([FromBody] E_ProductListDTO product)
        {
            //pl = Product / ps = ProductCategory / pb = Product + ProductCategory
            return await _context.Product
                .Join(_context.ProductCategory, pl => pl.ProductCategoryId, ps => ps.ProductCategoryId, (pl, ps) => new { pl, ps })
                .Where(pb => pb.ps.BrandName.Contains(product.productlistBrand) && pb.pl.ValIdity == true && pb.ps.CategoryName == product.productlistSort)
                .OrderByDescending(pb => pb.pl.AddTime).Select(x => new E_ProductListDTO
                {
                    productlistId = x.pl.ProductCategoryId,
                    productlistImg = x.pl.ImgFront,
                    productlistName = x.pl.Name,
                    productlistPrice = x.pl.Price,
                    productlistSort = x.ps.CategoryName,
                    productlistBrand = x.ps.BrandName,
                }).ToListAsync();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
