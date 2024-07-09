using System.Linq.Expressions;
using Core.Entidades;
using Core.Interfaces;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios;

public class GenericoRepositorio<T> : IGenericoRepositorio<T> where T : BaseEntidades
{
    protected readonly TiendaContext _context;

    public GenericoRepositorio(TiendaContext context)
    {
        _context = context;
    }

    public virtual void Add(T entidad){
        _context.Set<T>().Add(entidad);
        
    }

    public virtual void AddRango(IEnumerable<T> entidades){
        _context.Set<T>().AddRange(entidades);
        
    }

    public virtual IEnumerable<T> Buscar(Expression<Func<T,bool>> expression){
        return _context.Set<T>().Where(expression);
    }

    public virtual async Task<IEnumerable<T>> ObtenerTodoAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> ObtenerTodoAsync(int pageIndex, int pageSize, string buscar){
        
        var totalRegistros = await _context.Set<T>().CountAsync();

        var registros =await _context.Set<T>()
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();

        return (totalRegistros, registros);
    }


    public virtual async Task<T> ObtenerPorIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual void Remover(T entidad){
        _context.Set<T>().Remove(entidad);
        
    }

    public virtual void RemoverRango(IEnumerable<T> entidades){
        _context.Set<T>().RemoveRange(entidades);
        
    }

    public virtual void Actualizar(T entidad){
        _context.Set<T>().Update(entidad);
        
    }

  
}
