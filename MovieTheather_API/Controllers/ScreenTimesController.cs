using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Theater_Model;
using Movie_Theater_Model.Models;
using MovieTheather_API.DTO;

namespace MovieTheather_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenTimesController : ControllerBase
    {
        private readonly MovieTheatherContext _context;

        public ScreenTimesController(MovieTheatherContext context)
        {
            _context = context;
        }

        // GET: api/ScreenTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenTimeDTO>>> GetScreenTimes()
        {
            var list =  await _context.ScreenTimes.ToListAsync();

            if (list is null) {
                return NotFound("There was an error: " + ModelState);
            }
            var DTO_LIST = list.Adapt<IEnumerable<ScreenTimeDTO>>(); 
            return Ok(DTO_LIST);
        }

        // GET: api/ScreenTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScreenTime>> GetScreenTime(int id)
        {
            var screenTime = await _context.ScreenTimes.FindAsync(id);

            if (screenTime == null)
            {
                return NotFound();
            }
            var DTO_SCREEN = screenTime.Adapt<ScreenTimeDTO>();

            return Ok(DTO_SCREEN);
        }

        // PUT: api/ScreenTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScreenTime(int id, ScreenTime screenTime)
        {
            if (id != screenTime.ScreeningId)
            {
                return BadRequest();
            }

            _context.Entry(screenTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreenTimeExists(id))
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

        // POST: api/ScreenTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScreenTime>> PostScreenTime(ScreenTime screenTime)
        {
            _context.ScreenTimes.Add(screenTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScreenTime", new { id = screenTime.ScreeningId }, screenTime);
        }

        // DELETE: api/ScreenTimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreenTime(int id)
        {
            var screenTime = await _context.ScreenTimes.FindAsync(id);
            if (screenTime == null)
            {
                return NotFound();
            }

            _context.ScreenTimes.Remove(screenTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScreenTimeExists(int id)
        {
            return _context.ScreenTimes.Any(e => e.ScreeningId == id);
        }
    }
}
