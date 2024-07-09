using Core.Entidades;
using Core.Interfaces;
using Infraestructura.Data;

namespace Infraestructura.Repositorios;

public class RolRepositorio : GenericoRepositorio<Rol>, IRolRepositorio
{
    public RolRepositorio(TiendaContext context) : base(context)
    {
    }
}
