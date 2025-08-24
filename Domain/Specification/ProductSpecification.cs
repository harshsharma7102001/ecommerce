using Domain.Entities;

namespace Domain.Specification;

public class ProductSpecification :BaseSpecification<Product>
{
    public ProductSpecification(string? brand,string?type,string? query):base(x=>
    (string.IsNullOrWhiteSpace(brand)|| x.Brand == brand) 
    && (string.IsNullOrWhiteSpace(type)|| x.Type == type))
    {
        switch (query)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDes":
                AddOrderByDescending(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }

}
