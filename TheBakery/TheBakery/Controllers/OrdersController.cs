using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBakery.Data;
using TheBakery.Models;
using TheBakery.Models.DTOs.Customer;
using TheBakery.Models.DTOs.OrderDetailsDtos;
using TheBakery.Models.DTOs.OrderDtos;
using TheBakery.Services;

namespace TheBakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TheBakeryContext _context;
        private readonly IOrderService _orderService;

        public OrdersController(TheBakeryContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderDto>>> GetOrder()
        {
            return (await _orderService.GetAllAsync()).ToList();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDto>> GetOrder(Guid id)
        {
            var order = await _orderService.GetAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // GET: api/Orders/5/Customer
        [HttpGet("{id}/Customer")]
        public async Task<ActionResult<GetCustomerDto>> GetOrderCustomer(Guid id)
        {
            var customer = await _orderService.GetCustomerByOrderId(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Orders/5/OrderDetails
        [HttpGet("{id}/OrderDetails")]
        public async Task<ActionResult<List<GetOrderDetailsDto>>> GetOrderDetails(Guid id)
        {
            var orderDetails = await _orderService.GetOrderDetailsByOrderId(id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return orderDetails;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, PutOrderDto order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var result = await _orderService.UpdateAsync(id, order);

            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok(result.Message);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(PostOrderDto order)
        {
            var created = await _orderService.CreateAsync(order);

            if (!created.Item1)
            {
                return Problem("There was a problem creating the Customer.");
            }

            return CreatedAtAction("GetOrder", new { id = created.Item2?.OrderId }, created.Item2);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var result = await _orderService.DeleteAsync(id);

            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
