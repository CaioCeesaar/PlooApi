namespace PlooAPI.Models;

public class EquipeUsuario
{
    public int EquipeId { get; set; }
    public Equipe Equipe { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}