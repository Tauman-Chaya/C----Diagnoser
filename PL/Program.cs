
using BL;
using BL.API;
using BL.Model;
using BL.Services;
using DAL.Api;
using DAL.Models;
using DAL.Services;

namespace PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            // מסד הנתונים נמצא בתיקייה Data, ליד תיקיית PL – כך זה עובד בכל מחשב
            var dataDirectory = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "Data"));
            DbManager.ConnectionString = builder.Configuration
                .GetConnectionString("DefaultConnection")
                ?.Replace("|DataDirectory|", dataDirectory);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000")
                                      .AllowAnyHeader().AllowAnyMethod();
                                  });
            });


            builder.Services.AddScoped<BLManager>();
            //builder.Services.AddSingleton<IBLWorkshop, BLWorkshopServices>();
            builder.Services.AddScoped<AIOrchestratorService>();
            builder.Services.AddScoped<BLWorkshopAIService>();
            builder.Services.AddScoped<BLDiagnoserAIService>();

            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient<IAIService, AIService>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }

}
