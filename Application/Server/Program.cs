using StudyHelper.Application.Server.Services;

namespace StudyHelper.Application.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {

            #region Builder

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc().AddJsonTranscoding();

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<TestService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}