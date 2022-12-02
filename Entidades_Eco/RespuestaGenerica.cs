using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_Eco
{
    public class RespuestaGenerica
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string code { get; set; }
        public int ID { get; set; }

        public RespuestaGenerica()
        {
            this.success = false;
            this.message = String.Empty;
            this.code = string.Empty;
            this.ID = 0;
        }
    }
}
