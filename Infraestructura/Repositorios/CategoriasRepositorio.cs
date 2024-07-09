using Core.Entidades;
using Core.Interfaces;
using Infraestructura.Data;

namespace Infraestructura.Repositorios;

public class CategoriasRepositorio : GenericoRepositorio<Categoria>, ICategoriaRepositorio
{

    public CategoriasRepositorio(TiendaContext context): base(context) 
    {
        
    }

}
