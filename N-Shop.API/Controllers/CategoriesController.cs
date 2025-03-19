using Microsoft.AspNetCore.Mvc;
using N_Shop.API.Data;
using N_Shop.API.Models;

namespace N_Shop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("")]
    public IActionResult GetAll()
    {
        var categories = _context.Categories.ToList();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute]int id)
    {
        var category = _context.Categories.Find(id);
        return category == null ? NotFound() : Ok(category);
    }

    [HttpPost("")]
    public IActionResult Create([FromBody] Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
        //return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null) return NotFound();
        _context.Categories.Remove(category);
        _context.SaveChanges();
        return NoContent();
    }
}