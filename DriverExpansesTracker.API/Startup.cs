using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Repository.Repositories;
using DriverExpansesTracker.Services.Services;
using DriveTracker.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace DriverExpansesTracker.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
             {
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 1;
                 options.Password.RequiredUniqueChars = 0;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
             })
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Events.OnRedirectToLogin = context =>
                {
                    if (context.Response.StatusCode == StatusCodes.Status200OK)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                    return Task.CompletedTask;
                };
                config.Events.OnRedirectToAccessDenied = context =>
                {
                    if (context.Response.StatusCode == StatusCodes.Status200OK)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    }
                    return Task.CompletedTask;
                };

            });

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "DriverExpansesTracker", Version = "v1"
                });
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository.Repositories.Repository<>));
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IJourneyService, JourneyService>();
            services.AddScoped<IPassengerRouteService, PassengerRouteService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<ValidateIfUserExists>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.User, DriverExpansesTracker.Services.Models.User.UserDto>();
                cfg.CreateMap<DriverExpansesTracker.Services.Models.User.UserForCreationDto, DriverExpansesTracker.Repository.Entities.User>()
                   .ForMember(dest=>dest.PasswordHash,opt=>opt.Ignore());

                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.Car, DriverExpansesTracker.Services.Models.Car.CarDto>();
                cfg.CreateMap<DriverExpansesTracker.Services.Models.Car.CarForCreationDto, DriverExpansesTracker.Repository.Entities.Car>();

                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.Journey, DriverExpansesTracker.Services.Models.Journey.JourneyDto>();
                cfg.CreateMap<DriverExpansesTracker.Services.Models.Journey.JourneyForCreationDto, DriverExpansesTracker.Repository.Entities.Journey>()
                   .ForMember(dest=>dest.PassengerRoutes,opt=>opt.MapFrom(src=>src.Routes));

                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.PassengerRoute, DriverExpansesTracker.Services.Models.PassengerRoute.PassengerRouteDto>();
                cfg.CreateMap<DriverExpansesTracker.Services.Models.PassengerRoute.PassengerRouteForCreationDto, DriverExpansesTracker.Repository.Entities.PassengerRoute>();

                cfg.CreateMap<DriverExpansesTracker.Repository.Entities.Payment, DriverExpansesTracker.Services.Models.Payment.PaymentDto>();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DriverExpansesTracker");
            });

            app.UseAuthentication();

            app.UseMvc();

        }
    }
}
