using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_Eco
{
    public  class RobotModel
    {

    }

    public class EstadoClienteAnaRequest
    {
        public string TIPO_DOCUMENTO { get; set; }//COMBO
        public string NRO_DOCUMENTO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string EXTENSION { get; set; }//COMBO
        public EstadoClienteAnaRequest()
        {
            this.TIPO_DOCUMENTO = string.Empty;
            this.NRO_DOCUMENTO = string.Empty;
            this.COMPLEMENTO = string.Empty;
            this.EXTENSION = string.Empty;
        }
    }
    public class EstadoClienteAnaResponse
    {
        public CLIENTERESPONSEANA Cliente { get; set; }
        public bool DATA_ANA { get; set; }
    }

    public class CLIENTERESPONSEANA : RespuestaGeneral
    {
        public int ID_CLIENTE { get; set; }
        public string ESTADO_ANA { get; set; }
        public PERSONA DATOS_GENERALES { get; set; }
        public List<ACTIVIDAD_ECONOMICA> ACTIVIDADES_ECONOMICAS { get; set; }
        public List<REFERENCIA> REFERENCIAS { get; set; }
        public string USUARIO { get; set; }
        public CLIENTERESPONSEANA()
        {
            this.ID_CLIENTE = 0;
            this.ESTADO_ANA = string.Empty;
            this.DATOS_GENERALES = new PERSONA();
            this.ACTIVIDADES_ECONOMICAS = new List<ACTIVIDAD_ECONOMICA>();
            this.REFERENCIAS = new List<REFERENCIA>();
            this.USUARIO = string.Empty;
        }

    }



    public class CLIENTERESPONSESCI : RespuestaGeneral
    {
        public int ID_CLIENTE { get; set; }
        public List<REFERENCIA> REFERENCIAS { get; set; }
        public List<CREDITO> CREDITOS { get; set; }
        public string USUARIO { get; set; }
        public CLIENTERESPONSESCI()
        {
            this.ID_CLIENTE = 0;
            this.REFERENCIAS = new List<REFERENCIA>();
            this.CREDITOS = new List<CREDITO>();
            this.USUARIO = string.Empty;
        }

    }

    public class CAMBIOESTADOANA
    {
        public int ID_CLIENTE { get; set; }
        public int COD_ANA { get; set; }
        public string ROBOT { get; set; }

        public CAMBIOESTADOANA()
        {
            this.ID_CLIENTE = 0;
            this.COD_ANA = 0;
            this.ROBOT = string.Empty;

        }

    }

    public class CAMBIOESTADOANAPROBLEMAS
    {
        public int ID_CLIENTE { get; set; }
        public string ROBOT { get; set; }
        public string DESCRIPCION { get; set; }

        public CAMBIOESTADOANAPROBLEMAS()
        {
            this.ID_CLIENTE = 0;
            this.ROBOT = string.Empty;
            this.DESCRIPCION = string.Empty;
        }

    }

    public class CAMBIOESTADOSCI
    {
        public int ID_CREDITO { get; set; }
        public int COD_SCI { get; set; }
        public string ROBOT { get; set; }

        public CAMBIOESTADOSCI()
        {
            this.ID_CREDITO = 0;
            this.COD_SCI = 0;
            this.ROBOT = string.Empty;

        }

    }

    public class CAMBIOESTADOSCIPROBLEMAS
    {
        public int ID_CREDITO { get; set; }
        public string ROBOT { get; set; }
        public string DESCRIPCION { get; set; }

        public CAMBIOESTADOSCIPROBLEMAS()
        {
            this.ID_CREDITO = 0;
            this.ROBOT = string.Empty;
            this.DESCRIPCION = string.Empty;
        }

    }

    public class CAMBIOESTADOROBOT
    {
        //public int ID_CAMBIO_ESTADO { get; set; }
        public string TIPO { get; set; }  //SCI,ANA,CREDITO
        public int ID_CLIENTE { get; set; }
        public int ID_CREDITO { get; set; }
        public string USUARIO { get; set; }
        public string NUEVO_ESTADO { get; set; }
        public string DESCRIPCION { get; set; }

        public CAMBIOESTADOROBOT()
        {
            //this.ID_CAMBIO_ESTADO = 0;
            this.TIPO = string.Empty;
            this.ID_CLIENTE = 0;
            this.ID_CREDITO = 0;
            this.USUARIO = string.Empty;
            this.NUEVO_ESTADO = string.Empty;
            this.DESCRIPCION = string.Empty;
        }

    }
    public class SCORINGREQUEST
    {
        public string NRO_DOCUMENTO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string EXTENSION { get; set; }
        public string TICKET { get; set; }
        public string USUARIO { get; set; }
        
        public List<Puntaje_Scoring> CIC { get; set; }
        public List<Puntaje_Scoring> Infocred { get; set; }
        public List<Puntaje_Scoring> Hoja_Riesgos { get; set; }
        public List<Puntaje_Scoring> Puntaje { get; set; }
        public SCORINGREQUEST()
        {
            this.NRO_DOCUMENTO = string.Empty;
            this.COMPLEMENTO = string.Empty;
            this.EXTENSION = string.Empty;//COMBO
            this.TICKET = string.Empty;
            this.USUARIO = string.Empty;
            this.CIC = new List<Puntaje_Scoring>();
            this.Infocred = new List<Puntaje_Scoring>();
            this.Hoja_Riesgos = new List<Puntaje_Scoring>();
            this.Puntaje = new List<Puntaje_Scoring>();
        }
    }

    public class SCORINGREPONSE
    {
        public int ID_SCORING { get; set; }
        public int ID_CLIENTE { get; set; }
        public string TICKET { get; set; }
        public string USUARIO { get; set; }
        //public List<SCORING> PUNTAJE_BUROS_ROBOT { get; set; }
        public List<Puntaje_Scoring> CIC { get; set; }
        public List<Puntaje_Scoring> Infocred { get; set; }
        public List<Puntaje_Scoring> Hoja_Riesgos { get; set; }
        public List<Puntaje_Scoring> Puntaje { get; set; }
        public SCORINGREPONSE()
        {
            this.ID_SCORING = 0;
            this.ID_CLIENTE = 0;
            this.TICKET = string.Empty;
            this.USUARIO = string.Empty;
            this.CIC = new List<Puntaje_Scoring>();
            this.Infocred = new List<Puntaje_Scoring>();
            this.Hoja_Riesgos = new List<Puntaje_Scoring>();
            this.Puntaje = new List<Puntaje_Scoring>();
            //this.PUNTAJE_BUROS_ROBOT = new List<SCORING>();
        }
    }

    public class SCORING
    {
        public string AREA { get; set; }
        public string CRITERIO { get; set; }
        public string RESULTADO { get; set; }
        public string PUNTAJE { get; set; }

        public SCORING()
        {
            this.AREA = string.Empty;
            this.CRITERIO = string.Empty;
            this.RESULTADO = string.Empty;
            this.PUNTAJE = string.Empty;
        }
    }

    public class SCORINGRESPONSE
    {
        public int ID_SCORING { get; set; }
        public int ID_CLIENTE { get; set; }
        public string TICKET { get; set; }
        public string USUARIO { get; set; }
        public string AREA { get; set; }
        public string CRITERIO { get; set; }
        public string RESULTADO { get; set; }
        public string PUNTAJE { get; set; }

        public SCORINGRESPONSE()
        {
            this.ID_SCORING = 0;
            this.ID_CLIENTE = 0;
            this.TICKET = string.Empty;
            this.USUARIO = string.Empty;
            this.AREA = string.Empty;
            this.CRITERIO = string.Empty;
            this.RESULTADO = string.Empty;
            this.PUNTAJE = string.Empty;
        }
    }

    public class CIC
    {
        public int ID_CIC { get; set; }
        public string CODIGO_CIC { get; set; }
        public string DESCRIPCION { get; set; }
        public CIC()
        {
            this.ID_CIC = 0;
            this.CODIGO_CIC = string.Empty;
            this.DESCRIPCION = string.Empty;
        }
    }

    public class HOJA_RIESGOS
    {
        public int ID_HOJA_RIESGOS { get; set; }
        public string CODIGO_HOJA_RIESGOS { get; set; }
        public string DESCRIPCION { get; set; }
        public HOJA_RIESGOS()
        {
            this.ID_HOJA_RIESGOS = 0;
            this.CODIGO_HOJA_RIESGOS = string.Empty;
            this.DESCRIPCION = string.Empty;
        }
    }

    public class INFOCRED
    {
        public int ID_INFOCRED { get; set; }
        public string CODIGO_INFOCRED { get; set; }
        public string DESCRIPCION { get; set; }
        public INFOCRED()
        {
            this.ID_INFOCRED = 0;
            this.CODIGO_INFOCRED = string.Empty;
            this.DESCRIPCION = string.Empty;
        }
    }

    public class PUNTAJE
    {
        public int ID_PUNTAJE { get; set; }
        public string CODIGO_PUNTAJE { get; set; }
        public string DESCRIPCION { get; set; }
        
        public PUNTAJE()
        {
            this.ID_PUNTAJE = 0;
            this.CODIGO_PUNTAJE = string.Empty;
            this.DESCRIPCION = string.Empty;
        }

    }
    public class Puntaje_Scoring
    {
        public string CRITERIO { get; set; }
        public string RESULTADO { get; set; }
        public string PUNTAJE { get; set; }
        public Puntaje_Scoring()
        {
            this.CRITERIO = string.Empty;
            this.RESULTADO = string.Empty;
            this.PUNTAJE = string.Empty;
            
        }
    }

    public class CLIENTERESPONSEANAVALIDAR : RespuestaGeneral
    {
        public int ID_CLIENTE { get; set; }
        public string COD_ANA { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string TIPO_DOCUMENTO { get; set; }//COMBO
        public string NRO_DOCUMENTO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string EXTENSION { get; set; }//COMBO
        public CLIENTERESPONSEANAVALIDAR()
        {
            this.ID_CLIENTE = 0;
            this.COD_ANA = string.Empty;
            this.NOMBRE_COMPLETO = string.Empty;
            this.TIPO_DOCUMENTO = string.Empty;
            this.NRO_DOCUMENTO = string.Empty;
            this.COMPLEMENTO = string.Empty;
            this.EXTENSION = string.Empty;
        }
    }



    public class CLIENTERESPONSEANAVALIDARRESPONSE : RESPUESTA_CLIENTE
    {
        public CLIENTERESPONSEANAREQUEST CLIENTE { get; set; }
        public CLIENTERESPONSEANAVALIDARRESPONSE()
        {
            this.CLIENTE = new CLIENTERESPONSEANAREQUEST();
        }
    }

    public class CLIENTERESPONSEANAREQUEST
    {
        public int ID_CLIENTE { get; set; }
        public string ESTADO_ANA { get; set; }
        public PERSONA DATOS_GENERALES { get; set; }
        public List<ACTIVIDAD_ECONOMICA> ACTIVIDADES_ECONOMICAS { get; set; }
        public List<REFERENCIA> REFERENCIAS { get; set; }
        public string USUARIO { get; set; }
        public CLIENTERESPONSEANAREQUEST()
        {
            this.ID_CLIENTE = 0;
            this.ESTADO_ANA = string.Empty;
            this.DATOS_GENERALES = new PERSONA();
            this.ACTIVIDADES_ECONOMICAS = new List<ACTIVIDAD_ECONOMICA>();
            this.REFERENCIAS = new List<REFERENCIA>();
            this.USUARIO = string.Empty;
        }

    }

    public class CLIENTEROBOT : RespuestaGeneral
    {
        public int ID_CLIENTE { get; set; }
        public string ESTADO_ANA { get; set; }
        public PERSONAROBOT DATOS_GENERALES { get; set; }
        public List<ACTIVIDAD_ECONOMICAROBOT> ACTIVIDADES_ECONOMICAS { get; set; }
        public List<REFERENCIAROBOT> REFERENCIAS { get; set; }
        public string USUARIO { get; set; }

        public CLIENTEROBOT()
        {
            this.ID_CLIENTE = 0;
            this.ESTADO_ANA = string.Empty;
            this.DATOS_GENERALES = new PERSONAROBOT();
            this.ACTIVIDADES_ECONOMICAS = new List<ACTIVIDAD_ECONOMICAROBOT>();
            this.REFERENCIAS = new List<REFERENCIAROBOT>();
            this.USUARIO = string.Empty;
        }
    }


    public class CREDITOROBOT
    {
        public int ID_CREDITO { get; set; }
        public string COD_CREDITO { get; set; }
        //ESTRUCTURA SCI
        public string COD_SCI { get; set; }
        public string MONTO_CREDITO { get; set; }
        public string TIPO_OPERACION { get; set; }//COMBO
        public string ESTRATEGIA { get; set; }//COMBO
        public string DESTINO { get; set; }
        public string DESTINO_CLIENTE { get; set; }//COMBO
        public string COMPRA_PASIVO { get; set; }
        public string OBJETO_OPERACION { get; set; }//COMBO
        public string PLAZO { get; set; }
        public string DIA_PAGO { get; set; }
        public string ANTIGUEDAD_CLIENTE { get; set; }
        public string FECHA_SOLICITUD { get; set; }
        public List<DECLARACION_JURADAROBOT> DECLARACIONES { get; set; }
        public string CLIENTE_CPOP { get; set; } //COMBO
        public string NRO_CPOP { get; set; }
        public string TIPO_TASA { get; set; } //COMBO
        public string NRO_AUTORIZACION { get; set; }
        public string TASA_SELECCION { get; set; }
        public string TASA_PP { get; set; }
        public string USUARIO { get; set; }
        public List<DOCUMENTO_ENTREGADOROBOT> DOCUMENTOS_ENTREGADOS { get; set; }
        public List<AUTORIZACIONROBOT> AUTORIZACIONES { get; set; }
        public string ESTADO_CREDITO { get; set; }
        public string ESTADO_SCI { get; set; }

        public CREDITOROBOT()
        {
            this.ID_CREDITO = 0;
            this.COD_CREDITO = string.Empty;
            this.COD_SCI = string.Empty;
            this.MONTO_CREDITO = string.Empty;
            this.TIPO_OPERACION = string.Empty;//COMBO
            this.ESTRATEGIA = string.Empty;//COMBO
            this.DESTINO = string.Empty;
            this.DESTINO_CLIENTE = string.Empty;//COMBO
            this.COMPRA_PASIVO = string.Empty;
            this.OBJETO_OPERACION = string.Empty;//COMBO
            this.PLAZO = string.Empty;
            this.DIA_PAGO = string.Empty;
            this.ANTIGUEDAD_CLIENTE = string.Empty;
            this.FECHA_SOLICITUD = string.Empty;
            this.DECLARACIONES = new List<DECLARACION_JURADAROBOT>();
            this.CLIENTE_CPOP = string.Empty;
            this.NRO_CPOP = string.Empty;
            this.TIPO_TASA = string.Empty;
            this.NRO_AUTORIZACION = string.Empty;
            this.TASA_SELECCION = string.Empty;
            this.TASA_PP = string.Empty;
            this.DOCUMENTOS_ENTREGADOS = new List<DOCUMENTO_ENTREGADOROBOT>();
            this.AUTORIZACIONES = new List<AUTORIZACIONROBOT>();
            this.ESTADO_CREDITO = string.Empty;
            this.ESTADO_SCI = string.Empty;
            this.USUARIO = string.Empty;
        }

    }






    public class DOCUMENTO_PERSONALROBOT
    {
        public int ID_DOCUMENTO_PERSONAL { get; set; }
        public string TIPO_DOCUMENTO { get; set; }//COMBO
        public string NRO_DOCUMENTO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string EXTENSION { get; set; }//COMBO
        public string FECHA_VENC { get; set; }
        public string ESTADO_CIVIL { get; set; }//COMBO
        public string NACIONALIDAD { get; set; }//COMBO
        public string LUGAR_NACIMIENTO { get; set; }//COMBO
        public string FECHA_NACIMIENTO { get; set; }
        public string USUARIO { get; set; }
        public DOCUMENTO_PERSONALROBOT()
        {
            this.ID_DOCUMENTO_PERSONAL = 0;
            this.TIPO_DOCUMENTO = string.Empty;//COMBO
            this.NRO_DOCUMENTO = string.Empty;
            this.COMPLEMENTO = string.Empty;
            this.EXTENSION = string.Empty;//COMBO
            this.FECHA_VENC = string.Empty;
            this.ESTADO_CIVIL = string.Empty;//COMBO
            this.NACIONALIDAD = string.Empty;//COMBO
            this.LUGAR_NACIMIENTO = string.Empty;//COMBO
            this.FECHA_NACIMIENTO = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class PERSONAROBOT
    {
        public int ID_PERSONA { get; set; }
        //public string TIPO { get; set; }
        public string GENERO { get; set; }//COMBO
        public string PRIMER_APELLIDO { get; set; }
        public string SEGUNDO_APELLIDO { get; set; }
        public string NOMBRES { get; set; }
        public string CASADA_APELLIDO { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string CORREO { get; set; }
        public string NIVEL_EDUCACION { get; set; }//COMBO
        public string PROFESION { get; set; }//COMBO
        public string ESTADO_CIVIL { get; set; }//COMBO
        public string COD_ANA { get; set; }
        public List<DIRECCIONROBOT> DIRECCIONES_PERS { get; set; }
        public List<DOCUMENTO_PERSONALROBOT> DOCUMENTOS { get; set; }
        public TELEFONOROBOT CONTACTO { get; set; }
        public string USUARIO { get; set; }
        public PERSONAROBOT()
        {
            this.ID_PERSONA = 0;
            this.GENERO = string.Empty;//COMBO
            //this.TIPO = string.Empty;
            this.PRIMER_APELLIDO = string.Empty;
            this.SEGUNDO_APELLIDO = string.Empty;
            this.NOMBRES = string.Empty;
            this.CASADA_APELLIDO = string.Empty;
            this.NOMBRE_COMPLETO = string.Empty;
            this.CORREO = string.Empty;
            this.NIVEL_EDUCACION = string.Empty;//COMBO
            this.PROFESION = string.Empty;//COMBO
            this.ESTADO_CIVIL = string.Empty;//COMBO
            this.COD_ANA = string.Empty;
            this.DIRECCIONES_PERS = new List<DIRECCIONROBOT>();
            this.DOCUMENTOS = new List<DOCUMENTO_PERSONALROBOT>();
            this.CONTACTO = new TELEFONOROBOT();
            this.USUARIO = string.Empty;
        }
    }
    public class DIRECCIONROBOT
    {
        public int ID_DIRECCION { get; set; }
        public string PAIS_RESIDENCIA { get; set; }//COMBO
        public string CIUDAD_RESIDENCIA { get; set; }//COMBO
        public string TIPO_DIRECCION { get; set; }//COMBO
        public string DEPARTAMENTO { get; set; }//COMBO
        public string LOCALIDAD { get; set; }//COMBO
        public string ZONA_BARRIO { get; set; }
        public string CALLE_AVENIDA { get; set; }
        public string NRO_PUERTA { get; set; }
        public string NRO_PISO { get; set; }
        public string NRO_DEPARTAMENTO { get; set; }
        public string NOMBRE_EDIFICIO { get; set; }
        public string NRO_CASILLA { get; set; }
        public string TIPO_VIVIENDA_OFICINA { get; set; }
        public string REFERENCIA { get; set; }
        public List<TELEFONOROBOT> TELEFONOS { get; set; }
        public string USUARIO { get; set; }
        public DIRECCIONROBOT()
        {
            this.ID_DIRECCION = 0;
            this.PAIS_RESIDENCIA = string.Empty;//COMBO
            this.CIUDAD_RESIDENCIA = string.Empty;//COMBO
            this.TIPO_DIRECCION = "";//COMBO
            this.DEPARTAMENTO = string.Empty;//COMBO
            this.LOCALIDAD = string.Empty;//COMBO
            this.ZONA_BARRIO = string.Empty;
            this.CALLE_AVENIDA = string.Empty;
            this.NRO_PUERTA = string.Empty;
            this.NRO_PISO = string.Empty;
            this.NRO_DEPARTAMENTO = string.Empty;
            this.NOMBRE_EDIFICIO = string.Empty;
            this.NRO_CASILLA = string.Empty;
            this.TIPO_VIVIENDA_OFICINA = string.Empty;
            this.REFERENCIA = string.Empty;
            this.TELEFONOS = new List<TELEFONOROBOT>();
            this.USUARIO = string.Empty;
        }
    }
    public class ACTIVIDAD_ECONOMICAROBOT
    {
        public int ID_ACTIVIDAD_ECONOMICA { get; set; }
        public CAEDECROBOT CAE_DEC { get; set; }//COMBO
        public string NIVEL_LABORAL { get; set; }//COMBO
        public string NIT { get; set; }
        public string ACTIVIDAD_DECLARADA { get; set; }
        public string PRIORIDAD { get; set; }//COMBO
        public List<FECHA_A_MROBOT> TIEMPO_EXP { get; set; }
        public string ESTADO { get; set; }//COMBO
        public string INGRESOS_MENSUALES { get; set; }//COMBO
        public string OTROS_INGRESOS_MENSUALES { get; set; }
        public string EGRESOS_MENSUALES { get; set; }
        public string MARGEN_AHORRO { get; set; }
        public string NOMBRE_EMPRESA { get; set; }
        public string FECHA_INGRESO { get; set; }
        public DIRECCIONROBOT DIRECCION_ACT_ECO { get; set; }
        public string CARGO { get; set; }
        public string USUARIO { get; set; }
        public ACTIVIDAD_ECONOMICAROBOT()
        {
            this.ID_ACTIVIDAD_ECONOMICA = 0;
            this.CAE_DEC = new CAEDECROBOT();
            this.NIVEL_LABORAL = string.Empty;//COMBO
            this.NIT = string.Empty;
            this.ACTIVIDAD_DECLARADA = string.Empty;
            this.PRIORIDAD = string.Empty;//COMBO
            this.TIEMPO_EXP = new List<FECHA_A_MROBOT>();
            this.ESTADO = string.Empty;//COMBO
            this.INGRESOS_MENSUALES = string.Empty;//COMBO
            this.OTROS_INGRESOS_MENSUALES = string.Empty;
            this.EGRESOS_MENSUALES = string.Empty;
            this.MARGEN_AHORRO = string.Empty;
            this.NOMBRE_EMPRESA = string.Empty;
            this.FECHA_INGRESO = string.Empty;
            this.DIRECCION_ACT_ECO = new DIRECCIONROBOT();
            this.CARGO = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class REFERENCIAROBOT
    {
        public int ID_REFERENCIA { get; set; }
        public string TIPO { get; set; }
        public PERSONAROBOT REFERIDO { get; set; }
        public string RELACION { get; set; }//COMBO
        public string OBSERVACION { get; set; }
        public string CALIFICACION { get; set; }//COMBO
        public string DESCRIPCION_CALIFICACION { get; set; }
        public string USUARIO { get; set; }
        public REFERENCIAROBOT()
        {
            this.ID_REFERENCIA = 0;
            this.TIPO = string.Empty;
            this.REFERIDO = new PERSONAROBOT();
            this.RELACION = string.Empty;//COMBO
            this.OBSERVACION = string.Empty;
            this.CALIFICACION = string.Empty;//COMBO
            this.DESCRIPCION_CALIFICACION = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class TELEFONOROBOT
    {
        public int ID_TELEFONO { get; set; }
        public string TIPO { get; set; }//COMBO
        public string NUMERO { get; set; }
        public string DESCRIPCION { get; set; }
        public string USUARIO { get; set; }
        public TELEFONOROBOT()
        {
            this.ID_TELEFONO = 0;
            this.TIPO = string.Empty;//COMBO
            this.NUMERO = string.Empty;
            this.DESCRIPCION = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class CAEDECROBOT
    {
        public int ID_CAEDEC { get; set; }
        public string COD_CAEDEC { get; set; }//COMBO
        public string ACTIVIDAD_CAEDEC { get; set; }
        public string GRUPO_CAEDEC { get; set; }
        public string SECTOR_ECONOMICO { get; set; }
        public string USUARIO { get; set; }
        public CAEDECROBOT()
        {
            this.ID_CAEDEC = 0;
            this.COD_CAEDEC = string.Empty;//COMBO
            this.ACTIVIDAD_CAEDEC = string.Empty;
            this.GRUPO_CAEDEC = string.Empty;
            this.SECTOR_ECONOMICO = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class DECLARACION_JURADAROBOT
    {
        public int ID_DECLARACION_JURADA { get; set; }
        public string TIPO { get; set; }
        public string PATRIMONIO_ACTIVO { get; set; }
        public string PATRIMONIO_PASIVO { get; set; }
        public string PERSONAL_OCUPADO { get; set; }
        public string TOTAL_INGRESO_VENTAS { get; set; }
        public string OBSERVACIONES { get; set; }
        public string USUARIO { get; set; }
        public DECLARACION_JURADAROBOT()
        {
            this.ID_DECLARACION_JURADA = 0;
            this.TIPO = string.Empty;
            this.PATRIMONIO_ACTIVO = string.Empty;
            this.PATRIMONIO_PASIVO = string.Empty;
            this.PERSONAL_OCUPADO = string.Empty;
            this.TOTAL_INGRESO_VENTAS = string.Empty;
            this.OBSERVACIONES = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class DOCUMENTO_ENTREGADOROBOT
    {
        public int ID_DOCUMENTO_ENTREGADO { get; set; }
        public string TIPO { get; set; }
        public string EXTENSION { get; set; }
        public string ARCHIVO { get; set; }
        public string DOCUMENTO_DESCRIPCION { get; set; }
        public string ENTREGADO { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public string USUARIO { get; set; }
        public DOCUMENTO_ENTREGADOROBOT()
        {
            this.ID_DOCUMENTO_ENTREGADO = 0;
            this.EXTENSION = string.Empty;
            this.ARCHIVO = string.Empty;
            this.DOCUMENTO_DESCRIPCION = string.Empty;
            this.ENTREGADO = string.Empty;
            this.NOMBRE_ARCHIVO = string.Empty;
            this.USUARIO = string.Empty;
        }
    }

    public class AUTORIZACIONROBOT
    {
        public int ID_AUTORIZACION { get; set; }
        public string AUTORIZACION_ESPECIAL { get; set; }//COMBO
        public string DESCRIPCION { get; set; }
        public string USUARIO { get; set; }
        public AUTORIZACIONROBOT()
        {
            this.ID_AUTORIZACION = 0;
            this.AUTORIZACION_ESPECIAL = string.Empty;//COMBO
            this.DESCRIPCION = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class FECHA_A_MROBOT
    {
        public int ID_FECHA_A_M { get; set; }
        public string TIPO { get; set; }//COMBO
        public string ANIO { get; set; }
        public string MES { get; set; }
        public string USUARIO { get; set; }
        public FECHA_A_MROBOT()
        {
            this.ID_FECHA_A_M = 0;
            this.TIPO = string.Empty;//COMBO
            this.ANIO = string.Empty;
            this.MES = string.Empty;
            this.USUARIO = string.Empty;
        }
    }







}
