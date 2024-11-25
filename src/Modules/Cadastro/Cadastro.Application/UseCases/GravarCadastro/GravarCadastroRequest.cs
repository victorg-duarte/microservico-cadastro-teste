namespace Cadastro.Application.UseCases.GravarCadastro;

public class GravarCadastroRequest
{
    public string Email { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}
