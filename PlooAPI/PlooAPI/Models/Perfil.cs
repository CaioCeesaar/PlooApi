namespace PlooAPI.Models;

public class Perfil : Entity
{
    public Perfil(int id, DateTime dataCriacao, DateTime dataAtualizacao, string nome) : base(id, dataCriacao, dataAtualizacao)
    {
        Nome = nome;
    }
    
    public string Nome { get; set; }
}