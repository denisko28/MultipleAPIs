using Microsoft.OpenApi.Models;
using AutoMapper;
using Common;
using Common.Events.BranchEvents;
using Dapper;
using FluentValidation.AspNetCore;
using HR_BLL.Configurations;
using HR_BLL.Filters;
using HR_BLL.Grpc;
using HR_BLL.Services.Abstract;
using HR_BLL.Services.Concrete;
using HR_DAL.Repositories.Abstract;
using HR_DAL.Repositories.Concrete;
using HR_DAL.TypeHandlers;
using HR_DAL.UnitOfWork.Abstract;
using HR_DAL.UnitOfWork.Concrete;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using ConnectionFactory = HR_DAL.Connection.Concrete.ConnectionFactory;
using IConnectionFactory = HR_DAL.Connection.Abstract.IConnectionFactory;

namespace HR_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddGrpcReflection();

            services.AddSingleton<IConnectionFactory, ConnectionFactory>();

            // Adding Authentication  
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["jwtAuthorities:identityServerUrl"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Password(Configuration["EventBusSettings:Password"]);
                        h.Username(Configuration["EventBusSettings:Username"]);
                    });

                    cfg.Message<BranchInsertedEvent>(e =>
                        e.SetEntityName(EventBusConstants.TopicExchange)); // name of the primary exchange
                    cfg.Publish<BranchInsertedEvent>(e => e.ExchangeType = ExchangeType.Topic); // primary exchange type
                    cfg.Send<BranchInsertedEvent>(e => { e.UseRoutingKeyFormatter(_ => "branch."); });

                    cfg.Message<BranchUpdatedEvent>(e =>
                        e.SetEntityName(EventBusConstants.TopicExchange)); // name of the primary exchange
                    cfg.Publish<BranchUpdatedEvent>(e => e.ExchangeType = ExchangeType.Topic); // primary exchange type
                    cfg.Send<BranchUpdatedEvent>(e => { e.UseRoutingKeyFormatter(_ => "branch."); });

                    cfg.Message<BranchDeletedEvent>(e =>
                        e.SetEntityName(EventBusConstants.TopicExchange)); // name of the primary exchange
                    cfg.Publish<BranchDeletedEvent>(e => e.ExchangeType = ExchangeType.Topic); // primary exchange type
                    cfg.Send<BranchDeletedEvent>(e => { e.UseRoutingKeyFormatter(_ => "branch."); });
                });
            });

            services.AddTransient<IBarberRepository, BarberRepository>();
            services.AddTransient<IBranchRepository, BranchRepository>();
            services.AddTransient<IDayOffRepository, DayOffRepository>();
            services.AddTransient<IEmployeeDayOffRepository, EmployeeDayOffRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IBarberService, BarberService>();
            services.AddTransient<IBranchService, BranchService>();
            services.AddTransient<IDayOffService, DayOffService>();
            services.AddTransient<IEmployeeService, EmployeeService>();

            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            SqlMapper.AddTypeHandler(new DateTimeHandler());

            services.AddRazorPages();

            services
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.Filters.Add<ValidationFilter>();
                })
                .AddFluentValidation(options =>
                    options.RegisterValidatorsFromAssemblyContaining<ValidationFilter>());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HumanResourcesAPI",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "HumanResourcesAPI v1"));
            }

            app.UseRouting();

            app.UseCors(options => options
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcReflectionService();
                endpoints.MapGrpcService<BarbersService>();
                endpoints.MapGrpcService<EmployeesService>();
                endpoints.MapControllers();
            });
        }
    }
}