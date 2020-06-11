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
    public class LiveTourZonesController : ControllerBase
    {
        private readonly SmartTourContext _context;
        private readonly IUserService _userService;

        public LiveTourZonesController(SmartTourContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/LiveTourZones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LiveTourZone>>> GetLiveTourZoneFiltered(int contentId, int liveTourId)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (contentId != default)
            {
                return await _context.LiveTourZone.Where(b => b.AgencyID == idAgency && b.ContentID == contentId).ToListAsync();
            }
            else if (liveTourId != default)
            {
                return await _context.LiveTourZone.Where(b => b.AgencyID == idAgency && b.LiveTourID == liveTourId).ToListAsync();
            }
            else
            {
                return await _context.LiveTourZone.Where(b => b.AgencyID == idAgency).ToListAsync();
            }
        }

        // GET: api/LiveTourZones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LiveTourZone>> GetLiveTourZone(int id)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            var liveTourZone = await _context.LiveTourZone.Where(b => b.AgencyID == idAgency && b.Id == id).FirstOrDefaultAsync();

            if (liveTourZone == null)
            {
                return NotFound();
            }

            return liveTourZone;
        }

        // PUT: api/LiveTourZones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLiveTourZone(int id, LiveTourZone liveTourZone)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (id != liveTourZone.Id)
            {
                return BadRequest();
            }
            if (liveTourZone.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }

            _context.Entry(liveTourZone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LiveTourZoneExists(id))
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

        // POST: api/LiveTourZones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LiveTourZone>> PostLiveTourZone(LiveTourZone liveTourZone)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            liveTourZone.AgencyID = idAgency;
            _context.LiveTourZone.Add(liveTourZone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLiveTourZone", new { id = liveTourZone.Id }, liveTourZone);
        }

        // DELETE: api/LiveTourZones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LiveTourZone>> DeleteLiveTourZone(int id)
        {
            var liveTourZone = await _context.LiveTourZone.FindAsync(id);
            if (liveTourZone == null)
            {
                return NotFound();
            }
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (liveTourZone.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }

            _context.LiveTourZone.Remove(liveTourZone);
            await _context.SaveChangesAsync();

            return liveTourZone;
        }

        private bool LiveTourZoneExists(int id)
        {
            return _context.LiveTourZone.Any(e => e.Id == id);
        }
    }
}
