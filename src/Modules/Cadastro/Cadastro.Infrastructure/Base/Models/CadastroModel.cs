using Common.Exceptions;

namespace Cadastro.Infrastructure.Base.Models;

public class CadastroModel : BaseModel
{
    public CadastroModel(string id) : base(id)
    {
    }

    public string Email { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime DataDeCriacao { get; set; }


    internal static CadastroModel MapFromDomain(Domain.Entities.Cadastro cadastro)
    {
        if (cadastro is null) return null!;

        return new CadastroModel(cadastro.Id)
        {
            CPF = cadastro.CPF.Numero,
            DataDeCriacao = cadastro.DataCriacao,
            Email = cadastro.Email.Endereco,
            Nome = cadastro.Nome
        };
    }

    internal static Domain.Entities.Cadastro MapToDomain(CadastroModel cadastroModel)
    {
        if (cadastroModel is null) return null!;

        return new Domain.Entities.Cadastro(cadastroModel.Id,
                                            cadastroModel.DataDeCriacao,
                                            new Domain.ValueObjects.Email(cadastroModel.Email),
                                            new Domain.ValueObjects.Cpf(cadastroModel.CPF),
                                            cadastroModel.Nome);
    }

    internal static IList<Domain.Entities.Cadastro> MapToDomain(IEnumerable<CadastroModel> cadastroModel)
    {
        var mapList = new List<Domain.Entities.Cadastro>();

        if (cadastroModel is null || !cadastroModel.Any()) return mapList;

        foreach (var model in cadastroModel)
        {
            try
            {
                mapList.Add(MapToDomain(model));
            }
            catch(DomainNotificationException ex)
            {
                throw new DomainNotificationException($"Erro inesperado. {ex}");
            }
        }

        return mapList;
    }
}
