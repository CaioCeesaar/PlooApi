namespace PlooAPI.Models;

public class Perfil 
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public bool Ativo { get; set; }
} 

public class PerfilModel
{
    public string Nome { get; set; }
}

public class PerfilUpdateModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
}

