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

            builder.Services.AddTransient<IUrlRepository, UrlRepository>();
            builder.Services.AddTransient<IUrlService, UrlService>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddMvc();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseReact(config =>
            {
                config
                  .AddScript("~/js/urlListComponent.jsx")
                  .SetJsonSerializerSettings(new JsonSerializerSettings
                  {
                      StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                      ContractResolver = new CamelCasePropertyNamesContractResolver()
                  });
            });

            app.UseStaticFiles();

            app.UseRouting();

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