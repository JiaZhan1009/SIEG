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
    public class B_personalinformationController : ControllerBase
    {
        private readonly SIEGContext _context;

        public B_personalinformationController(SIEGContext context)
        {
            _context = context;
        }

        // GET: api/B_personalinformation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            return await _context.Member.ToListAsync();
        }

        // GET: api/B_personalinformation/5
        [HttpGet("{Memberid}")]
        public async Task<ActionResult<B_personalinformationDTO>> GetMember(int Memberid)
        {
            var member = await _context.Member.FindAsync(Memberid);

            if (member == null)
            {
                return NotFound();
            }
            B_personalinformationDTO personal = new B_personalinformationDTO
            {
                MemberId = member.MemberId,
                NickName = member.NickName,
                Email = member.Email,
                Password = member.Password,
                Address = member.Address,
                Phone = member.Phone,
                Name = member.Name,
            };

            return personal;
        }

        // PUT: api/B_personalinformation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Memberid}")]
        public async Task<string> PutMember(int Memberid, B_personalinformationDTO member)
        {
            if (Memberid != member.MemberId)
            {
                return "不正確";
            }
            Member memberinformation = await _context.Member.FindAsync(member.MemberId);
            memberinformation.NickName = member.NickName;
            memberinformation.Phone = member.Phone;
            memberinformation.Name = member.Name;
            _context.Entry(memberinformation).State = EntityState.Modified;

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

        // POST: api/B_personalinformation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            _context.Member.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
        }

        // DELETE: api/B_personalinformation/5
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