using System;
using System.Text;
using AutoMapper;
using Common;
using Common.Events.ServiceEvents;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using Services_Application.Configurations;
using Services_Application.EventBusConsumers.BranchConsumers;
using Services_Application.Filters;
using Services_Application.Queries.Services.GetByIdService;
using Services_Infrastructure;

namespace Services_API
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
            services.AddSingleton(_ =>
                new MongoDbContext(Configuration.GetConnectionString("MongoDBConnection")));

            // Adding Authentication  
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:7065";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddMediatR(typeof(GetByIdServiceQuery));

            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<BranchInsertedConsumer>();
                config.AddConsumer<BranchUpdatedConsumer>();
                config.AddConsumer<BranchDeletedConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Password(Configuration["EventBusSettings:Password"]);
                        h.Username(Configuration["EventBusSettings:Username"]);
                    });

                    cfg.Message<ServiceInsertedEvent>(e =>
                        e.SetEntityName(EventBusConstants.TopicExchange)); // name of the primary exchange
                    cfg.Publish<ServiceInsertedEvent>(e =>
                        e.ExchangeType = ExchangeType.Topic); // primary exchange type
                    cfg.Send<ServiceInsertedEvent>(e => { e.UseRoutingKeyFormatter(_ => "service."); });

                    cfg.Message<ServiceUpdatedEvent>(e =>
                        e.SetEntityName(EventBusConstants.TopicExchange)); // name of the primary exchange
                    cfg.Publish<ServiceUpdatedEvent>(e => e.ExchangeType = ExchangeType.Topic); // primary exchange type
                    cfg.Send<ServiceUpdatedEvent>(e => { e.UseRoutingKeyFormatter(_ => "service."); });

                    cfg.Message<ServiceDeletedEvent>(e =>
                        e.SetEntityName(EventBusConstants.TopicExchange)); // name of the primary exchange
                    cfg.Publish<ServiceDeletedEvent>(e => e.ExchangeType = ExchangeType.Topic); // primary exchange type
                    cfg.Send<ServiceDeletedEvent>(e => { e.UseRoutingKeyFormatter(_ => "service."); });

                    cfg.ReceiveEndpoint(EventBusConstants.BranchForServicesQueue, c =>
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
                });
            });

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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}