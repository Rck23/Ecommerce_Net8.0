using Core.Entidades;
using Core.Interfaces;
using Infraestructura.Data;

namespace Infraestructura.Repositorios;

public class MarcasRepositorio : GenericoRepositorio<Marca>, IMarcaRepositorio
{
    public MarcasRepositorio(TiendaContext context) : base(context)
    {
    }
}
