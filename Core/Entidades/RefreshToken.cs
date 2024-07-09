
namespace Core.Entidades;
public class RefreshToken : BaseEntidades
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public string Token { get; set; }
    public DateTime Exp { get; set; }
    public bool IsExp => DateTime.UtcNow >= Exp;

    public DateTime Creado { get; set; }
    public DateTime? Revokado { get; set; }
    public bool IsActive => Revokado == null && !IsExp;
}
