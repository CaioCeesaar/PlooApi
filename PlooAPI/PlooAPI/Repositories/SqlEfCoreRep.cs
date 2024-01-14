using Microsoft.EntityFrameworkCore;
using PlooAPI.Business;
using PlooAPI.Data;

namespace PlooAPI.Repositories;

public class SqlEfCoreRep
{
    private readonly PlooDbContext _context;

    public SqlEfCoreRep(PlooDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result> PostQueryAsync<T>(T entity) where T : class
    {
        try
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new(true, "Ok");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return new(false, $"Conflito de concorrencia detectado: {ex.Message}");
        }
        catch (DbUpdateException ex)
        {
            return new(false, $"Erro ao inserir no banco de dados: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new(false, $"Ocorreu um erro: {ex.Message}");
        }
    }
    
    public async Task<Result> PatchQueryAsync<T>(T entity) where T : class
    {
        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new(true, "Ok");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return new(false, $"Conflito de concorrencia detectado: {ex.Message}");
        }
        catch (DbUpdateException ex)
        {
            return new(false, $"Erro ao atualizar o banco de dados: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return new(false, $"Operação inválida: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new(false, $"Ocorreu um erro: {ex.Message}");
        }
    }

    public async Task<Result> DeleteQueryAsync<T>(T entity) where T : class
    {
        try
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return new(true, "Ok");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return new(false, $"Conflito de concorrencia detectado: {ex.Message}");
        }
        catch (DbUpdateException ex)
        {
            return new(false, $"Erro ao deletar do banco de dados: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return new(false, $"Operação inválida: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new(false, $"Ocorreu um erro: {ex.Message}");
        }
    }
}