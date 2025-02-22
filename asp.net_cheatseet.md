# ASP.NET Cheatsheet

## RESTful Naming
- Nouns are *good*, verbs are *bad*
- *Pragmatic REST sometimes e.g.* ```events/OrderByName```

## NSwag
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

## DTO Naming
- POST: ```Create{Entity}Model```
- GET: ```{Entity}Dto```
- PUT: ```Update{Entity}Model```
- DELETE: No model needed

## Data Annotaions for Validation
- Package: Microsoft.Extensions.Configuration / .Json
- ```[Required]```
- ```[Range(min, max)]```
- ```[MinLength(length)]``` *(string only)*
- ```[MaxLength(length)]``` *(string only)*
- ```[Compare(otherPropertyName)]```
- ```[RegularExpression(regex)]```

## Configuration
- *Order matters: last added source will overwrite other existing setting values*
- Add with e.g. ```builder.Configuration.AddJsonFile("customsettings.json", optional: true, reloadOnChange: true)```
- Inject with in constructor with ```IConfiguration``` interface
- Access values: e.g. ```configuration["MySetting"]```
- For nested values use ```Parent:Child```
- Add user secrets (if needed):  ```builder.Configuration.AddUserSecrets()```

## Options
```
builder.Services.Configure<MySettings>(Configuration.GetSection("MySettings"));
```
- Use ```IOptions<T>``` interface (```IOptionsSnapshot<T>``` if updates occur)

## AutoMapper
- Package: AutoMapper
- Add: ```builder.Services.AddAutoMapper(typeof(MyController))```
- Flattening by convention

### Create Mapper Profile
```
using AutoMapper;

// Assuming your profile is in the same assembly as Controller
// otherwise you have to register it manually
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
    	// Default mappings
        CreateMap<User, UserDto>();
        CreateMap<Product, ProductDto>();
		
		// Customize mappings - configure members
        CreateMap<Order, OrderDto>()
        	.ForMember(dest => dest.Customer, 
            		   opt => opt.MapFrom(src => src.Customer.Name));
    }
}
```

### Usage in Service
```
public class UserService : IUserService
{
    private readonly IMapper mapper;
    
    public UserService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public async Task<UserDto> GetUserByIdAsync(int userId)
    {
    	// load user with userID async
        var user = await  ...

        return mapper.Map<UserDto>(user);
    }
}
```

### Usage in Controller
```
private readonly IMapper mapper;
private readonly IUserSerive userService;

public UsersController(IUserSerive userService, IMapper mapper)
{
	this.userService = userService;
    this.mapper = mapper;
}

public async Task<IActionResult<IEnumerable<UserDto>>> GetUsers()
{
    var users = await userService.GetAllUsersAsync();
    var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

    return Ok(userDtos);
}
```

## Logging
- ASP.NET has integrated logging in ```appsettings.json```
- Serilog: Enhanced logging
- Packages: ```Serilog.AspNetCore``` and ```Serilog.Sinks.Console```

### Setup
```
// Add Serilog before builder.Build()
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => 
   loggerConfiguration
     .ReadFrom.Configuration(hostingContext.Configuration)
     .Enrich.FromLogContext()
     .WriteTo.Console());
```

### Inject
```
public class MyService : IMyService
{
    private readonly ILogger<MyService> logger;

    public MyService(ILogger<MyService> logger)
    {
        this.logger = logger;
    }

    public void DoWork()
    {
	// Your code here
        logger.LogInformation("Doing work");
       
        // Different Log Levels
        logger.LogWarning("This is a warning message");
	logger.LogError("This is an error message");   
    }
}
```

### Sample Usages
```
logger.Information("Processing payment for user {UserId} at {Time}", userId, DateTime.Now);
```

- ```@``` operator in Serilog's message templates to log an object as structured data

```
public class OrderController : ControllerBase
{
  private readonly ILogger<OrderController> logger;

  public OrderController(ILogger<OrderController> logger)
  {
    this.logger = logger;
  }

  public IActionResult Create(Order order)
  {
    // Log the order object as structured data
    logger.LogInformation("Creating order {@Order}", order);

    // Order processing logic...
  }
}
```

## HttpClient
- see [Slides](https://hauercodes.github.io/htlwrn_pos4c_23_24/)
