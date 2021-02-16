using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.App.API.Data;
using Library.App.API.Models;
using LibraryAppAPI.Interfaces;
using LibraryAppAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _service;
        private readonly LibraryDBContext _context;
        public CustomerController(ICustomerServices service, LibraryDBContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpPost, Route("Login"), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginRequest _request)
        {
            if (_request == null || string.IsNullOrEmpty(_request.Username) ||
                string.IsNullOrEmpty(_request.Password))
                return BadRequest("Missing login details...");
            var _response = await _service.Login(_request);
            if (_response == null)
                return BadRequest($"Invalid Credentials");
            return Ok(_response);
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IEnumerable<Customers>> Get()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
