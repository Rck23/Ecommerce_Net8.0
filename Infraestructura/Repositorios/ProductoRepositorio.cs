using Core.Entidades;
using Core.Interfaces;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios;

public class ProductoRepositorio : GenericoRepositorio<Producto>, IProductoRepositorio
{
    public ProductoRepositorio(TiendaContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Producto>> ObetnerProductosMasCaros(int cantidad) => 
        await _context.Productos.OrderByDescending(p => p.Precio).Take(cantidad).ToListAsync();

        public override async Task<Producto> ObtenerPorIdAsync(int id)
    {
        return await _context.Productos
            .Include(p => p.Marca)
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // SOBREESCRIBIENDO EL METODO DE GENERIC REPOSITORY
    public override async Task<IEnumerable<Producto>> ObtenerTodoAsync()
    {
        return await _context.Productos
            .Include(u => u.Marca)
            .Include(u => u.Categoria)
            .ToListAsync();
    }

     public override async Task<(int totalRegistros, IEnumerable<Producto> registros)> ObtenerTodoAsync(int pageIndex, 
        int pageSize, string buscar)
    {
        var consulta = _context.Productos as IQueryable<Producto>;

        if(!String.IsNullOrEmpty(buscar))
        {
            consulta = consulta.Where(p=> p.Nombre.ToLower().Contains(buscar));
        }

        var totalRegistros = await consulta
                                    .CountAsync();

        var registros = await consulta
                                .Include(u => u.Marca)
                                .Include(u => u.Categoria)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return (totalRegistros, registros);
    }


    

}
