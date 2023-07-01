//using Demo.BLL.Interfaces;
//using Demo.BLL.Repositories;
//using Demo.DAL.Contexts;
//using Demo.DAL.Entities;
//using Demo.PL.Mapper;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Demo.PL
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddControllersWithViews();

//            services.AddDbContext<MVCAPPDBContext>(options => 
//            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
//            );

//            //Service Add Singleton

//            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

//            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
//            services.AddScoped<IUnitOfWork, UnitRepository>();

//            services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));

//            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//                {
//                    options.LoginPath = new PathString("/Account/SignIn");
//                    options.AccessDeniedPath = new PathString("/Home/Error");
//                });

//            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
//            {
//                options.Password.RequireDigit = true;
//                options.Password.RequireLowercase = true;
//                options.Password.RequireNonAlphanumeric = true;
//                options.Password.RequireUppercase = true;
//                options.Password.RequiredLength = 6;
//                options.SignIn.RequireConfirmedAccount = false;
//            })
//            .AddEntityFrameworkStores<MVCAPPDBContext>()
//            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }
//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();
//            app.UseAuthentication();
//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(
//                    name: "default",
//                    pattern: "{controller=Account}/{action=SignIn}/{id?}");
//            });
//        }
//    }
//}
