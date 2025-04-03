using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using N_Shop.API.Data;
using N_Shop.API.Models;

namespace N_Shop.API.Services;

public class BrandService(ApplicationDbContext context): IBrandService
{
    ApplicationDbContext _context=context;

    public IEnumerable<Brand> GetAll()
    {
        return _context.Brands.ToList();
    }

    public Brand? Get(Expression<Func<Brand, bool>> predicate)
    {
        return _context.Brands.FirstOrDefault(predicate);
    }

    public Brand Add(Brand brand)
    {
        _context.Brands.Add(brand);
        _context.SaveChanges();
        return brand;
    }

    public bool Edit(int id,Brand brand)
    {
        Brand? brandInDb = _context.Brands.AsNoTracking().FirstOrDefault(b => b.Id == id);
        if (brandInDb == null) return false;
        brand.Id=brandInDb.Id;
        _context.Brands.Update(brand);
        _context.SaveChanges();
        return true;
    }

    public bool Remove(int id)
    {
        Brand? brandInDb = _context.Brands.Find(id);
        if (brandInDb == null) return false;
        _context.Brands.Remove(brandInDb);
        _context.SaveChanges();
        return true;
    }
}