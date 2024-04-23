using AnagramTool.Logic.Service;
using AnagramTool.Logic.Services.Implementations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IAnagramService, AnagramService>(); // DI
builder.Services.AddSingleton<IAnagramDictionaryService, AnagramDictionaryService>(); //DI
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add NSwag
builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info.Title = "Anagram Tool API";
        document.Info.Description = "Checks if words are anagrams.";
        document.Info.Version = "v1";
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // NSwag
    app.UseOpenApi();
    app.UseSwaggerUi();
}

// HAU: ℹ️ https redirection was not required and should be used in production
app.UseHttpsRedirection();

// HAU: ℹ️ authorization was not required
app.UseAuthorization();

app.MapControllers();

app.Run();
