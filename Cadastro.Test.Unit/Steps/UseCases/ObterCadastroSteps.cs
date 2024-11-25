using Cadastro.Application.UseCases.ObterCadastro;
using Cadastro.Domain.Abstractions;
using Cadastro.Domain.Entities;
using Cadastro.Domain.ValueObjects;
using Moq;
using TechTalk.SpecFlow;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Cadastro.Test.Unit.Steps.UseCases
{
    [Binding]
    [ExcludeFromCodeCoverage]
    public class ObterCadastroSteps
    {
        private readonly ObterCadastroUseCase _useCase;
        private readonly Mock<ICadastroRepository> _cadastroRepositoryMock;

        private string _CPF;
        private ObterCadastroResponse? _cadastroEncontrado;

        public ObterCadastroSteps()
        {
            _cadastroRepositoryMock = new Mock<ICadastroRepository>();
            _useCase = new ObterCadastroUseCase(_cadastroRepositoryMock.Object);
        }

        private Cadastro.Domain.Entities.Cadastro CriarCadastro()
        {
            string id = Guid.NewGuid().ToString();
            string nome = "Felipe";
            string email = "felipe@gmail.com";
            string cpf = "17993850002";

            return new Cadastro.Domain.Entities.Cadastro(
                id,
                DateTime.UtcNow,
                new Email(email),
                new Cpf(cpf),
                nome);
        }

        [Given(@"CPF foi passado corretamente")]
        public void GivenCPFFoiPassadoCorretamente()
        {
            _CPF = CriarCadastro().CPF.Numero;
        }

        [When(@"usuario e encontrado")]
        public async Task WhenOUsuarioEEncontrado()
        {
            var cadastro = CriarCadastro();
            _cadastroRepositoryMock.Setup(x => x.ObterCadastroAsync(cadastro.CPF.Numero)).ReturnsAsync(cadastro);

            _cadastroEncontrado = await _useCase.ExecuteAsync(new ObterCadastroRequest() { CPF = _CPF });
        }

        [Then(@"os dados do cadastro sao retornados")]
        public void ThenOsDadosDoCadastroSaoRetornados()
        {
            _cadastroRepositoryMock.Verify(x => x.ObterCadastroAsync(_CPF), Times.Once);
            Assert.NotNull(_cadastroEncontrado); // Certifique-se de que o cadastro foi encontrado
        }

        [Given(@"CPF foi passado corretamente ou incorretamente")]
        public void GivenCPFFoiPassadoCorretamenteOuIncorretamente()
        {
            _CPF = "12345678900"; // Use um CPF inexistente para o cenário de cadastro não encontrado
        }

        [When(@"usuario nao e encontrado")]
        public async Task WhenOUsuarioNaoEEncontrado()
        {
            _cadastroRepositoryMock.Setup(x => x.ObterCadastroAsync(_CPF)).ReturnsAsync((Cadastro.Domain.Entities.Cadastro?)null);
            _cadastroEncontrado = await _useCase.ExecuteAsync(new ObterCadastroRequest() { CPF = _CPF });
        }

        [Then(@"nada e retornado")]
        public void ThenNadaERetornado()
        {
            _cadastroRepositoryMock.Verify(x => x.ObterCadastroAsync(_CPF), Times.Once);
            Assert.Null(_cadastroEncontrado);
        }
    }
}
