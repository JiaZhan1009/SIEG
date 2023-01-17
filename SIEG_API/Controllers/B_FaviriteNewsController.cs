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
    public class B_FaviriteNewsController : ControllerBase
    {
        private readonly SIEGContext _context;

        public B_FaviriteNewsController(SIEGContext context)
        {
            _context = context;
        }

        // GET: api/B_FaviriteNews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FaviriteNews>>> GetFaviriteNews()
        {
            return await _context.FaviriteNews.ToListAsync();
        }

        // GET: api/B_FaviriteNews/5
        [HttpGet("{MemberId}")]
        public async Task<IEnumerable<B_FaviriteNewsDTO>> GetFaviriteNews(int MemberId)
        {
            var FaviriteAritcleID= _context.FaviriteArticle.Where(FAD=>FAD.MemberId==MemberId).Select(FAID=> FAID.FaviriteAritcleId).ToArray();
            var NewsID=_context.News



            //var CouponId = _context.MemberCoupon.Where(cp => cp.MemberId == MemberId).Select(cpid => cpid.CouponId).ToArray();
            //var Newcoupon = new List<B_MemberCouponsDTO>();
            //foreach (var NewCouponId in CouponId)
            //{
            //    var CouponCount = _context.MemberCoupon.Where(cp => cp.MemberId == MemberId && cp.CouponId == NewCouponId).Select(cp2 => cp2.Count).First();
            //    var CouponAll = _context.Coupon.Where(cp => cp.CouponId == NewCouponId).Select(newcp => new B_MemberCouponsDTO
            //    {
            //        MemberId = MemberId,
            //        CouponName = newcp.Name,
            //        CouponId = NewCouponId,
            //        count = CouponCount,
            //        SerialNumber = newcp.Sn,
            //        DiscountPrice = newcp.DiscountPrice,


            //    }).First();
            //    Newcoupon.Add(CouponAll);
            //}
            //return Newcoupon;
        }

        // PUT: api/B_FaviriteNews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaviriteNews(int id, FaviriteNews faviriteNews)
        {
            if (id != faviriteNews.FaviriteNewsId)
            {
                return BadRequest();
            }

            _context.Entry(faviriteNews).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaviriteNewsExists(id))
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

        // POST: api/B_FaviriteNews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FaviriteNews>> PostFaviriteNews(FaviriteNews faviriteNews)
        {
            _context.FaviriteNews.Add(faviriteNews);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaviriteNews", new { id = faviriteNews.FaviriteNewsId }, faviriteNews);
        }

        // DELETE: api/B_FaviriteNews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaviriteNews(int id)
        {
            var faviriteNews = await _context.FaviriteNews.FindAsync(id);
            if (faviriteNews == null)
            {
                return NotFound();
            }

            _context.FaviriteNews.Remove(faviriteNews);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FaviriteNewsExists(int id)
        {
            return _context.FaviriteNews.Any(e => e.FaviriteNewsId == id);
        }
    }
}
