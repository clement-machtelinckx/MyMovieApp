using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace MovieAppApi.Src.Services.Env;

public class EnvService : IEnvService
{
    public IVariables Vars { get; }

    public EnvService()
    {
        DotNetEnv.Env.Load();

        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        Vars = new Variables(config);
    }

    public string? Get(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }

    private class Variables : IVariables
    {
        [Required(AllowEmptyStrings = false)]
        public string TmdbApiKey { get; }

        private readonly IConfiguration _configuration;

        public Variables(IConfiguration configuration)
        {
            _configuration = configuration;
            TmdbApiKey = GetMandatoryVariable("TMDB_API_KEY");
            Validate();
        }

        private void Validate()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this);

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                foreach (var error in validationResults)
                {
                    Console.WriteLine($"Environment validation error: {error.ErrorMessage}");
                }
                throw new InvalidOperationException("Environment validation failed.");
            }
        }

        private string GetMandatoryVariable(string key)
        {
            return _configuration[key]
                ?? throw new ArgumentNullException($"Environment variable {key} is null or missing.");
        }
    }
}
