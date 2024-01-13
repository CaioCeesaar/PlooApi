namespace PlooAPI.Models;

public class Equipe : Entity
{
    public Equipe(int id, DateTime dataCriacao, DateTime dataAtualizacao, string nome, ICollection<Usuario> usuarios) : base(id, dataCriacao, dataAtualizacao)
    {
        Nome = nome;
        Usuarios = usuarios;
    }
    
    public string Nome { get; set; }
    
    public ICollection<Usuario> Usuarios { get; set; }

}