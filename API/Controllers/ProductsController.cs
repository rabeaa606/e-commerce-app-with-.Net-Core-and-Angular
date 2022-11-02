using Product = Core.Entites.Product;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericREpository<Product> _productRepo;
    private readonly IGenericREpository<ProductBrand> _productBrandRepo;
    private readonly IGenericREpository<ProductType> _productTypeRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericREpository<Product> productRepo,
     IGenericREpository<ProductBrand> productBrandRepo,
    IGenericREpository<ProductType> productTypeRepo,
    IMapper mapper)
    {
        _productRepo = productRepo;
        _productBrandRepo = productBrandRepo;
        _productTypeRepo = productTypeRepo;
        _mapper = mapper;
    }
    [Cached(600)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
                  [FromQuery] ProductSpecParams productParams)

    {
        var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
        var countSpec = new ProductWithFiltersForCountSpecification(productParams);

        var totalItems = await _productRepo.CountAsync(countSpec);
        var products = await _productRepo.ListAsync(spec);

        var data = _mapper
        .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

        return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }
    [Cached(600)]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _productRepo.GetEntityWithSpec(spec);

        if (product == null) return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }
    [Cached(600)]
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _productBrandRepo.ListAllAsync());
    }
    [Cached(600)]
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await _productTypeRepo.ListAllAsync());
    }
}
