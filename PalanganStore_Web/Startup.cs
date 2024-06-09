using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Palangan.Core.Services;
using Palangan.Core.Services.Interfaces;
using Palangan.Core.Services.Interfaces.Order;
using Palangan.Core.Services.Interfaces.Product;
using Palangan.Core.Services.Interfaces.ProductGroup;
using Palangan.Core.Services.Interfaces.Role;
using Palangan.Core.Services.Interfaces.Slider;
using Palangan.DataLayer.Context;
using SendEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalanganStore_Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();


            #region Ioc
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IGroupService, GroupService>(); 
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<ISliderService, SliderService>();
            #endregion

            #region Identity
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme=CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme=CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme=CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath="/Login";
                options.LogoutPath="/Logout";
                options.ExpireTimeSpan=TimeSpan.FromDays(70);
            });
            #endregion

            #region ConnectionString
            services.AddDbContext<MyContext>(option =>
            {
               option.UseSqlServer("Data Source=.;Initial Catalog=PalanganStoreCore_DB;Integrated Security=True;MultipleActiveResultSets=True");
               //option.UseSqlServer("data source=.;initial catalog=palangan_storeDB;User ID=palangan_User;Password=@ramin@2020*;MultipleActiveResultSets=True;");
          
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}");
            });

        }
    }
}
