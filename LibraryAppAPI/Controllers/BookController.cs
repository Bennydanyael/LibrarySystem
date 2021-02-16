using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.App.API.Data;
using Library.App.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDBContext _context;
        public BookController(LibraryDBContext context)
        {
            _context = context;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<IEnumerable<Books>> Get()
        {
            return await _context.Books.ToListAsync();
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> Get(int id)
        {
            var _book = await _context.Books.FindAsync(id);
            if (_book == null)
                return NotFound();
            return _book;
        }

        // POST api/<BookController>
        [HttpPost, ProducesResponseType(StatusCodes.Status200OK),ProducesResponseType(StatusCodes.Status400BadRequest), 
            ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Books>> Post([FromBody] Books _books)
        {
            _context.Books.Add(_books);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBooks", new { id = _books.Id }, _books);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}"),ProducesResponseType(StatusCodes.Status200OK),ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(int id, [FromBody] Books _books)
        {
            if (id != _books.Id)
                return BadRequest();
            _context.Entry(_books).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}"),ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Books>> Delete(int id)
        {
            var _del = await _context.Books.FindAsync(id);
            if (_del == null)
                return NotFound();

            _context.Books.Remove(_del);
            await _context.SaveChangesAsync();
            return _del;
        }

        public bool BookExists(int id)
        {
            return _context.Books.Any(_b => _b.Id == id);
        }
    }
}
