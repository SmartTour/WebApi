using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class BaseToursController : ControllerBase
    {
        private readonly SmartTourContext _context;
        private IUserService _userService;
        public BaseToursController(SmartTourContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/BaseTours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaseTour>>> GetBaseTours()
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            return await _context.BaseTours.Where(b=> b.AgencyID==idAgency)
                .ToListAsync();
        }

        // GET: api/BaseTours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseTour>> GetBaseTour(int id)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            var baseTour = await _context.BaseTours.Where(b => b.AgencyID == idAgency && b.Id == id).FirstOrDefaultAsync();

            if (baseTour == null)
            {
                return NotFound();
            }

            return baseTour;
        }

        // PUT: api/BaseTours/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaseTour(int id, BaseTour baseTour)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            
            if (id != baseTour.Id)
            {
                return BadRequest();
            }
            //var oldBaseTour = await _context.BaseTours.Where(b => b.AgencyID == idAgency && b.Id == id).FirstOrDefaultAsync();
            if (baseTour.AgencyID != idAgency)
            {
                return BadRequest(new { message = "Is not your business" });
            }
            
            _context.Entry(baseTour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BaseTourExists(id))
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

        // POST: api/BaseTours
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BaseTour>> PostBaseTour(BaseTour baseTour)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            baseTour.AgencyID = idAgency;
            
            _context.BaseTours.Add(baseTour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBaseTour", new { id = baseTour.Id }, baseTour);
        }

        // DELETE: api/BaseTours/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseTour>> DeleteBaseTour(int id)
        {
            var baseTour = await _context.BaseTours.FindAsync(id);
            if (baseTour == null)
            {
                return NotFound();
            }
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (baseTour.AgencyID != idAgency)
            {
                return BadRequest(new { message = "Is not your business" });
            }

            _context.BaseTours.Remove(baseTour);
            await _context.SaveChangesAsync();

            return baseTour;
        }

        private bool BaseTourExists(int id)
        {
            return _context.BaseTours.Any(e => e.Id == id);
        }
    }
}
