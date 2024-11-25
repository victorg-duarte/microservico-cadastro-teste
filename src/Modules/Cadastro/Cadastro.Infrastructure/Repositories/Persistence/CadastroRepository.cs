using Cadastro.Domain.Abstractions;
using Cadastro.Infrastructure.Base.Models;
using Cadastro.Infrastructure.Repositories.MongoDB.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Infrastructure.Repositories.Persistence;

public class CadastroRepository : ICadastroRepository
{
    private readonly CadastroDbContext _cadastroDbContext;

    public CadastroRepository(CadastroDbContext cadastroDbContext)
    {
        _cadastroDbContext = cadastroDbContext;
    }

    public async Task CadastrarAsync(Domain.Entities.Cadastro cadastro)
    {
        var cadastroModel = CadastroModel.MapFromDomain(cadastro);
        await _cadastroDbContext.Cadastro.InsertOneAsync(cadastroModel); ;
    }

    public async Task<Domain.Entities.Cadastro> ObterCadastroAsync(string cpf)
    {
        var cadastroEncontrado = await _cadastroDbContext.Cadastro.Find(x => x.CPF == cpf).FirstOrDefaultAsync();
        var cadastro = CadastroModel.MapToDomain(cadastroEncontrado);
        return cadastro;
    }

    public async Task<IList<Domain.Entities.Cadastro>> ObterTodosCadastrosAsync()
    {
        var cadastrosEncontrados = await _cadastroDbContext.Cadastro.Find(_ => true).ToListAsync();
        var cadastros = CadastroModel.MapToDomain(cadastrosEncontrados);
        return cadastros;
    }
}
