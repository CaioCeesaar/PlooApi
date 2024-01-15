namespace PlooAPI.Models;

public class Equipe 
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ICollection<Usuario>? Usuarios { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public bool Ativo { get; set; }
}

public class EquipeModel
{
    public string Nome { get; set; }
}

