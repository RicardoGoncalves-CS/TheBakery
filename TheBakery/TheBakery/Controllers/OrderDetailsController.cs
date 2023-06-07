using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBakery.Data;
using TheBakery.Models;
using TheBakery.Models.DTOs.OrderDetailsDtos;
using TheBakery.Services;

namespace TheBakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;

        public OrderDetailsController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderDetailsDto>>> GetOrderDetails()
        {
            return (await _orderDetailsService.GetAllAsync()).ToList();
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDetailsDto>> GetOrderDetails(Guid id)
        {
            var orderDetails = await _orderDetailsService.GetAsync(id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return orderDetails;
        }

        // GET: api/Customers/5/Product
        [HttpGet("{id}/Product")]
        public async Task<ActionResult<Product>> GetCustomerAddress(Guid id)
        {
            var product = await _orderDetailsService.GetProductByOrderDetailsId(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/OrderDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetails(Guid id, PutOrderDetailsDto orderDetails)
        {
            if (id != orderDetails.OrderDetailsId)
            {
                return BadRequest();
            }

            var result = await _orderDetailsService.UpdateAsync(id, orderDetails);

            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok(result.Message);
        }

        // POST: api/OrderDetails
        [HttpPost]
        public async Task<ActionResult<OrderDetails>> PostOrderDetails(PostOrderDetailsDto orderDetails)
        {
            var created = await _orderDetailsService.CreateAsync(orderDetails);

            if (!created.Item1)
            {
                return Problem("There was a problem creating the Customer.");
            }

            return CreatedAtAction("GetOrderDetails", new { id = created.Item2?.OrderDetailsId }, created.Item2);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetails(Guid id)
        {
            var result = await _orderDetailsService.DeleteAsync(id);

            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
