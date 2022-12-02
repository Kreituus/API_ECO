using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_Eco
{
    public  class EntrevistaModel
    {

    }
    public class ENTREVISTA
    {
        public int ID_CLIENTE { get; set; }
        public ENTREVISTA_VERIFICACION_CLIENTE VERIFICACION_CLIENTE { get; set; }
        public ENTREVISTA_SCRIPT_EVALUACION SCRIPT_EVALUACION { get; set; }
        public ENTREVISTA_SEGURO_DESGRAVAME SEGURO_DESGRAVAME { get; set; }
        public ENTREVISTA()
        {
            this.ID_CLIENTE = 0;
            this.VERIFICACION_CLIENTE = new ENTREVISTA_VERIFICACION_CLIENTE();
            this.SCRIPT_EVALUACION = new ENTREVISTA_SCRIPT_EVALUACION();
            this.SEGURO_DESGRAVAME = new ENTREVISTA_SEGURO_DESGRAVAME();
        }
    }

    public class ENTREVISTA_VERIFICACION_CLIENTE
    {
        public int ID_ENTREVISTA_VERI_CLIENTE { get; set; }
        public List<VERIFICACION_CLIENTE_RESPONSE> LISTADO_VERIFICACION_CLIENTE { get; set; }
        public ENTREVISTA_VERIFICACION_CLIENTE()
        {
            this.ID_ENTREVISTA_VERI_CLIENTE = 0;
            this.LISTADO_VERIFICACION_CLIENTE = new List<VERIFICACION_CLIENTE_RESPONSE>();
        }
    }

    public class VERIFICACION_CLIENTE_RESPONSE
    {
        public string PREGUNTA { get; set; }
        public string RESPUESTA { get; set; }
        public string CONFIRMACION { get; set; }
        public string OBSERVACIONES { get; set; }
        public string USUARIO { get; set; }
        public VERIFICACION_CLIENTE_RESPONSE()
        {
            this.PREGUNTA = string.Empty;
            this.RESPUESTA = string.Empty;
            this.CONFIRMACION = string.Empty;
            this.OBSERVACIONES = string.Empty;
            this.USUARIO = string.Empty;
        }
    }

    public class ENTREVISTA_SCRIPT_EVALUACION
    {
        public int ID_SCRIPT_EVALUACION { get; set; }
        public List<SCRIPT_EVALUACION_RESPONSE> LISTADO_SCRIPT_EVALUACION { get; set; }
        public ENTREVISTA_SCRIPT_EVALUACION()
        {
            this.ID_SCRIPT_EVALUACION = 0;
            this.LISTADO_SCRIPT_EVALUACION = new List<SCRIPT_EVALUACION_RESPONSE>();
        }
    }

    public class SCRIPT_EVALUACION_RESPONSE
    {
        public string PREGUNTA { get; set; }
        public string RESPUESTA { get; set; }
        public int ID_INSTITUCIONES { get; set; }
        public List<INSTITUCION> INSTITUCIONES { get; set; }
        public string TOTAL { get; set; }
        public string OBSERVACIONES { get; set; }
        public string USUARIO { get; set; }
        public SCRIPT_EVALUACION_RESPONSE()
        {
            this.PREGUNTA = string.Empty;
            this.RESPUESTA = string.Empty;
            this.ID_INSTITUCIONES = 0;
            this.INSTITUCIONES = new List<INSTITUCION>();
            this.TOTAL = "0";
            this.OBSERVACIONES = string.Empty;
            this.USUARIO = string.Empty;
        }
    }

    public class INSTITUCION
    {
        public string NOMBRE { get; set; }
        public string MONTO { get; set; }
        public string USUARIO { get; set; }
        public INSTITUCION()
        {
            this.NOMBRE = string.Empty;
            this.MONTO = "0";
            this.USUARIO = string.Empty;
        }
    }

    public class ENTREVISTA_SEGURO_DESGRAVAME
    {
        public int ID_SEGURO_DESGRAVAME { get; set; }
        public List<SEGURO_DESGRAVAME_RESPONSE> LISTADO_SEGURO_DESGRAVAME { get; set; }
        public ENTREVISTA_SEGURO_DESGRAVAME()
        {
            this.ID_SEGURO_DESGRAVAME = 0;
            this.LISTADO_SEGURO_DESGRAVAME = new List<SEGURO_DESGRAVAME_RESPONSE>();
        }
    }

    public class SEGURO_DESGRAVAME_RESPONSE
    {
        public string PREGUNTA { get; set; }
        public string RESPUESTA { get; set; }
        public string CONFIRMACION { get; set; }
        public string OBSERVACIONES { get; set; }
        public string USUARIO { get; set; }
        public SEGURO_DESGRAVAME_RESPONSE()
        {
            this.PREGUNTA = string.Empty;
            this.RESPUESTA = string.Empty;
            this.CONFIRMACION = string.Empty;
            this.OBSERVACIONES = string.Empty;
            this.USUARIO = string.Empty ;
        }
    }


}
