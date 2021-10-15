using GeneralStoreAPI_SD105.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI_SD105.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> Post(Customer model)
        {
            //if (model is null)
            //    return BadRequest("You must enter information.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Customers.Add(model);
            if (!(await _context.SaveChangesAsync() == 1))
                return InternalServerError();
            return Ok($"{model.FullName} successfully added.");
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _context.Customers.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            //if (sku is null)
            //    return BadRequest($"You must provide a {nameof(sku)} parameter.");
            Customer customer = await _context.Customers.FindAsync(id);

            if (customer is null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Put([FromUri] int id, [FromBody] Customer model)
        {
            //if (sku is null)
            //    return BadRequest($"You must provide a {nameof(sku)} parameter.");
            if (model is null)
                return BadRequest("You need to enter information.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer is null)
                return NotFound();

            //property conversion
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            customer.IsRewardsMember = model.IsRewardsMember;
            customer.ShippingAddress = model.ShippingAddress;

            if (!(await _context.SaveChangesAsync() == 1))
                return InternalServerError();
            return Ok($"{model.FullName} successfully updated.");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            //if (sku is null)
            //    return BadRequest($"You must provide a {nameof(sku)} parameter.");
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer is null)
                return NotFound();
            _context.Customers.Remove(customer);
            if (!(await _context.SaveChangesAsync() == 1))
                return InternalServerError();
            return Ok($"{customer.FullName} successfully removed.");
        }
    }
}
