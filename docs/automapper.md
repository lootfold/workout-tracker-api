# Configure automapper in ASP.NET Core 3.1

- Add AutoMapper nuget package

  `dotnet add package AutoMapper`

- Add package for dependency injection

  `dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection`

- Create a mapping profile
  ```
  public class MappingProfile : Profile
  {
      public MappingProfile()
      {
          CreateMap<Status, StatusDto>();
      }
  }
  ```
- Register mapping profile in `StartUp.cs`

  `services.AddAutoMapper(typeof(MappingProfile));`

- Inject Imapper in the ctor

  ```
  private readonly IMapper mapper;

  public StatusController(IMapper mapper)
  {
      this.mapper = mapper;
  }
  ```
