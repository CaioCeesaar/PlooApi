namespace PlooAPI.Models;

public class Usuario : Entity
{
    public Usuario(int id, DateTime dataCriacao, DateTime dataAtualizacao, string nome, string email, DateTime dataNascimento, 
        string numeroCasa, string cep, string logradouro, string complemento, string bairro, string localidade, string uf, 
        string ibge, string gia, string ddd, string siafi, int perfilId, ICollection<Equipe>? equipes) : base(id, dataCriacao, dataAtualizacao)
    {
        Nome = nome;
        Email = email;
        DataNascimento = dataNascimento;
        NumeroCasa = numeroCasa;
        Cep = cep;
        Logradouro = logradouro;
        Complemento = complemento;
        Bairro = bairro;
        Localidade = localidade;
        Uf = uf;
        Ibge = ibge;
        Gia = gia;
        Ddd = ddd;
        Siafi = siafi;
        PerfilId = perfilId;
        Equipes = equipes;
    }
    
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
    
    public ICollection<Equipe>? Equipes { get; set; }
    
}

