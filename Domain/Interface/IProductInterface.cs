using Domain.Entities;
namespace Domain.Interface.IProductInterface;

public interface IProductInterface
{
    public Task<Product> GetProductById(int id);
    public Task<IEnumerable<Product>> GetProducts(string?brand,string?type,string ?query);
    public Task<bool> DeleteProduct(int id);

    public Task<bool> AddProduct(Product product);
    public Task<bool> UpdateProducts(int id, Product product);

    public Task<IReadOnlyList<string>> GetProductBrands();
    public Task<IReadOnlyList<string>> GetProductTypes();

    public bool checkIfExits(int id);

}
