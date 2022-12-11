using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

namespace ClinicServiceV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(options => 
            { 
                options.Listen(IPAddress.Any, 5001, listenOptons => 
                { 
                    listenOptons.Protocols = HttpProtocols.Http2;
                })   ; 
            });

            // Add services to the container.
            builder.Services.AddGrpc();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();
            app.Run();

        }
    }
}