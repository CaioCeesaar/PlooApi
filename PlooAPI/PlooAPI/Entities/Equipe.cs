namespace PlooAPI.Entities;

public class Equipe
{
    public int Id { get; set; }
    
    public string Nome { get; set; }
    
    public ICollection<Usuario> Usuarios { get; set; }
}