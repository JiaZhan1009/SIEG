using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.EntityFrameworkCore;
using SIEG_API.DTO;
using SIEG_API.Models;

namespace SIEG_API.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class B_PaymentInformationController : ControllerBase
    {
        private readonly SIEGContext _context;

        public B_PaymentInformationController(SIEGContext context)
        {
            _context = context;
        }

        // GET: api/B_PaymentInformation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            return await _context.Member.ToListAsync();
        }

        // GET: api/B_PaymentInformation/5
        [HttpGet("{Memberid}")]
        public async Task<ActionResult<B_PaymentInformationDTO>> GetMember(int Memberid)
        {
            var member = await _context.Member.FindAsync(Memberid);

            if (member == null)
            {
                return NotFound();
            }
            B_PaymentInformationDTO PaymentInformation = new B_PaymentInformationDTO
            {
                MemberId = member.MemberId,
                CreditCard = member.CreditCard,
                CreditCardDate = member.CreditCardDate,
                CreditCardCCV = member.CreditCardCcv,
                Name = member.Name,
                BillingAddress = member.BillingAddress,
                Phone = member.Phone,
            };
            return PaymentInformation;
        }

        // PUT: api/B_PaymentInformation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Memberid}")]
        public async Task<string> PutMember(int Memberid, B_PaymentInformationDTO member)
        {
            if (Memberid != member.MemberId)
            {
                return "不正確";
            }
            Member PaymentInformation = await _context.Member.FindAsync(member.MemberId);
            PaymentInformation.BillingAddress=member.BillingAddress;
            PaymentInformation.CreditCard = member.CreditCard;
            PaymentInformation.CreditCardDate = member.CreditCardDate;
            PaymentInformation.CreditCardCcv = member.CreditCardCCV;
            _context.Entry(PaymentInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(Memberid))
                {
                    return "找不到欲修改紀錄";
                }
                else
                {
                    throw;
                }
            }

            return "修改成功!";
        }

        // POST: api/B_PaymentInformation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            _context.Member.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
        }

        // DELETE: api/B_PaymentInformation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Member.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.MemberId == id);
        }
    }
}
