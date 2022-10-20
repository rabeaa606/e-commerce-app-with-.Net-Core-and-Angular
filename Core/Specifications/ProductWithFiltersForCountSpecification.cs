using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
             : base(x =>
            (string.IsNullOrEmpty(productParams.Search)
               || x.Name.ToLower().Contains(productParams.Search)) //search      
              &&
               (!productParams.BrandId.HasValue
               || x.ProductBrandId == productParams.BrandId) //filter
              &&
              (!productParams.TypeId.HasValue
              || x.ProductTypeId == productParams.TypeId))//filter
        {

        }
    }
}