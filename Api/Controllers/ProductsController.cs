using Domain.Entities;
using Domain.Interface;
using Domain.Interface.IProductInterface;
using Domain.Specification;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Api.Controllers.ProductsControllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericInterface<Product> _productInterface) : ControllerBase
{
    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct(string ?brand,string ?type,string ?sort) {
        var spec = new ProductSpecification(brand, type,sort);
        var result = await _productInterface.GetAllWithSpecification(spec);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var result = await _productInterface.GetById(id);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<bool> AddNewProduct(Product product)
    {
        _productInterface.Add(product);
        var result = _productInterface.SaveChangesAsync();
        if(!result) return BadRequest("Something went wrong while saving the product");
        return Ok(result);
    }

    [HttpDelete("{id:Int}")]
    public ActionResult<bool> DeleteProduct(int id)
    {
        var data  = _productInterface.GetById(id);
        if(data == null)
        {
            return NotFound("Product not found");
        }
         bool result = _productInterface.Delete(data.Result);
        return Ok(result);
    }
    [HttpPut("{id:int}")]
    public ActionResult<bool> UpdateProduct(int id,Product product)
    {
        bool result = _productInterface.Update(product);
        if (!result)
        {
            return BadRequest("Something went wrong while updating the product");
        }
        return Ok(result);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        return Ok(await _productInterface.GetAllWithSpecification(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec =  new TypeListSpecification();
        return Ok(await _productInterface.GetAllWithSpecification(spec));

    }


}
