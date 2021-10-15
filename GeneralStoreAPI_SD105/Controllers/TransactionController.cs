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
    [RoutePrefix("api/Transaction")]
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> Post(Transaction model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer customer = await _context.Customers.FindAsync(model.CustomerId);

            if (customer is null)
                return NotFound();

            Product product = await _context.Products.FindAsync(model.ProductSKU);

            if (product is null)
                return NotFound();

            if (!product.IsInStock)
                return BadRequest($"{product.Name} is out of stock.");

            if (product.QuantityInStock < model.ItemCount)
                return BadRequest($"The stock level of {product.Name} is {product.QuantityInStock}, which is lower than the requested amount.");
            product.QuantityInStock -= model.ItemCount;

            model.DateOfTransaction = DateTime.Now;
            _context.Transactions.Add(model);
            if (!(await _context.SaveChangesAsync() == 2))
                return InternalServerError();
            return Ok($"Transaction #{model.Id} successfully added.");
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _context.Transactions.ToListAsync());
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            Transaction transaction = await _context.Transactions.FindAsync(id);

            if (transaction is null)
                return NotFound();
            return Ok(transaction);
        }
        [HttpGet]
        [Route("GetBy/Customer/{id}")]
        public async Task<IHttpActionResult> GetByCustomer(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer is null)
            {
                return BadRequest($"Not Found: Customer with id of {id}");
            }
            List<Transaction> returnValue = await _context.Transactions.Where(t => t.CustomerId == id).ToListAsync();
            if (returnValue.Count < 1)
            {
                return BadRequest($"Customer {customer.FullName} does not have any transactions yet.");
            }
            return Ok(returnValue);
        }

        [HttpGet]
        [Route("GetBy/Product/{sku}")]
        public async Task<IHttpActionResult> GetByProduct(string sku)
        {
            Product product = await _context.Products.FindAsync(sku);
            if (product is null)
            {
                return BadRequest($"Not Found: Product with SKU of {sku}");
            }
            List<Transaction> returnValue = await _context.Transactions.Where(t => t.ProductSKU == sku).ToListAsync();
            if (returnValue.Count < 1)
            {
                return BadRequest($"Product {product.Name} does not have any transactions yet.");
            }
            return Ok(returnValue);
        }

        /*[HttpPut]
        public async Task<IHttpActionResult> Put(int id, Transaction model)
        {

        }*/
    }
}
