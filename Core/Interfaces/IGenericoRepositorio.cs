using System.Linq.Expressions;
using Core.Entidades;

namespace Core.Interfaces;

public interface IGenericoRepositorio<T> where T : BaseEntidades
{
    Task<T> ObtenerPorIdAsync(int id);
    Task<IEnumerable<T>> ObtenerTodoAsync();
    IEnumerable<T> Buscar(Expression<Func<T, bool>> expression);

    void Add(T entidad); 
    void Remover(T entidad); 
    void Actualizar(T entidad); 
    void AddRango(IEnumerable<T> entidades); 
    void RemoverRango(IEnumerable<T> entidades); 

    Task<(int totalRegistros, IEnumerable<T> registros)> ObtenerTodoAsync(int pageIndex, int pageSize, string buscar);


}
