using Core.Interfaces;
using Infrastructure.Config;
using Infrastructure.Repositories.MongoDB;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

//add config
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDBDatabase"));

//add repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IPurchaseRepository, PurchaseRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

app.Run();