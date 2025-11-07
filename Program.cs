using DotNetEnv;  
using MovieAppApi.Src.Services.Env;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MovieAppApi;

public class Program
{
    public static void Main(string[] args)
    {
        // Création du builder
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IEnvService>(new EnvService());

        // Ajouter les services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpClient(); // important pour ton controller

        var app = builder.Build();

        // Configurer le pipeline HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
