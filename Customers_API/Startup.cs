using AutoMapper;
using Customers_BLL.Configurations;
using Customers_BLL.Services.Abstract;
using Customers_BLL.Services.Concrete;
using Customers_DAL;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.Repositories.Concrete;
using Customers_DAL.UnitOfWork.Abstract;
using Customers_DAL.UnitOfWork.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddDbContext<BarbershopDbContext>();

            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IBarberRepository, BarberRepository>();
            services.AddTransient<IPossibleTimeRepository, PossibleTimeRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<ITimePickerService, TimePickerService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddRazorPages();

            services.AddMvc(options =>
                    {
                        options.EnableEndpointRouting = false;
                    });

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

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
