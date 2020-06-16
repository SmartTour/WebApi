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
    public class LiveToursController : ControllerBase
    {
        private readonly SmartTourContext _context;
        private IUserService _userService;

        public LiveToursController(SmartTourContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LiveTour>>> GetLiveToursPublic(int agencyId)
        {
            return await _context.LiveTours.Where(b => b.AgencyID == agencyId)
                .ToListAsync();
        }
        // GET: api/LiveTours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LiveTour>>> GetLiveTours()
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            return await _context.LiveTours.Where(b => b.AgencyID == idAgency)
                .ToListAsync();
        }

        // GET: api/LiveTours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LiveTour>> GetLiveTour(int id)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            var liveTour = await _context.LiveTours.Where(b => b.AgencyID == idAgency && b.Id == id).FirstOrDefaultAsync();

            if (liveTour == null)
            {
                return NotFound();
            }

            return liveTour;
        }

        // PUT: api/LiveTours/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLiveTour(int id, LiveTour liveTour)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            if (id != liveTour.Id)
            {
                return BadRequest();
            }

            if (liveTour.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }

            _context.Entry(liveTour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LiveTourExists(id))
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

        // POST: api/LiveTours
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LiveTour>> PostLiveTour(LiveTour liveTour)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            liveTour.AgencyID = idAgency;

            _context.LiveTours.Add(liveTour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLiveTour", new { id = liveTour.Id }, liveTour);
        }

        // DELETE: api/LiveTours/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LiveTour>> DeleteLiveTour(int id)
        {
            var liveTour = await _context.LiveTours.FindAsync(id);
            if (liveTour == null)
            {
                return NotFound();
            }
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (liveTour.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }
            _context.LiveTours.Remove(liveTour);
            await _context.SaveChangesAsync();

            return liveTour;
        }

        private bool LiveTourExists(int id)
        {
            return _context.LiveTours.Any(e => e.Id == id);
        }
    }
}
