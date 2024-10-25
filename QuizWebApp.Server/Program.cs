
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizWebApp.Server.Interfaces;
using QuizWebApp.Server.Services;
namespace QuizWebApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("*")
                                            .AllowAnyMethod()
                                            .AllowAnyHeader();
                                  }
                                  );
            });
            // Load .env variables
            Env.Load();

            // Get the connection string from the environment variables
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add services to the container.
            builder.Services.AddTransient<IUser, UserService>();
            builder.Services.AddTransient<IQuiz, QuizService>();
            builder.Services.AddTransient<IQuestion, QuestionService>();
            builder.Services.AddTransient<ITake, TakeService>();
            builder.Services.AddTransient<ITakeAnswer, TakeAnswerService>();

            builder.Services.AddDbContext<QuizWebAppContext>(options =>
options.UseNpgsql(connectionString));

            var app = builder.Build();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseDefaultFiles();
            app.UseStaticFiles();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
