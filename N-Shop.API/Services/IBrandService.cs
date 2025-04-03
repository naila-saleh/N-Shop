using System.Linq.Expressions;
using N_Shop.API.Models;

namespace N_Shop.API.Services;

public interface IBrandService
{
    IEnumerable<Brand> GetAll();
    Brand? Get(Expression<Func<Brand, bool>> expression);
    Brand Add(Brand brand);
    bool Edit(int id,Brand brand);
    bool Remove(int id);
}