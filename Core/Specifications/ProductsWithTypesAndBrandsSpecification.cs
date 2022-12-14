namespace Core.Specifications;
public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams)
       : base(x =>
      (string.IsNullOrEmpty(productSpecParams.Search)
      || x.Name.ToLower().Contains(productSpecParams.Search)) //search      
      &&
      (!productSpecParams.BrandId.HasValue
      || x.ProductBrandId == productSpecParams.BrandId) //filter
      &&
      (!productSpecParams.TypeId.HasValue
      || x.ProductTypeId == productSpecParams.TypeId))//filter
    {
        AddInclude(x => x.ProductType);//include
        AddInclude(x => x.ProductBrand);//include

        AddOrderBy(x => x.Name);//order
        if (!string.IsNullOrEmpty(productSpecParams.Sort)) //order
        {
            switch (productSpecParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
        }

        ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1),
productSpecParams.PageSize);//pagination

    }

    public ProductsWithTypesAndBrandsSpecification(int id)
    : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}
