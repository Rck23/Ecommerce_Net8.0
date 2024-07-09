using Core.Entidades;
using Core.Interfaces;
using Infraestructura.Data;

namespace Infraestructura.Repositorios;

public class UsuarioRepositorio : GenericoRepositorio<Usuario>, IUsuarioRepositorio
{
    public UsuarioRepositorio(TiendaContext context) : base(context)
    {
    }

    public async Task<Usuario> ObtenerPorRefreshTokenAsync(string refreshToken)
    {
        return await _context.Usuarios.Include(u. => u.Roles).Include(u. => u.RefreshToken)              
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken)); 

    }

    public Task<Usuario> ObtenerPorUsernameAsync(string username)
    {
        return await _context.Usuarios
            .Include(u => u.Roles) // INCLUYE LOS ROLES
            .Include(u => u.RefreshTokens) // INCLUYE EL TOKEN REFRESH
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower()); 
    }
}
