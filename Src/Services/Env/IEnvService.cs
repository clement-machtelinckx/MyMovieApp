namespace MovieAppApi.Src.Services.Env;

public interface IEnvService
{
    string? Get(string key);
    IVariables Vars { get; }
}
