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
    public class AuthorController : ControllerBase
    {
        private readonly LibraryDBContext _context;
        public AuthorController(LibraryDBContext context)
        {
            _context = context;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public async Task<IEnumerable<Authors>> Get()
        {
            return await _context.Authors.ToListAsync();
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Authors>> Get(int id)
        {
            var _author = await _context.Authors.FindAsync(id);
            if (_author == null)
                return NotFound();
            return _author;
        }

        // POST api/<AuthorController>
        [HttpPost, ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <ActionResult<Authors>> Post([FromBody] Authors _author)
        {
            _context.Authors.Add(_author);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAuthors", new { id = _author.AuthorsID}, _author);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}"), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Put(int id, [FromBody] Authors _author)
        {
            if (id != _author.AuthorsID)
                return BadRequest();

            _context.Entry(_author).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Authors>> Delete(int id)
        {
            var _author = await _context.Authors.FindAsync(id);
            if (_author == null)
                return NotFound();
            _context.Authors.Remove(_author);
            await _context.SaveChangesAsync();
            return _author;
        }

        public bool AuthorExists(int _id)
        {
            return _context.Authors.Any(_a => _a.AuthorsID == _id);
        }
    }
}
