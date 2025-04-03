using Mapster;
using Microsoft.AspNetCore.Mvc;
using N_Shop.API.Data;
using N_Shop.API.DTOs.Requests;
using N_Shop.API.DTOs.Responses;
using N_Shop.API.Models;

namespace N_Shop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController (ApplicationDbContext context): ControllerBase
{
    private readonly ApplicationDbContext _context=context;
    [HttpGet("")]
    public IActionResult GetAll()
    {
        var products = _context.Products.ToList();
        if (!products.Any()) return NotFound();
        return Ok(products.Adapt<IEnumerable<ProductResponse>>());
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute]int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return NotFound();
        return Ok(product.Adapt<ProductResponse>());
    }

    [HttpPost("")]
    public IActionResult Create([FromForm] ProductRequest product)
    {
        var file = product.Image;
        var productInDb = product.Adapt<Product>();
        if (file != null && file.Length > 0)
        {
            var fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(),"Images",fileName);
            using (var stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }
            productInDb.Image=fileName;
            _context.Products.Add(productInDb);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = productInDb.Id }, productInDb);
        }
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return NotFound();
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", product.Image);
        if(System.IO.File.Exists(path)) System.IO.File.Delete(path);
        _context.Products.Remove(product);
        _context.SaveChanges();
        return NoContent();
    }
}