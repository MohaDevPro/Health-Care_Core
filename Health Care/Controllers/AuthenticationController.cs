using Health_Care.Data;
using Health_Care.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mr.Delivery.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Health_Care.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Health_CareContext _context;
        private readonly JWTSettings _jwtsettings;
        private readonly IWebHostEnvironment _environment;
            
            public AuthenticationController(Health_CareContext context, IOptions<JWTSettings> jwtsettings, IWebHostEnvironment environment)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;_environment = environment;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(User user)
        {
            user = _context.User.Include(u => u.RefreshTokens).Where(x => x.phoneNumber == user.phoneNumber && x.Password == user.Password && x.active == true).FirstOrDefault();
            if (user != null)
            {
                RefreshToken refreshToken = user.RefreshTokens;
                if (refreshToken.ExpiryDate < DateTime.UtcNow)
                {
                    refreshToken = GenerateRefreshToken();
                    user.RefreshTokens = refreshToken;
                    await _context.SaveChangesAsync();
                }
                RefreshRequest refreshRequest = new RefreshRequest();
                refreshRequest.RefreshToken = refreshToken.Token;
                refreshRequest.AccessToken = GenerateAccessToken(user);
                string type = _context.Role.Where(r => r.id == user.Roleid).FirstOrDefault().RoleName;

                int SpecificId;
                if (user.Roleid == 1)
                {
                    var healthcareWorker = _context.HealthcareWorker.Where(x => x.userId == user.id).FirstOrDefault();
                    SpecificId = healthcareWorker.id;
                }else if (user.Roleid == 2)
                {
                    var clinic = _context.ExternalClinic.Where(x => x.userId == user.id).FirstOrDefault();
                        SpecificId = clinic.id;
                }else if (user.Roleid == 3)
                {
                    var hospital = _context.Hospitals.Where(x => x.UserId == user.id).FirstOrDefault();
                        SpecificId = hospital.id;
                }else if (user.Roleid == 4)
                {
                    var patient = _context.Patient.Where(x => x.userId == user.id).FirstOrDefault();
                    SpecificId = patient.id;
                }
                else
                {
                    var doctor = _context.Doctor.Where(x => x.Userid == user.id).FirstOrDefault();
                    SpecificId = doctor.id;
                }



                return Ok(new UserWithToken()
                {
                    id = user.id,
                    nameAR = user.nameAR,
                    nameEN = user.nameEN,
                    phoneNumber = user.phoneNumber,
                    address=user.address,
                    DeviceId=user.DeviceId,
                    email=user.email,
                    roleid = user.Roleid,
                    SpecificId = SpecificId,
                    type = type,
                    isActiveAccount = user.isActiveAccount,
                    regionId =user.regionId,
                    AccessToken = refreshRequest.AccessToken,
                    RefreshToken = refreshRequest.RefreshToken
                }) ;
            }

            return NotFound();
        }       
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Logout(User user)
        {
            var fcm_token = _context.FCMTokens.Where(t => t.UserID == user.id).FirstOrDefault();
            _context.FCMTokens.Remove(fcm_token);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // POST: api/User
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {

                if (_context.User.Any(u => u.phoneNumber == user.phoneNumber))
                    return Conflict();

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                FCM_Tokens fCM_Token = _context.FCMTokens.Where(t => t.DeviceID == user.DeviceId).FirstOrDefault();
                if (fCM_Token != null)
                {
                    fCM_Token.UserID = user.id;
                }
                
                user =  _context.User.Where(u => u.phoneNumber == user.phoneNumber).FirstOrDefault();

                //if (user != null)
                //{
                RefreshToken refreshToken = GenerateRefreshToken();
                user.RefreshTokens = refreshToken;
                await _context.SaveChangesAsync();

                //}


                //sign your token here here..

                RefreshRequest refreshRequest = new RefreshRequest();
                refreshRequest.AccessToken = GenerateAccessToken(user);
                refreshRequest.RefreshToken = refreshToken.Token;
                string type = _context.Role.Where(r => r.id == user.Roleid).FirstOrDefault().RoleName;
                int SpecificId;
                //var hospital = _context.Hospitals.Where(x => x.UserId == user.id).FirstOrDefault();
                //var healthcareWorker = _context.HealthcareWorker.Where(x => x.userId == user.id).FirstOrDefault();
                //var patient = _context.Patient.Where(x => x.userId == user.id).FirstOrDefault();
                //var clinic = _context.ExternalClinic.Where(x => x.userId == user.id).FirstOrDefault();
                //var doctor = _context.Doctor.Where(x => x.Userid == user.id).FirstOrDefault();

                 if (user.Roleid == 1)
                {
                    var healthcareWorker = new HealthcareWorker()
                    {
                        userId = user.id,
                        active = true,
                        Name = user.nameAR,
                    };
                    _context.HealthcareWorker.Add(healthcareWorker) ;
                    _context.SaveChanges();
                    SpecificId = healthcareWorker.id;
                }
                    
                else if (user.Roleid == 2)
                {
                    var clinic = new ExternalClinic()
                    {
                        userId = user.id,
                        active = true,
                        Name = user.nameAR,
                    };
                    _context.ExternalClinic.Add(clinic);
                    _context.SaveChanges();
                    SpecificId = clinic.id;
                }
                else if (user.Roleid == 3)
                {
                    var hospital = new Hospital()
                    {
                        UserId = user.id,
                        active = true,
                        Name = user.nameAR,
                    };
                    _context.Hospitals.Add(hospital);
                    _context.SaveChanges();
                    SpecificId = hospital.id;
                }
                else if (user.Roleid == 4)
                {
                    var patient = new Patient()
                    {
                        userId = user.id,
                        active = true,
                        Name = user.nameAR,
                    };
                    _context.Patient.Add(patient);
                    _context.SaveChanges();
                    SpecificId = patient.id;
                }
                else if (user.Roleid == 5)
                {
                    var doctor = new Doctor()
                    {
                        Userid = user.id,
                        active = true,
                        name = user.nameAR,
                    };
                    _context.Doctor.Add(doctor);
                    _context.SaveChanges();
                    SpecificId = doctor.id;
                }
                else SpecificId = 0;


                return Ok(new UserWithToken()
                {
                    id = user.id,
                    nameAR = user.nameAR,
                    nameEN = user.nameEN,
                    phoneNumber = user.phoneNumber,
                    DeviceId = user.DeviceId,
                    email = user.email,
                    type = type,
                    roleid = user.Roleid,
                    SpecificId = SpecificId, 
                    isActiveAccount=user.isActiveAccount,
                    AccessToken = refreshRequest.AccessToken,
                    RefreshToken = refreshRequest.RefreshToken
                });
            }
            return BadRequest();

        }
        [HttpPost("{id}")]
        //[Authorize(Roles = "admin, doctor")]
        public async Task<ActionResult<Doctor>> signUpDoctorDetails(int id, [FromForm] Doctor doctor , IFormFile Picture, IFormFile bg, IFormFile idImage, IFormFile certificateImage)
        {
            
            User user = _context.User.Where(d => d.id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            Doctor doctor2 = _context.Doctor.Where(d=>d.Userid == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (idImage != null && certificateImage != null && Picture != null && bg != null)
                {
                    try
                    {
                        if(doctor != null)
                        {
                            doctor2.appointmentPrice = doctor.appointmentPrice;
                            doctor2.numberOfAvailableAppointment = doctor.numberOfAvailableAppointment;
                            doctor2.active = doctor.active;
                        }
                        doctor2.name = user.nameAR;
                        doctor2.Userid = id;
                        string path = _environment.WebRootPath + @"\images\";
                        FileStream fileStream;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fileStream = System.IO.File.Create(path + "logo_doctor_" + doctor2.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        doctor2.Picture = @"\images\" + "logo_doctor_" + doctor2.id + "." + Picture.ContentType.Split('/')[1];

                        fileStream = System.IO.File.Create(path + "bg_doctor_" + doctor2.id + "." + bg.ContentType.Split('/')[1]);
                        bg.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        doctor2.backgroundImage = @"\images\" + "bg_doctor_" + doctor2.id + "." + bg.ContentType.Split('/')[1];

                        fileStream = System.IO.File.Create(path + "idImage_doctor_" + doctor2.id + "." + idImage.ContentType.Split('/')[1]);
                        idImage.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        doctor2.identificationImage = @"\images\" + "idImage_doctor_" + doctor2.id + "." + idImage.ContentType.Split('/')[1];

                        fileStream = System.IO.File.Create(path + "certificateImage_doctor_" + doctor2.id + "." + certificateImage.ContentType.Split('/')[1]);
                        certificateImage.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        fileStream.Dispose();
                        doctor2.graduationCertificateImage = @"\images\" + "certificateImage_doctor_" + doctor2.id + "." + certificateImage.ContentType.Split('/')[1];

                        //_context.Doctor.Add(doctor);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {


                        throw;
                    }

                }
                else
                {
                    return BadRequest();
                }

            }

            return CreatedAtAction("Getdoctor", new { id = doctor2.id }, doctor2);
        }
        [HttpPost("{id}")]
        //[Authorize(Roles = "admin, doctor")]
        public async Task<ActionResult<Doctor>> signUpHealthWorkerDetails( int id,[FromForm] IFormFile Picture, IFormFile bg, IFormFile idImage, IFormFile certificateImage)
        {
            HealthcareWorker healthcareWorker = _context.HealthcareWorker.Where(d => d.userId == id).FirstOrDefault();
            if (healthcareWorker == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (idImage != null && certificateImage != null && Picture != null && bg != null)
                {
                    try
                    {

                        string path = _environment.WebRootPath + @"\images\";
                        FileStream fileStream;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fileStream = System.IO.File.Create(path + "logo_healthcareWorker" + healthcareWorker.id + "." + Picture.ContentType.Split('/')[1]);
                        Picture.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        healthcareWorker.Picture = @"\images\" + "logo_healthcareWorker" + healthcareWorker.id + "." + Picture.ContentType.Split('/')[1];

                        fileStream = System.IO.File.Create(path + "bg_healthcareWorker" + healthcareWorker.id + "." + bg.ContentType.Split('/')[1]);
                        bg.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        healthcareWorker.BackGroundPicture = @"\images\" + "bg_healthcareWorker" + healthcareWorker.id + "." + bg.ContentType.Split('/')[1];

                        fileStream = System.IO.File.Create(path + "idImage_healthcareWorker" + healthcareWorker.id + "." + idImage.ContentType.Split('/')[1]);
                        idImage.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        healthcareWorker.identificationImage = @"\images\" + "idImage_healthcareWorker" + healthcareWorker.id + "." + idImage.ContentType.Split('/')[1];

                        fileStream = System.IO.File.Create(path + "certificateImage_healthcareWorker" + healthcareWorker.id + "." + certificateImage.ContentType.Split('/')[1]);
                        certificateImage.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        fileStream.Dispose();
                        healthcareWorker.graduationCertificateImage = @"\images\" + "certificateImage_healthcareWorker" + healthcareWorker.id + "." + certificateImage.ContentType.Split('/')[1];
                        
                        _context.Entry(healthcareWorker).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {


                        throw;
                    }

                }
                else
                {
                    return BadRequest();
                }

            }

            return CreatedAtAction("Getdoctor", new { id = healthcareWorker.id }, healthcareWorker);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            User user = await GetUserFromAccessToken(refreshRequest.AccessToken);

            if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                RefreshRequest refreshRequest2 = new RefreshRequest();
                refreshRequest2.AccessToken = GenerateAccessToken(user);
                refreshRequest2.RefreshToken = refreshRequest.RefreshToken;
                return Ok(refreshRequest2);
            }

            return NotFound();
        }

        // GET: api/User
        [HttpPost]
        public async Task<ActionResult<User>> GetUserByAccessToken([FromBody] string accessToken)
        {
            User user = await GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        private bool ValidateRefreshToken(User user, string refreshToken)
        {

            RefreshToken refreshTokenUser = _context.RefreshTokens.Where(rt => rt.Token == refreshToken).FirstOrDefault();

            if (refreshTokenUser != null && refreshTokenUser.UserId == user.id
                && refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

        private async Task<User> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                    ,
                    ValidateLifetime = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userPhone = principle.FindFirst(ClaimTypes.MobilePhone)?.Value;

                    return await _context.User.Where(u => u.phoneNumber == userPhone).FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {

                return new User();
            }

            return new User();
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);

            return refreshToken;
        }

        private string GenerateAccessToken(User user)
        {
            string role = _context.Role.Where(r => r.id == user.Roleid).Select(r => r.RoleName).FirstOrDefault();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.MobilePhone,user.phoneNumber),
                    new Claim(ClaimTypes.Role, role),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenWithPrifix = "Bearer " + tokenHandler.WriteToken(token);
            return tokenWithPrifix;
        }

        
    }
}
