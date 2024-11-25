using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cadastro.Infrastructure.Base.Models;

public class BaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public BaseModel(string id)
    {
        Id = string.IsNullOrWhiteSpace(id) ? ObjectId.GenerateNewId().ToString() : id;
    }
}
