# ASP.NET Cheatsheet

### RESTful Naming
- Nouns are *good*, verbs are *bad*
- *Pragmatic REST sometimes e.g.* ```events/OrderByName```

### NSwag
- Package: NSwag.AspNetCore
- Add ```<GenerateDocumentationFile>true</GenerateDocumentationFile>``` to ```<PropertyGroup>```
- Add ```<NoWarn>$(NoWarn);1591</NoWarn>``` to ```<PropertyGroup>```
- Add XML-Comments
- Change ```app.UseSwagger()``` to ```app.UseOpenApi()```
- Change ```app.UseSwaggerUI()``` to ```app.UseSwaggerUi()```
```
builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new OpenApiInfo
        {
            Version = "v1",
            Title = "ApiTest",
            Description = "A simple example ASP.NET Core Web API",
            TermsOfService = "None",
            Contact = new OpenApiContact
            {
                Name = "Max Mustermann",
                Email = "max.mustermann@gmail.com",
                Url = "https://twitter.com/elonmusk"
            },
        };
    };
});
```

### DTO Naming
- POST: ```Create{Entity}Model```
- GET: ```{Entity}Dto```
- PUT: ```Update{Entity}Model```
- DELETE: No model needed

### Data Annotaions for Validation
- Package: Microsoft.Extensions.Configuration / .Json
- ```[Required]```
- ```[Range(min, max)]```
- ```[MinLength(length)]``` *(string only)*
- ```[MaxLength(length)]``` *(string only)*
- ```[Compare(otherPropertyName)]```
- ```[RegularExpression(regex)]```

### Configuration
- *Order matters: last added source will overwrite other existing setting values*
- Add with e.g. ```builder.Configuration.AddJsonFile("customsettings.json", optional: true, reloadOnChange: true)```
- Inject with in constructor with ```IConfiguration``` interface
- Access values: e.g. ```configuration["MySetting"]```
- For nested values use ```Parent:Child```
- Add user secrets (if needed):  ```builder.Configuration.AddUserSecrets()```
