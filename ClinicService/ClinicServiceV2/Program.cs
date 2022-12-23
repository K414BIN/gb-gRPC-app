using ClinicService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;

namespace ClinicServiceV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Kestrel

            builder.WebHost.ConfigureKestrel(options => 
            { 
                options.Listen(IPAddress.Any, 5101, listenOptons => 
                { 
                    listenOptons.Protocols = HttpProtocols.Http1;
                });
                options.Listen(IPAddress.Any, 5100, listenOptons =>
                {
                    listenOptons.Protocols = HttpProtocols.Http2;
                });

            });

            #endregion

            #region Configure EF DBContext Service
            builder.Services.AddDbContext<ClinicServiceDbContext>(options  => 
            {
                options.UseSqlServer(builder.Configuration["Settings:DatabaseOptions:ConnectionString"]);
            });
            #endregion

            #region Configure gRPC
            
            builder.Services.AddGrpc()
                    .AddJsonTranscoding();

            #endregion

            #region Configure Swagger
            builder.Services.AddGrpcSwagger();
            builder.Services.AddSwaggerGen( props =>
                {
                    props.SwaggerDoc("v1", new OpenApiInfo {Title = "ClinicService", Version = "v1" });
                    var filePath = Path.Combine(System.AppContext.BaseDirectory, "ClinicService.xml");
                    props.IncludeGrpcXmlComments(filePath);
                    props.IncludeGrpcXmlComments(filePath, includeControllerXmlComments : true);
                });
           
            #endregion
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            { 
                app.UseSwagger();
                app.UseSwaggerUI (props => { props.SwaggerEndpoint("/swagger/v1/swagger.json","v1"); });

            }

            app.UseRouting();
            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
            app.MapGrpcService<ClinicServiceV2.Services.ClinicService>().EnableGrpcWeb();
            app.MapGet("/",
                () => " Communication with gRPC endpoints must be made through a gRPC client."
                );
            //app.UseAuthorization();
            app.Run();

        }
    }
}