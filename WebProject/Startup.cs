
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;
using WebProject.Interface;
using WebProject.Models;
using WebProject.Repository;
using WebProject.Service.CartServ;

namespace WebProject
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region services.AddAuthentication (Дефолтные Куки)

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/FormLog");
                    options.AccessDeniedPath = new PathString("/Account/FormLog");
                });

            #endregion

            services.AddTransient<IAllMobile, MobileRepository>();
            services.AddTransient<CartService>();
            services.AddTransient<ItemsInCart>();


            #region services.AddDbContext (Регистрация использования базы данных MSSQL)

            string connection = Configuration.GetConnectionString("MobileDb");
            services.AddDbContext<MobileContext>(options => options.UseSqlServer(connection));

            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMemoryCache();
            services.AddSession();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region (Проверка на состояние проекта)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            #endregion

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            #region app.UseStaticFiles (Фото)

            app.UseStaticFiles (new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("/wwwroot")
            });

            #endregion

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region app.UseEndpoints (Маршрут)

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #endregion
        }
    }
}
