
namespace Core.Entidades;

    public class Rol: BaseEntidades
    {
        public string Nombre { get; set; }
         public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
        public ICollection<UsuariosRoles> UsuariosRoles { get; set; }
    }
