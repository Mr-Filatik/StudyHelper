using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using StudyHelper.Application.Web.WebSite.Data;
using StudyHelper.Library.Network.Server;

namespace StudyHelper.Application.Web.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor(); //-
            builder.Services.AddSingleton<WeatherForecastService>(); //-

            builder.Services.AddGrpc().AddJsonTranscoding();

            builder.Services.AddGrpcSwagger();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "gRPC transcoding",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseStaticFiles(); //для обслуживания статических файлов, таких как файлы *.css или *.js.
            app.UseBlazorFrameworkFiles(); //для обслуживания статических файлов Blazor, таких как blazor.server.jsили, blazor.webassembly.jsи сборок приложений, загруженных в WebAssembly.
            app.UseBlazorFrameworkFiles("/student");

            //для указания свох wwwroot для WebSite (совместно с href="../)
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"./../WebSite/wwwroot")),
                //RequestPath = new PathString("/")
            });

            app.UseRouting(); //при смене их мест, можно пользоваться редиректами в мапинге
            app.UsePathBase("/web");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api/swagger";
            });

            app.MapGrpcService<TestService>();
            app.MapGrpcService<FileService>();

            app.MapGet("/", () => Results.Redirect("/web"));
            app.MapGet("/api", () => Results.Redirect("main.html"));

            app.MapBlazorHub();
            app.MapFallbackToPage("/{param?}", "/_Host"); //для доступа к файлам с точкой

            app.MapRazorPages();
            app.MapFallbackToFile("/student/{*path:nonfile}", "/student/index.html");

            app.Run();

            //пока проблемы над тем как исключить один путь начинающийся с api для server blasor app
        }
    }
}

//Таким образом криво работает вариант когда есть транскодинг и блейзор сервер приложение, без блейзор сервера всё работает и возможно если понять как исключить все адреса начинающиеся на api из отслеживания роутинга blazor
/*
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpc().AddJsonTranscoding();

        builder.Services.AddGrpcSwagger();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "gRPC transcoding",
                Version = "v1"
            });
        });

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();

        var app = builder.Build();

        app.UsePathBase("/web");

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        //для указания свох wwwroot для WebSite
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(
    Path.Combine(Directory.GetCurrentDirectory(), @"./../WebSite/wwwroot")),
            //RequestPath = new PathString("/")
        });

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "My API V1");
        });

        app.MapGrpcService<TestService>();
        app.MapGrpcService<FileService>();

        app.MapGet("/api", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.MapRazorPages();
        //app.MapGet("/api", () => Results.Redirect("index.html"));
        app.MapBlazorHub();
        //app.MapFallbackToPage("/{param?}", "/_Host"); //для доступа к файлам с точкой
        app.MapFallbackToPage("/_Host");

        app.Run();

        //пока проблемы над тем как исключить один путь начинающийся с api для server blasor app
    }
}
*/