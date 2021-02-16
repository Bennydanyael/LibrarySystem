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
    public class LibraryController : ControllerBase
    {
        private readonly LibraryDBContext _context;
        public LibraryController(LibraryDBContext context)
        {
            _context = context;
        }

        // GET: api/<LibraryController>
        [HttpGet]
        public async Task<IEnumerable<LibraryTrancs>> Get()
        {
            return await _context.LibraryTrancs.ToListAsync();
        }

        // GET api/<LibraryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryTrancs>> Get(int id)
        {
            var _library = await _context.LibraryTrancs.FindAsync(id);
            if (_library == null)
                return NotFound();
            return _library;
        }

        // POST api/<LibraryController>
        [HttpPost,ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status204NoContent),
            ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LibraryTrancs>> Post([FromBody] LibraryTrancs _library)
        {
            if (_library == null)
                return NoContent();
            _context.LibraryTrancs.Add(_library);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLibraryTancs", new { Id = _library.Id }, _library);
        }

        // PUT api/<LibraryController>/5
        [HttpPut("{id}"),ProducesResponseType(StatusCodes.Status200OK),ProducesResponseType(StatusCodes.Status404NotFound),
            ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] LibraryTrancs _library)
        {
            if (id != _library.Id)
                return BadRequest();
            _context.Entry(_library).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException)
            {
                if (!LibraryExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE api/<LibraryController>/5
        [HttpDelete("{id}"),ProducesResponseType(StatusCodes.Status200OK),ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LibraryTrancs>> Delete(int id)
        {
            var _library = await _context.LibraryTrancs.FindAsync(id);
            if (_library == null)
                return NotFound();
            _context.LibraryTrancs.Remove(_library);
            await _context.SaveChangesAsync();
            return _library;
        }

        public bool LibraryExists(int _id)
        {
            return _context.LibraryTrancs.Any(_l => _l.Id == _id);
        }
    }
}
