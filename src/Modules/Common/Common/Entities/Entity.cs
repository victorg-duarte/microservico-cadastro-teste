namespace Common.Entities;

public abstract class Entity(string id, DateTime dataCriacao)
{
    public string Id { get; } = id;
    public DateTime DataCriacao { get; } = dataCriacao;
    protected abstract void Validate();
}
