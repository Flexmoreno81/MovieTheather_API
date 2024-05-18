using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
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
    public class TheathersController : ControllerBase
    {
        private readonly MovieTheatherContext _context;

        public TheathersController(MovieTheatherContext context)
        {
            _context = context;
        }

        // GET: api/Theathers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TheaterDTO>>> GetTheathers()
        {
            var theather_list =  await _context.Theathers.ToListAsync();

            if (theather_list is null) {
                return BadRequest("There was an error getting theather information");
            }

            var DTO_LIST = theather_list.Adapt<IEnumerable<TheaterDTO>>();

            return Ok(DTO_LIST); 
        }

        // GET: api/Theathers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TheaterDTO>> GetTheather(int id)
        {
            var theather = await _context.Theathers.FindAsync(id);

            if (theather == null)
            {
                return NotFound();
            }

            return (theather.Adapt<TheaterDTO>());
        }

        // PUT: api/Theathers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
      
        public async Task<IActionResult> PutTheather(int id, Theather theather)
        {
            if (id != theather.TheatherId)
            {
                return BadRequest();
            }

            _context.Entry(theather).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TheatherExists(id))
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

       
        [HttpPost]

        public async Task<ActionResult<Theather>> PostTheather(Theather theather)
        {
            _context.Theathers.Add(theather);
            await _context.SaveChangesAsync();

            return Ok(theather); 
        }

      
        [HttpDelete("{id}")]
       
        public async Task<IActionResult> DeleteTheather(int id)
        {
            var theather = await _context.Theathers.FindAsync(id);
            if (theather == null)
            {
                return NotFound();
            }

            _context.Theathers.Remove(theather);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TheatherExists(int id)
        {
            return _context.Theathers.Any(e => e.TheatherId == id);
        }

        [HttpGet("/BY-ZIPCODE{zipcode}")]

        public async Task<ActionResult<List<TheaterDTO>>> getByZipCode(string zipcode) { 
        
            var theatherList = await _context.Theathers.Where(x=> x.Zipcode == zipcode).ToListAsync();

            if (theatherList is null) {
                return NotFound("Theather not found"); 
            }


            return Ok(theatherList); 

        } 
    }


    

}
