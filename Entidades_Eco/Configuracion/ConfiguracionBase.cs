using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_Eco.Configuracion
{
    public class ConfiguracionBase
    {
        public ConfiguracionAplicacion configuracionAplicacion { get; set; }
        public ConfiguracionLogs configuracionLog { get; set; }
        //public ConfiguracionAutorizacion configuracionAutorizacion { get; set; }
        public ConexionBaseDeDatos bases { get; set; }
        public string Segurinet_APP { get; set; }
    }
}
