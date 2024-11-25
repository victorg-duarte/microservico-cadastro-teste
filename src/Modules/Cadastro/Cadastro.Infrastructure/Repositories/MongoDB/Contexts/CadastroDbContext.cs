using Cadastro.Infrastructure.Base.Models;
using Common.Exceptions;
using MongoDB.Driver;
using System.Configuration;

namespace Cadastro.Infrastructure.Repositories.MongoDB.Contexts;

public class CadastroDbContext
{
    public IMongoClient Client { get;}
    internal IMongoDatabase Database { get;}

    #region Collections
    internal IMongoCollection<CadastroModel> Cadastro => Database.GetCollection<CadastroModel>(Collections.Cadastro);
    #endregion

    public CadastroDbContext()
    {
        MongoUrl url;

        try
        {
            url = new(GetConnectionString());
        }
        catch
        {
            throw new InfrastructureNotificationException("Conexão Inválida");
        }

        var mongoSettings = MongoClientSettings.FromUrl(url);

        Client = new MongoClient(mongoSettings);
        Database = Client.GetDatabase("controleCadastroDB");
    }

    private static string GetConnectionString()
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__ControlePedidosDB");
        return Environment.ExpandEnvironmentVariables(connectionString!);
    }
}
