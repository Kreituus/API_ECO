using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_Eco
{
    public class RespuestaGeneral
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string code { get; set; }
        public RespuestaGeneral()
        {
            this.success = false;
            this.message = string.Empty;
            this.code = string.Empty;
        }
    }
}
