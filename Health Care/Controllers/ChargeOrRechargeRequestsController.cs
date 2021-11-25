using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Health_Care.Data;
using Health_Care.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;


namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = "admin")]
    public class ChargeOrRechargeRequestsController : ControllerBase
    {
        private readonly Health_CareContext _context;
        private readonly IWebHostEnvironment _environment;

        public ChargeOrRechargeRequestsController(Health_CareContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        // GET: api/ChargeOrRechargeRequests
        [HttpGet("{pageKey}/{pageSize}")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<IEnumerable<object>>> GetChargeOrRechargeRequest(int pageKey, int pageSize)
        {
            var recharges=await(from recharge in _context.ChargeOrRechargeRequest.OrderByDescending(x=>x.id)
                                join user in _context.User on recharge.userId equals user.id
                                select new
                                {
                                    recharge.id,
                                    recharge.IsCanceled,
                                    recharge.IsRestore,
                                    recharge.IsRestored,
                                    recharge.NumberOfReceipt,
                                    recharge.rechargeDate,
                                    recharge.ResounOfCancel,
                                    recharge.BalanceReceipt,
                                    recharge.BalanceReceiptImage,
                                    recharge.ConfirmToAddBalance,
                                    recharge.userId,
                                    user.nameAR,
                                    user.phoneNumber,


                                }
                                
                                ).ToListAsync();
            if (pageSize != 0)
            {
                return recharges.Skip(pageKey).Take(pageSize).ToList();
            }
            else
            {
                return recharges;
            }
        }
        [HttpGet("{id}/{pageKey}/{pageSize}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ChargeOrRechargeRequest>>> GetChargeOrRechargeRequestByUserID(int id, int pageKey, int pageSize)
        {
            var recharge= await _context.ChargeOrRechargeRequest.Where(x=>x.userId==id).OrderByDescending(x=>x.id).ToListAsync();
            if (pageSize != 0)
            {
                return recharge.Skip(pageKey).Take(pageSize).ToList();
            }
            else
            {
                return recharge;
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public List<string> GetChargeOrRechargeRequestByUserI(int id)
        {
            var x = _context.ChargeOrRechargeRequest.Where(x => x.userId == id).ToList();
            var dateList = x[0].rechargeDate.Split("/");
            return dateList.ToList();
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "مريض,admin")]
        public bool IsRestorable(int id)
        {
            var p = _context.Patient.Where(x=>x.userId == id).FirstOrDefault();
            var charges = _context.ChargeOrRechargeRequest.Where(x => x.userId == id).ToList();
            if (charges.Count == 0)
            {
                return false;
            }
            var balance = 0;
            //var First = _context.ChargeOrRechargeRequest.Where(x=>x.IsRestore == true && x.userId == id).ToList();
            foreach (var item in charges)
            {
                if(!item.IsCanceled && item.ConfirmToAddBalance)
                    balance += item.BalanceReceipt;
            }
            var dateList = charges[0].rechargeDate.Split("/");
            var date =new DateTime(Convert.ToInt32(dateList[2]), Convert.ToInt32(dateList[1]), Convert.ToInt32(dateList[0])).AddDays(5) ;
            return date >= DateTime.Now && p.Balance == balance && balance != 0;
        }

        // GET: api/ChargeOrRechargeRequests/5
        [HttpGet("{id}")]
        [Authorize(Roles = "مريض,admin")]
        public async Task<ActionResult<ChargeOrRechargeRequest>> GetChargeOrRechargeRequest(int id)
        {
            var chargeOrRechargeRequest = await _context.ChargeOrRechargeRequest.FindAsync(id);
            if (chargeOrRechargeRequest == null)
            {
                return NotFound();
            }

            return chargeOrRechargeRequest;
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> GetIsCharged(int id)
        {
            var chargeOrRechargeRequest = await _context.ChargeOrRechargeRequest.Where(c=>c.userId == id).FirstOrDefaultAsync();

            if (chargeOrRechargeRequest == null)
            {
                return false;
            }
            else if (chargeOrRechargeRequest != null)
            {
                return chargeOrRechargeRequest.ConfirmToAddBalance;
            }
            return true;
        }

        // PUT: api/ChargeOrRechargeRequests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "مريض,admin")]
        public async Task<IActionResult> PutChargeOrRechargeRequest(int id, ChargeOrRechargeRequest chargeOrRechargeRequest)
        {
            if (id != chargeOrRechargeRequest.id)
            {
                return BadRequest();
            }

            _context.Entry(chargeOrRechargeRequest).State = EntityState.Modified;

            try
            {
                var  user = _context.User.Where(u=>u.id == chargeOrRechargeRequest.userId).FirstOrDefault();
                if (!chargeOrRechargeRequest.IsCanceled)
                {
                    var patient = _context.Patient.Where(x => x.userId == user.id).FirstOrDefault();
                    patient.Balance += chargeOrRechargeRequest.BalanceReceipt;
                    patient.LastBalanceChargeDate = DateTime.Now.ToString("dd/MM/yyyy");
                }
                user.isActiveAccount = true;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChargeOrRechargeRequestExists(id))
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

        [HttpPost("{id}")]
        [Authorize(Roles = "مريض,admin")]
        public async Task<IActionResult> RestoreChargeOrRechargeRequest(int id)
        {
            //var c = _context.ChargeOrRechargeRequest.Where(x=>x.userId == id).ToList();
            //if (c.Count>1)
            //{
            //    return BadRequest();
            //}
            

            try
            {
               var charge = _context.ChargeOrRechargeRequest.Where(x => x.userId == id).FirstOrDefault();
                charge.IsRestore = true;
                var p = _context.Patient.Where(x=> x.userId == id).FirstOrDefault();
                p.Balance -= charge.BalanceReceipt;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChargeOrRechargeRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/ChargeOrRechargeRequests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "مريض,admin")]
        public async Task<ActionResult<Doctor>> PostChargeOrRechargeRequest( [FromForm] ChargeOrRechargeRequest rechargeRequest, IFormFile Picture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var charges = _context.ChargeOrRechargeRequest.Where(x => x.userId == rechargeRequest.userId).ToList();
                    if (charges.Count==0)
                    {
                        rechargeRequest.IsRestore = true;
                    }
                    _context.ChargeOrRechargeRequest.Add(rechargeRequest);
                    await _context.SaveChangesAsync();

                    string path = _environment.WebRootPath + @"\images\";
                    FileStream fileStream;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (Picture != null)
                    {
                        fileStream = System.IO.File.Create(path + "rechargeRequest_logo_" + rechargeRequest.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        rechargeRequest.BalanceReceiptImage = @"\images\" + "rechargeRequest_logo_" + rechargeRequest.id + "." + Picture.ContentType.Split('/')[1];
                    }
                    if (Picture != null )
                    {
                        _context.Entry(rechargeRequest).State = EntityState.Modified;

                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {


                    throw;
                }



            }

            return CreatedAtAction("GetDoctor", new { id = rechargeRequest.id }, rechargeRequest);
        }


        // DELETE: api/ChargeOrRechargeRequests/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ChargeOrRechargeRequest>> DeleteChargeOrRechargeRequest(int id)
        {
            var chargeOrRechargeRequest = await _context.ChargeOrRechargeRequest.FindAsync(id);
            if (chargeOrRechargeRequest == null)
            {
                return NotFound();
            }

            _context.ChargeOrRechargeRequest.Remove(chargeOrRechargeRequest);
            await _context.SaveChangesAsync();

            return chargeOrRechargeRequest;
        }

        private bool ChargeOrRechargeRequestExists(int id)
        {
            return _context.ChargeOrRechargeRequest.Any(e => e.id == id);
        }
    }
}
