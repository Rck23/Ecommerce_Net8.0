using Core.Interfaces;
using Infraestructura.Data;
using Infraestructura.Repositorios;

namespace Infraestructura.UnidadDeTrabajo;

public class UnidadDeTrabajo : IUnidadDeTrabajo, IDisposable
{
    private readonly TiendaContext _context;
    private IProductoRepositorio _Productos; 
    private IUsuarioRepositorio _Usuarios; 
    private IRolRepositorio _Roles;
    private IMarcaRepositorio _Marcas; 
    private ICategoriaRepositorio _Categorias; 

    public UnidadDeTrabajo(TiendaContext context)
    {
        _context = context; 
    }
    public IProductoRepositorio Productos {
        get{
            if (_Productos == null)
                _Productos = new ProductoRepositorio(_context);
            return _Productos;
        }
    }

    public IMarcaRepositorio Marcas {
        get{
            if (_Marcas == null)
                _Marcas = new MarcasRepositorio(_context);
            return _Marcas;
        }
    }

    public ICategoriaRepositorio Categorias {
        get{
            if (_Categorias == null)
                _Categorias = new CategoriasRepositorio(_context);
            return _Categorias;
        }
    }

    public IRolRepositorio Roles {
        get{
            if (_Roles == null)
                _Roles = new RolRepositorio(_context);
            return _Roles;
        }
    }

    public IUsuarioRepositorio Usuarios {
        get{
            if (_Usuarios == null)
                _Usuarios = new UsuarioRepositorio(_context);
            return _Usuarios;
        }
    }

    public void Dispose()
    {
        _context.Dispose(); 
    }

    public async Task<int> GuardarAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
