using Domain.Entities;

namespace Domain.Specification;



public class BrandListSpecification:BaseSpecification<Product,string>
{
    public BrandListSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();
    }
}
