namespace PlooAPI.Models;

public abstract class Entity(int id, DateTime dataCriacao, DateTime dataAtualizacao, bool ativo)
{
    protected int Id { get; set; } = id;

    protected internal DateTime DataCriacao { get; set; } = dataCriacao;

    protected internal DateTime DataAtualizacao { get; set; } = dataAtualizacao;
    
    protected bool Ativo { get; set; } = true;
}