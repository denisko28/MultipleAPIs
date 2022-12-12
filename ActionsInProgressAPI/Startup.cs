using ActionsInProgressAPI.Configurations;
using ActionsInProgressAPI.Entities;
using ActionsInProgressAPI.Repositories.Abstract;
using ActionsInProgressAPI.Repositories.Concrete;
using AutoMapper;
using Common;
using Common.Events.AppointmentEvents;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Redis.OM;

namespace ActionsInProgressAPI
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
            // Redis Configuration
            var redisProvider = new RedisConnectionProvider(Configuration["CacheSettings:ConnectionString"]);
            services.AddSingleton(redisProvider);
            redisProvider.Connection.CreateIndex(typeof(UnfinishedAppointment));
            
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

                    cfg.Message<FinishedAppointmentEvent>(e =>
                        e.SetEntityName(EventBusConstants.TopicExchange)); // name of the primary exchange
                    cfg.Publish<FinishedAppointmentEvent>(e =>
                        e.ExchangeType = ExchangeType.Topic); // primary exchange type
                    cfg.Send<FinishedAppointmentEvent>(e => { e.UseRoutingKeyFormatter(_ => "appointment."); });
                });
            });

            services.AddTransient<IUnfinishedAppointmentRepository, UnfinishedAppointmentRepository>();

            var mapperConfig = new MapperConfiguration(mc =>
                mc.AddProfile(new AutoMapperProfile()));

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpContextAccessor();

            services.AddHttpContextAccessor();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ActionInProgress",
                    Version = "v1"
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
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}