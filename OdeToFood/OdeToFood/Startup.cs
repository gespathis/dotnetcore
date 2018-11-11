using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(options =>
            {
                _configuration.Bind("AzureAd", options);
            }
            )
            .AddCookie();

            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IGreeter greeter, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());

            //app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

            app.UseAuthentication();

            app.UseMvc(ConfigureRoutes);

            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        logger.LogInformation("Requset incoming");
            //        if(context.Request.Path.StartsWithSegments("/mym"))
            //        {
            //            logger.LogInformation("Requset handled");
            //            await context.Response.WriteAsync("Hit");
            //        }
            //        else
            //        {
            //            await next(context);
            //            logger.LogInformation("Requset outgoing");
            //        }
            //    };
            //});

            //app.UseWelcomePage(new WelcomePageOptions { Path = "/wp" });

            //app.Run(async (context) =>
            //{
            //    var greeting = greeter.GetMessageOfTheDay();
            //    context.Response.ContentType = "text/plain";
            //    await context.Response.WriteAsync(greeting);
            //});
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");

        }
    }
}
