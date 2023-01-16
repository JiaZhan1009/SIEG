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
    public class B_SellerorderController : ControllerBase
    {
        private readonly SIEGContext _context;

        public B_SellerorderController(SIEGContext context)
        {
            _context = context;
        }

        // GET: api/B_Sellerorder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Order.ToListAsync();
        }

        // GET: api/B_Sellerorder/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<B_SellerorderDTO>> GetOrder(int MemberId)
        {
            var productIDs = _context.Order.Where(pd => pd.SellerId == MemberId);
            var Products = productIDs.OrderByDescending(time => time.AddTime)
                .Join(_context.Product, pd => pd.ProductId, pds => pds.ProductId, (pd, pds) => new B_SellerorderDTO
                {
                    ImgFront = pds.ImgFront,
                    ProductName = pds.Name,
                    OrderId = pd.OrderId,
                    Price = pd.Price,
                    Size = pds.Size,
                    State = pd.State,
                    CompleteTime = pd.AddTime,
                    ShippingAddress = pd.ShippingAddress,
                    Receiver = pd.Receiver,
                    Model=pds.Model,

                });
            return Products;
        }

        // PUT: api/B_Sellerorder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/B_Sellerorder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/B_Sellerorder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
