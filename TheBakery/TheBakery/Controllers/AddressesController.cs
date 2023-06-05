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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(Guid id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            var updatedSuccessfully = await _addressService.UpdateAsync(id, address);

            if (!updatedSuccessfully)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(PostAddressDto address)
        {
            var created = await _addressService.CreateAsync(address);

            if (!created.Item1)
            {
                return Problem("There was a problem creating the Address.");
            }

            return CreatedAtAction("GetAddress", new { id = created.Item2?.AddressId }, created.Item2);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var deleted = await _addressService.DeleteAsync(id);

            if (deleted == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
