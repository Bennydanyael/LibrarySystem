using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryAppAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _order;
        public OrderController(IOrderService order)
        {
            _order = order;
        }

        [HttpGet, Authorize(Policy = "OnlyNonBlockedCustomer")]
        public async Task<IActionResult> Get()
        {
            var _claimIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var _claim = _claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (_claim == null)
                return Unauthorized("Invalid Customer...");
            var _orders = await _order.GetOrderByCustomerId(int.Parse(_claim.Value));
            if (_orders == null || !_orders.Any())
                return BadRequest($"No order was found...");
            return Ok(_orders);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
