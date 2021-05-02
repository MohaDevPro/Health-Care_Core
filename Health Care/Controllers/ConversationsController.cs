using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;

namespace Health_Care.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public ConversationsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/Conversations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversation()
        {
            return await _context.Conversation.ToListAsync();
        }

        // GET: api/Conversations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conversation>> GetConversation(int id)
        {
            var conversation = await _context.Conversation.FindAsync(id);

            if (conversation == null)
            {
                return NotFound();
            }

            return conversation;
        }

        // PUT: api/Conversations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConversation(int id, Conversation conversation)
        {
            if (id != conversation.id)
            {
                return BadRequest();
            }

            _context.Entry(conversation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversationExists(id))
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

        // POST: api/Conversations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Conversation>> PostConversation(Conversation conversation)
        {
            _context.Conversation.Add(conversation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConversation", new { id = conversation.id }, conversation);
        }

        // DELETE: api/Conversations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Conversation>> DeleteConversation(int id)
        {
            var conversation = await _context.Conversation.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }

            _context.Conversation.Remove(conversation);
            await _context.SaveChangesAsync();

            return conversation;
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversation.Any(e => e.id == id);
        }
    }
}
