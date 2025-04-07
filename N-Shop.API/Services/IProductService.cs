using System.Linq.Expressions;
using N_Shop.API.DTOs.Requests;
using N_Shop.API.Models;

namespace N_Shop.API.Services;

public interface IProductService
{
    IEnumerable<Product> GetAll(string? query, int page = 1, int limit = 10);
    Product? Get(Expression<Func<Product, bool>> expression);
    Product Add(ProductRequest product);
    bool Edit(int id,ProductUpdateRequest product);
    bool Remove(int id);
}