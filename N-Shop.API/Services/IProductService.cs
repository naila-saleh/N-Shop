using System.Linq.Expressions;
using N_Shop.API.DTOs.Requests;
using N_Shop.API.Models;

namespace N_Shop.API.Services;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? Get(Expression<Func<Product, bool>> expression);
    Product Add(ProductRequest product);
    bool Edit(int id,Product product);
    bool Remove(int id);
}