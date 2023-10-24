using MiniLMS.Application;
using MiniLMS.Infrastructure;
using NuGet.Protocol;
using Serilog;
using Serilog.Core;
using Serilog.Core.Enrichers;
using Serilog.Formatting.Display;

namespace MiniLMS;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Logger log = new LoggerConfiguration()
            .WriteTo.Console()
            .Enrich.WithProcessName()
            .Enrich.WithThreadName()
            .Enrich.WithClientIp()
            .Enrich.WithMachineName()
            .WriteTo.File($@"C:\Users\User\Desktop\New folder (2)\log.json")
            
            .WriteTo.Telegram(botToken: "6763735144:AAHHe_CvIPx9KrJTC428MsheMaSUKNbYH8U", "591208356")
            //.ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
        
            log.Information("app starting");
            // CreateAsync services to the container.
            //builder.Services.AddResponseCaching();
            builder.Services.AddOutputCache();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddSerilog(log);
       
        builder.Services.AddStackExchangeRedisCache(setupAction =>
            {
                setupAction.Configuration = "127.0.0.1:6379";
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.DisplayRequestDuration());
            }

            app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();
            app.UseAuthorization();
            app.UseOutputCache();
            //app.UseResponseCaching();

            app.MapControllers();
            app.Run();
        
    }
}
