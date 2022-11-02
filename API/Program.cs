
var builder = WebApplication.CreateBuilder(args);

//add services to container
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddControllers();

//database
builder.Services.AddDbContext<StoreContext>(x =>
    x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//IdentityDbContext
builder.Services.AddDbContext<AppIdentityDbContext>(x =>
    x.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

//Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration
    .GetConnectionString("Redis"), true);

    return ConnectionMultiplexer.Connect(configuration);
});

//Owen IServiceCollection
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumention();
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CoresPolicy", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("https://localhost:4200");
    });
});

//configer http request
var app = builder.Build();
app.UseMiddleware<ExceptionMiddelware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), "Content")
     ),
    RequestPath = "/content"
});

app.UseCors("CoresPolicy");

app.UseAuthentication();
app.UseAuthorization();

//Owen IApplicationBuilder
app.UseSwaggerDocumention();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

//seed data
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
try
{
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context, loggerFactory);

    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
    await identityContext.Database.MigrateAsync();
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occured during migration");

}

await app.RunAsync();



