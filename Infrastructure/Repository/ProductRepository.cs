using Domain.Entities;
using Domain.Interface.IProductInterface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class ProductRepository : IProductInterface
{
    private readonly StoreContext _context;
    public ProductRepository(StoreContext context)
    { 
      _context = context;
    }
    public async Task<bool> AddProduct(Product product)
    {
        _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public bool checkIfExits(int id)
    {
        return _context.Products.Any(x => x.Id == id);
    }

    public async Task<bool> DeleteProduct(int id)
    {
        Product product = await  _context.Products.FindAsync(id);
        if (product ==null)
        {
            return false;
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Product> GetProductById(int id)
    {
        if (!checkIfExits(id))
        {
            return null;
        }
        return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProducts(string?brand,string?types,string ?qry)
    {
        var query = _context.Products.AsQueryable();
        if (!string.IsNullOrEmpty(brand))
        {
            return await query.Where(x => x.Brand == brand).ToListAsync();
        }
        if (!string.IsNullOrEmpty(types))
        {
            return await query.Where(x=>x.Type == types).ToListAsync();
        }
        query = qry switch
        {
            "priceAsc" => query.OrderBy(x => x.Price),
            "priceDesc" => query.OrderByDescending(x => x.Price),
            _ => query.OrderBy(x => x.Name)
        };
        return await query.ToListAsync();

    }

    public async Task<IReadOnlyList<string>> GetProductBrands()
    {
        var brands = await _context.Products.Select(x => x.Brand).Distinct().ToListAsync();
        return brands;
    }

    public async Task<IReadOnlyList<string>> GetProductTypes()
    {
        var types = await _context.Products.Select(x=>x.Type).Distinct().ToListAsync();
        return types;
    }

    public async Task<bool> UpdateProducts(int id, Product product)
    {
        if(!checkIfExits(id))
        {
            return false;
        }
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;

    }
}
