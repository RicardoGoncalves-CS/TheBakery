using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBakery.Data;
using TheBakery.Models;
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
            /*
          if (_context.Order == null)
          {
              return NotFound();
          }
            return await _context.Order.ToListAsync();*/
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
            /*
          if (_context.Order == null)
          {
              return NotFound();
          }
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;*/
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, PutOrderDto order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var updatedSuccessfully = await _orderService.UpdateAsync(id, order);

            if (!updatedSuccessfully)
            {
                return NotFound();
            }
            return NoContent();
            /*
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(PostOrderDto order)
        {
            var created = await _orderService.CreateAsync(order);

            if (!created.Item1)
            {
                return Problem("There was a problem creating the Customer.");
            }

            return CreatedAtAction("GetOrderDetails", new { id = created.Item2?.OrderId }, created.Item2);
            /*
          if (_context.Order == null)
          {
              return Problem("Entity set 'TheBakeryContext.Order'  is null.");
          }
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);*/
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var deleted = await _orderService.DeleteAsync(id);

            if (deleted == false)
            {
                return NotFound();
            }

            return NoContent();
            /*
            if (_context.Order == null)
            {
                return NotFound();
            }
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();*/
        }

        private bool OrderExists(Guid id)
        {
            return (_context.Order?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
