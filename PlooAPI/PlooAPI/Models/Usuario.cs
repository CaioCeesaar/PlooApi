namespace PlooAPI.Models;

public class Usuario 
{
    public int Id { get; set; }
    
    public string Nome { get; set; }
    
    public string Email { get; set; }
    
    public DateTime DataNascimento { get; set; }
    
    public string NumeroCasa { get; set; }
    
    public string Cep { get; set; }
    
    public string Logradouro { get; set; }
    
    public string Complemento { get; set; }
    
    public string Bairro { get; set; }
    
    public string Localidade { get; set; }
    
    public string Uf { get; set; }
    
    public string Ibge { get; set; }
    
    public string Gia { get; set; }
    
    public string Ddd { get; set; }
    
    public string Siafi { get; set; }
    
    public int PerfilId { get; set; }
    
    public DateTime DataCriacao { get; set; }
    
    public DateTime DataAtualizacao { get; set; }
    
    public bool Ativo { get; set; }
}

public class UsuarioModel
{
    public string Nome { get; set; }
    
    public string Email { get; set; }
    
    public DateTime DataNascimento { get; set; }
    
    public string NumeroCasa { get; set; }
    
    public string Complemento { get; set; }
    
    public string Cep { get; set; }
    
    public int PerfilId { get; set; }
    
}

public class UsuarioUpdateModel
{
    public int Id { get; set; }
    
    public string Nome { get; set; }
    
    public string Email { get; set; }
    
    public DateTime DataNascimento { get; set; }
    
    public string NumeroCasa { get; set; }
    
    public string Complemento { get; set; }
    
    public string Cep { get; set; }
    
    public int PerfilId { get; set; }
    
}
