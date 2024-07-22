using API.Ayudas;
using API.Dtos;
using Core.Entidades;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Servicios;
public class UsuarioServicio : IUsuarioServicio
{
    private readonly JWT _jwt; 
    private readonly IUnidadDeTrabajo _unidadDeTrabajo; 
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public UsuarioServicio(IOptions<JWT> jwt, IUnidadDeTrabajo unidadDeTrabajo, IPasswordHasher<Usuario> passwordHasher)
    {
        _jwt = jwt.Value;
        _unidadDeTrabajo = unidadDeTrabajo; 
        _passwordHasher = passwordHasher;
    }

    public Task<string> AddRolAsync(AddRolDto model)
    {
        throw new NotImplementedException();
    }

    public Task<DatosUsuarioDto> ObtenerTokenAsync(LoginDto model)
    {
        throw new NotImplementedException();
    }

    public Task<DatosUsuarioDto> RefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public async Task<string> RegistroAsync(RegistroDto model)
    {
        var usuario = new Usuario{
            Nombre = model.Nombre,
            ApellidoPaterno = model.ApellidoPaterno,
            ApellidoMaterno = model.ApellidoMaterno,
            Email = model.Email,
            Username = model.Username,
        }; 

        usuario.Password = _passwordHasher.HashPassword(usuario, model.Password);
        var usuarioExiste = _unidadDeTrabajo.Usuarios
                                    .Buscar(u => u.Username.ToLower() == model.Username.ToLower())
                                    .FirstOrDefault();


    }
}