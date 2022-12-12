using AutoMapper;
using GrpcAggregator.Configurations;
using GrpcAggregator.Services.Abstract;
using GrpcAggregator.Services.Concrete;
using Microsoft.OpenApi.Models;

namespace GrpcAggregator;

public class Startup
{
    public IWebHostEnvironment Environment { get; }
    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment environment, IConfiguration configuration)
    {
        Environment = environment;
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
            mc.AddProfile(new AutoMapperProfile()));

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddAuthorization();
        services.AddAuthentication();

        services.Configure<UrlsConfig>(Configuration.GetSection("urls"));
        services.AddGrpcServices();
        
        services.AddScoped<IAvailableTimeService, AvailableTimeService>();
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IBarbersService, BarbersService>();

        services.AddHttpContextAccessor();
        services.AddRazorPages();

        services.AddControllers();
        services.AddSwaggerGen(c => c.SwaggerDoc(
            "v1", 
            new OpenApiInfo
            {
                Title = "Grpc Aggregator API",
                Version = "v1"
            })
        );
    }

    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}