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
    public class G_ForumReply2Controller : ControllerBase
    {
        private readonly SIEGContext _context;

        public G_ForumReply2Controller(SIEGContext context)
        {
            _context = context;
        }

        // GET: api/G_ForumReply2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForumReply2>>> GetForumReply2()
        {
            return await _context.ForumReply2.ToListAsync();
        }

        // GET: api/G_ForumReply2/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ForumReply2>>> GetForumReply2(int id)
        {
            return await _context.ForumReply2.Where(c => c.ArticleId == id).ToListAsync();

        }

        // PUT: api/G_ForumReply2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutForumReply2(int id, G_ForumReply2DTO g_ForumReply2DTO)
        {
            if (id != g_ForumReply2DTO.ForumReply2Id)
            {
                return "ID不正確";
            }

            ForumReply2 pos = await _context.ForumReply2.FindAsync(id);
            pos.ForumReply2Content = g_ForumReply2DTO.ForumReply2Content;
            pos.Img = g_ForumReply2DTO.Img;
            pos.ValIdity = g_ForumReply2DTO.ValIdity;
            pos.LikeCount = g_ForumReply2DTO.LikeCount;

            _context.Entry(pos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForumReply2Exists(id))
                {
                    return "找不到欲修改的資料";
                }
                else
                {
                    throw;
                }
            }

            return "修改成功";
        }

        // POST: api/G_ForumReply2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ForumReply2>> PostForumReply2(G_ForumReply2DTO forumReply2)
        {
            ForumReply2 pos = new ForumReply2
            {
                ArticleId = forumReply2.ArticleId,
                ForumReplyId = forumReply2.ForumReplyId,
                MemberId = forumReply2.MemberId,
                ForumReplyFloor = forumReply2.ForumReplyFloor,
                Floor = forumReply2.Floor,
                ForumReply2Content = forumReply2.ForumReply2Content,
                Img = forumReply2.Img,
            };

            _context.ForumReply2.Add(pos);
            await _context.SaveChangesAsync();

            return pos;
        }

        // DELETE: api/G_ForumReply2/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForumReply2(int id)
        {
            var forumReply2 = await _context.ForumReply2.FindAsync(id);
            if (forumReply2 == null)
            {
                return NotFound();
            }

            _context.ForumReply2.Remove(forumReply2);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForumReply2Exists(int id)
        {
            return _context.ForumReply2.Any(e => e.ForumReply2Id == id);
        }
    }
}
