# ASP.NET Cheatsheet

### RESTful Naming
- Nouns are *good*, verbs are *bad*
- *Pragmatic REST sometimes*

### NSwag
- Package: NSwag.AspNetCore
- ```<GenerateDocumentationFile>true</GenerateDocumentationFile>```
- ```<NoWarn>$(NoWarn);1591</NoWarn>```
- XML-Comments
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

