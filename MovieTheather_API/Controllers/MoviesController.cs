
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
    public class MoviesController : ControllerBase
    {
        private readonly MovieTheatherContext _context;

        public MoviesController(MovieTheatherContext context)
        {
            _context = context;
        }


        // GET: api/Movies
        [HttpGet]

        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var movieList = await _context.Movies.ToListAsync();

            if (movieList is null || movieList.Count == 0)
            {
                return NotFound("NO MOVIES FOUND IN THE DATABASE");
            }

            var listToDTO = movieList.Adapt<IEnumerable<MovieDTO>>();

            return Ok(listToDTO);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok((movie.Adapt<MovieDTO>()));
        }


        [HttpGet("FilterByGenre/{genre}")]


        public async Task<ActionResult<List<MovieDTO>>> FilterbyGenre(string genre) {


            if (genre.ToLower().Equals("any".ToLower())) {

               var list_of_movies = await _context.Movies.ToListAsync();
                var DTO = list_of_movies.Adapt<List<MovieDTO>>(); 
                return Ok(DTO);
            } 
            
            var list_movies = await _context.Movies.Where(x => x.Genre.ToLower() == genre.ToLower()).ToListAsync();

            if (list_movies is null) {
                return NotFound("Cannot find any movies with that genre");
            }

          

            var DTO_LIST = list_movies.Adapt<List<MovieDTO>>();

            return Ok(DTO_LIST);
        }


        [HttpGet("RatingList")]

        public async Task<ActionResult<List<string>>> getRatingList() {
            List<string> rating_list = await _context.Movies.Select(x => x.Rating).Distinct().ToListAsync();

            if (rating_list is null) {
                return BadRequest("there is an error");
            }

            foreach (string x in rating_list) {

            }
            return Ok(rating_list);
        }


        [HttpGet("ListOfGenre")]

        public async Task<ActionResult<List<string>>> GetgenreList() {
            var genreList = await _context.Movies.Select(x => x.Genre).Distinct().ToListAsync();

            if (genreList is null) {
                return BadRequest("Not able to generate Gnere List");
            }

            return Ok(genreList);
        }

        [HttpGet("ListOfYears")]

        public async Task<ActionResult<List<string>>> getListofYears()
        {
            var yearList = await _context.Movies.Select(x => x.ReleaseYear).Distinct().ToListAsync();

            if (yearList is null)
            {
                return BadRequest("Not able to generate Gnere List");
            }

            return Ok(yearList);
        }


        [HttpGet("FilterByRating/{rating}")]


        public async Task<ActionResult<List<MovieDTO>>> FilterByRating(string rating)
        {
            var list_movies = await _context.Movies.Where(x => x.Rating.ToLower() == rating.ToLower()).ToListAsync();

            if (list_movies is null && list_movies.Count() <= 0)
            {
                return NotFound("Cannot find any movies with that rating");
            }

            var DTO_LIST = list_movies.Adapt<List<MovieDTO>>();

            return Ok(DTO_LIST);
        }





        [HttpPut("{id}")]
     
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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
        


        public async Task<ActionResult<Movie>> PostMovie(MovieDTO movie)
        {




            var DTO = movie.Adapt<Movie>();
            _context.Movies.Add(DTO);
            await _context.SaveChangesAsync();

            return Ok(DTO);
        }


        [HttpGet("FilterByRealseYear/{releaseYear}")]
        public async Task<ActionResult<List<MovieDTO>>> getRealseYears(DateOnly releaseYear)
        {
            List<Movie>? realseYears = await _context.Movies.Where(x => x.ReleaseYear == releaseYear).ToListAsync();

            if ((realseYears is null) &&(realseYears.Count() <= 0) ) {
                return NotFound("there no movies within that year"); 
            }

            List<MovieDTO> movies_DTO = realseYears.Adapt<List<MovieDTO>>(); 

            return Ok(movies_DTO);
        }




        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound("there was an error");
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(); 
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }


        
    }
}
