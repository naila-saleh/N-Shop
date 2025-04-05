using Mapster;
using Microsoft.AspNetCore.Mvc;
using N_Shop.API.Data;
using N_Shop.API.DTOs.Requests;
using N_Shop.API.DTOs.Responses;
using N_Shop.API.Models;
using N_Shop.API.Services;

namespace N_Shop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController (ProductService productService): ControllerBase
{
    private readonly ProductService _productService=productService;
    [HttpGet("")]
    public IActionResult GetAll()
    {
        var products = _productService.GetAll();
        return Ok(products.Adapt<IEnumerable<ProductResponse>>());
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute]int id)
    {
        var product = _productService.Get(x=>x.Id == id);
        return product == null? NotFound(): Ok(product.Adapt<ProductResponse>());
    }

    [HttpPost("")]
    public IActionResult Create([FromForm] ProductRequest product)
    {
        var productInDb = _productService.Add(product);
        if(productInDb == null)return BadRequest();
        return CreatedAtAction(nameof(GetById), new { id = productInDb.Id }, productInDb);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromForm] ProductRequest product)
    {
        var productInDb = _productService.Edit(id, product.Adapt<Product>());
        if (!productInDb) return NotFound();
        return Ok(productInDb);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var product = _productService.Remove(id);
        if (!product) return NotFound();
        return NoContent();
    }
}