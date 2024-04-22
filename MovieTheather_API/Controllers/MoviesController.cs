using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Azure.Identity;
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
            var list_movies = await _context.Movies.Where(x=> x.Genre.ToLower() ==genre.ToLower()).ToListAsync();

            if (list_movies is null) { 
                return NotFound("Cannot find any movies with that genre");
            }
            
            var DTO_LIST = list_movies.Adapt<List<MovieDTO>>();

            return Ok(DTO_LIST); 
        }




        [HttpGet("FilterByRating/{rating}")]


        public async Task<ActionResult<List<MovieDTO>>> FilterByRating(string rating)
        {
            var list_movies = await _context.Movies.Where(x => x.Rating.ToLower() == rating.ToLower()).ToListAsync();

            if (list_movies is null)
            {
                return NotFound("Cannot find any movies with that rating");
            }

            var DTO_LIST = list_movies.Adapt<List<MovieDTO>>();

            return Ok(DTO_LIST);
        }



      
        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize]

        
        public async Task<ActionResult<Movie>> PostMovie(MovieDTO movie)
        {

          
            

            var DTO = movie.Adapt<Movie>(); 
            _context.Movies.Add(DTO);
            await _context.SaveChangesAsync();

            return Ok(DTO); 
        }




        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound("there was an error");
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
