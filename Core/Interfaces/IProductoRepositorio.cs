using Core.Entidades;

namespace Core.Interfaces;

public interface IProductoRepositorio: IGenericoRepositorio<Producto>
{
    Task<IEnumerable<Producto>> ObetnerProductosMasCaros(int cantidad); 
}
