using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBakery.Data;
using TheBakery.Models;
using TheBakery.Models.DTOs;
using TheBakery.Models.DTOs.Customer;
using TheBakery.Services;

namespace TheBakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly TheBakeryContext _context;

        public CustomersController(TheBakeryContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCustomerDto>>> GetCustomer()
        {
            return (await _customerService.GetAllAsync()).ToList();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerDto>> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Customers/5/Address
        [HttpGet("{id}/Address")]
        public async Task<ActionResult<Address>> GetCustomerAddress(Guid id)
        {
            var address = await _customerService.GetAddressByCustomerId(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, PutCustomerDto customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            var updatedSuccessfully = await _customerService.UpdateAsync(id, customer);

            if (!updatedSuccessfully)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(PostCustomerDto customer)
        {
            var created = await _customerService.CreateAsync(customer);

            if (!created.Item1)
            {
                return Problem("There was a problem creating the Customer.");
            }

            return CreatedAtAction("GetCustomer", new { id = created.Item2?.CustomerId }, created.Item2);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var deleted = await _customerService.DeleteAsync(id);

            if (deleted == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
