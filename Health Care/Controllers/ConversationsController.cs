using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;
using Microsoft.AspNetCore.Authorization;
using FirebaseAdmin.Messaging;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
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
            return await _context.Conversation.Where(c=>c.isRecived == false).ToListAsync();
        }
        [HttpGet("{userIdFrom}/{userIdTo}/{appointmentId}")]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversationByUserId(int userIdFrom,int userIdTo,int appointmentId)
        {
            return await _context.Conversation.Where(c=>c.isRecived == false && c.isReaded == false && c.userIdFrom == userIdFrom && c.userIdTo == userIdTo && c.appointmentId == appointmentId).ToListAsync();
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
        [HttpPost]
        public async Task<ActionResult<Conversation>> SendMessage(Conversation conversation)
        {
            _context.Conversation.Add(conversation);
            await _context.SaveChangesAsync();
            string ToUserFCMToken = _context.FCMTokens.Where(t => t.UserID == conversation.userIdTo).FirstOrDefault().Token;
            User FromUser = _context.User.Where(u => u.id == conversation.userIdFrom).FirstOrDefault();

            var message = new Message()
            {
                Notification = new Notification()
                {
                    Body = conversation.message,
                    Title = FromUser.nameAR,
                },
                Data = new Dictionary<string, string>() {
                    { "id", conversation.id.ToString() },
                    { "FromUser", FromUser.nameAR },
                    { "message", conversation.message }
                },
                Android = new AndroidConfig()
                {
                    Priority = Priority.High,

                    //TimeToLive = TimeSpan.FromDays(7),
                    Notification = new AndroidNotification()
                    {
                        Icon = "ic_launcher",
                        Color = "#f45342",
                    },
                },
                Apns = new ApnsConfig()
                {
                    Aps = new Aps()
                    {
                        //Alert = new ApsAlert() { Body = FromUser.name, Title = conversation.message, },
                        ContentAvailable = true,
                        Badge = 42,
                    },
                    Headers = new Dictionary<string, string>() {
                        { "apns-priority", "5" }, // Must be `5` when `contentAvailable` is set to true.
                        { "apns-topic", "io.flutter.plugins.firebase.messaging" } // bundle identifier
                    },
                },

                Token = ToUserFCMToken,
                };

                var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                // See the BatchResponse reference documentation
                // for the contents of response.
                Console.WriteLine($"{response} messages were sent successfully");

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
        [HttpPut("{id}")]
        public async Task<IActionResult> messageReceived(int id)
        {
            var converstion =  _context.Conversation.Where(x => x.id == id).FirstOrDefault();
            if (converstion == null)
            {
                return NotFound();
            }
            converstion.isRecived = true;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> messageReaded(int id)
        {
            var converstion =  _context.Conversation.Where(x => x.id == id).FirstOrDefault();
            if (converstion == null)
            {
                return NotFound();
            }
            converstion.isReaded = true;
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
