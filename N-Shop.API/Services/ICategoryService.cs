using System.Linq.Expressions;
using N_Shop.API.Models;

namespace N_Shop.API.Services;

public interface ICategoryService
{
    IEnumerable<Category> GetAll();
    Category? Get(Expression<Func<Category, bool>> expression);
    Category Add(Category category);
    bool Edit(int id,Category category);
    bool Remove(int id);
}