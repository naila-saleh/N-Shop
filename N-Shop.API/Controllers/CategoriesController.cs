using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using N_Shop.API.Data;
using N_Shop.API.DTOs.Requests;
using N_Shop.API.DTOs.Responses;
using N_Shop.API.Models;
using N_Shop.API.Services;

namespace N_Shop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService categoryService=categoryService;
    
    [HttpGet("")]
    public IActionResult GetAll()
    {
        var categories = categoryService.GetAll();
        return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute]int id)
    {
        var category = categoryService.Get(e=>e.Id == id);
        return category == null ? NotFound() : Ok(category.Adapt<CategoryResponse>());
    }

    [HttpPost("")]
    public IActionResult Create([FromBody] CategoryRequest category)
    {
        var categoryInDb = categoryService.Add(category.Adapt<Category>());
        //return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category);
        return CreatedAtAction(nameof(GetById), new { categoryInDb.Id }, categoryInDb.Adapt<CategoryResponse>());
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id,[FromBody] CategoryRequest category)
    {
        var categoryToUpdate = categoryService.Edit(id,category.Adapt<Category>());
        return !categoryToUpdate? NotFound(): NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var categoryToDelete = categoryService.Remove(id);
        if (!categoryToDelete) return NotFound();
        return NoContent();
    }
}