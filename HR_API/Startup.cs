using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using MultipleAPIs.HR_DAL.Connection.Abstract;
using MultipleAPIs.HR_DAL.Connection.Concrete;
using MultipleAPIs.HR_DAL.Repositories.Abstract;
using MultipleAPIs.HR_DAL.Repositories.Concrete;
using MultipleAPIs.HR_DAL.UnitOfWorks.Abstract;
using MultipleAPIs.HR_DAL.UnitOfWorks.Concrete;
using MultipleAPIs.HR_BLL.Services.Abstract;
using MultipleAPIs.HR_BLL.Services.Concrete;
using MultipleAPIs.HR_BLL.Configurations;
using AutoMapper;

namespace HR_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();

            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IBarberRepository, BarberRepository>();
            services.AddTransient<IBranchRepository, BranchRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDayOffRepository, DayOffRepository>();
            services.AddTransient<IEmployeeDayOffRepository, EmployeeDayOffRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IEmployeeStatusRepository, EmployeeStatusRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IBarberService, BarberService>();
            services.AddTransient<IBranchService, BranchService>();
            services.AddTransient<IDayOffService, DayOffService>();
            services.AddTransient<IEmployeeDayOffService, EmployeeDayOffService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeStatusService, EmployeeStatusService>();

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HumanResoursesAPI",
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
                    "HumanResoursesAPI v1"));
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
