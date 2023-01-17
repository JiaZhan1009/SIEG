﻿using System;
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
            //var ProductList = await _context.Order.Include(o => o.Product).Include(o => o.Product.SellerAddProduct).Include(o => o.Product.ProductCategory)

                var ProductList = await _context.Product.Include(o => o.ProductCategory)
                .Where(x => x.ValIdity == true)
                .GroupBy(x => new { x.Name, x.ImgFront })
                .Select(g => new E_ProductListDTO
                {
                    productlistSellCount = g.Count(),
                    productlistName = g.Key.Name,
                    productlistImg = g.Key.ImgFront,
                    productlistPrice = g.Min(x => x.Price),
                }).ToListAsync();
            //if (!string.IsNullOrEmpty(name)) 
            //{
            //    ProductList = ProductList.Where(x => x.productlistName.Contains(name)).ToList();
            //}
            return ProductList;
        }

        // Below500: api/E_ProductListDTO/Below500
        [HttpGet("Below500")]
        public async Task<IEnumerable<E_ProductListDTO>> Below500()
        {
            var ProductList = await _context.Product.Include(o => o.ProductCategory)
                .Where(x => x.ValIdity == true && x.Price <= 500)
                .GroupBy(x => new { x.Name, x.ImgFront })
                .Select(g => new E_ProductListDTO
                {
                    productlistSellCount = g.Count(),
                    productlistName = g.Key.Name,
                    productlistImg = g.Key.ImgFront,
                    productlistPrice = g.Min(x => x.Price),
                }).ToListAsync();

            return ProductList;
        }

        // Below1000: api/E_ProductListDTO/Below1000
        [HttpGet("Below1000")]
        public async Task<IEnumerable<E_ProductListDTO>> Below1000()
        {
            var ProductList = await _context.Product.Include(o => o.ProductCategory)
                .Where(x => x.ValIdity == true && x.Price <= 1000 && x.Price >= 500)
                .GroupBy(x => new { x.Name, x.ImgFront })
                .Select(g => new E_ProductListDTO
                {
                    productlistSellCount = g.Count(),
                    productlistName = g.Key.Name,
                    productlistImg = g.Key.ImgFront,
                    productlistPrice = g.Min(x => x.Price),
                }).ToListAsync();

            return ProductList;
        }

        // Below1500: api/E_ProductListDTO/Below1500
        [HttpGet("Below1500")]
        public async Task<IEnumerable<E_ProductListDTO>> Below1500()
        {
            var ProductList = await _context.Product.Include(o => o.ProductCategory)
                .Where(x => x.ValIdity == true && x.Price <= 1500 && x.Price >= 1000)
                .GroupBy(x => new { x.Name, x.ImgFront })
                .Select(g => new E_ProductListDTO
                {
                    productlistSellCount = g.Count(),
                    productlistName = g.Key.Name,
                    productlistImg = g.Key.ImgFront,
                    productlistPrice = g.Min(x => x.Price),
                }).ToListAsync();

            return ProductList;
        }

        // Below2000: api/E_ProductListDTO/Below2000
        [HttpGet("Below2000")]
        public async Task<IEnumerable<E_ProductListDTO>> Below2000()
        {
            var ProductList = await _context.Product.Include(o => o.ProductCategory)
                .Where(x => x.ValIdity == true && x.Price <= 2000 && x.Price >= 1500)
                .GroupBy(x => new { x.Name, x.ImgFront })
                .Select(g => new E_ProductListDTO
                {
                    productlistSellCount = g.Count(),
                    productlistName = g.Key.Name,
                    productlistImg = g.Key.ImgFront,
                    productlistPrice = g.Min(x => x.Price),
                }).ToListAsync();

            return ProductList;
        }

        // More2000: api/E_ProductListDTO/More2000
        [HttpGet("More2000")]
        public async Task<IEnumerable<E_ProductListDTO>> More2000()
        {
            var ProductList = await _context.Product.Include(o => o.ProductCategory)
                .Where(x => x.ValIdity == true && x.Price >= 2000)
                .GroupBy(x => new { x.Name, x.ImgFront })
                .Select(g => new E_ProductListDTO
                {
                    productlistSellCount = g.Count(),
                    productlistName = g.Key.Name,
                    productlistImg = g.Key.ImgFront,
                    productlistPrice = g.Min(x => x.Price),
                }).ToListAsync();

            return ProductList;
        }

        // More2000: api/E_ProductListDTO/TopProduct
        [HttpGet("TopProduct")]
        public async Task<IEnumerable<E_ProductListDTO>> TopProduct()
        {

            var ProductList = await _context.Order.Include(o => o.Product).Include(o => o.Product.SellerAddProduct).Include(o => o.Product.ProductCategory)
            .Where(x => x.Product.ValIdity == true && x.State == "已完成" )
            .GroupBy(x => new { x.Product.Name, x.Product.ImgFront })
            .OrderByDescending(x => x.Count()).Take(8)
            .Select(g => new E_ProductListDTO
            {
                productlistSellCount = g.Count(),
                productlistName = g.Key.Name,
                productlistImg = g.Key.ImgFront,
                productlistPrice = g.Min(x => x.Price),
            }).ToListAsync();

            return ProductList;
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
        public async Task<IActionResult> PutProduct(int id, E_ProductListDTO E_ProductViewDTO)
        {
            if (id != E_ProductViewDTO.productlistId)
            {
                return BadRequest();
            }
            Product ProductView = await _context.Product.FindAsync(E_ProductViewDTO.productlistId);

            ProductView.ViewsCount = E_ProductViewDTO.productlistViewcount;

            _context.Entry(ProductView).State = EntityState.Modified;

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
            var ProductList = await _context.Product.Include(o => o.ProductCategory)
                .Where(x => x.ValIdity == true && x.Name.Contains(product.productlistName))
                .GroupBy(x => new { x.Name, x.ImgFront })
                .Select(g => new E_ProductListDTO
                {
                    productlistSellCount = g.Count(),
                    productlistName = g.Key.Name,
                    productlistImg = g.Key.ImgFront,
                    productlistPrice = g.Min(x => x.Price),
                }).ToListAsync();

            return ProductList;
        }

        // FilterSort: api/E_ProductList/FilterSort
        [HttpPost("FilterSort")]
        public async Task<IEnumerable<E_ProductListDTO>> FilterSort([FromBody] E_ProductListDTO product)
        {

            var ProductList = await _context.Product.Include(o => o.ProductCategory)
                .Where(x => x.ValIdity == true && x.ProductCategory.CategoryName.Contains(product.productlistSort))
                .GroupBy(x => new { x.Name, x.ImgFront })
                .Select(g => new E_ProductListDTO
                {
                    productlistSellCount = g.Count(),
                    productlistName = g.Key.Name,
                    productlistImg = g.Key.ImgFront,
                    productlistPrice = g.Min(x => x.Price),
                }).ToListAsync();

            return ProductList;
        }

        // FilterBrand: api/E_ProductList/FilterBrand
        // Contains=包含
        [HttpPost("FilterBrand")]
        public async Task<IEnumerable<E_ProductListDTO>> FilterBrand([FromBody] E_ProductListDTO product)
        {
            var ProductList = await _context.Product.Include(o => o.ProductCategory)
                .Where(x => x.ValIdity == true && x.ProductCategory.CategoryName.Contains(product.productlistSort) && x.ProductCategory.BrandName.Contains(product.productlistBrand))
                .GroupBy(x => new { x.Name, x.ImgFront })
                .Select(g => new E_ProductListDTO
                {
                    productlistSellCount = g.Count(),
                    productlistName = g.Key.Name,
                    productlistImg = g.Key.ImgFront,
                    productlistPrice = g.Min(x => x.Price),
                }).ToListAsync();
            return ProductList;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
