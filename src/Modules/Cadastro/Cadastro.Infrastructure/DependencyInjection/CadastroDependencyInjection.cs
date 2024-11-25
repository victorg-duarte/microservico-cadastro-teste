using Cadastro.Application.Abstractions;
using Cadastro.Application.UseCases.GravarCadastro;
using Cadastro.Application.UseCases.ObterCadastro;
using Cadastro.Domain.Abstractions;
using Cadastro.Infrastructure.Repositories.MongoDB;
using Cadastro.Infrastructure.Repositories.MongoDB.Contexts;
using Cadastro.Infrastructure.Repositories.Persistence;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Cadastro.Infrastructure.DependencyInjection;

public static class CadastroDependencyInjection
{
    public static void AddCadastro(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterContexts(services);
        RegisterServices(services, configuration);
        TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.Load("Cadastro.Application"));

    }

    private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICadastroRepository, CadastroRepository>();
        services.AddScoped<IUseCase<ObterCadastroRequest, ObterCadastroResponse>, ObterCadastroUseCase>();
        services.AddScoped<IUseCase<GravarCadastroRequest>, GravarCadastroUseCase>();
    }

    private static void RegisterContexts(this IServiceCollection services)
    {
        MongoDBRegistror.RegisterDocumentResolver();
        services.AddScoped<CadastroDbContext>();
    }
}
