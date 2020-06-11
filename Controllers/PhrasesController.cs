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
    public class PhrasesController : ControllerBase
    {
        private readonly SmartTourContext _context;
        private readonly IUserService _userService;

        public PhrasesController(SmartTourContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Phrases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phrase>>> GetPhrases()
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            return await _context.Phrases.Where(b => b.AgencyID == idAgency).ToListAsync();
        }

        // GET: api/Phrases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phrase>> GetPhrase(int id)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            var phrase = await _context.Phrases.Where(b => b.AgencyID == idAgency && b.Id == id).FirstOrDefaultAsync();

            if (phrase == null)
            {
                return NotFound();
            }

            return phrase;
        }

        // PUT: api/Phrases/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhrase(int id, Phrase phrase)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);

            if (id != phrase.Id)
            {
                return BadRequest();
            }
            if (phrase.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }
            _context.Entry(phrase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhraseExists(id))
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

        // POST: api/Phrases
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Phrase>> PostPhrase(Phrase phrase)
        {
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            phrase.AgencyID = idAgency;

            _context.Phrases.Add(phrase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhrase", new { id = phrase.Id }, phrase);
        }

        // DELETE: api/Phrases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Phrase>> DeletePhrase(int id)
        {
            var phrase = await _context.Phrases.FindAsync(id);
            if (phrase == null)
            {
                return NotFound();
            }
            int idAgency = await _userService.GetAuthorizedAgencyId(this.User);
            if (phrase.AgencyID != idAgency)
            {
                return BadRequest(new { message = "resource is not yours" });
            }
            _context.Phrases.Remove(phrase);
            await _context.SaveChangesAsync();

            return phrase;
        }

        private bool PhraseExists(int id)
        {
            return _context.Phrases.Any(e => e.Id == id);
        }
    }
}
