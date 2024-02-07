using BakeryADO.Configuration;
using BakeryADO.Interfaces.Repository;
using BakeryADO.Interfaces.Service;
using BakeryADO.Repository;
using BakeryADO.Service;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DBConnection>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductsService, ProductsService>(); 
builder.Services.AddScoped<IIngredientsRepository, IngredientsRepository>();
builder.Services.AddScoped<IIngredientsService, IngredientsService>();
builder.Services.AddScoped<IDistributorsRepository, DistributorsRepository>();
builder.Services.AddScoped<IDistributorsService, DistributorsService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<CacheService>();
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, config) =>
    {
        config.Host("amqp://guest:guest@localhost:5672");
    });
});
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
