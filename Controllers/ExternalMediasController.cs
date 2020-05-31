using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ExternalMediasController : ControllerBase
    {
        private readonly SmartTourContext _context;
        private readonly IUserService _userService;
        public ExternalMediasController(SmartTourContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/ExternalMedias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExternalMedia>>> GetExternalMedias()
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            return await _context.ExternalMedias.Where(b => b.AgencyID == idAgency)
                .ToListAsync();
        }

        // GET: api/ExternalMedias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExternalMedia>> GetExternalMedia(int id)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            var externalMedia = await _context.ExternalMedias.Where(b => b.AgencyID == idAgency && b.Id == id).FirstOrDefaultAsync();

            if (externalMedia == null)
            {
                return NotFound();
            }

            return externalMedia;
        }

        // PUT: api/ExternalMedias/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExternalMedia(int id, ExternalMedia externalMedia)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            if (id != externalMedia.Id)
            {
                return BadRequest();
            }
            if (externalMedia.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }

            _context.Entry(externalMedia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExternalMediaExists(id))
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

        // POST: api/ExternalMedias
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ExternalMedia>> PostExternalMedia(ExternalMedia externalMedia)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            externalMedia.AgencyID = idAgency;

            _context.ExternalMedias.Add(externalMedia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExternalMedia", new { id = externalMedia.Id }, externalMedia);
        }

        // DELETE: api/ExternalMedias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExternalMedia>> DeleteExternalMedia(int id)
        {
            var externalMedia = await _context.ExternalMedias.FindAsync(id);
            if (externalMedia == null)
            {
                return NotFound();
            }

            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (externalMedia.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }

            _context.ExternalMedias.Remove(externalMedia);
            await _context.SaveChangesAsync();

            return externalMedia;
        }

        private bool ExternalMediaExists(int id)
        {
            return _context.ExternalMedias.Any(e => e.Id == id);
        }
    }
}
