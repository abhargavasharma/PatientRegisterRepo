using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientContext _context;

        public PatientController(PatientContext context)
        {
            _context = context;
        }

        // GET: api/patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDetail>>> Get()
        {
            return await _context.PatientDetails?.ToListAsync();
        }

        // GET api/patient/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, PatientDetail patientDetail)
        {
            if (id != patientDetail.Id)
            {
                return BadRequest();
            }
            _context.Entry(patientDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST api/patient
        [HttpPost]
        public async Task<ActionResult<PatientDetail>> Post([FromBody] PatientDetail patientDetail)
        {
            _context.PatientDetails.Add(patientDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = patientDetail.Id }, patientDetail);
        }

        // PUT api/patient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PatientDetail patientDetail)
        {
            if (id != patientDetail.Id)
            {
                return BadRequest();
            }
            _context.Entry(patientDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // DELETE api/patient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientDetail>> Delete(int id)
        {
            var patientDetail = await _context.PatientDetails.FindAsync(id);
            if (patientDetail == null)
            {
                return NotFound();
            }

            _context.PatientDetails.Remove(patientDetail);
            await _context.SaveChangesAsync();

            return patientDetail;
        }

        private bool PatientExists(int id)
        {
            return _context.PatientDetails.Any(e => e.Id == id);
        }
    }
}
