using Microsoft.EntityFrameworkCore;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;
using TestTaskForInforce.Data;
using TestTaskForInforce.Repositories.Abstractions;
using TestTaskForInforce.Repositories;
using TestTaskForInforce.Services.Abstractions;
using TestTaskForInforce.Services;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using JavaScriptEngineSwitcher.ChakraCore;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace TestTaskForInforce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName)
                .AddChakraCore();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddReact();
            builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName).AddV8();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUrlRepository, UrlRepository>();
            builder.Services.AddTransient<IUrlService, UrlService>();

            var sqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(sqlConnection));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Auth/Login");
                    options.AccessDeniedPath = new PathString("/Auth/Login");
                });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseReact(config =>
            {
                config
                  .AddScript("~/js/urlListComponent.jsx")
                  .AddScript("~/js/urlInfoComponent.jsx")
                  .AddScript("~/js/loginComponent.jsx")
                  .SetJsonSerializerSettings(new JsonSerializerSettings
                  {
                      StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                      ContractResolver = new CamelCasePropertyNamesContractResolver()
                  });
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Url}/{action=ShortUrlsTable}/{id?}");
            });

            app.Run();
        }
    }
}