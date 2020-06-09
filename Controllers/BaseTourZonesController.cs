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
    public class BaseTourZonesController : ControllerBase
    {
        private readonly SmartTourContext _context;
        private readonly IUserService _userService;

        public BaseTourZonesController(SmartTourContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        //GET: api/BaseTourZones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaseTourZone>>> GetBaseTourZoneFiltered(int contentId,int baseTourId)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (contentId != default)
            {
                return await _context.BaseTourZone.Where(b => b.AgencyID == idAgency && b.ContentID == contentId).ToListAsync();
            }
            else if(baseTourId != default)
            {
                return await _context.BaseTourZone.Where(b => b.AgencyID == idAgency && b.BaseTourID == baseTourId).ToListAsync();
            }
            else
            {
                return await _context.BaseTourZone.Where(b => b.AgencyID == idAgency).ToListAsync();
            }
        }

        // GET: api/BaseTourZones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseTourZone>> GetBaseTourZone(int id)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            var baseTourZone = await _context.BaseTourZone.Where(b => b.AgencyID == idAgency && b.Id == id).FirstOrDefaultAsync();

            if (baseTourZone == null)
            {
                return NotFound();
            }

            return baseTourZone;
        }

        // PUT: api/BaseTourZones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaseTourZone(int id, BaseTourZone baseTourZone)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            if (id != baseTourZone.Id)
            {
                return BadRequest();
            }

            if (baseTourZone.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }
            _context.Entry(baseTourZone).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!BaseTourZoneExists(id))
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

        // POST: api/BaseTourZones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BaseTourZone>> PostBaseTourZone(BaseTourZone baseTourZone)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            baseTourZone.AgencyID = idAgency;

            _context.BaseTourZone.Add(baseTourZone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBaseTourZone", new { id = baseTourZone.Id }, baseTourZone);
        }

        // DELETE: api/BaseTourZones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseTourZone>> DeleteBaseTourZone(int id)
        {
            var baseTourZone = await _context.BaseTourZone.FindAsync(id);
            if (baseTourZone == null)
            {
                return NotFound();
            }
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (baseTourZone.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }

            _context.BaseTourZone.Remove(baseTourZone);
            await _context.SaveChangesAsync();

            return baseTourZone;
        }

        private bool BaseTourZoneExists(int id)
        {
            return _context.BaseTourZone.Any(e => e.Id == id);
        }
    }
}
