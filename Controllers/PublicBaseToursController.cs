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
    public class PublicBaseToursController : ControllerBase
    {
        private readonly SmartTourContext _context;

        public PublicBaseToursController(SmartTourContext context)
        {
            _context = context;
        }

        // GET: api/PublicBaseTours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaseTour>>> GetBaseTours()
        {
            return await _context.BaseTours.ToListAsync();
        }

        // GET: api/PublicBaseTours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseTour>> GetBaseTour(int id)
        {
            var baseTour = await _context.BaseTours.FindAsync(id);

            if (baseTour == null)
            {
                return NotFound();
            }

            _context.Entry(baseTour).Collection(b => b.BaseTourZones).Load();
            return baseTour;
        }
    }
}
