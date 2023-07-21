using Microsoft.Extensions.FileProviders;
using StudyHelper.Application.Web.WebSite.Data;
using StudyHelper.Library.Network.Server;

namespace StudyHelper.Application.Web.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc().AddJsonTranscoding();

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

            app.MapGrpcService<TestService>();

            //app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            //app.MapGet("/api", () => Results.Redirect("index.html"));
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}