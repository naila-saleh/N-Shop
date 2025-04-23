using Mapster;
using Microsoft.AspNetCore.Authorization;
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
        var categories = categoryService.GetAllAsync();
        return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute]int id)
    {
        var category = categoryService.GetAsync(e=>e.Id == id);
        return category == null ? NotFound() : Ok(category.Adapt<CategoryResponse>());
    }

    [HttpPost("")]
    public IActionResult Create([FromBody] CategoryRequest category,CancellationToken cancellationToken)
    {
        var categoryInDb = categoryService.AddAsync(category.Adapt<Category>(), cancellationToken);
        //return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category);
        return CreatedAtAction(nameof(GetById), new { categoryInDb.Id }, categoryInDb.Adapt<CategoryResponse>());
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id,[FromBody] CategoryRequest category)
    {
        var categoryToUpdate = categoryService.EditAsync(id,category.Adapt<Category>());
        if (categoryToUpdate == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var categoryToDelete = categoryService.RemoveAsync(id);
        if (categoryToDelete == null) return NotFound();
        return NoContent();
    }
}