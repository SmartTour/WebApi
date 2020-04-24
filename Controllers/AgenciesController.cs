using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smart_tour_api.Data;
using smart_tour_api.Entities;
using smart_tour_api.Servicies;

namespace smart_tour_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AgenciesController : ControllerBase
    {
        private readonly SmartTourContext _context;
        private IUserService _userService;

        public AgenciesController(SmartTourContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet("data")]
        public async Task<ActionResult<Agency>> GetAgency()
        {
            int userId = _userService.GetUserId(this.User);
            var user = await _context.Users.FindAsync(userId);
            var agency= await _context.Agencies.FindAsync(user.IDAgency);


            if (agency == null)
            {
                return NotFound();
            }

            return agency;
        }

        //API DI SUPPORTO
        // GET: api/Agencies
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Agency>>> GetAgencies()
        {
            return await _context.Agencies.ToListAsync();
        }

        // GET: api/Agencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agency>> GetAgency(int id)
        {
            var agency = await _context.Agencies.FindAsync(id);

            if (agency == null)
            {
                return NotFound();
            }

            return agency;
        }

        // PUT: api/Agencies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutAgency(int id, Agency agency)
        {
            if (id != agency.Id)
            {
                return BadRequest();
            }

            _context.Entry(agency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencyExists(id))
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

        // POST: api/Agencies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Agency>> PostAgency(Agency agency)
        {
            _context.Agencies.Add(agency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgency", new { id = agency.Id }, agency);
        }

        // DELETE: api/Agencies/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Agency>> DeleteAgency(int id)
        {
            var agency = await _context.Agencies.FindAsync(id);
            if (agency == null)
            {
                return NotFound();
            }

            _context.Agencies.Remove(agency);
            await _context.SaveChangesAsync();

            return agency;
        }

        private bool AgencyExists(int id)
        {
            return _context.Agencies.Any(e => e.Id == id);
        }
    }
}
