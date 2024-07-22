using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Ayudas.Errores
{
    public class ApiValidacion: ApiRespuesta
    {
        public IEnumerable<string> Errores { get; set; }
        
        public ApiValidacion(): base(400)
        {
            
        }
    }
}