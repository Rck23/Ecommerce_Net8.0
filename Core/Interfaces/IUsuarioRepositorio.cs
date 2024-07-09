using Core.Entidades;

namespace Core.Interfaces;

public interface IUsuarioRepositorio: IGenericoRepositorio<Usuario>
{
    Task<Usuario> ObtenerPorUsernameAsync(string username);

    Task<Usuario> ObtenerPorRefreshTokenAsync(string refreshToken);
}
