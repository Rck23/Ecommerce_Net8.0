using API.Dtos;

namespace API.Servicios;

public interface IUsuarioServicio
{
    Task<string> RegistroAsync(RegistroDto model);
    Task<DatosUsuarioDto> ObtenerTokenAsync(LoginDto model);
    Task<string> AddRolAsync(AddRolDto model);

    Task<DatosUsuarioDto> RefreshTokenAsync(string refreshToken);    
}
