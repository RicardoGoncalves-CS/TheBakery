using Microsoft.AspNetCore.Mvc;
using TheBakery.Data;
using TheBakery.Models;
using TheBakery.Models.DTOs;
using TheBakery.Services;

namespace TheBakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return (await _addressService.GetAllAsync()).ToList();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(Guid id)
        {
            var address = await _addressService.GetAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(Guid id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            var result = await _addressService.UpdateAsync(id, address);

            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok(result.Message);
        }

        // POST: api/Addresses
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(PostAddressDto address)
        {
            var created = await _addressService.CreateAsync(address);

            if (!created.Item1)
            {
                return Problem("There was a problem creating the Address. Verify if Address already exists in the database!");
            }

            return CreatedAtAction("GetAddress", new { id = created.Item2?.AddressId }, created.Item2);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var result = await _addressService.DeleteAsync(id);

            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
