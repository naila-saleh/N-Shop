using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using N_Shop.API.Data;
using N_Shop.API.DTOs.Requests;
using N_Shop.API.DTOs.Responses;
using N_Shop.API.Models;

namespace N_Shop.API.Services;

public class ProductService(ApplicationDbContext context):IProductService
{
    private readonly ApplicationDbContext _context=context;
    public IEnumerable<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public Product? Get(Expression<Func<Product, bool>> expression)
    {
        return _context.Products.FirstOrDefault(expression);
    }

    public Product Add(ProductRequest product)
    {
        var file = product.Image;
        var productInDb = product.Adapt<Product>();
        if (file != null && file.Length > 0)
        {
            var fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);
            using (var stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }
            productInDb.Image=fileName;
            _context.Products.Add(productInDb);
            _context.SaveChanges();
        }
        return productInDb;
    }

    public bool Edit(int id, Product product)
    {
        var productInDb = _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (productInDb == null) return false;
        product.Id = id;
        _context.Products.Update(productInDb);
        _context.SaveChanges();
        return true;
    }

    public bool Remove(int id)
    {
        var productInDb = _context.Products.Find(id);
        if (productInDb == null) return false;
        _context.Products.Remove(productInDb);
        _context.SaveChanges();
        return true;
    }
}