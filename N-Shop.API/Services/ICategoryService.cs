using System.Linq.Expressions;
using N_Shop.API.Models;

namespace N_Shop.API.Services;

public interface ICategoryService
{
    IEnumerable<Category> GetAllAsync();
    Category? GetAsync(Expression<Func<Category, bool>> expression);
    Task<Category> AddAsync(Category category,CancellationToken cancellationToken = default);
    Task<bool> EditAsync(int id,Category category,CancellationToken cancellationToken = default);
    Task<bool> UpdateToggleAsync(int id,CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(int id,CancellationToken cancellationToken = default);
}