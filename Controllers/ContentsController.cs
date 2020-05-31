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
    public class ContentsController : ControllerBase
    {
        private readonly SmartTourContext _context;
        private readonly IUserService _userService;

        public ContentsController(SmartTourContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Contents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Content>>> GetContents()
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            return await _context.Contents.Where(b => b.AgencyID == idAgency)
                .ToListAsync();
        }

        // GET: api/Contents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> GetContent(int id)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            var content = await _context.Contents.Where(b => b.AgencyID == idAgency && b.Id == id).FirstOrDefaultAsync();

            if (content == null)
            {
                return NotFound();
            }

            return content;
        }

        // PUT: api/Contents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContent(int id, Content content)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            if (id != content.Id)
            {
                return BadRequest();
            }
            if (content.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }

            _context.Entry(content).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(id))
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

        // POST: api/Contents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Content>> PostContent(Content content)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            content.AgencyID = idAgency;

            _context.Contents.Add(content);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContent", new { id = content.Id }, content);
        }

        // DELETE: api/Contents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Content>> DeleteContent(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (content.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }
            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();

            return content;
        }

        private bool ContentExists(int id)
        {
            return _context.Contents.Any(e => e.Id == id);
        }
    }
}
