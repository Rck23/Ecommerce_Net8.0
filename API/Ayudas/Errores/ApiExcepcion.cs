using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Ayudas.Errores
{
    public class ApiExcepcion: ApiRespuesta
    {
        public string Detalles { get; set; }
        
        public ApiExcepcion(int estatusCodigo, string mensaje = null, string detalles = null) :base(estatusCodigo, mensaje) 
        {
            Detalles = detalles;
        }

    }
}