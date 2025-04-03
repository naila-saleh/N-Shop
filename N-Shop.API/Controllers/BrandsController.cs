using Mapster;
using Microsoft.AspNetCore.Mvc;
using N_Shop.API.DTOs.Requests;
using N_Shop.API.DTOs.Responses;
using N_Shop.API.Models;
using N_Shop.API.Services;

namespace N_Shop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController(IBrandService brandService) : ControllerBase
{
    private readonly IBrandService _brandService=brandService;

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var brands = _brandService.GetAll();
        return Ok(brands.Adapt<IEnumerable<BrandResponse>>());
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var brand=_brandService.Get(b=>b.Id==id);
        return brand == null ? NotFound() : Ok(brand.Adapt<BrandResponse>());
    }

    [HttpPost("")]
    public IActionResult Create([FromBody] BrandRequest brand)
    {
        var brandToCreate = _brandService.Add(brand.Adapt<Brand>());
        return CreatedAtAction(nameof(GetById), new { brandToCreate.Id }, brandToCreate);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] BrandRequest brand)
    {
        var brandToUpdate = _brandService.Edit(id, brand.Adapt<Brand>());
        return (!brandToUpdate)? NotFound() : NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var brandToDelete = _brandService.Remove(id);
        if(!brandToDelete) return NotFound();
        return NoContent();
    }
}