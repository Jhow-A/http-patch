using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatchTest.Data;
using PatchTest.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatchTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movie.ToListAsync();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<IEnumerable<Movie>>> PartiallyUpdateMovie(int id, [FromBody] JsonPatchDocument<Movie> movieModified)
        {
            if (movieModified == null)
                return BadRequest();

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie is null)
                return NotFound();

            movieModified.ApplyTo(movie, ModelState);

            var isValid = TryValidateModel(movie);
            if (!isValid)
                return BadRequest(ModelState);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
