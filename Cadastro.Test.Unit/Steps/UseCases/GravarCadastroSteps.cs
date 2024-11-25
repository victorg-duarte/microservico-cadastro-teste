using Cadastro.Application.UseCases.GravarCadastro;
using Cadastro.Application.UseCases.ObterCadastro;
using Cadastro.Domain.Abstractions;
using Cadastro.Domain.ValueObjects;
using Common.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cadastro.Test.Unit.Steps.UseCases;

[Binding]
[ExcludeFromCodeCoverage]
public class GravarCadastroSteps
{
    private readonly GravarCadastroUseCase _useCase;
    private readonly Mock<ICadastroRepository> _cadastroRepositoryMock;

    private GravarCadastroRequest? _request;

    public GravarCadastroSteps()
    {
        _cadastroRepositoryMock = new();
        _useCase = new(_cadastroRepositoryMock.Object);
    }


    [Given(@"dados passados")]
    public void GivenDadosPassados()
    {
        _request = new GravarCadastroRequest()
        {
            CPF = "17993850002",
            Email = "felipe@gamil.com",
            Nome = "Felipe",
        };

        _cadastroRepositoryMock.Setup(x => x.CadastrarAsync(It.IsAny<Cadastro.Domain.Entities.Cadastro>()));
    }

    [When(@"dados passados estao de acordo")]
    public async Task WhenDadosPassadosEstaoDeAcordo()
    {
        await _useCase.ExecuteAsync(_request!);
    }

    [Then(@"cadastro realizado")]
    public void ThenCadastroRealizado()
    {
        _cadastroRepositoryMock.Verify(x => x.CadastrarAsync(It.IsAny<Cadastro.Domain.Entities.Cadastro>()), Times.Once);
    }


    private Exception _excecaoCapturada;

    [Given(@"dados passados com CPF ou outro dado invalido")]
    public void GivenDadosPassadosComCPFOuOutroDadoInvalido()
    {
        _request = new GravarCadastroRequest()
        {
            CPF = "",
            Email = "felipe@gmail.com",
            Nome = "Felipe",
        };
    }

    [When(@"cadastro tentado")]
    public async Task WhenCadastroETentado()
    {
        try
        {
            _cadastroRepositoryMock.Setup(x => x.CadastrarAsync(It.IsAny<Cadastro.Domain.Entities.Cadastro>()));
            await _useCase.ExecuteAsync(_request!);
        }
        catch (DomainNotificationException ex)
        {
            _excecaoCapturada = ex;
        }
    }

    [Then(@"uma excecao de validacao e lancada")]
    public void ThenUmaExcecaoDeValidacaoELancada()
    {
        _excecaoCapturada.Should().NotBeNull();
        _excecaoCapturada.Should().BeOfType<DomainNotificationException>();
    }


}
