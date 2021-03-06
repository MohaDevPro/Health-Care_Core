using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class UserContractsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public UserContractsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/UserContracts
        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<IEnumerable<UserContract>>> GetUserContract()
        {
            return await _context.UserContract.ToListAsync();
        }

        // GET: api/UserContracts/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<UserContract>> GetUserContract(int id)
        {
            var userContract = await _context.UserContract.FindAsync(id);

            if (userContract == null)
            {
                return NotFound();
            }

            return userContract;
        }
        [HttpGet("{roleID}/{statusId}/{pageKey}/{pageSize}")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<IEnumerable<object>>> GetAllUserContracts(int roleID ,int statusId,int pageKey, int pageSize)
        {
            var contracts = await (from user in _context.User
                                   join userContract in _context.UserContract on user.id equals userContract.userId
                                   where user.active == true
                                   select new
                                   {
                                       ContractId = userContract.id,
                                       userId = user.id,
                                       name = user.nameAR,
                                       phonenumber = user.phoneNumber,
                                       startDate = userContract.contractStartDate,
                                       endDate = userContract.contractEndDate,
                                   }).ToListAsync();
            if (roleID != 0)
            {
                contracts = await (from user in _context.User
                                   join userContract in _context.UserContract on user.id equals userContract.userId
                                   where user.active == true && user.Roleid == roleID
                                   select new
                                   {
                                       ContractId = userContract.id,
                                       userId = user.id,
                                       name = user.nameAR,
                                       phonenumber = user.phoneNumber,
                                       startDate = userContract.contractStartDate,
                                       endDate = userContract.contractEndDate,
                                   }).ToListAsync();
            }
            List<object> result = new List<object>();
            foreach(var item in contracts)
            {
                var date = DateTime.Parse(item.endDate);
                if ((date - DateTime.Now).Days <= 14 && (date - DateTime.Now).Days >0)
                {
                    if(statusId==1 || statusId==3)
                    result.Add(new
                    {
                        item.ContractId,
                        item.userId,
                        item.name,
                        item.phonenumber,
                        item.startDate,
                        item.endDate,
                        status="WillEnd",
                    });
                }
                else if((date - DateTime.Now).Days > 14)
                {
                    if (statusId == 1 || statusId == 4)
                        result.Add(new
                    {
                        item.ContractId,
                        item.userId,
                        item.name,
                        item.phonenumber,
                        item.startDate,
                        item.endDate,
                        status = "Ok",
                    });
                }
                else
                {
                    if (statusId == 1 || statusId == 2)
                        result.Add(new
                    {
                        item.ContractId,
                        item.userId,
                        item.name,
                        item.phonenumber,
                        item.startDate,
                        item.endDate,
                        status = "End",
                    });
                }
            }
            if (pageSize != 0)
                return result.Skip(pageKey).Take(pageSize).ToList();
            else
                return result;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,عامل صحي,عيادة,مستشفى,دكتور")]

        public async Task<ActionResult<bool>> GetIsUserAcceptContract(int id)
        {
            var userContract = await _context.UserContract.Where(u=>u.userId == id).FirstOrDefaultAsync();
            if (userContract == null)
            {
                return false;
            }
            var splitEndDate = userContract.contractEndDate.Split('/');
            var enddate = new DateTime(Convert.ToInt32(splitEndDate[0]), Convert.ToInt32(splitEndDate[1]), Convert.ToInt32(splitEndDate[2]));
            var n = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")).AddDays(14);
            if (enddate == n)
            {
                NotificationsController notificationsController = new NotificationsController(_context);
                await notificationsController.SendNotificationsToUser(id,new Notifications() {
                title = "فترة العقد قاربت على الإنتهاء",
                body = "فترة العقد قاربت على الإنتهاء يرجى الموافقة على الشروط العقد الجديد التي ستظهر لك بعد إسبوعين",
                isRepeated = false,
                
                });
            }
            if ( enddate< Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")))
            {
                return false;
            }
            return true;
        }
        // PUT: api/UserContracts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> PutUserContract(int id, UserContract userContract)
        {
            if (id != userContract.id)
            {
                return BadRequest();
            }

            _context.Entry(userContract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserContractExists(id))
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

        // POST: api/UserContracts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "admin,عامل صحي,عيادة,مستشفى,دكتور")]

        public async Task<ActionResult<UserContract>> PostUserContract(UserContract userContract)
        {
            _context.UserContract.Add(userContract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserContract", new { id = userContract.id }, userContract);
        }

        // DELETE: api/UserContracts/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<UserContract>> DeleteUserContract(int id)
        {
            var userContract = await _context.UserContract.FindAsync(id);
            if (userContract == null)
            {
                return NotFound();
            }

            _context.UserContract.Remove(userContract);
            await _context.SaveChangesAsync();

            return userContract;
        }

        private bool UserContractExists(int id)
        {
            return _context.UserContract.Any(e => e.id == id);
        }
    }
}
