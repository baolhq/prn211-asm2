using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Databases;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context) => _context = context;

        public IActionResult GetAll() => Ok(_context.Products);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _context.Products.Find(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (product == null) return BadRequest();

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product upProduct)
        {
            if (upProduct == null) return BadRequest();

            var product = _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductID == id);
            if (product == null) return NotFound();

            upProduct.ProductID = id;
            _context.Products.Update(upProduct);
            _context.SaveChanges();
            return Ok(upProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
    }
}