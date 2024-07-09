
namespace Core.Entidades; 

public class Marca: BaseEntidades
{
    public string Nombre { get; set; }
    public ICollection<Producto> Productos { get; set; }

}