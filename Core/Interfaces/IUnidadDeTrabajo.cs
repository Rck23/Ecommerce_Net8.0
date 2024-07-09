namespace Core.Interfaces;

public interface IUnidadDeTrabajo
{
    IProductoRepositorio Productos { get; }
    IMarcaRepositorio Marcas { get; }
    ICategoriaRepositorio Categorias { get; }
    IRolRepositorio Roles { get; }
    IUsuarioRepositorio Usuarios { get; }
    Task<int> GuardarAsync();
}
