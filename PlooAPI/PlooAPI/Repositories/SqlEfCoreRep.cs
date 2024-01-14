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
            return new(true, "Ok", 200);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return new(false, $"Conflito de concorrencia detectado: {ex.Message}", 409);
        }
        catch (DbUpdateException ex)
        {
            return new(false, $"Erro ao inserir no banco de dados: {ex.Message}", 500);
        }
        catch (Exception ex)
        {
            return new(false, $"Ocorreu um erro: {ex.Message}", 500);
        }
    }
    
    public async Task<Result> PatchQueryAsync<T>(T entity) where T : class
    {
        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new(true, "Ok", 200);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return new(false, $"Conflito de concorrencia detectado: {ex.Message}", 409);
        }
        catch (DbUpdateException ex)
        {
            return new(false, $"Erro ao atualizar o banco de dados: {ex.Message}", 500);
        }
        catch (InvalidOperationException ex)
        {
            return new(false, $"Operação inválida: {ex.Message}", 400);
        }
        catch (Exception ex)
        {
            return new(false, $"Ocorreu um erro: {ex.Message}", 500);
        }
    }

    public async Task<Result> DeleteQueryAsync<T>(T entity) where T : class
    {
        try
        {
            
            
            await _context.SaveChangesAsync();
            return new Result(true, "Ok", 200);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return new(false, $"Conflito de concorrencia detectado: {ex.Message}", 409);
        }
        catch (DbUpdateException ex)
        {
            return new(false, $"Erro ao deletar do banco de dados: {ex.Message}", 500);
        }
        catch (InvalidOperationException ex)
        {
            return new(false, $"Operação inválida: {ex.Message}", 400);
        }
        catch (Exception ex)
        {
            return new(false, $"Ocorreu um erro: {ex.Message}", 500);
        }
    }
}