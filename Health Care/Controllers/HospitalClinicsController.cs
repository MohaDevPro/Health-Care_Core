﻿using System;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HospitalClinicsController : ControllerBase
    {
        private readonly Health_CareContext _context;

        public HospitalClinicsController(Health_CareContext context)
        {
            _context = context;
        }

        // GET: api/HospitalClinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetHospitalClinic()
        {
            return await (from hospital in _context.HospitalClinic
                          select new
                          {
                              id = hospital.hospitalId,
                              //Name = hospital.Name,
                              //Picture = hospital.Picture,
                              //Description=hospital.Description

                          }

                          ).ToListAsync();
        }

        //[HttpGet("{hospitalId}")]
        //public async Task<ActionResult<IEnumerable<HospitalClinicDoctorViewModel>>> GetClinicAndDoctorByHospitalID(int hospitalId)
        //{
            //var hospitalinfo_clinicinfo = await (from hospitalObj in _context.User
            //                                     where hospitalObj.id == hospitalId
            //                                     select new HospitalClinicDoctorViewModel
            //                                     {
            //                                         HospitalInfo = hospitalObj,
            //                                         HospitalClinicInfoList = (from hospitalClinicObj in _context.HospitalClinic
            //                                                               join clinicTypeObj in _context.ClinicType
            //                                                               on hospitalClinicObj.clinicId equals clinicTypeObj.id
            //                                                               where hospitalClinicObj.hospitalId == hospitalId
            //                                                               select new HospitalClinic
            //                                                               {
            //                                                                   hospitalId = hospitalClinicObj.hospitalId,
            //                                                                   //clinicId = hospitalClinicObj.clinicId,
            //                                                                   //appointmentPrice = hospitalClinicObj.appointmentPrice,
            //                                                                   //numberOfAvailableAppointment = hospitalClinicObj.numberOfAvailableAppointment,
            //                                                               }).ToList(),


            //                                     }).ToListAsync();

            //var hospitalinfo_clinicinfo = await(from hospitalClinicObj in _context.HospitalClinic
            //                                     join hospitalObj in _context.User on hospitalClinicObj.hospitalId equals hospitalObj.id
            //                                     join clinicTypeObj in _context.ClinicType on hospitalClinicObj.clinicId equals clinicTypeObj.id
            //                                     join clinicDoctorObj in _context.clinicDoctors on clinicTypeObj.id equals clinicDoctorObj.Clinicid
            //                                     where hospitalClinicObj.hospitalId == hospitalId
            //                                     select new HospitalClinicDoctorViewModel
            //                                     {
            //                                         HospitalInfo = hospitalObj,
            //                                         ClinicInfo = clinicTypeObj,
            //                                         ClinicDoctorInfo = clinicDoctorObj,
            //                                         HospitalClinicInfo = hospitalClinicObj
            //                                     }).ToListAsync();

            //var hospitalinfo_clinicinfo = await (from hospitalClinicObj in _context.HospitalClinic
            //                               join hospitalObj in _context.User on hospitalClinicObj.hospitalId equals hospitalObj.id
            //                               join clinicTypeObj in _context.ClinicType on hospitalClinicObj.clinicId equals clinicTypeObj.id
            //                               join clinicDoctorObj in _context.clinicDoctors on clinicTypeObj.id equals clinicDoctorObj.Clinicid
            //                               where hospitalClinicObj.hospitalId == hospitalId
            //                               select new HospitalClinicDoctorViewModel
            //                               {
            //                                   HospitalInfo = hospitalObj ,
            //                                   ClinicInfo = clinicTypeObj ,
            //                                   ClinicDoctorInfo = clinicDoctorObj ,
            //                                   HospitalClinicInfo = hospitalClinicObj
            //                               }).ToListAsync();


            //var externalClinic = await (from clinic in _context.ExternalClinic.Where(x => x.userId == id)
            //                            select new
            //                            {
            //                                id = clinic.id,
            //                                Name = clinic.Name,
            //                                Picture = clinic.Picture,
            //                                specialitylist = (from specialitydoctor in _context.SpeciallyDoctors
            //                                                  join specialit in _context.Speciality on specialitydoctor.Specialityid equals specialit.id
            //                                                  where specialitydoctor.Doctorid == clinic.id && specialit.isBasic == true && specialitydoctor.Roleid == 1
            //                                                  select specialit).ToList(),
            //                            }

            //              ).ToListAsync();

        //    return hospitalinfo_clinicinfo;
        //}

        // GET: api/HospitalClinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalClinic>> GetHospitalClinic(int id)
        {
            var hospitalClinic = await _context.HospitalClinic.FindAsync(id);

            if (hospitalClinic == null)
            {
                return NotFound();
            }

            return hospitalClinic;
        }

        // PUT: api/HospitalClinics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalClinic(int id, HospitalClinic hospitalClinic)
        {
            if (id != hospitalClinic.id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalClinicExists(id))
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

        // POST: api/HospitalClinics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HospitalClinic>> PostHospitalClinic(HospitalClinic hospitalClinic)
        {
            _context.HospitalClinic.Add(hospitalClinic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalClinic", new { id = hospitalClinic.id }, hospitalClinic);
        }

        // DELETE: api/HospitalClinics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalClinic>> DeleteHospitalClinic(int id)
        {
            var hospitalClinic = await _context.HospitalClinic.FindAsync(id);
            if (hospitalClinic == null)
            {
                return NotFound();
            }

            _context.HospitalClinic.Remove(hospitalClinic);
            await _context.SaveChangesAsync();

            return hospitalClinic;
        }

        private bool HospitalClinicExists(int id)
        {
            return _context.HospitalClinic.Any(e => e.id == id);
        }
    }
}
