namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericREpository<>), (typeof(GenericREpository<>)));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.Configure<ApiBehaviorOptions>(options =>
          {
              options.InvalidModelStateResponseFactory = actionContext =>
              {
                  var errors = actionContext.ModelState
                  .Where(e => e.Value.Errors.Count > 0)
                  .SelectMany(x => x.Value.Errors)
                  .Select(x => x.ErrorMessage).ToArray();

                  var errResponse = new ApiValidationErrorResponse
                  {
                      Errors = errors
                  };

                  return new BadRequestObjectResult(errResponse);
              };
          });
            return services;
        }
    }
}