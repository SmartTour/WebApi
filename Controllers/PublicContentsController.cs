using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smart_tour_api.Data;
using smart_tour_api.Entities;

namespace smart_tour_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicContentsController : ControllerBase
    {
        private readonly SmartTourContext _context;

        public PublicContentsController(SmartTourContext context)
        {
            _context = context;
        }

        // GET: api/PublicContents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Content>>> GetContents(int agencyId)
        {
            return await _context.Contents.Where(b => b.AgencyID == agencyId).ToListAsync();
        }

        // GET: api/PublicContents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> GetContent(int id)
        {
            var content = await _context.Contents.FindAsync(id);

            if (content == null)
            {
                return NotFound();
            }

            return content;
        }

        
    }
}
