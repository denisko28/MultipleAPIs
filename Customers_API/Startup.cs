using AutoMapper;
using Common;
using Customers_BLL.Configurations;
using Customers_BLL.EventBusConsumers.AppointmentConsumers;
using Customers_BLL.EventBusConsumers.BranchConsumers;
using Customers_BLL.EventBusConsumers.ServiceConsumers;
using Customers_BLL.Filters;
using Customers_BLL.Grpc;
using Customers_BLL.Protos;
using Customers_BLL.Services.Abstract;
using Customers_BLL.Services.Concrete;
using Customers_DAL;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.Repositories.Concrete;
using Customers_DAL.UnitOfWork.Abstract;
using Customers_DAL.UnitOfWork.Concrete;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using AppointmentService = Customers_BLL.Services.Concrete.AppointmentService;

namespace Customers_API
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
            
            services.AddDbContext<BarbershopDbContext>();

            //Adding Authentication  
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["jwtAuthorities:identityServerUrl"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            // // Adding Authentication  
            // services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //     .AddIdentityServerAuthentication(options =>
            //     {
            //         options.Authority = "https://localhost:7065";
            //         options.ApiName = "customerAPI";
            //         options.LegacyAudienceValidation = false;
            //     });
            
            services.AddGrpcClient<Employees.EmployeesClient>((_, options) =>
            {
                options.Address = new Uri(Configuration["urls:grpcEmployees"]);
            });

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<BranchInsertedConsumer>();
                config.AddConsumer<BranchUpdatedConsumer>();
                config.AddConsumer<BranchDeletedConsumer>();
                
                config.AddConsumer<ServiceInsertedConsumer>();
                config.AddConsumer<ServiceUpdatedConsumer>();
                config.AddConsumer<ServiceDeletedConsumer>();
                
                config.AddConsumer<FinishedAppointmentConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Password(Configuration["EventBusSettings:Password"]);
                        h.Username(Configuration["EventBusSettings:Username"]);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.BranchForCustomersQueue, c =>
                    {
                        // turns off default fanout settings
                        c.ConfigureConsumeTopology = false;

                        // a replicated queue to provide high availability and data safety. available in RMQ 3.8+
                        c.SetQuorumQueue();

                        c.ConfigureConsumer<BranchInsertedConsumer>(ctx);
                        c.ConfigureConsumer<BranchUpdatedConsumer>(ctx);
                        c.ConfigureConsumer<BranchDeletedConsumer>(ctx);
                        c.Bind(EventBusConstants.TopicExchange, e =>
                        {
                            e.RoutingKey = "branch.*";
                            e.ExchangeType = ExchangeType.Topic;
                        });
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.ServiceForCustomersQueue, c =>
                    {
                        // turns off default fanout settings
                        c.ConfigureConsumeTopology = false;

                        // a replicated queue to provide high availability and data safety. available in RMQ 3.8+
                        c.SetQuorumQueue();

                        c.ConfigureConsumer<ServiceInsertedConsumer>(ctx);
                        c.ConfigureConsumer<ServiceUpdatedConsumer>(ctx);
                        c.ConfigureConsumer<ServiceDeletedConsumer>(ctx);
                        c.Bind(EventBusConstants.TopicExchange, e =>
                        {
                            e.RoutingKey = "service.*";
                            e.ExchangeType = ExchangeType.Topic;
                        });
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.AppointmentForCustomersQueue, c =>
                    {
                        // turns off default fanout settings
                        c.ConfigureConsumeTopology = false;

                        // a replicated queue to provide high availability and data safety. available in RMQ 3.8+
                        c.SetQuorumQueue();

                        c.ConfigureConsumer<FinishedAppointmentConsumer>(ctx);
                        c.Bind(EventBusConstants.TopicExchange, e =>
                        {
                            e.RoutingKey = "appointment.*";
                            e.ExchangeType = ExchangeType.Topic;
                        });
                    });
                });
            });

            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IAppointmentServiceRepository, AppointmentServiceRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IPossibleTimeRepository, PossibleTimeRepository>();
            services.AddTransient<IBranchRepository, BranchRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<ICustomerService, CustomerService>();

            var mapperConfig = new MapperConfiguration(mc =>
                mc.AddProfile(new AutoMapperProfile()));

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpContextAccessor();

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
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // app.UseCors(options => options
            //     .WithOrigins("http://localhost:5010")
            //     .AllowAnyHeader()
            //     .AllowAnyMethod()
            //     .AllowCredentials());

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcReflectionService();
                endpoints.MapGrpcService<PossibleTimeService>();
                endpoints.MapGrpcService<CustomersService>();
                endpoints.MapGrpcService<AppointmentsService>();
                endpoints.MapControllers();
            });
        }
    }
}