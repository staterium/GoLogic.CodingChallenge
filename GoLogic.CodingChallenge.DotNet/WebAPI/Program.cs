using Infrastructure.Config;
using WebAPI;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

//add config
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDBDatabase"));

//add repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPurchaseRepository, PurchaseRepository>();
builder.Services.RegisterModules();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//automapper
builder.Services.AddAutoMapper(c => c.AddMaps("WebAPI"));

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

app.MapEndpoints();
app.Run();