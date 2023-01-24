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
    [Route("api/J")]
    [ApiController]
    public class J_UpdateController : ControllerBase
    {
        private readonly SIEGContext _context;

        public J_UpdateController(SIEGContext context)
        {
            _context = context;
        }
      
        // PUT: api/J_Update/5
        [HttpPut("UpdataMemberInfo/{id}")]
        public async Task<IActionResult> PutMember(int id, J_MenberInfo member)
        {
            var memberList = _context.Member.Find(id);
            memberList.MemberId = id;
            memberList.Address = member.mAddress;
            memberList.Phone = member.mPhone;
            memberList.Name = member.mName;
            if (id != member.mID)
            {
                return BadRequest();
            }
            _context.Member.Update(memberList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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


        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.MemberId == id);
        }
    }
}