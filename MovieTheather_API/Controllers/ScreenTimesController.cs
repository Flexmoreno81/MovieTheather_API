
using Mapster;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenTimeDTO>>> GetScreenTimes()
        {
            var screentimesWithDetails = await _context.ScreenTimes
                .Include(st => st.Movie)
                .Include(st => st.Theather)
                .ToListAsync();

            if (screentimesWithDetails == null || screentimesWithDetails.Count == 0)
            {
                return NotFound("No screentimes found.");
            }



            var DTO_LIST = screentimesWithDetails.Adapt<IEnumerable<ScreenTimeDTO>>();
            return Ok(DTO_LIST);
        }


        // GET: api/ScreenTimes/5

        // this will be used as a  filter feature to filter movies based on screentime movies //  
        [HttpGet("screentime/{id}")]
        public async Task<ActionResult<ScreenTimeDTO>> GetScreenTime(int id)
        {
            var screenTime = await _context.ScreenTimes.FindAsync(id);

            if (screenTime == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(screenTime.MovieId);

            var theather = await _context.Theathers.FindAsync(screenTime.TheatherId);

            if (movie == null && theather == null) {
                return BadRequest("Unable to locate the movies or theathers; thearfore I cannot provide screenTime");
            }

            screenTime.Movie = movie;
            screenTime.Theather = theather;


            var DTO_SCREEN = screenTime.Adapt<ScreenTimeDTO>();

            return Ok(DTO_SCREEN);
        }



        [HttpGet("movietime/{time}")]
        public async Task<ActionResult<List<MovieDTO>>> MoviescreenTime(string time)
        {
            var screentimesWithDetails = await _context.ScreenTimes
               .Include(st => st.Movie)
               .Include(st => st.Theather)
               .ToListAsync();

           

            var list_movies = screentimesWithDetails.Where(x => x.ScreenTime1 == time).Select(x => x.Movie).ToList();

            if (list_movies is null) {
                return NotFound("Unable to find movies based on the time");
            }

            var List_DTO = list_movies.Adapt<List<MovieDTO>>();

            return Ok(List_DTO);


        }




        // PUT: api/ScreenTimes/5

        [HttpPut("putScreen/{id}")]
      
        public async Task<IActionResult> PutScreenTime(int id, ScreenTime screenTimeDTO)
        {
            if (id != screenTimeDTO.ScreeningId)
            {
                return BadRequest();
            }

            // Retrieve the ScreenTime entity from the database
            var screenTime = await _context.ScreenTimes.FindAsync(id);
            if (screenTime == null)
            {
                return NotFound();
            }

           
            screenTime.ScreeningId = screenTimeDTO.ScreeningId;
            screenTime.MovieId = screenTimeDTO.MovieId;
            screenTime.TheatherId = screenTimeDTO.TheatherId;
            screenTime.ScreenTime1 = screenTimeDTO.ScreenTime1;
            screenTime.Movie = screenTimeDTO.Movie;
            screenTime.Theather = screenTimeDTO.Theather;

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

        [HttpPost]
      

        public async Task<ActionResult<ScreenTime>> PostScreenTime(ScreenTime screenTime)
        {
            _context.ScreenTimes.Add(screenTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScreenTime", new { id = screenTime.ScreeningId }, screenTime);
        }

        // DELETE: api/ScreenTimes/5
        [HttpDelete("{id}")]
        [Authorize]
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


        [HttpGet("ByMovies/{movie_id}")]

        public async Task<ActionResult<List<ScreenTimeDTO>>> moviesInfo(int movie_id) {
            Movie? movie = await _context.Movies.FindAsync(movie_id);

            if (movie == null) {
                return BadRequest("Unable to locate the movies");
            }


            var movie_info_list = await _context.ScreenTimes
                .Include(st => st.Movie) // Eagerly load the Movie navigation property
                .Include(st => st.Theather) // Eagerly load the Theather navigation property
                 .Where(x => x.Movie == movie)
                    .ToListAsync();


            if (movie_info_list is null) {
                return NotFound("Unable to display");
            }

            return Ok(movie_info_list);
        }


        [HttpGet("ByTheather{id}")]


        public async Task<ActionResult<List<MovieDTO>>> MoviesByTheater(int id)
        {
            List<Movie> movieList = await _context.ScreenTimes
                .Include(st => st.Movie)
                .Where(x => x.TheatherId == id)
                .Select(st => st.Movie) 
                .ToListAsync();

            if (movieList == null || movieList.Count == 0)
            {
                return NotFound("Unable to receive the data");
            }

            var movieDTOList = movieList.Adapt<List<MovieDTO>>();

            return Ok(movieDTOList);
        }




    }
}
