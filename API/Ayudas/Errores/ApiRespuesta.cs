namespace API.Ayudas.Errores;

public class ApiRespuesta
{
    public int EstatusCodigo { get; set; }  
    public string Mensaje { get; set; }  

    public ApiRespuesta(int estatusCodigo, string mensaje = null)
    {
        EstatusCodigo = estatusCodigo;
        Mensaje = mensaje ?? ObtenerMensajeDefault(estatusCodigo);
    }

    private string ObtenerMensajeDefault(int estatusCodigo){
        return EstatusCodigo switch{
            400 => "Haz realizado una petición incorrecta.",
            401 => "Usuario no autorizado.",
            404 => "El recurso que has intentado solicitar no existe.",
            405 => "Este método HTTP no está permitido en el servidor.",
            500 => "Error en el servidor. No eres tú, soy yo.",
        };
    }
    
}
