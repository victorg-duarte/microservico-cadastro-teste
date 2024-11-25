using Cadastro.Application.UseCases.ObterCadastro;
using Mapster;
using System.Diagnostics.CodeAnalysis;

namespace Cadastro.Application.Mapping;

[ExcludeFromCodeCoverage]
public class Map : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Entities.Cadastro, ObterCadastroResponse>()
            .Map(dest => dest.CPF, src => src.CPF.Numero)
            .Map(dest => dest.Email, src => src.Email.Endereco);
    }
}
