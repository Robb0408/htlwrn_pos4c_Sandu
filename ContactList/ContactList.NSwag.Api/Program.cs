using ContactList.Logic.Models;
using ContactList.Logic.Services;
using Microsoft.OpenApi.Models;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IContactService, ContactService>(); // Dependency Injection
builder.Services.AddSingleton<IList<Person>, List<Person>>(); // Dependency Injection
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(options => {
    options.PostProcess = document =>
        {
            document.Info = new NSwag.OpenApiInfo
            {
                Version = "1.0.0",
                Title = "Address Book",
                Description = "HTL Programming Homework",
            };
        };
    }); // NSwag
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
