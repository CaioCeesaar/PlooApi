using Microsoft.EntityFrameworkCore;
using PlooAPI.Models;

namespace PlooAPI.Data;

public class PlooDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    // TODO: Adicionar outras entidades

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
            .Where(e => e.Entity is Usuario && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((Usuario)entityEntry.Entity).DataAtualizacao = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((Usuario)entityEntry.Entity).DataCriacao = DateTime.Now;
            }
        }
    }
}