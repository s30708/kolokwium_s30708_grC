using kolokwium_s30708_grC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();



var app = builder.Build();

app.UseAuthorization();
app.MapControllers();





app.Run();

