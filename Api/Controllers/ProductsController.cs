using Domain.Entities;
using Infrastructure.Data.StoreContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Api.Controllers.ProductsControllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    public readonly StoreContext _context;
    public ProductsController(StoreContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct() {
        return  await _context.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> AddNewProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:Int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        Product p = await _context.Products.FirstOrDefaultAsync(x=>x.Id==id);
        if(p==null)
        {
            return  NotFound();
        }
        _context.Products.Remove(p);
        await _context.SaveChangesAsync();
        return Ok("Successfully Deleted");
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePoduct(int id,Product product)
    {
        try
        {
            if (!checkIfExits(id))
            {
                return NotFound("Product not found");
            }
             if (id != product.Id)
            {
                return BadRequest("Product ID mismatch");
            }
             _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Successfully Updated");

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public bool checkIfExits(int id)
    {
        return _context.Products.Any(x=>x.Id ==id);
    }

}
