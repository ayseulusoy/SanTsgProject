using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SanTsgProject.Application.Interfaces;
using SanTsgProject.Application.Services;
using SanTsgProject.Data;
using SanTsgProject.Data.Interfaces;
using SanTsgProject.Data.Repositories;
using SanTsgProject.Shared.SettingsModels;

namespace SanTsgProject.Web
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<CityDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                 AddCookie();

            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IGetArrivalAutocompleteService, GetArrivalAutocompleteService>();
            services.AddTransient<IPriceSearchService, PriceSearchService>();
            services.AddTransient<IGetProductInfoService, GetProductInfoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
