using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Authorization;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "admin")]

    public class NotificationsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public NotificationsController(Health_CareContext context)
        {
            _context = context;

        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notifications>>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notifications>> GetNotifications(int id)
        {
            var notifications = await _context.Notifications.FindAsync(id);

            if (notifications == null)
            {
                return NotFound();
            }

            return notifications;
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotifications(int id, Notifications notifications)
        {
            if (id != notifications.ID)
            {
                return BadRequest();
            }

            _context.Entry(notifications).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationsExists(id))
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

        // POST: api/Notifications
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Notifications>> PostNotifications(Notifications notifications)
        {
            _context.Notifications.Add(notifications);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotifications", new { id = notifications.ID }, notifications);
        }

        [HttpPost]
        public async Task<ActionResult> SendNotifications(Notifications notifications)
        {
            //_context.Notifications.Add(notifications);
            // Create a list containing up to 500 registration tokens.
            // These registration tokens come from the client FCM SDKs.

            List<string> registrationTokens = _context.FCMTokens.Select(t => t.Token).ToList();
            var y = registrationTokens.GetRange(0, 0 + 500 < registrationTokens.Count ? 0 + 500 : registrationTokens.Count);
            for (int i = 0; i < registrationTokens.Count; i += 500)
            {
                var message = new MulticastMessage()
                {
                    Notification = new Notification()
                    {
                        Body = notifications.body,
                        Title = notifications.title
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
                            Alert = new ApsAlert() { Body = notifications.body, Title = notifications.title, },
                          CriticalSound =new CriticalSound(){ Critical = true,Volume=1.0},
                            ContentAvailable = true,
                            Badge = 42,
                        },
                        Headers = new Dictionary<string, string>() {
                            {"apns-push-type", "background"},
                            { "apns-priority", "5" }, // Must be `5` when `contentAvailable` is set to true.
                            { "apns-topic", "io.flutter.plugins.firebase.messaging"} // bundle identifier
                    },
                    },

                    Tokens = registrationTokens.GetRange(i, i + 500 < registrationTokens.Count ? i + 500 : registrationTokens.Count),
                };

                var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
                // See the BatchResponse reference documentation
                // for the contents of response.
                Console.WriteLine($"{response.SuccessCount} messages were sent successfully");
            }

            return Ok();
        }
        [NonAction]
        public async Task<ActionResult> SendNotificationsToManyUsers(List<int> usersIDs,Notifications notifications)
        {
            //_context.Notifications.Add(notifications);
            // Create a list containing up to 500 registration tokens.
            // These registration tokens come from the client FCM SDKs.

            List<string> registrationTokens = _context.FCMTokens.Where(f=> usersIDs.Contains(f.UserID)).Select(t => t.Token).ToList();
            var y = registrationTokens.GetRange(0, 0 + 500 < registrationTokens.Count ? 0 + 500 : registrationTokens.Count);
            for (int i = 0; i < registrationTokens.Count; i += 500)
            {
                var message = new MulticastMessage()
                {
                    Notification = new Notification()
                    {
                        Body = notifications.body,
                        Title = notifications.title
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
                            Alert = new ApsAlert() { Body = notifications.body, Title = notifications.title, },
                          CriticalSound =new CriticalSound(){ Critical = true,Volume=1.0},
                            ContentAvailable = true,
                            Badge = 42,
                        },
                        Headers = new Dictionary<string, string>() {
                            {"apns-push-type", "background"},
                            { "apns-priority", "5" }, // Must be `5` when `contentAvailable` is set to true.
                            { "apns-topic", "io.flutter.plugins.firebase.messaging"} // bundle identifier
                    },
                    },

                    Tokens = registrationTokens.GetRange(i, i + 500 < registrationTokens.Count ? i + 500 : registrationTokens.Count),
                };

                var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
                // See the BatchResponse reference documentation
                // for the contents of response.
                Console.WriteLine($"{response.SuccessCount} messages were sent successfully");
            }

            return Ok();
        }

        [Authorize(Roles = "admin, مريض")]
        [HttpPost("{userIDTo}")]
        public async Task<ActionResult> SendNotificationsToUser( int userIDTo, Notifications notifications)
        {

            var ToUser = _context.User.Where(u => u.id == userIDTo).FirstOrDefault();
            if (userIDTo == 0)
            {
                return NotFound();
            }
            else
            {
                string ToUserFCMToken = _context.FCMTokens.Where(t => t.UserID == userIDTo).FirstOrDefault().Token;

                {
                    var message = new Message()
                    {
                        Notification = new Notification()
                        {
                            Title = notifications.title,
                            Body = notifications.body,
                        },
                        Data = new Dictionary<string, string>() {
                        //{ "rout", rout },
                        { "message", $"{ToUser.nameAR } " },
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
                                Alert = new ApsAlert() { Body = notifications.body, Title = notifications.title, },
                                Sound = "",
                                ContentAvailable = true,
                                Badge = 42,
                            },
                            Headers = new Dictionary<string, string>() {
                            { "apns-push-type", "background" },
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
                }

                return Ok();

            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> FCMTokenSave(FCM_Tokens token)
        {

            if (token.Token.Count() == 163 && token.Token.Split(':')[0].Count() == 22 && token.DeviceID != null)
            {
                var Token = _context.FCMTokens.Where(t => t.DeviceID == token.DeviceID).ToList();
                if (Token.Count == 0)
                {
                    
                    _context.FCMTokens.Add(token);
                    await _context.SaveChangesAsync();
                    return Ok();
                }else
                {
                    foreach (var t in Token)
                    {
                        t.Token = token.Token;
                    }
                }
                await _context.SaveChangesAsync();
                //var user = _context.User.Where(u => u.DeviceId == token.DeviceID).ToList();
                //if (user.Count != 0)
                //{
                //    token.UserID = user.Last().id;
                //}
                //Token.Token = token.Token;
                //Token.DeviceID = token.DeviceID;
                //Token.UserID = token.UserID;
                //_context.Entry(Token).State = EntityState.Modified;

                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Notifications>> DeleteNotifications(int id)
        {
            var notifications = await _context.Notifications.FindAsync(id);
            if (notifications == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notifications);
            await _context.SaveChangesAsync();

            return notifications;
        }

        private bool NotificationsExists(int id)
        {
            return _context.Notifications.Any(e => e.ID == id);
        }
    }
}
