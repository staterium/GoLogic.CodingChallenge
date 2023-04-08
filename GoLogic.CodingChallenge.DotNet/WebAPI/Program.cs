using WebAPI;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

//add modules
builder.Services.RegisterModules();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add automapper
builder.Services.AddAutoMapper(c => c.AddMaps("WebAPI"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await ClearAndSeedData.InitializeAsync(services);
}

app.MapEndpoints();
app.Run();

public partial class Program
{
}