using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using StudyHelper.Application.Web.WebSiteWithApi.Data;
using StudyHelper.Library.Network.Client.Grpc;
using StudyHelper.Library.Network.Server;

namespace StudyHelper.Application.Web.WebSiteWithApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<TestClient>();

            builder.Services.AddGrpc().AddJsonTranscoding();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UsePathBase("/web");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapGrpcService<TestService>();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}