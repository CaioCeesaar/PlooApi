using Microsoft.EntityFrameworkCore;
using PlooAPI.Models;

namespace PlooAPI.Data;

public class PlooDbContext : DbContext
{
    public PlooDbContext(DbContextOptions<PlooDbContext> options) : base(options)
    {
    }
    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Perfil> Perfis { get; set; }
    // public DbSet<Equipe> Equipes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string? connString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connString);
        }
    }
    
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Usuario || e.Entity is Perfil && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            switch (entityEntry.Entity)
            {
                case Usuario usuario:
                    usuario.DataAtualizacao = DateTime.Now;
                    if (entityEntry.State == EntityState.Added)
                    {
                        usuario.DataCriacao = DateTime.Now;
                    }
                    break;
                case Perfil perfil:
                    perfil.DataAtualizacao = DateTime.Now;
                    if (entityEntry.State == EntityState.Added)
                    {
                        perfil.DataCriacao = DateTime.Now;
                    }
                    break;
            }
        }
    }
}