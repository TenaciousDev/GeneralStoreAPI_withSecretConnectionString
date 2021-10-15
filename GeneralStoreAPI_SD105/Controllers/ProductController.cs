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
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> Post(Product model)
        {
            //if (model is null)
            //    return BadRequest("You must enter information.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Products.Add(model);
            if (!(await _context.SaveChangesAsync() == 1))
                return InternalServerError();
            return Ok($"{model.Name} successfully added.");
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet]
        [Route("{sku}")]
        public async Task<IHttpActionResult> Get(string sku)
        {
            //if (sku is null)
            //    return BadRequest($"You must provide a {nameof(sku)} parameter.");
            Product product = await _context.Products.FindAsync(sku);

            if (product is null)
                return NotFound();
            return Ok(product);
        }

        [HttpPut]
        [Route("{sku}")]
        public async Task<IHttpActionResult> Put([FromUri] string sku, [FromBody] Product model)
        {
            //if (sku is null)
            //    return BadRequest($"You must provide a {nameof(sku)} parameter.");
            if (model is null)
                return BadRequest("You need to enter information.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Product product = await _context.Products.FindAsync(sku);
            if (product is null)
                return NotFound();

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.QuantityInStock = model.QuantityInStock;

            if (!(await _context.SaveChangesAsync() == 1))
                return InternalServerError();
            return Ok($"{model.Name} successfully updated.");
        }

        [HttpDelete]
        [Route("{sku}")]
        public async Task<IHttpActionResult> Delete(string sku)
        {
            //if (sku is null)
            //    return BadRequest($"You must provide a {nameof(sku)} parameter.");
            Product product = await _context.Products.FindAsync(sku);
            if (product is null)
                return NotFound();
            _context.Products.Remove(product);
            if (!(await _context.SaveChangesAsync() == 1))
                return InternalServerError();
            return Ok($"{product.Name} successfully removed.");
        }
    }
}
