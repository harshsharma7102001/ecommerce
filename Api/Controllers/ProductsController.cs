using Domain.Entities;
using Domain.Interface.IProductInterface;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Api.Controllers.ProductsControllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductInterface _productInterface) : ControllerBase
{
    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct(string ?brand,string ?type,string ?sort) {
        return  Ok(await _productInterface.GetProducts(brand,type,sort));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var result = await _productInterface.GetProductById(id);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddNewProduct(Product product)
    {
        var result =await _productInterface.AddProduct(product);
        if (result==null)
        {
            return BadRequest("Something went wrong");
        }
        return Ok("Product added successfully");
    }

    [HttpDelete("{id:Int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productInterface.DeleteProduct(id);
        if (result == false)
        {
            return NotFound("Product not found");
        }
        return Ok("Record Deletd Successfully");
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<bool>> UpdatePoduct(int id,Product product)
    {
       var result = await _productInterface.UpdateProducts(id, product);
        if (result == false)
        {
            return NotFound("Product not found");
        }
        return Ok("Product updated successfully");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var result =  await _productInterface.GetProductBrands();
        if (result == null)
        {
            return BadRequest("Something went wrong");
        }
        return Ok(result);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var result = await _productInterface.GetProductTypes();
        if (result == null)
        {
            return BadRequest("Something went wrong");
        }
        return Ok(result);
    }


}
