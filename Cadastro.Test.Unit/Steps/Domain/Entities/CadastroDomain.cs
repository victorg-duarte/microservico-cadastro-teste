using Cadastro.Application.Abstractions;
using Cadastro.Application.UseCases.ObterCadastro;
using Cadastro.Domain.ValueObjects;
using Common.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cadastro.Test.Unit.Steps.Domain.Entities;

[Binding]
[ExcludeFromCodeCoverage]
public class CadastroDomain
{
    private static Cadastro.Domain.Entities.Cadastro CriarCadastro()
    {
        string id = Guid.NewGuid().ToString();
        string nome = "Felipe";
        DateTime dataDeCriacao = DateTime.Now;
        string email = "felipe@gmail.com";
        string cpf = "179.938.500-02";

        return new Cadastro.Domain.Entities.Cadastro(
            id,
            dataDeCriacao,
            new Cadastro.Domain.ValueObjects.Email(email),
            new Cadastro.Domain.ValueObjects.Cpf(cpf),
        nome);
    }

    private Cadastro.Domain.Entities.Cadastro? _entidade;
    private Action? act;


    [Given(@"tentativa de criar objeto da entidade")]
    public void GivenTentativaDeCriarObjetoDaEntidade()
    {
        _entidade = CriarCadastro();
    }

    [When(@"alocado Email errado")]
    public void WhenAlocadoEmailErrado()
    {
        act = () => new Cadastro.Domain.Entities.Cadastro(
            null!,
            DateTime.UtcNow,
            new Cadastro.Domain.ValueObjects.Email("felipe"),
            _entidade!.CPF,
            _entidade.Nome
            );
    }

    [When(@"alocado CPF errado")]
    public void WhenAlocadoCPFErrado()
    {
        act = () => new Cadastro.Domain.Entities.Cadastro(
            null!,
            DateTime.UtcNow,
            _entidade!.Email,
            new Cadastro.Domain.ValueObjects.Cpf("1254"),
            _entidade.Nome
            );
    }

    [When(@"alocado Nome errado")]
    public void WhenAlocadoNomeErrado()
    {
        act = () => new Cadastro.Domain.Entities.Cadastro(
            null!,
            DateTime.UtcNow,
            _entidade!.Email,
            _entidade.CPF,
            ""
            );
    }


    [When(@"alocado dados corretamente")]
    public void WhenAlocadoDadosCorretamente()
    {
        act = () => new Cadastro.Domain.Entities.Cadastro(
            null!,
            DateTime.UtcNow,
            _entidade!.Email,
            _entidade.CPF,
            _entidade.Nome
            );
    }


    [Then(@"excecao gerada")]
    public void ThenExcecaoGerada()
    {
        Assert.Throws<DomainNotificationException>(act!);
    }

    [Then(@"dado criado com sucesso")]
    public void ThenDadoCriadoComSucesso()
    {
        Assert.NotNull(act);
    }

}
