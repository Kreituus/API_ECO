using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_Eco
{
    public class LISTARCLIENTE : RespuestaGeneral
    {
        public List<CLIENTERESUMEN> CLIENTES_RESUMEN { get; set; }
        public LISTARCLIENTE()
        {
            this.CLIENTES_RESUMEN = new List<CLIENTERESUMEN>();
        }
    }

    public class LISTACLIENTES : RespuestaGeneral
    {
        public List<CLIENTE> LISTARCLIENTES { get; set; }
        public LISTACLIENTES()
        {
            this.LISTARCLIENTES = new List<CLIENTE>();
        }
    }




    public class CLIENTERESUMEN
    {
        public int ID_CLIENTE { get; set; }
        public string ESTADO_ANA { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string CORREO { get; set; }
        public List<DOCUMENTO_PERSONAL> DOCUMENTOS { get; set; }
        public TELEFONO CONTACTO { get; set; }
        public List<CREDITORESUMEN> LISTARCREDITOS { get; set; }
        public List<RESPUESTA_CLIENTE_RESPONSE> ESTADOS_ROBOT { get; set; }
        public CLIENTERESUMEN()
        {
            this.NOMBRE_COMPLETO = String.Empty;
            this.CORREO = String.Empty;
            this.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();
            this.CONTACTO = new TELEFONO();
            this.LISTARCREDITOS = new List<CREDITORESUMEN>();
            this.ESTADOS_ROBOT = new List<RESPUESTA_CLIENTE_RESPONSE>();
        }
    }
    public class BuscarClienteRequest
    {
        public int ID_CLIENTE { get; set; }
        public BuscarClienteRequest()
        {
            this.ID_CLIENTE = 0;
        }
    }

    public class CLIENTERESPONSE : RespuestaGeneral
    {
        public int ID_CLIENTE { get; set; }
        public string ESTADO_ANA { get; set; }
        public PERSONA SOLICITANTE { get; set; }
        public List<ACTIVIDAD_ECONOMICA> ACTIVIDADES_ECONOMICAS { get; set; }
        public List<REFERENCIA> REFERENCIAS { get; set; }
        public List<CREDITO> CREDITOS { get; set; }
        public SCORINGREPONSE PUNTAJE_BUROS { get; set; }
        public ENTREVISTA ENTREVISTA { get; set; }
        public List<RESPUESTA_CLIENTE_RESPONSE> ESTADOS_ROBOT { get; set; }
        public string USUARIO { get; set; }
        public CLIENTERESPONSE()
        {
            this.ID_CLIENTE = 0;
            this.ESTADO_ANA = string.Empty;
            this.SOLICITANTE = new PERSONA();
            this.ACTIVIDADES_ECONOMICAS = new List<ACTIVIDAD_ECONOMICA>();
            this.REFERENCIAS = new List<REFERENCIA>();
            this.CREDITOS = new List<CREDITO>();
            this.PUNTAJE_BUROS = new SCORINGREPONSE();
            this.ENTREVISTA = new ENTREVISTA();
            this.USUARIO = string.Empty;
            this.ESTADOS_ROBOT = new List<RESPUESTA_CLIENTE_RESPONSE>();
        }
    }






    public class CLIENTE
    {
        public int ID_CLIENTE { get; set; }
        public string ESTADO_ANA { get; set; }
        public PERSONA SOLICITANTE { get; set; }
        public List<ACTIVIDAD_ECONOMICA> ACTIVIDADES_ECONOMICAS { get; set; }
        public List<REFERENCIA> REFERENCIAS { get; set; }
        public List<CREDITO> CREDITOS { get; set; }
        public ENTREVISTA ENTREVISTA { get; set; }

        public string USUARIO { get; set; }

        public CLIENTE()
        {
            this.ID_CLIENTE = 0;
            this.ESTADO_ANA = string.Empty;
            this.SOLICITANTE = new PERSONA();
            this.ACTIVIDADES_ECONOMICAS = new List<ACTIVIDAD_ECONOMICA>();
            this.REFERENCIAS = new List<REFERENCIA>();
            this.CREDITOS = new List<CREDITO>();
            this.ENTREVISTA = new ENTREVISTA();
            this.USUARIO = string.Empty;
        }

    }


    public class CREDITO
    {
        public int ID_CREDITO { get; set; }
        public string COD_CREDITO { get; set; }
        //ESTRUCTURA SCI
        public string COD_SCI { get; set; }
        public decimal MONTO_CREDITO { get; set; }
        public string TIPO_OPERACION { get; set; }//COMBO
        public string ESTRATEGIA { get; set; }//COMBO
        public string DESTINO { get; set; }
        public string DESTINO_CLIENTE { get; set; }//COMBO
        public bool COMPRA_PASIVO { get; set; }
        public string OBJETO_OPERACION { get; set; }//COMBO
        public int PLAZO { get; set; }
        public int DIA_PAGO { get; set; }
        public string ANTIGUEDAD_CLIENTE { get; set; }
        public DateTime FECHA_SOLICITUD { get; set; }
        public List<DECLARACION_JURADA> DECLARACIONES { get; set; }
        public string CLIENTE_CPOP { get; set; } //COMBO
        public int NRO_CPOP { get; set; }
        public string TIPO_TASA { get; set; } //COMBO
        public int NRO_AUTORIZACION { get; set; }
        public string TASA_SELECCION { get; set; }
        public int TASA_PP { get; set; }
        public List<DOCUMENTO_ENTREGADO> DOCUMENTOS_ENTREGADOS { get; set; }
        public List<AUTORIZACION> AUTORIZACIONES { get; set; }
        public string ESTADO_CREDITO { get; set; }
        public string ESTADO_SCI { get; set; }
        public string USUARIO { get; set; }

        public CREDITO()
        {
            this.ID_CREDITO = 0;
            this.COD_CREDITO = string.Empty;
            this.COD_SCI = string.Empty;
            this.MONTO_CREDITO = 0;
            this.TIPO_OPERACION = string.Empty;//COMBO
            this.ESTRATEGIA = string.Empty;//COMBO
            this.DESTINO = string.Empty;
            this.DESTINO_CLIENTE = string.Empty;//COMBO
            this.COMPRA_PASIVO = false;
            this.OBJETO_OPERACION = string.Empty;//COMBO
            this.PLAZO = 0;
            this.DIA_PAGO = 0;
            this.ANTIGUEDAD_CLIENTE = string.Empty;
            this.FECHA_SOLICITUD = new DateTime(1900, 01, 01);
            this.DECLARACIONES = new List<DECLARACION_JURADA>();
            this.CLIENTE_CPOP = string.Empty;
            this.NRO_CPOP = 0;
            this.TIPO_TASA = string.Empty;
            this.NRO_AUTORIZACION = 0;
            this.TASA_SELECCION = string.Empty;
            this.TASA_PP = 0;
            this.DOCUMENTOS_ENTREGADOS = new List<DOCUMENTO_ENTREGADO>();
            this.AUTORIZACIONES = new List<AUTORIZACION>();
            this.ESTADO_CREDITO = string.Empty;
            this.ESTADO_SCI = string.Empty;
            this.USUARIO = string.Empty;
        }

    }

    public class CREDITORESUMEN
    {
        public int ID_CREDITO { get; set; }
        public string COD_CREDITO { get; set; }
        //ESTRUCTURA SCI

        public string ESTADO_CREDITO { get; set; }
        public string ESTADO_SCI { get; set; }
        public string USUARIO { get; set; }

        public CREDITORESUMEN()
        {
            this.ID_CREDITO = 0;
            this.COD_CREDITO = string.Empty;
            this.ESTADO_CREDITO = string.Empty;
            this.ESTADO_SCI = string.Empty;
            this.USUARIO = string.Empty;
        }

    }



    public class CAMBIOESTADO
    {
        //public int ID_CAMBIO_ESTADO { get; set; }
        public string TIPO { get; set; }  //SCI,ANA,CREDITO
        public int ID_CLIENTE { get; set; }
        public int ID_CREDITO { get; set; }
        public string USUARIO { get; set; }
        public string NUEVO_ESTADO { get; set; }

        public CAMBIOESTADO()
        {
            //this.ID_CAMBIO_ESTADO = 0;
            this.TIPO = string.Empty;
            this.ID_CLIENTE = 0;
            this.ID_CREDITO = 0;
            this.USUARIO = string.Empty;
            this.NUEVO_ESTADO = string.Empty;
        }

    }

    public class DOCUMENTO_PERSONAL
    {
        public int ID_DOCUMENTO_PERSONAL { get; set; }
        public string TIPO_DOCUMENTO { get; set; }//COMBO
        public string NRO_DOCUMENTO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string EXTENSION { get; set; }//COMBO
        public DateTime FECHA_VENC { get; set; }
        public string ESTADO_CIVIL { get; set; }//COMBO
        public string NACIONALIDAD { get; set; }//COMBO
        public string LUGAR_NACIMIENTO { get; set; }//COMBO
        public DateTime FECHA_NACIMIENTO { get; set; }
        public string USUARIO { get; set; }
        public DOCUMENTO_PERSONAL()
        {
            this.ID_DOCUMENTO_PERSONAL = 0;
            this.TIPO_DOCUMENTO = string.Empty;//COMBO
            this.NRO_DOCUMENTO = string.Empty;
            this.COMPLEMENTO = string.Empty;
            this.EXTENSION = string.Empty;//COMBO
            this.FECHA_VENC = new DateTime(1900, 01, 01);
            this.ESTADO_CIVIL = string.Empty;//COMBO
            this.NACIONALIDAD = string.Empty;//COMBO
            this.LUGAR_NACIMIENTO = string.Empty;//COMBO
            this.FECHA_NACIMIENTO = new DateTime(1900, 01, 01);
            this.USUARIO = string.Empty;
        }
    }
    public class PERSONA
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
        public List<DIRECCION> DIRECCIONES_PERS { get; set; }
        public List<DOCUMENTO_PERSONAL> DOCUMENTOS { get; set; }
        public TELEFONO CONTACTO { get; set; }
        public string USUARIO { get; set; }
        public PERSONA()
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
            this.DIRECCIONES_PERS = new List<DIRECCION>();
            this.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();
            this.CONTACTO = new TELEFONO();
            this.USUARIO = string.Empty;
        }
    }
    public class DIRECCION
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
        public int NRO_CASILLA { get; set; }
        public string TIPO_VIVIENDA_OFICINA { get; set; }
        public string REFERENCIA { get; set; }
        public List<TELEFONO> TELEFONOS { get; set; }
        public string USUARIO { get; set; }
        public DIRECCION()
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
            this.NRO_CASILLA = 0;
            this.TIPO_VIVIENDA_OFICINA = string.Empty;
            this.REFERENCIA = string.Empty;
            this.TELEFONOS = new List<TELEFONO>();
            this.USUARIO = string.Empty;
        }
    }
    public class ACTIVIDAD_ECONOMICA
    {
        public int ID_ACTIVIDAD_ECONOMICA { get; set; }
        public CAEDEC CAE_DEC { get; set; }//COMBO
        public string NIVEL_LABORAL { get; set; }//COMBO
        public int NIT { get; set; }
        public string ACTIVIDAD_DECLARADA { get; set; }
        public string PRIORIDAD { get; set; }//COMBO
        public List<FECHA_A_M> TIEMPO_EXP { get; set; }
        public string ESTADO { get; set; }//COMBO
        public string INGRESOS_MENSUALES { get; set; }//COMBO
        public int OTROS_INGRESOS_MENSUALES { get; set; }
        public int EGRESOS_MENSUALES { get; set; }
        public int MARGEN_AHORRO { get; set; }
        public string NOMBRE_EMPRESA { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public DIRECCION DIRECCION_ACT_ECO { get; set; }
        public string CARGO { get; set; }
        public string USUARIO { get; set; }
        public ACTIVIDAD_ECONOMICA()
        {
            this.ID_ACTIVIDAD_ECONOMICA = 0;
            this.CAE_DEC = new CAEDEC();
            this.NIVEL_LABORAL = string.Empty;//COMBO
            this.NIT = 0;
            this.ACTIVIDAD_DECLARADA = string.Empty;
            this.PRIORIDAD = string.Empty;//COMBO
            this.TIEMPO_EXP = new List<FECHA_A_M>();
            this.ESTADO = string.Empty;//COMBO
            this.INGRESOS_MENSUALES = string.Empty;//COMBO
            this.OTROS_INGRESOS_MENSUALES = 0;
            this.EGRESOS_MENSUALES = 0;
            this.MARGEN_AHORRO = 0;
            this.NOMBRE_EMPRESA = string.Empty;
            this.FECHA_INGRESO = new DateTime(1900, 01, 01);
            this.DIRECCION_ACT_ECO = new DIRECCION();
            this.CARGO = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class REFERENCIA
    {
        public int ID_REFERENCIA { get; set; }
        public string TIPO { get; set; }
        public PERSONA REFERIDO { get; set; }
        public string RELACION { get; set; }//COMBO
        public string OBSERVACION { get; set; }
        public string CALIFICACION { get; set; }//COMBO
        public string DESCRIPCION_CALIFICACION { get; set; }
        public string USUARIO { get; set; }
        public REFERENCIA()
        {
            this.ID_REFERENCIA = 0;
            this.TIPO = string.Empty;
            this.REFERIDO = new PERSONA();
            this.RELACION = string.Empty;//COMBO
            this.OBSERVACION = string.Empty;
            this.CALIFICACION = string.Empty;//COMBO
            this.DESCRIPCION_CALIFICACION = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class TELEFONO
    {
        public int ID_TELEFONO { get; set; }
        public string TIPO { get; set; }//COMBO
        public string NUMERO { get; set; }
        public string DESCRIPCION { get; set; }
        public string USUARIO { get; set; }
        public TELEFONO()
        {
            this.ID_TELEFONO = 0;
            this.TIPO = string.Empty;//COMBO
            this.NUMERO = string.Empty;
            this.DESCRIPCION = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class CAEDEC
    {
        public int ID_CAEDEC { get; set; }
        public string COD_CAEDEC { get; set; }//COMBO
        public string ACTIVIDAD_CAEDEC { get; set; }
        public string GRUPO_CAEDEC { get; set; }
        public string SECTOR_ECONOMICO { get; set; }
        public string USUARIO { get; set; }
        public CAEDEC()
        {
            this.ID_CAEDEC = 0;
            this.COD_CAEDEC = string.Empty;//COMBO
            this.ACTIVIDAD_CAEDEC = string.Empty;
            this.GRUPO_CAEDEC = string.Empty;
            this.SECTOR_ECONOMICO = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class DECLARACION_JURADA
    {
        public int ID_DECLARACION_JURADA { get; set; }
        public string TIPO { get; set; }
        public int PATRIMONIO_ACTIVO { get; set; }
        public int PATRIMONIO_PASIVO { get; set; }
        public int PERSONAL_OCUPADO { get; set; }
        public int TOTAL_INGRESO_VENTAS { get; set; }
        public string OBSERVACIONES { get; set; }
        public string USUARIO { get; set; }
        public DECLARACION_JURADA()
        {
            this.ID_DECLARACION_JURADA = 0;
            this.TIPO = string.Empty;
            this.PATRIMONIO_ACTIVO = 0;
            this.PATRIMONIO_PASIVO = 0;
            this.PERSONAL_OCUPADO = 0;
            this.TOTAL_INGRESO_VENTAS = 0;
            this.OBSERVACIONES = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class DOCUMENTO_ENTREGADO
    {
        public int ID_DOCUMENTO_ENTREGADO { get; set; }
        public string TIPO { get; set; }
        public string EXTENSION { get; set; }
        public string ARCHIVO { get; set; }
        public string DOCUMENTO_DESCRIPCION { get; set; }
        public bool ENTREGADO { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public string USUARIO { get; set; }
        public DOCUMENTO_ENTREGADO()
        {
            this.ID_DOCUMENTO_ENTREGADO = 0;
            this.EXTENSION = string.Empty;
            this.ARCHIVO = string.Empty;
            this.DOCUMENTO_DESCRIPCION = string.Empty;
            this.ENTREGADO = false;
            this.NOMBRE_ARCHIVO = string.Empty;
            this.USUARIO = string.Empty;
        }
    }

    public class AUTORIZACION
    {
        public int ID_AUTORIZACION { get; set; }
        public bool HABILITADO { get; set; }
        public string AUTORIZACION_ESPECIAL { get; set; }//COMBO
        public string DESCRIPCION { get; set; }
        public string USUARIO { get; set; }
        public AUTORIZACION()
        {
            this.ID_AUTORIZACION = 0;
            this.AUTORIZACION_ESPECIAL = string.Empty;//COMBO
            this.DESCRIPCION = string.Empty;
            this.USUARIO = string.Empty;
        }
    }
    public class FECHA_A_M
    {
        public int ID_FECHA_A_M { get; set; }
        public string TIPO { get; set; }//COMBO
        public int ANIO { get; set; }
        public int MES { get; set; }
        public string USUARIO { get; set; }
        public FECHA_A_M()
        {
            this.ID_FECHA_A_M = 0;
            this.TIPO = string.Empty;//COMBO
            this.ANIO = 0;
            this.MES = 0;
            this.USUARIO = string.Empty;
        }
    }

    public class RESPUESTA_CLIENTE_REQUEST
    {
        public bool SUCCESS { get; set; }
        public string MENSAJE { get; set; }
        public string CODIGO { get; set; }
        public int ID_CLIENTE { get; set; }
        public string USUARIO { get; set; }

        public RESPUESTA_CLIENTE_REQUEST()
        {
            this.SUCCESS = false;
            this.MENSAJE = String.Empty;
            this.CODIGO = string.Empty;
            this.ID_CLIENTE = 0;
            this.USUARIO = string.Empty;
        }
    }

    public class RESPUESTA_CLIENTE
    {
        public bool SUCCESS { get; set; }
        public string MENSAJE { get; set; }
        public string CODIGO { get; set; }
        public int ID_CLIENTE { get; set; }
        public string USUARIO { get; set; }

        public RESPUESTA_CLIENTE()
        {
            this.SUCCESS = false;
            this.MENSAJE = String.Empty;
            this.CODIGO = string.Empty;
            this.ID_CLIENTE = 0;
            this.USUARIO = string.Empty;
        }
    }

    public class RESPUESTA_CLIENTE_RESPONSE
    {
        public int ID_RESPUESTA_CLIENTE { get; set; }
        public string CATEGORIA { get; set; }
        public bool SUCCESS { get; set; }
        public string MENSAJE { get; set; }
        public string CODIGO { get; set; }
        public RESPUESTA_CLIENTE_RESPONSE()
        {
            this.ID_RESPUESTA_CLIENTE = 0;
            this.SUCCESS = false;
            this.MENSAJE = String.Empty;
            this.CODIGO = string.Empty;
        }
    }
}


