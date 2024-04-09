using CurrencyConverter.Logic;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info.Title = "Currency Converter API";
        document.Info.Version = "v1";
    };
});
// Add services to the container.
builder.Services.AddHttpClient("ExchangeRates", client =>
{
    client.BaseAddress = new Uri("https://sthtlwrn.blob.core.windows.net/share/");
});
builder.Services.AddTransient<ICurrencyConverterService, CurrencyConverterService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
