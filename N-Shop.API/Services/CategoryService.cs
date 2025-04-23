using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using N_Shop.API.Data;
using N_Shop.API.Models;

namespace N_Shop.API.Services;

public class CategoryService:ICategoryService
{
    ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Category> GetAllAsync()
    {
        return _context.Categories.ToList();
    }

    public Category? GetAsync(Expression<Func<Category, bool>> expression)
    {
        return _context.Categories.FirstOrDefault(expression);
    }

    public async Task<Category> AddAsync(Category category,CancellationToken cancellationToken = default)
    {
        await _context.Categories.AddAsync(category,cancellationToken);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> EditAsync(int id, Category category, CancellationToken cancellationToken)
    {
        Category? categoryInDb = _context.Categories.Find(id);
        if (categoryInDb == null) return false;
        categoryInDb.Name = category.Name;
        categoryInDb.Description = category.Description;
        _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateToggleAsync(int id, CancellationToken cancellationToken = default)
    {
        Category? categoryInDb = _context.Categories.Find(id);
        if (categoryInDb == null) return false;
        categoryInDb.Status = !categoryInDb.Status;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RemoveAsync(int id,CancellationToken cancellationToken = default)
    {
        Category? categoryInDb = _context.Categories.Find(id);
        if (categoryInDb == null) return false;
        _context.Categories.Remove(categoryInDb);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}