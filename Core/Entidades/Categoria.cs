
namespace Core.Entidades;

    public class Categoria: BaseEntidades
    {
        public string Nombre { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
