using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<ForumReply2>> GetForumReply2(int id)
        {
            return await _context.ForumReply2.Where(c => c.文章Id == id).ToListAsync();

        }

        // PUT: api/G_ForumReply2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForumReply2(int id, ForumReply2 forumReply2)
        {
            if (id != forumReply2.ForumReply2Id)
            {
                return BadRequest();
            }

            _context.Entry(forumReply2).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForumReply2Exists(id))
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

        // POST: api/G_ForumReply2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ForumReply2>> PostForumReply2(ForumReply2 forumReply2)
        {
            _context.ForumReply2.Add(forumReply2);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetForumReply2", new { id = forumReply2.ForumReply2Id }, forumReply2);
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
