using Access_Eco;
using Common_Eco;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using Entidades_Eco;
using Newtonsoft.Json;
using Service_Eco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataTable = System.Data.DataTable;

namespace Business_Eco
{

    public interface IBussinessPendientes
    {
        #region ROBOT
        Task<RespuestaGeneral> CambioEstadoAna(CAMBIOESTADOANA request);
        Task<RespuestaGeneral> InsertarScoring(SCORINGREQUEST request);
        Task<RespuestaGeneral> CambioEstadoAnaProblemas(CAMBIOESTADOANAPROBLEMAS request);
        Task<RespuestaGeneral> CambioEstadoSCI(CAMBIOESTADOSCI request);
        Task<RespuestaGeneral> CambioEstadoSCIProblemas(CAMBIOESTADOSCIPROBLEMAS request);
        Task<CLIENTERESPONSEANA> ListaClienteAna(string ESTADO);
        Task<CLIENTEROBOT> ListaClienteAnaRobot(string ESTADO);
        Task<CLIENTERESPONSEANAVALIDAR> ListaClienteAnaValidar(string ESTADO);
        Task<CLIENTERESPONSESCI> ListaClienteSCI();
        Task<RespuestaGeneral> ModificarClienteValidar(CLIENTERESPONSEANAVALIDARRESPONSE cliente);
        Task<RespuestaGeneral> ModificarClienteActualizar(RESPUESTA_CLIENTE_REQUEST cliente);
        #endregion

        #region LOGIN
        ResponseUser Logueo_User(RequestUser request);
        #endregion

        #region CLIENTE
        Task<RespuestaGeneral> CreditoCambioEstado(CAMBIOESTADO request);
        Task<RespuestaGeneral> InsertarCliente(CLIENTE request);
        Task<RespuestaGeneral> ModificarCliente(CLIENTE request);
        Task<LISTARCLIENTE> ListarCliente();
        Task<CLIENTERESPONSE> BuscarCliente(BuscarClienteRequest request);
        Task<CLIENTERESPONSE> BuscarClienteValidar(BuscarClienteRequest request);
        string CodigoScoring(string AREA, string BUSCAR);
        string DescripcionScoring(string BUSCAR, ref string AREA);
        #endregion

        #region ENTREVISTA
        #region Verificacion Cliente
        string CodigoVeriCliente(string BUSCAR);
        string DescripcionVeriCliente(string BUSCAR);
        #endregion
        #region Seguro Desgravame
        string CodigoSeguroDesgravame(string BUSCAR);
        string DescripcionSeguroDesgravame(string BUSCAR);
        #endregion
        #region Script Evaluacion
        string CodigoScripEvaluacion(string BUSCAR);
        string DescripcionScripEvaluacion(string BUSCAR);
        #endregion

        #endregion

        #region COMBOS
        Task<ComboCredito> ComboCredito();
        Task<ComboDocumentoPersonal> ComboDocumentoPersonal();
        Task<ComboPersona> ComboPersona();
        Task<ComboDireccion> ComboDireccion();
        Task<ComboActividadEconomica> ComboActividadEconomica();
        Task<ComboReferencia> ComboReferencia();
        Task<ComboTelefono> ComboTelefono();
        Task<ComboCaedec> ComboCaedec();
        Task<ComboAutorizacionesEspeciales> ComboAutorizacionesEspeciales();
        Task<ComboTipoFechaAM> ComboTipoFechaAM();
        Task<ComboDocumentosEntregados> ComboDocumentosEntregados();
        Task<ComboScoring> ComboScoring();
        Task<ComboCaedec> BusquedaCaedec(BusquedaCaedecRequest request);
        int BuscarPersonaCliente(string NRO_DOCUMENTO, string COMPLEMENTO, string EXTENSION);
        Task<ComboEntrevista> ComboEntrevista();

        #endregion

        #region OTROS
        CLIENTEROBOT CompararCliente(CLIENTE A, CLIENTE B);
        PERSONAROBOT CompararPersona(PERSONA A, PERSONA B);
        DIRECCIONROBOT CompararDireccion(DIRECCION A, DIRECCION B);
        TELEFONOROBOT Comparartelefono(TELEFONO A, TELEFONO B);
        CREDITOROBOT CompararCredito(CREDITO A, CREDITO B);
        DOCUMENTO_PERSONALROBOT CompararDocumentoPersonal(DOCUMENTO_PERSONAL A, DOCUMENTO_PERSONAL B);
        ACTIVIDAD_ECONOMICAROBOT CompararActividadEconomica(ACTIVIDAD_ECONOMICA A, ACTIVIDAD_ECONOMICA B);
        REFERENCIAROBOT CompararReferencia(REFERENCIA A, REFERENCIA B);
        CAEDECROBOT CompararCaedec(CAEDEC A, CAEDEC B);
        DECLARACION_JURADAROBOT CompararDeclaracionJurada(DECLARACION_JURADA A, DECLARACION_JURADA B);
        DOCUMENTO_ENTREGADOROBOT CompararDocumentoEntregado(DOCUMENTO_ENTREGADO A, DOCUMENTO_ENTREGADO B);
        AUTORIZACIONROBOT CompararAutorizacion(AUTORIZACION A, AUTORIZACION B);
        FECHA_A_MROBOT CompararFecha_A_m(FECHA_A_M A, FECHA_A_M B);


        #endregion
    }
    public class BussinessPendientes : IBussinessPendientes
    {
        AccessBD _conectorBD;

        DataConsultaServicio _conectorServices;
        public BussinessPendientes()
        {
            _conectorBD = new AccessBD();
            _conectorServices = new DataConsultaServicio();
        }

        #region ROBOT
        public async Task<RespuestaGeneral> CambioEstadoAna(CAMBIOESTADOANA request)
        {
            RespuestaGeneral response = new RespuestaGeneral();
            DataTable clienteresponse = new DataTable();
            DataTable personaresponse = new DataTable();
            CAMBIOESTADOROBOT robot = new CAMBIOESTADOROBOT();
            DataTable respuestaclientedata = new DataTable();

            

            try
            {
                robot.TIPO = "ANA";
                robot.ID_CLIENTE = request.ID_CLIENTE;
                robot.ID_CREDITO = 0;
                robot.USUARIO = request.ROBOT;
                robot.NUEVO_ESTADO = "FINALIZADO";
                robot.DESCRIPCION = "";

                clienteresponse = _conectorBD.ClienteCambioEstadoRobot(robot);

                if (clienteresponse.Rows.Count!=0)
                {
                    foreach (DataRow item in clienteresponse.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }
                        else
                        {
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                    }
                }

                personaresponse = _conectorBD.ActualizarCodigoAnaPersona(request);

                if (personaresponse.Rows.Count!=0)
                {
                    foreach (DataRow item in personaresponse.Rows)
                    {
                        if (item.Field<int>("CODIGO") != 1)
                        {
                            response.message = item.Field<string>("MENSAJE");
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                    }
                }

                RESPUESTA_CLIENTE_REQUEST cliente = new RESPUESTA_CLIENTE_REQUEST();
                cliente.SUCCESS = true;
                
                cliente.MENSAJE = "";
                cliente.CODIGO = "200";
                cliente.ID_CLIENTE = request.ID_CLIENTE;
                cliente.USUARIO = request.ROBOT;

                respuestaclientedata = _conectorBD.ModificarRespuestaCliente(cliente, "PENDIENTE");
                if (respuestaclientedata.Rows.Count != 0)
                {
                    foreach (DataRow item in respuestaclientedata.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        if (item.Field<int>("CODIGO") == -1)
                        {
                            response.message = item.Field<string>("MENSAJE");
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                        if (item.Field<int>("CODIGO") == -2)
                        {
                            if (!string.IsNullOrEmpty(cliente.MENSAJE) ||
                                !string.IsNullOrEmpty(cliente.CODIGO) ||
                                !string.IsNullOrEmpty(cliente.USUARIO) ||
                                cliente.ID_CLIENTE != 0)
                            {
                                respuestaclientedata = _conectorBD.InsertarRespuestaCliente(cliente, "PENDIENTE");
                                if (respuestaclientedata.Rows.Count != 0)
                                {
                                    foreach (DataRow cli in respuestaclientedata.Rows)
                                    {
                                        response.message = cli.Field<string>("MENSAJE");
                                        if (cli.Field<int>("CODIGO") == 1)
                                        {
                                            response.code = Configuraciones.GetCode("OK");
                                            response.success = true;
                                        }
                                        else
                                        {
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                            return response;
                                        }
                                    }
                                }
                            }
                        }
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }

                    }
                }



            }
            catch (Exception ex)
            {

                throw;
            }
            return response;

        }
        public async Task<RespuestaGeneral> InsertarScoring(SCORINGREQUEST request)
        {
            RespuestaGeneral response = new RespuestaGeneral();
            DataTable scoringdata = new DataTable();
            string CRITERIO = string.Empty;
            string RESULTADO = string.Empty;
            string PUNTAJE = string.Empty;
            SCORINGRESPONSE respuesta = new SCORINGRESPONSE();

            try
            {
                if (request.CIC.Count!=0)
                {
                    foreach (var item in request.CIC)
                    {
                        if (string.IsNullOrEmpty(CRITERIO))
                        {

                            CRITERIO = CodigoScoring("CIC", item.CRITERIO);
                            RESULTADO = item.RESULTADO;
                            PUNTAJE = item.PUNTAJE;
                        }
                        else
                        {
                            CRITERIO = CRITERIO + " | " + CodigoScoring("CIC", item.CRITERIO);
                            RESULTADO = RESULTADO + " | " + item.RESULTADO;
                            PUNTAJE = PUNTAJE + " | " + item.PUNTAJE;
                        }
                    }
                }
                if (request.Infocred.Count!=0)
                {
                    foreach (var item in request.Infocred)
                    {
                        if (string.IsNullOrEmpty(CRITERIO))
                        {

                            CRITERIO = CodigoScoring("Infocred", item.CRITERIO);
                            RESULTADO = item.RESULTADO;
                            PUNTAJE = item.PUNTAJE;
                        }
                        else
                        {
                            CRITERIO = CRITERIO + " | " + CodigoScoring("Infocred", item.CRITERIO);
                            RESULTADO = RESULTADO + " | " + item.RESULTADO;
                            PUNTAJE = PUNTAJE + " | " + item.PUNTAJE;
                        }
                    }
                }
                if (request.Hoja_Riesgos.Count!=0)
                {
                    foreach (var item in request.Hoja_Riesgos)
                    {
                        if (string.IsNullOrEmpty(CRITERIO))
                        {

                            CRITERIO = CodigoScoring("Hoja Riesgos", item.CRITERIO);
                            RESULTADO = item.RESULTADO;
                            PUNTAJE = item.PUNTAJE;
                        }
                        else
                        {
                            CRITERIO = CRITERIO + " | " + CodigoScoring("Hoja Riesgos", item.CRITERIO);
                            RESULTADO = RESULTADO + " | " + item.RESULTADO;
                            PUNTAJE = PUNTAJE + " | " + item.PUNTAJE;
                        }
                    }
                }

                if (request.Puntaje.Count!=0)
                {
                    foreach (var item in request.Puntaje)
                    {
                        if (string.IsNullOrEmpty(CRITERIO))
                        {

                            CRITERIO = CodigoScoring("General", item.CRITERIO);
                            RESULTADO = item.RESULTADO;
                            PUNTAJE = item.PUNTAJE;
                        }
                        else
                        {
                            CRITERIO = CRITERIO + " | " + CodigoScoring("General", item.CRITERIO);
                            RESULTADO = RESULTADO + " | " + item.RESULTADO;
                            PUNTAJE = PUNTAJE + " | " + item.PUNTAJE;
                        }
                    }
                }
                
                
                
                if (request!=null)
                {
                    respuesta.ID_CLIENTE = BuscarPersonaCliente(request.NRO_DOCUMENTO,request.COMPLEMENTO,request.EXTENSION);
                    respuesta.CRITERIO = CRITERIO;
                    respuesta.RESULTADO = RESULTADO;
                    respuesta.PUNTAJE = PUNTAJE;
                    respuesta.TICKET = request.TICKET;
                    respuesta.USUARIO = request.USUARIO;

                        scoringdata = _conectorBD.InsertarScoring(respuesta);

                        if (scoringdata.Rows.Count!=0)
                        {
                            foreach (DataRow item in scoringdata.Rows)
                            {
                                response.message = item.Field<string>("MENSAJE");
                                if (item.Field<int>("CODIGO") == 1)
                                {
                                    response.code = Configuraciones.GetCode("OK");
                                    response.success = true;
                                }
                                else
                                {
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                            }
                        }
                    
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }
        public async Task<RespuestaGeneral> CambioEstadoAnaProblemas(CAMBIOESTADOANAPROBLEMAS request)
        {
            RespuestaGeneral response = new RespuestaGeneral();
            DataTable clienteresponse = new DataTable();
            DataTable respuestaclientedata = new DataTable();
            CAMBIOESTADOROBOT robot = new CAMBIOESTADOROBOT();
            try
            {
                robot.TIPO = "ANA";
                robot.ID_CLIENTE = request.ID_CLIENTE;
                robot.ID_CREDITO = 0;
                robot.USUARIO = request.ROBOT;
                robot.NUEVO_ESTADO = "ERROR";
                robot.DESCRIPCION = request.DESCRIPCION;

                clienteresponse = _conectorBD.ClienteCambioEstadoRobot(robot);

                if (clienteresponse.Rows.Count!=0)
                {
                    foreach (DataRow item in clienteresponse.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }
                        else
                        {
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                    }
                }

                RESPUESTA_CLIENTE_REQUEST cliente = new RESPUESTA_CLIENTE_REQUEST();
                cliente.SUCCESS = true;

                cliente.MENSAJE = request.DESCRIPCION;
                cliente.CODIGO = "500";
                cliente.ID_CLIENTE = request.ID_CLIENTE;
                cliente.USUARIO = request.ROBOT;

                respuestaclientedata = _conectorBD.ModificarRespuestaCliente(cliente, "PENDIENTE");
                if (respuestaclientedata.Rows.Count != 0)
                {
                    foreach (DataRow item in respuestaclientedata.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        if (item.Field<int>("CODIGO") == -1)
                        {
                            response.message = item.Field<string>("MENSAJE");
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                        if (item.Field<int>("CODIGO") == -2)
                        {
                            if (!string.IsNullOrEmpty(cliente.MENSAJE) ||
                                !string.IsNullOrEmpty(cliente.CODIGO) ||
                                !string.IsNullOrEmpty(cliente.USUARIO) ||
                                cliente.ID_CLIENTE != 0)
                            {
                                respuestaclientedata = _conectorBD.InsertarRespuestaCliente(cliente, "PENDIENTE");
                                if (respuestaclientedata.Rows.Count != 0)
                                {
                                    foreach (DataRow cli in respuestaclientedata.Rows)
                                    {
                                        response.message = cli.Field<string>("MENSAJE");
                                        if (cli.Field<int>("CODIGO") == 1)
                                        {
                                            response.code = Configuraciones.GetCode("OK");
                                            response.success = true;
                                        }
                                        else
                                        {
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                            return response;
                                        }
                                    }
                                }
                            }
                        }
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }

                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }
        public async Task<RespuestaGeneral> CambioEstadoSCI(CAMBIOESTADOSCI request)
        {
            RespuestaGeneral response = new RespuestaGeneral();
            DataTable creditoresponse = new DataTable();
            DataTable credresponse = new DataTable();
            CAMBIOESTADOROBOT robot = new CAMBIOESTADOROBOT();
            try
            {
                robot.TIPO = "SCI";
                robot.ID_CLIENTE = 0;
                robot.ID_CREDITO = request.ID_CREDITO;
                robot.USUARIO = request.ROBOT;
                robot.NUEVO_ESTADO = "FINALIZADO";
                robot.DESCRIPCION = "";

                creditoresponse = _conectorBD.CreditoCambioEstadoRobot(robot);

                if (creditoresponse.Rows.Count!=0)
                {
                    foreach (DataRow item in creditoresponse.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }
                        else
                        {
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                    }
                }


                credresponse = _conectorBD.ActualizarCodigoSCICredito(request);

                if (credresponse.Rows.Count!=0)
                {
                    foreach (DataRow item in credresponse.Rows)
                    {
                        if (item.Field<int>("CODIGO") != 1)
                        {
                            response.message = item.Field<string>("MENSAJE");
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }
        public async Task<RespuestaGeneral> CambioEstadoSCIProblemas(CAMBIOESTADOSCIPROBLEMAS request)
        {
            RespuestaGeneral response = new RespuestaGeneral();
            DataTable creditoresponse = new DataTable();
            CAMBIOESTADOROBOT robot = new CAMBIOESTADOROBOT();
            try
            {
                robot.TIPO = "SCI";
                robot.ID_CLIENTE = 0;
                robot.ID_CREDITO = request.ID_CREDITO;
                robot.USUARIO = request.ROBOT;
                robot.NUEVO_ESTADO = "ERROR";
                robot.DESCRIPCION = request.DESCRIPCION;

                creditoresponse = _conectorBD.CreditoCambioEstadoRobot(robot);

                if (creditoresponse.Rows.Count!=0)
                {
                    foreach (DataRow item in creditoresponse.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }
                        else
                        {
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }
        public async Task<CLIENTERESPONSEANA> ListaClienteAna(string ESTADO)
        {
            //ListarCredito ListarCredito = new ListarCredito();
            //ListarCredito.LISTARCREDITOS = new List<CREDITO>();
            //CREDITORESPONSE respuesta = new CREDITORESPONSE();

            DataTable data = new DataTable();
            DataTable personadata = new DataTable();
            DataTable creditodata = new DataTable();
            DataTable telefonodata = new DataTable();
            DataTable direcciondata = new DataTable();
            DataTable documentopersonaldata = new DataTable();
            DataTable actividadeconomicadata = new DataTable();
            DataTable caedecdata = new DataTable();
            DataTable fechaAMdata = new DataTable();
            DataTable direccionAEdata = new DataTable();
            DataTable referenciadata = new DataTable();
            DataTable personareferenciadata = new DataTable();
            DataTable declaracionjuradadata = new DataTable();
            DataTable autorizaciondata = new DataTable();
            DataTable documentoentregadodata = new DataTable();

            CLIENTERESPONSEANA respuesta = new CLIENTERESPONSEANA();


            try
            {
                data = _conectorBD.ListaClienteAna(ESTADO);

                if (data.Rows.Count!=0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        //CLIENTE respuesta = new CLIENTE();
                        //respuesta.SOLICITANTE = new PERSONA();
                        respuesta.ACTIVIDADES_ECONOMICAS = new List<ACTIVIDAD_ECONOMICA>();
                        respuesta.REFERENCIAS = new List<REFERENCIA>();


                        //respuesta = null;
                        respuesta.ID_CLIENTE = item.Field<int>("ID_CLIENTE");
                        //respuesta.ESTADO_ANA = item.Field<string>("ESTADO_ANA").ToString();




                        personadata = _conectorBD.ListarPersona(respuesta.ID_CLIENTE);

                        foreach (DataRow per in personadata.Rows)
                        {
                            PERSONA persona = new PERSONA();

                            persona.DIRECCIONES_PERS = new List<DIRECCION>();
                            persona.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();


                            persona.ID_PERSONA = per.Field<int>("ID_PERSONA");
                            persona.GENERO = string.IsNullOrEmpty(per.Field<string>("GENERO")) ? "" : per.Field<string>("GENERO").ToString().Trim();
                            persona.PRIMER_APELLIDO = string.IsNullOrEmpty(per.Field<string>("PRIMER_APELLIDO")) ? "" : per.Field<string>("PRIMER_APELLIDO").ToString().Trim();
                            persona.SEGUNDO_APELLIDO = string.IsNullOrEmpty(per.Field<string>("SEGUNDO_APELLIDO")) ? "" : per.Field<string>("SEGUNDO_APELLIDO").ToString().Trim();
                            persona.NOMBRES = string.IsNullOrEmpty(per.Field<string>("NOMBRES")) ? "" : per.Field<string>("NOMBRES").ToString().Trim();
                            persona.CASADA_APELLIDO = string.IsNullOrEmpty(per.Field<string>("CASADA_APELLIDO")) ? "" : per.Field<string>("CASADA_APELLIDO").ToString().Trim();
                            persona.NOMBRE_COMPLETO = string.IsNullOrEmpty(per.Field<string>("NOMBRE_COMPLETO")) ? "" : per.Field<string>("NOMBRE_COMPLETO").ToString().Trim();

                            persona.CORREO = string.IsNullOrEmpty(per.Field<string>("CORREO")) ? "" : per.Field<string>("CORREO").ToString().Trim();
                            persona.NIVEL_EDUCACION = string.IsNullOrEmpty(per.Field<string>("NIVEL_EDUCACION")) ? "" : per.Field<string>("NIVEL_EDUCACION").ToString().Trim();
                            persona.PROFESION = string.IsNullOrEmpty(per.Field<string>("PROFESION")) ? "" : per.Field<string>("PROFESION").ToString().Trim();
                            persona.ESTADO_CIVIL = string.IsNullOrEmpty(per.Field<string>("ESTADO_CIVIL")) ? "" : per.Field<string>("ESTADO_CIVIL").ToString().Trim();
                            persona.COD_ANA = string.IsNullOrEmpty(per.Field<string>("COD_ANA")) ? "" : per.Field<string>("COD_ANA").ToString().Trim();
                            persona.USUARIO = string.Empty;

                            direcciondata = _conectorBD.ListarDireccionPersona(persona.ID_PERSONA);
                            persona.DIRECCIONES_PERS.Clear();
                            foreach (DataRow dire in direcciondata.Rows)
                            {
                                DIRECCION direccion = new DIRECCION();
                                direccion.TELEFONOS = new List<TELEFONO>();

                                direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                                direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                                direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                                direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                                direccion.USUARIO = string.Empty;
                                telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                direccion.TELEFONOS.Clear();
                                foreach (DataRow tele in telefonodata.Rows)
                                {
                                    TELEFONO telefono = new TELEFONO();
                                    telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                    telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                    telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                    telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                    telefono.USUARIO = string.Empty;
                                    direccion.TELEFONOS.Add(telefono);

                                }


                                persona.DIRECCIONES_PERS.Add(direccion);
                            }
                            documentopersonaldata = _conectorBD.ListarDocumentoPersonal(persona.ID_PERSONA);
                            persona.DOCUMENTOS.Clear();
                            foreach (DataRow doc in documentopersonaldata.Rows)
                            {
                                DOCUMENTO_PERSONAL documento_personal = new DOCUMENTO_PERSONAL();

                                documento_personal.ID_DOCUMENTO_PERSONAL = doc.Field<int>("ID_DOCUMENTO_PERSONAL");
                                documento_personal.TIPO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("TIPO_DOCUMENTO")) ? "" : doc.Field<string>("TIPO_DOCUMENTO").ToString().Trim();
                                documento_personal.NRO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("NRO_DOCUMENTO")) ? "" : doc.Field<string>("NRO_DOCUMENTO").ToString().Trim();
                                documento_personal.COMPLEMENTO = string.IsNullOrEmpty(doc.Field<string>("COMPLEMENTO")) ? "" : doc.Field<string>("COMPLEMENTO").ToString().Trim();
                                documento_personal.EXTENSION = string.IsNullOrEmpty(doc.Field<string>("EXTENSION")) ? "" : doc.Field<string>("EXTENSION").ToString().Trim();
                                documento_personal.FECHA_VENC = doc.Field<DateTime>("FECHA_VENC");
                                documento_personal.ESTADO_CIVIL = string.IsNullOrEmpty(doc.Field<string>("ESTADO_CIVIL")) ? "" : doc.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                documento_personal.NACIONALIDAD = string.IsNullOrEmpty(doc.Field<string>("NACIONALIDAD")) ? "" : doc.Field<string>("NACIONALIDAD").ToString().Trim();
                                documento_personal.LUGAR_NACIMIENTO = string.IsNullOrEmpty(doc.Field<string>("LUGAR_NACIMIENTO")) ? "" : doc.Field<string>("LUGAR_NACIMIENTO").ToString().Trim();
                                documento_personal.FECHA_NACIMIENTO = doc.Field<DateTime>("FECHA_NACIMIENTO");
                                documento_personal.USUARIO = string.Empty;
                                persona.DOCUMENTOS.Add(documento_personal);

                            }

                            telefonodata = _conectorBD.ListarTelefonoPersona(persona.ID_PERSONA);
                            foreach (DataRow tele in telefonodata.Rows)
                            {
                                TELEFONO telefono = new TELEFONO();
                                telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                telefono.USUARIO = string.Empty;
                                persona.CONTACTO = telefono;

                            }
                            persona.USUARIO = String.Empty;
                            respuesta.DATOS_GENERALES = persona;
                        }

                        actividadeconomicadata = _conectorBD.ListarActividadEconomica(respuesta.ID_CLIENTE);
                        respuesta.ACTIVIDADES_ECONOMICAS.Clear();
                        foreach (DataRow ae in actividadeconomicadata.Rows)
                        {
                            ACTIVIDAD_ECONOMICA actividad_economica = new ACTIVIDAD_ECONOMICA();
                            actividad_economica.CAE_DEC = new CAEDEC();
                            actividad_economica.TIEMPO_EXP = new List<FECHA_A_M>();
                            actividad_economica.DIRECCION_ACT_ECO = new DIRECCION();

                            actividad_economica.ID_ACTIVIDAD_ECONOMICA = ae.Field<int>("ID_ACTIVIDAD_ECONOMICA");

                            caedecdata = _conectorBD.ListarCaedec(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                            foreach (DataRow cae in caedecdata.Rows)
                            {
                                CAEDEC caedec = new CAEDEC();
                                caedec.ID_CAEDEC = cae.Field<int>("ID_CAEDEC");
                                caedec.COD_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("COD_CAEDEC")) ? "" : cae.Field<string>("COD_CAEDEC").ToString().Trim();
                                caedec.ACTIVIDAD_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("ACTIVIDAD_CAEDEC")) ? "" : cae.Field<string>("ACTIVIDAD_CAEDEC").ToString().Trim();
                                caedec.GRUPO_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("GRUPO_CAEDEC")) ? "" : cae.Field<string>("GRUPO_CAEDEC").ToString().Trim();
                                caedec.SECTOR_ECONOMICO = string.IsNullOrEmpty(cae.Field<string>("SECTOR_ECONOMICO")) ? "" : cae.Field<string>("SECTOR_ECONOMICO").ToString().Trim();
                                caedec.USUARIO = string.Empty;
                                actividad_economica.CAE_DEC = caedec;

                            }

                            actividad_economica.NIVEL_LABORAL = string.IsNullOrEmpty(ae.Field<string>("NIVEL_LABORAL")) ? "" : ae.Field<string>("NIVEL_LABORAL").ToString().Trim();
                            actividad_economica.NIT = ae.Field<int>("NIT");
                            actividad_economica.ACTIVIDAD_DECLARADA = string.IsNullOrEmpty(ae.Field<string>("ACTIVIDAD_DECLARADA")) ? "" : ae.Field<string>("ACTIVIDAD_DECLARADA").ToString().Trim();
                            actividad_economica.PRIORIDAD = string.IsNullOrEmpty(ae.Field<string>("PRIORIDAD")) ? "" : ae.Field<string>("PRIORIDAD").ToString().Trim();

                            fechaAMdata = _conectorBD.ListarFechaAM(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                            actividad_economica.TIEMPO_EXP.Clear();
                            foreach (DataRow fec in fechaAMdata.Rows)
                            {
                                FECHA_A_M fecha_a_m = new FECHA_A_M();
                                fecha_a_m.ID_FECHA_A_M = fec.Field<int>("ID_FECHA_A_M");
                                fecha_a_m.TIPO = string.IsNullOrEmpty(fec.Field<string>("TIPO")) ? "" : fec.Field<string>("TIPO").ToString().Trim();
                                fecha_a_m.ANIO = fec.Field<int>("ANIO");
                                fecha_a_m.MES = fec.Field<int>("MES");
                                fecha_a_m.USUARIO = string.Empty;
                                actividad_economica.TIEMPO_EXP.Add(fecha_a_m);

                            }
                            actividad_economica.ESTADO = string.IsNullOrEmpty(ae.Field<string>("ESTADO")) ? "" : ae.Field<string>("ESTADO").ToString().Trim();
                            actividad_economica.INGRESOS_MENSUALES = string.IsNullOrEmpty(ae.Field<string>("INGRESOS_MENSUALES")) ? "0" : ae.Field<string>("INGRESOS_MENSUALES").ToString().Trim();
                            actividad_economica.OTROS_INGRESOS_MENSUALES = ae.Field<int>("OTROS_INGRESOS_MENSUALES");
                            actividad_economica.EGRESOS_MENSUALES = ae.Field<int>("EGRESOS_MENSUALES");
                            actividad_economica.MARGEN_AHORRO = ae.Field<int>("MARGEN_AHORRO");
                            actividad_economica.NOMBRE_EMPRESA = string.IsNullOrEmpty(ae.Field<string>("NOMBRE_EMPRESA")) ? "" : ae.Field<string>("NOMBRE_EMPRESA").ToString().Trim();
                            actividad_economica.FECHA_INGRESO = ae.Field<DateTime>("FECHA_INGRESO");
                            actividad_economica.USUARIO = string.Empty;
                            direccionAEdata = _conectorBD.ListarDireccionActividadEconomica(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                            foreach (DataRow dire in direccionAEdata.Rows)
                            {
                                DIRECCION direccion = new DIRECCION();
                                direccion.TELEFONOS = new List<TELEFONO>();
                                direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                                direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                                direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                                direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                                direccion.USUARIO = string.Empty;
                                telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                direccion.TELEFONOS.Clear();
                                foreach (DataRow tele in telefonodata.Rows)
                                {
                                    TELEFONO telefono = new TELEFONO();
                                    telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                    telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                    telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                    telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                    telefono.USUARIO = string.Empty;
                                    direccion.TELEFONOS.Add(telefono);

                                }

                                actividad_economica.DIRECCION_ACT_ECO = direccion;
                            }

                            actividad_economica.CARGO = string.IsNullOrEmpty(ae.Field<string>("CARGO")) ? "" : ae.Field<string>("CARGO").ToString().Trim();
                            actividad_economica.USUARIO = String.Empty;

                            respuesta.ACTIVIDADES_ECONOMICAS.Add(actividad_economica);

                        }

                        referenciadata = _conectorBD.ListarReferencia(respuesta.ID_CLIENTE);
                        respuesta.REFERENCIAS.Clear();
                        foreach (DataRow refe in referenciadata.Rows)
                        {
                            REFERENCIA referencia = new REFERENCIA();
                            referencia.REFERIDO = new PERSONA();

                            referencia.ID_REFERENCIA = refe.Field<int>("ID_REFERENCIA");
                            referencia.TIPO = string.IsNullOrEmpty(refe.Field<string>("TIPO")) ? "" : refe.Field<string>("TIPO").ToString().Trim();
                            referencia.RELACION = string.IsNullOrEmpty(refe.Field<string>("RELACION")) ? "" : refe.Field<string>("RELACION").ToString().Trim();
                            referencia.OBSERVACION = string.IsNullOrEmpty(refe.Field<string>("OBSERVACION")) ? "" : refe.Field<string>("OBSERVACION").ToString().Trim();
                            referencia.CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("CALIFICACION")) ? "" : refe.Field<string>("CALIFICACION").ToString().Trim();
                            referencia.DESCRIPCION_CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("DESCRIPCION_CALIFICACION")) ? "" : refe.Field<string>("DESCRIPCION_CALIFICACION").ToString().Trim();
                            referencia.CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("CALIFICACION")) ? "" : refe.Field<string>("CALIFICACION").ToString().Trim();
                            referencia.USUARIO = "";

                            personareferenciadata = _conectorBD.ListarPersonaReferencia(referencia.ID_REFERENCIA);
                            foreach (DataRow per in personareferenciadata.Rows)
                            {
                                PERSONA persona = new PERSONA();
                                persona.DIRECCIONES_PERS = new List<DIRECCION>();
                                persona.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();


                                persona.ID_PERSONA = per.Field<int>("ID_PERSONA");
                                persona.GENERO = string.IsNullOrEmpty(per.Field<string>("GENERO")) ? "" : per.Field<string>("GENERO").ToString().Trim();
                                persona.PRIMER_APELLIDO = string.IsNullOrEmpty(per.Field<string>("PRIMER_APELLIDO")) ? "" : per.Field<string>("PRIMER_APELLIDO").ToString().Trim();
                                persona.SEGUNDO_APELLIDO = string.IsNullOrEmpty(per.Field<string>("SEGUNDO_APELLIDO")) ? "" : per.Field<string>("SEGUNDO_APELLIDO").ToString().Trim();
                                persona.NOMBRES = string.IsNullOrEmpty(per.Field<string>("NOMBRES")) ? "" : per.Field<string>("NOMBRES").ToString().Trim();
                                persona.CASADA_APELLIDO = string.IsNullOrEmpty(per.Field<string>("CASADA_APELLIDO")) ? "" : per.Field<string>("CASADA_APELLIDO").ToString().Trim();
                                persona.NOMBRE_COMPLETO = string.IsNullOrEmpty(per.Field<string>("NOMBRE_COMPLETO")) ? "" : per.Field<string>("NOMBRE_COMPLETO").ToString().Trim();
                                persona.CORREO = string.IsNullOrEmpty(per.Field<string>("CORREO")) ? "" : per.Field<string>("CORREO").ToString().Trim();
                                persona.NIVEL_EDUCACION = string.IsNullOrEmpty(per.Field<string>("NIVEL_EDUCACION")) ? "" : per.Field<string>("NIVEL_EDUCACION").ToString().Trim();
                                persona.PROFESION = string.IsNullOrEmpty(per.Field<string>("PROFESION")) ? "" : per.Field<string>("PROFESION").ToString().Trim();
                                persona.ESTADO_CIVIL = string.IsNullOrEmpty(per.Field<string>("ESTADO_CIVIL")) ? "" : per.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                persona.COD_ANA = string.IsNullOrEmpty(per.Field<string>("COD_ANA")) ? "" : per.Field<string>("COD_ANA").ToString().Trim();
                                persona.USUARIO = string.Empty;

                                direcciondata = _conectorBD.ListarDireccionPersona(persona.ID_PERSONA);
                                persona.DIRECCIONES_PERS.Clear();
                                foreach (DataRow dire in direcciondata.Rows)
                                {
                                    DIRECCION direccion = new DIRECCION();
                                    direccion.TELEFONOS = new List<TELEFONO>();
                                    direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                                    direccion.PAIS_RESIDENCIA = String.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                    direccion.CIUDAD_RESIDENCIA = String.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                    direccion.TIPO_DIRECCION = String.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                    direccion.DEPARTAMENTO = String.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                    direccion.LOCALIDAD = String.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                                    direccion.ZONA_BARRIO = String.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                    direccion.CALLE_AVENIDA = String.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                    direccion.NRO_PUERTA = String.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                    direccion.NRO_PISO = String.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                                    direccion.NRO_DEPARTAMENTO = String.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                    direccion.NOMBRE_EDIFICIO = String.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                    direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                    direccion.TIPO_VIVIENDA_OFICINA = String.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                    direccion.REFERENCIA = String.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                                    direccion.USUARIO = string.Empty;
                                    telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                    direccion.TELEFONOS.Clear();
                                    foreach (DataRow tele in telefonodata.Rows)
                                    {
                                        TELEFONO telefono = new TELEFONO();
                                        telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                        telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                        telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                        telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                        telefono.USUARIO = string.Empty;
                                        direccion.TELEFONOS.Add(telefono);

                                    }

                                    persona.DIRECCIONES_PERS.Add(direccion);
                                }
                                documentopersonaldata = _conectorBD.ListarDocumentoPersonal(persona.ID_PERSONA);
                                persona.DOCUMENTOS.Clear();
                                foreach (DataRow doc in documentopersonaldata.Rows)
                                {
                                    DOCUMENTO_PERSONAL documento_personal = new DOCUMENTO_PERSONAL();

                                    documento_personal.ID_DOCUMENTO_PERSONAL = doc.Field<int>("ID_DOCUMENTO_PERSONAL");
                                    documento_personal.TIPO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("TIPO_DOCUMENTO")) ? "" : doc.Field<string>("TIPO_DOCUMENTO").ToString().Trim();
                                    documento_personal.NRO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("NRO_DOCUMENTO")) ? "" : doc.Field<string>("NRO_DOCUMENTO").ToString().Trim();
                                    documento_personal.COMPLEMENTO = string.IsNullOrEmpty(doc.Field<string>("COMPLEMENTO")) ? "" : doc.Field<string>("COMPLEMENTO").ToString().Trim();
                                    documento_personal.EXTENSION = string.IsNullOrEmpty(doc.Field<string>("EXTENSION")) ? "" : doc.Field<string>("EXTENSION").ToString().Trim();
                                    documento_personal.FECHA_VENC = doc.Field<DateTime>("FECHA_VENC");
                                    documento_personal.ESTADO_CIVIL = string.IsNullOrEmpty(doc.Field<string>("ESTADO_CIVIL")) ? "" : doc.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                    documento_personal.NACIONALIDAD = string.IsNullOrEmpty(doc.Field<string>("NACIONALIDAD")) ? "" : doc.Field<string>("NACIONALIDAD").ToString().Trim();
                                    documento_personal.LUGAR_NACIMIENTO = string.IsNullOrEmpty(doc.Field<string>("LUGAR_NACIMIENTO")) ? "" : doc.Field<string>("LUGAR_NACIMIENTO").ToString().Trim();
                                    documento_personal.FECHA_NACIMIENTO = doc.Field<DateTime>("FECHA_NACIMIENTO");
                                    documento_personal.USUARIO = string.Empty;
                                    persona.DOCUMENTOS.Add(documento_personal);

                                }

                                telefonodata = _conectorBD.ListarTelefonoPersona(persona.ID_PERSONA);
                                foreach (DataRow tele in telefonodata.Rows)
                                {
                                    TELEFONO telefono = new TELEFONO();
                                    telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                    telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                    telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                    telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                    telefono.USUARIO = string.Empty;
                                    persona.CONTACTO = telefono;
                                }
                                persona.USUARIO = String.Empty;
                                referencia.REFERIDO = persona;
                            }

                            respuesta.REFERENCIAS.Add(referencia);

                        }

                        creditodata = _conectorBD.ListarCredito(respuesta.ID_CLIENTE);
                        //respuesta.CREDITOS.Clear();
                        //foreach (DataRow cre in creditodata.Rows)
                        //{
                        //    CREDITO credito = new CREDITO();
                        //    credito.DECLARACIONES = new List<DECLARACION_JURADA>();
                        //    credito.DOCUMENTOS_ENTREGADOS = new List<DOCUMENTO_ENTREGADO>();
                        //    credito.AUTORIZACIONES = new List<AUTORIZACION>();

                        //    credito.ID_CREDITO = cre.Field<int>("ID_CREDITO");
                        //    credito.COD_CREDITO = cre.Field<string>("COD_CREDITO").ToString();
                        //    credito.COD_ANA = string.IsNullOrEmpty(cre.Field<string>("COD_ANA")) ? "" : cre.Field<string>("COD_ANA").ToString();
                        //    credito.MONTO_CREDITO = cre.Field<decimal>("MONTO_CREDITO");
                        //    credito.TIPO_OPERACION = string.IsNullOrEmpty(cre.Field<string>("TIPO_OPERACION")) ? "" : cre.Field<string>("TIPO_OPERACION").ToString();
                        //    credito.ESTRATEGIA = string.IsNullOrEmpty(cre.Field<string>("ESTRATEGIA")) ? "" : cre.Field<string>("ESTRATEGIA").ToString();
                        //    credito.DESTINO = string.IsNullOrEmpty(cre.Field<string>("DESTINO")) ? "" : cre.Field<string>("DESTINO").ToString();
                        //    credito.DESTINO_CLIENTE = string.IsNullOrEmpty(cre.Field<string>("DESTINO_CLIENTE")) ? "" : cre.Field<string>("DESTINO_CLIENTE").ToString();
                        //    credito.COMPRA_PASIVO = cre.Field<bool>("COMPRA_PASIVO");
                        //    credito.OBJETO_OPERACION = string.IsNullOrEmpty(cre.Field<string>("OBJETO_OPERACION")) ? "" : cre.Field<string>("OBJETO_OPERACION").ToString();
                        //    credito.PLAZO = cre.Field<int>("PLAZO");
                        //    credito.DIA_PAGO = cre.Field<int>("DIA_PAGO");
                        //    credito.FECHA_SOLICITUD = cre.Field<DateTime>("FECHA_SOLICITUD");

                        //    declaracionjuradadata = _conectorBD.ListarDeclaracionJurada(credito.ID_CREDITO);
                        //    credito.DECLARACIONES.Clear();
                        //    foreach (DataRow dj in declaracionjuradadata.Rows)
                        //    {
                        //        DECLARACION_JURADA declaracion_jarada = new DECLARACION_JURADA();
                        //        declaracion_jarada.ID_DECLARACION_JURADA = dj.Field<int>("ID_DECLARACION_JURADA");
                        //        declaracion_jarada.TIPO = string.IsNullOrEmpty(dj.Field<string>("TIPO")) ? "" : dj.Field<string>("TIPO").ToString();
                        //        declaracion_jarada.PATRIMONIO_ACTIVO = dj.Field<int>("PATRIMONIO_ACTIVO");
                        //        declaracion_jarada.PATRIMONIO_PASIVO = dj.Field<int>("PATRIMONIO_PASIVO");
                        //        declaracion_jarada.PERSONAL_OCUPADO = dj.Field<int>("PERSONAL_OCUPADO");
                        //        declaracion_jarada.TOTAL_INGRESO_VENTAS = dj.Field<int>("TOTAL_INGRESO_VENTAS");
                        //        declaracion_jarada.OBSERVACIONES = string.IsNullOrEmpty(dj.Field<string>("OBSERVACIONES")) ? "" : dj.Field<string>("OBSERVACIONES").ToString();
                        //        declaracion_jarada.USUARIO = string.Empty;
                        //        credito.DECLARACIONES.Add(declaracion_jarada);

                        //    }


                        //    credito.CLIENTE_CPOP = string.IsNullOrEmpty(cre.Field<string>("CLIENTE_CPOP")) ? "" : cre.Field<string>("CLIENTE_CPOP").ToString();
                        //    credito.NRO_CPOP = cre.Field<int>("NRO_CPOP");
                        //    credito.TIPO_TASA = string.IsNullOrEmpty(cre.Field<string>("TIPO_TASA")) ? "" : cre.Field<string>("TIPO_TASA").ToString();
                        //    credito.NRO_AUTORIZACION = cre.Field<int>("NRO_AUTORIZACION");
                        //    credito.TASA_SELECCION = string.IsNullOrEmpty(cre.Field<string>("TASA_SELECCION")) ? "" : cre.Field<string>("TASA_SELECCION").ToString();
                        //    credito.TASA_PP = cre.Field<int>("TASA_PP");
                        //    credito.ESTADO_CREDITO = string.IsNullOrEmpty(cre.Field<string>("ESTADO_CREDITO")) ? "" : cre.Field<string>("ESTADO_CREDITO").ToString();
                        //    credito.ESTADO_SCI = string.IsNullOrEmpty(cre.Field<string>("ESTADO_SCI")) ? "" : cre.Field<string>("ESTADO_SCI").ToString();

                        //    documentoentregadodata = _conectorBD.ListarDocumentoEntregado(credito.ID_CREDITO);
                        //    credito.DOCUMENTOS_ENTREGADOS.Clear();
                        //    foreach (DataRow de in documentoentregadodata.Rows)
                        //    {
                        //        DOCUMENTO_ENTREGADO documentos_entregados = new DOCUMENTO_ENTREGADO();
                        //        documentos_entregados.ID_DOCUMENTO_ENTREGADO = de.Field<int>("ID_DOCUMENTO_ENTREGADO");
                        //documentos_entregados.TIPO = string.IsNullOrEmpty(de.Field<string>("TIPO")) ? "" : de.Field<string>("TIPO").ToString().Trim();
                        //        documentos_entregados.EXTENSION = string.IsNullOrEmpty(de.Field<string>("EXTENSION")) ? "" : de.Field<string>("EXTENSION").ToString();
                        //        documentos_entregados.ARCHIVO = string.IsNullOrEmpty(de.Field<string>("ARCHIVO")) ? "" : de.Field<string>("ARCHIVO").ToString();
                        //        documentos_entregados.DOCUMENTO = string.IsNullOrEmpty(de.Field<string>("DOCUMENTO")) ? "" : de.Field<string>("DOCUMENTO").ToString();
                        //        documentos_entregados.ENTREGADO = de.Field<bool>("ENTREGADO");

                        //        credito.DOCUMENTOS_ENTREGADOS.Add(documentos_entregados);

                        //    }

                        //    autorizaciondata = _conectorBD.ListarAutorizacion(credito.ID_CREDITO);
                        //    credito.AUTORIZACIONES.Clear();
                        //    foreach (DataRow auto in autorizaciondata.Rows)
                        //    {
                        //        AUTORIZACION autorizacion = new AUTORIZACION();
                        //        autorizacion.ID_AUTORIZACION = auto.Field<int>("ID_AUTORIZACION");
                        //autorizacion.HABILITADO = auto.Field<bool>("HABILITADO");
                        //        autorizacion.AUTORIZACION_ESPECIAL = string.IsNullOrEmpty(auto.Field<string>("AUTORIZACION_ESPECIAL")) ? "" : auto.Field<string>("AUTORIZACION_ESPECIAL").ToString();
                        //        autorizacion.DESCRIPCION = string.IsNullOrEmpty(auto.Field<string>("DESCRIPCION")) ? "" : auto.Field<string>("DESCRIPCION").ToString();
                        //        credito.AUTORIZACIONES.Add(autorizacion);
                        //    }
                        //    credito.USUARIO = String.Empty;
                        //    respuesta.CREDITOS.Add(credito);

                        //}
                    }
                    DataTable creditoresponse = new DataTable();
                    CAMBIOESTADO request = new CAMBIOESTADO();
                    request.TIPO = "ANA";
                    request.ID_CLIENTE = respuesta.ID_CLIENTE;
                    request.NUEVO_ESTADO = "PROCESANDO";

                    creditoresponse = _conectorBD.ClienteCambioEstado(request);

                    if (creditoresponse.Rows.Count!=0)
                    {
                        foreach (DataRow item in creditoresponse.Rows)
                        {

                            if (item.Field<int>("CODIGO") != 1)
                            {
                                respuesta.message = item.Field<string>("MENSAJE");
                                respuesta.code = Configuraciones.GetCode("ERROR_FATAL");
                                respuesta.success = false;
                            }
                        }
                    }
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = Configuraciones.GetMessage("OK");
                    respuesta.success = true;
                }
                else
                {
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = "LISTA DE CREDITOS SE ENCUENTRA VACIO";
                    respuesta.success = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public async Task<CLIENTEROBOT> ListaClienteAnaRobot(string ESTADO)
        {
            //ListarCredito ListarCredito = new ListarCredito();
            //ListarCredito.LISTARCREDITOS = new List<CREDITO>();
            //CREDITORESPONSE respuesta = new CREDITORESPONSE();

            DataTable data = new DataTable();
            DataTable buscado = new DataTable();


            CLIENTEROBOT respuesta = new CLIENTEROBOT();
            CLIENTEROBOT clienterobot = new CLIENTEROBOT();
            string resultado = string.Empty;


            try
            {
                data = _conectorBD.ListaClienteAna(ESTADO);

                #region OPCIONAL
                if (data.Rows.Count!=0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        respuesta.ID_CLIENTE = item.Field<int>("ID_CLIENTE");
                    }
                    buscado = _conectorBD.BuscarClienteTemporal("ACTUALIZAR", respuesta.ID_CLIENTE);
                    if (buscado.Rows.Count != 0)
                    {
                        foreach (DataRow item in buscado.Rows)
                        {
                            resultado = item.Field<string>("DESCRIPCION");
                        }
                        respuesta = JsonConvert.DeserializeObject<CLIENTEROBOT>(resultado);
                    }
                    
                    //respuesta


                    DataTable creditoresponse = new DataTable();
                    CAMBIOESTADO request = new CAMBIOESTADO();
                    request.TIPO = "ANA";
                    request.ID_CLIENTE = respuesta.ID_CLIENTE;
                    request.NUEVO_ESTADO = "PROCESANDO";

                    creditoresponse = _conectorBD.ClienteCambioEstado(request);

                    if (creditoresponse.Rows.Count!=0)
                    {
                        foreach (DataRow item in creditoresponse.Rows)
                        {

                            if (item.Field<int>("CODIGO") != 1)
                            {
                                respuesta.message = item.Field<string>("MENSAJE");
                                respuesta.code = Configuraciones.GetCode("ERROR_FATAL");
                                respuesta.success = false;
                            }
                        }
                    }
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = Configuraciones.GetMessage("OK");
                    respuesta.success = true;

                    var borrar = _conectorBD.BorrarClienteTemporal("ACTUALIZAR", respuesta.ID_CLIENTE);
                }
                #endregion
                else
                {
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = "LISTA DE CREDITOS SE ENCUENTRA VACIO";
                    respuesta.success = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public async Task<CLIENTERESPONSEANAVALIDAR> ListaClienteAnaValidar(string ESTADO)
        {
            //ListarCredito ListarCredito = new ListarCredito();
            //ListarCredito.LISTARCREDITOS = new List<CREDITO>();
            //CREDITORESPONSE respuesta = new CREDITORESPONSE();

            DataTable data = new DataTable();
            DataTable personadata = new DataTable();
            DataTable creditodata = new DataTable();
            DataTable telefonodata = new DataTable();
            DataTable direcciondata = new DataTable();
            DataTable documentopersonaldata = new DataTable();
            DataTable actividadeconomicadata = new DataTable();
            DataTable caedecdata = new DataTable();
            DataTable fechaAMdata = new DataTable();
            DataTable direccionAEdata = new DataTable();
            DataTable referenciadata = new DataTable();
            DataTable personareferenciadata = new DataTable();
            DataTable declaracionjuradadata = new DataTable();
            DataTable autorizaciondata = new DataTable();
            DataTable documentoentregadodata = new DataTable();

            CLIENTERESPONSEANAVALIDAR respuesta = new CLIENTERESPONSEANAVALIDAR();


            try
            {
                data = _conectorBD.ListaClienteAna(ESTADO);

                if (data.Rows.Count != 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        //CLIENTE respuesta = new CLIENTE();
                        //respuesta.SOLICITANTE = new PERSONA();
                        //respuesta.ACTIVIDADES_ECONOMICAS = new List<ACTIVIDAD_ECONOMICA>();
                        //respuesta.REFERENCIAS = new List<REFERENCIA>();


                        //respuesta = null;
                        respuesta.ID_CLIENTE = item.Field<int>("ID_CLIENTE");
                        //respuesta.ESTADO_ANA = item.Field<string>("ESTADO_ANA").ToString();




                        personadata = _conectorBD.ListarPersona(respuesta.ID_CLIENTE);

                        foreach (DataRow per in personadata.Rows)
                        {
                            PERSONA persona = new PERSONA();

                            persona.DIRECCIONES_PERS = new List<DIRECCION>();
                            persona.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();


                            persona.ID_PERSONA = per.Field<int>("ID_PERSONA");
                            //persona.GENERO = string.IsNullOrEmpty(per.Field<string>("GENERO")) ? "" : per.Field<string>("GENERO").ToString();
                            //persona.PRIMER_APELLIDO = string.IsNullOrEmpty(per.Field<string>("PRIMER_APELLIDO")) ? "" : per.Field<string>("PRIMER_APELLIDO").ToString();
                            //persona.SEGUNDO_APELLIDO = string.IsNullOrEmpty(per.Field<string>("SEGUNDO_APELLIDO")) ? "" : per.Field<string>("SEGUNDO_APELLIDO").ToString();
                            //persona.NOMBRES = string.IsNullOrEmpty(per.Field<string>("NOMBRES")) ? "" : per.Field<string>("NOMBRES").ToString();
                            //persona.CASADA_APELLIDO = string.IsNullOrEmpty(per.Field<string>("CASADA_APELLIDO")) ? "" : per.Field<string>("CASADA_APELLIDO").ToString();
                            respuesta.NOMBRE_COMPLETO = string.IsNullOrEmpty(per.Field<string>("NOMBRE_COMPLETO")) ? "" : per.Field<string>("NOMBRE_COMPLETO").ToString().Trim();

                            persona.CORREO = string.IsNullOrEmpty(per.Field<string>("CORREO")) ? "" : per.Field<string>("CORREO").ToString().Trim();
                            persona.NIVEL_EDUCACION = string.IsNullOrEmpty(per.Field<string>("NIVEL_EDUCACION")) ? "" : per.Field<string>("NIVEL_EDUCACION").ToString().Trim();
                            persona.PROFESION = string.IsNullOrEmpty(per.Field<string>("PROFESION")) ? "" : per.Field<string>("PROFESION").ToString().Trim();
                            persona.ESTADO_CIVIL = string.IsNullOrEmpty(per.Field<string>("ESTADO_CIVIL")) ? "" : per.Field<string>("ESTADO_CIVIL").ToString().Trim();
                            respuesta.COD_ANA = string.IsNullOrEmpty(per.Field<string>("COD_ANA")) ? "" : per.Field<string>("COD_ANA").ToString().Trim();
                            //persona.ESTADO_ANA = string.IsNullOrEmpty(per.Field<string>("ESTADO_ANA")) ? "" : per.Field<string>("ESTADO_ANA").ToString().Trim();
                            persona.USUARIO = string.Empty;

                            direcciondata = _conectorBD.ListarDireccionPersona(persona.ID_PERSONA);
                            persona.DIRECCIONES_PERS.Clear();
                            foreach (DataRow dire in direcciondata.Rows)
                            {
                                DIRECCION direccion = new DIRECCION();
                                direccion.TELEFONOS = new List<TELEFONO>();

                                direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                                direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                                direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                                direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                                direccion.USUARIO = string.Empty;
                                telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                direccion.TELEFONOS.Clear();
                                foreach (DataRow tele in telefonodata.Rows)
                                {
                                    TELEFONO telefono = new TELEFONO();
                                    telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                    telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                    telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                    telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                    telefono.USUARIO = string.Empty;
                                    direccion.TELEFONOS.Add(telefono);

                                }


                                persona.DIRECCIONES_PERS.Add(direccion);
                            }
                            documentopersonaldata = _conectorBD.ListarDocumentoPersonal(persona.ID_PERSONA);
                            persona.DOCUMENTOS.Clear();
                            foreach (DataRow doc in documentopersonaldata.Rows)
                            {
                                DOCUMENTO_PERSONAL documento_personal = new DOCUMENTO_PERSONAL();

                                documento_personal.ID_DOCUMENTO_PERSONAL = doc.Field<int>("ID_DOCUMENTO_PERSONAL");
                                respuesta.TIPO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("TIPO_DOCUMENTO")) ? "" : doc.Field<string>("TIPO_DOCUMENTO").ToString().Trim();
                                respuesta.NRO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("NRO_DOCUMENTO")) ? "" : doc.Field<string>("NRO_DOCUMENTO").ToString().Trim();
                                respuesta.COMPLEMENTO = string.IsNullOrEmpty(doc.Field<string>("COMPLEMENTO")) ? "" : doc.Field<string>("COMPLEMENTO").ToString().Trim();
                                respuesta.EXTENSION = string.IsNullOrEmpty(doc.Field<string>("EXTENSION")) ? "" : doc.Field<string>("EXTENSION").ToString().Trim();
                                documento_personal.FECHA_VENC = doc.Field<DateTime>("FECHA_VENC");
                                documento_personal.ESTADO_CIVIL = string.IsNullOrEmpty(doc.Field<string>("ESTADO_CIVIL")) ? "" : doc.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                documento_personal.NACIONALIDAD = string.IsNullOrEmpty(doc.Field<string>("NACIONALIDAD")) ? "" : doc.Field<string>("NACIONALIDAD").ToString().Trim();
                                documento_personal.LUGAR_NACIMIENTO = string.IsNullOrEmpty(doc.Field<string>("LUGAR_NACIMIENTO")) ? "" : doc.Field<string>("LUGAR_NACIMIENTO").ToString().Trim();
                                documento_personal.FECHA_NACIMIENTO = doc.Field<DateTime>("FECHA_NACIMIENTO");
                                documento_personal.USUARIO = string.Empty;
                                persona.DOCUMENTOS.Add(documento_personal);

                            }

                            telefonodata = _conectorBD.ListarTelefonoPersona(persona.ID_PERSONA);
                            foreach (DataRow tele in telefonodata.Rows)
                            {
                                TELEFONO telefono = new TELEFONO();
                                telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                telefono.USUARIO = string.Empty;
                                persona.CONTACTO = telefono;

                            }
                            persona.USUARIO = String.Empty;
                            //respuesta.DATOS_GENERALES = persona;
                        }

                        actividadeconomicadata = _conectorBD.ListarActividadEconomica(respuesta.ID_CLIENTE);
                        //respuesta.ACTIVIDADES_ECONOMICAS.Clear();
                        foreach (DataRow ae in actividadeconomicadata.Rows)
                        {
                            ACTIVIDAD_ECONOMICA actividad_economica = new ACTIVIDAD_ECONOMICA();
                            actividad_economica.CAE_DEC = new CAEDEC();
                            actividad_economica.TIEMPO_EXP = new List<FECHA_A_M>();
                            actividad_economica.DIRECCION_ACT_ECO = new DIRECCION();

                            actividad_economica.ID_ACTIVIDAD_ECONOMICA = ae.Field<int>("ID_ACTIVIDAD_ECONOMICA");

                            caedecdata = _conectorBD.ListarCaedec(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                            foreach (DataRow cae in caedecdata.Rows)
                            {
                                CAEDEC caedec = new CAEDEC();
                                caedec.ID_CAEDEC = cae.Field<int>("ID_CAEDEC");
                                caedec.COD_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("COD_CAEDEC")) ? "" : cae.Field<string>("COD_CAEDEC").ToString().Trim();
                                caedec.ACTIVIDAD_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("ACTIVIDAD_CAEDEC")) ? "" : cae.Field<string>("ACTIVIDAD_CAEDEC").ToString().Trim();
                                caedec.GRUPO_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("GRUPO_CAEDEC")) ? "" : cae.Field<string>("GRUPO_CAEDEC").ToString().Trim();
                                caedec.SECTOR_ECONOMICO = string.IsNullOrEmpty(cae.Field<string>("SECTOR_ECONOMICO")) ? "" : cae.Field<string>("SECTOR_ECONOMICO").ToString().Trim();
                                caedec.USUARIO = string.Empty;
                                actividad_economica.CAE_DEC = caedec;

                            }

                            actividad_economica.NIVEL_LABORAL = string.IsNullOrEmpty(ae.Field<string>("NIVEL_LABORAL")) ? "" : ae.Field<string>("NIVEL_LABORAL").ToString().Trim();
                            actividad_economica.NIT = ae.Field<int>("NIT");
                            actividad_economica.ACTIVIDAD_DECLARADA = string.IsNullOrEmpty(ae.Field<string>("ACTIVIDAD_DECLARADA")) ? "" : ae.Field<string>("ACTIVIDAD_DECLARADA").ToString().Trim();
                            actividad_economica.PRIORIDAD = string.IsNullOrEmpty(ae.Field<string>("PRIORIDAD")) ? "" : ae.Field<string>("PRIORIDAD").ToString().Trim();

                            fechaAMdata = _conectorBD.ListarFechaAM(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                            actividad_economica.TIEMPO_EXP.Clear();
                            foreach (DataRow fec in fechaAMdata.Rows)
                            {
                                FECHA_A_M fecha_a_m = new FECHA_A_M();
                                fecha_a_m.ID_FECHA_A_M = fec.Field<int>("ID_FECHA_A_M");
                                fecha_a_m.TIPO = string.IsNullOrEmpty(fec.Field<string>("TIPO")) ? "" : fec.Field<string>("TIPO").ToString().Trim();
                                fecha_a_m.ANIO = fec.Field<int>("ANIO");
                                fecha_a_m.MES = fec.Field<int>("MES");
                                fecha_a_m.USUARIO = string.Empty;
                                actividad_economica.TIEMPO_EXP.Add(fecha_a_m);

                            }
                            actividad_economica.ESTADO = string.IsNullOrEmpty(ae.Field<string>("ESTADO")) ? "" : ae.Field<string>("ESTADO").ToString().Trim();
                            actividad_economica.INGRESOS_MENSUALES = string.IsNullOrEmpty(ae.Field<string>("INGRESOS_MENSUALES")) ? "0" : ae.Field<string>("INGRESOS_MENSUALES").ToString().Trim();
                            actividad_economica.OTROS_INGRESOS_MENSUALES = ae.Field<int>("OTROS_INGRESOS_MENSUALES");
                            actividad_economica.EGRESOS_MENSUALES = ae.Field<int>("EGRESOS_MENSUALES");
                            actividad_economica.MARGEN_AHORRO = ae.Field<int>("MARGEN_AHORRO");
                            actividad_economica.NOMBRE_EMPRESA = string.IsNullOrEmpty(ae.Field<string>("NOMBRE_EMPRESA")) ? "" : ae.Field<string>("NOMBRE_EMPRESA").ToString().Trim();
                            actividad_economica.FECHA_INGRESO = ae.Field<DateTime>("FECHA_INGRESO");
                            actividad_economica.USUARIO = string.Empty;
                            direccionAEdata = _conectorBD.ListarDireccionActividadEconomica(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                            foreach (DataRow dire in direccionAEdata.Rows)
                            {
                                DIRECCION direccion = new DIRECCION();
                                direccion.TELEFONOS = new List<TELEFONO>();
                                direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                                direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                                direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                                direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                                direccion.USUARIO = string.Empty;
                                telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                direccion.TELEFONOS.Clear();
                                foreach (DataRow tele in telefonodata.Rows)
                                {
                                    TELEFONO telefono = new TELEFONO();
                                    telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                    telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                    telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                    telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                    telefono.USUARIO = string.Empty;
                                    direccion.TELEFONOS.Add(telefono);

                                }

                                actividad_economica.DIRECCION_ACT_ECO = direccion;
                            }

                            actividad_economica.CARGO = string.IsNullOrEmpty(ae.Field<string>("CARGO")) ? "" : ae.Field<string>("CARGO").ToString().Trim();
                            actividad_economica.USUARIO = String.Empty;

                            //respuesta.ACTIVIDADES_ECONOMICAS.Add(actividad_economica);

                        }

                        referenciadata = _conectorBD.ListarReferencia(respuesta.ID_CLIENTE);
                        //respuesta.REFERENCIAS.Clear();
                        foreach (DataRow refe in referenciadata.Rows)
                        {
                            REFERENCIA referencia = new REFERENCIA();
                            referencia.REFERIDO = new PERSONA();

                            referencia.ID_REFERENCIA = refe.Field<int>("ID_REFERENCIA");
                            referencia.TIPO = string.IsNullOrEmpty(refe.Field<string>("TIPO")) ? "" : refe.Field<string>("TIPO").ToString().Trim();
                            referencia.RELACION = string.IsNullOrEmpty(refe.Field<string>("RELACION")) ? "" : refe.Field<string>("RELACION").ToString().Trim();
                            referencia.OBSERVACION = string.IsNullOrEmpty(refe.Field<string>("OBSERVACION")) ? "" : refe.Field<string>("OBSERVACION").ToString().Trim();
                            referencia.CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("CALIFICACION")) ? "" : refe.Field<string>("CALIFICACION").ToString().Trim();
                            referencia.DESCRIPCION_CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("DESCRIPCION_CALIFICACION")) ? "" : refe.Field<string>("DESCRIPCION_CALIFICACION").ToString().Trim();
                            referencia.CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("CALIFICACION")) ? "" : refe.Field<string>("CALIFICACION").ToString().Trim();
                            referencia.USUARIO = "";

                            personareferenciadata = _conectorBD.ListarPersonaReferencia(referencia.ID_REFERENCIA);
                            foreach (DataRow per in personareferenciadata.Rows)
                            {
                                PERSONA persona = new PERSONA();
                                persona.DIRECCIONES_PERS = new List<DIRECCION>();
                                persona.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();


                                persona.ID_PERSONA = per.Field<int>("ID_PERSONA");
                                persona.GENERO = string.IsNullOrEmpty(per.Field<string>("GENERO")) ? "" : per.Field<string>("GENERO").ToString().Trim();
                                persona.PRIMER_APELLIDO = string.IsNullOrEmpty(per.Field<string>("PRIMER_APELLIDO")) ? "" : per.Field<string>("PRIMER_APELLIDO").ToString().Trim();
                                persona.SEGUNDO_APELLIDO = string.IsNullOrEmpty(per.Field<string>("SEGUNDO_APELLIDO")) ? "" : per.Field<string>("SEGUNDO_APELLIDO").ToString().Trim();
                                persona.NOMBRES = string.IsNullOrEmpty(per.Field<string>("NOMBRES")) ? "" : per.Field<string>("NOMBRES").ToString().Trim();
                                persona.CASADA_APELLIDO = string.IsNullOrEmpty(per.Field<string>("CASADA_APELLIDO")) ? "" : per.Field<string>("CASADA_APELLIDO").ToString().Trim();
                                persona.NOMBRE_COMPLETO = string.IsNullOrEmpty(per.Field<string>("NOMBRE_COMPLETO")) ? "" : per.Field<string>("NOMBRE_COMPLETO").ToString().Trim();
                                persona.CORREO = string.IsNullOrEmpty(per.Field<string>("CORREO")) ? "" : per.Field<string>("CORREO").ToString().Trim();
                                persona.NIVEL_EDUCACION = string.IsNullOrEmpty(per.Field<string>("NIVEL_EDUCACION")) ? "" : per.Field<string>("NIVEL_EDUCACION").ToString().Trim();
                                persona.PROFESION = string.IsNullOrEmpty(per.Field<string>("PROFESION")) ? "" : per.Field<string>("PROFESION").ToString().Trim();
                                persona.ESTADO_CIVIL = string.IsNullOrEmpty(per.Field<string>("ESTADO_CIVIL")) ? "" : per.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                persona.COD_ANA = string.IsNullOrEmpty(per.Field<string>("COD_ANA")) ? "" : per.Field<string>("COD_ANA").ToString().Trim();
                                //persona.ESTADO_ANA = string.IsNullOrEmpty(per.Field<string>("ESTADO_ANA")) ? "" : per.Field<string>("ESTADO_ANA").ToString().Trim();
                                persona.USUARIO = string.Empty;

                                direcciondata = _conectorBD.ListarDireccionPersona(persona.ID_PERSONA);
                                persona.DIRECCIONES_PERS.Clear();
                                foreach (DataRow dire in direcciondata.Rows)
                                {
                                    DIRECCION direccion = new DIRECCION();
                                    direccion.TELEFONOS = new List<TELEFONO>();
                                    direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                                    direccion.PAIS_RESIDENCIA = String.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA"))?"": dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                    direccion.CIUDAD_RESIDENCIA = String.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA"))?"": dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                    direccion.TIPO_DIRECCION = String.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION"))?"": dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                    direccion.DEPARTAMENTO = String.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO"))?"":dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                    direccion.LOCALIDAD = String.IsNullOrEmpty(dire.Field<string>("LOCALIDAD"))?"": dire.Field<string>("LOCALIDAD").ToString().Trim();
                                    direccion.ZONA_BARRIO = String.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO"))?"": dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                    direccion.CALLE_AVENIDA = String.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA"))?"": dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                    direccion.NRO_PUERTA = String.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA"))?"": dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                    direccion.NRO_PISO = String.IsNullOrEmpty(dire.Field<string>("NRO_PISO"))?"":dire.Field<string>("NRO_PISO").ToString().Trim();
                                    direccion.NRO_DEPARTAMENTO = String.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO"))?"": dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                    direccion.NOMBRE_EDIFICIO = String.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO"))?"": dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                    direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                    direccion.TIPO_VIVIENDA_OFICINA = String.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA"))?"": dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                    direccion.REFERENCIA = String.IsNullOrEmpty(dire.Field<string>("REFERENCIA"))?"": dire.Field<string>("REFERENCIA").ToString().Trim();
                                    direccion.USUARIO = string.Empty;
                                    telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                    direccion.TELEFONOS.Clear();
                                    foreach (DataRow tele in telefonodata.Rows)
                                    {
                                        TELEFONO telefono = new TELEFONO();
                                        telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                        telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                        telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                        telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                        telefono.USUARIO = string.Empty;
                                        direccion.TELEFONOS.Add(telefono);

                                    }

                                    persona.DIRECCIONES_PERS.Add(direccion);
                                }
                                documentopersonaldata = _conectorBD.ListarDocumentoPersonal(persona.ID_PERSONA);
                                persona.DOCUMENTOS.Clear();
                                foreach (DataRow doc in documentopersonaldata.Rows)
                                {
                                    DOCUMENTO_PERSONAL documento_personal = new DOCUMENTO_PERSONAL();

                                    documento_personal.ID_DOCUMENTO_PERSONAL = doc.Field<int>("ID_DOCUMENTO_PERSONAL");
                                    documento_personal.TIPO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("TIPO_DOCUMENTO")) ? "" : doc.Field<string>("TIPO_DOCUMENTO").ToString().Trim();
                                    documento_personal.NRO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("NRO_DOCUMENTO")) ? "" : doc.Field<string>("NRO_DOCUMENTO").ToString().Trim();
                                    documento_personal.COMPLEMENTO = string.IsNullOrEmpty(doc.Field<string>("COMPLEMENTO")) ? "" : doc.Field<string>("COMPLEMENTO").ToString().Trim();
                                    documento_personal.EXTENSION = string.IsNullOrEmpty(doc.Field<string>("EXTENSION")) ? "" : doc.Field<string>("EXTENSION").ToString().Trim();
                                    documento_personal.FECHA_VENC = doc.Field<DateTime>("FECHA_VENC");
                                    documento_personal.ESTADO_CIVIL = string.IsNullOrEmpty(doc.Field<string>("ESTADO_CIVIL")) ? "" : doc.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                    documento_personal.NACIONALIDAD = string.IsNullOrEmpty(doc.Field<string>("NACIONALIDAD")) ? "" : doc.Field<string>("NACIONALIDAD").ToString().Trim();
                                    documento_personal.LUGAR_NACIMIENTO = string.IsNullOrEmpty(doc.Field<string>("LUGAR_NACIMIENTO")) ? "" : doc.Field<string>("LUGAR_NACIMIENTO").ToString().Trim();
                                    documento_personal.FECHA_NACIMIENTO = doc.Field<DateTime>("FECHA_NACIMIENTO");
                                    documento_personal.USUARIO = string.Empty;
                                    persona.DOCUMENTOS.Add(documento_personal);

                                }

                                telefonodata = _conectorBD.ListarTelefonoPersona(persona.ID_PERSONA);
                                foreach (DataRow tele in telefonodata.Rows)
                                {
                                    TELEFONO telefono = new TELEFONO();
                                    telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                    telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                    telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                    telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                    telefono.USUARIO = string.Empty;
                                    persona.CONTACTO = telefono;
                                }
                                persona.USUARIO = String.Empty;
                                referencia.REFERIDO = persona;
                            }

                            //respuesta.REFERENCIAS.Add(referencia);

                        }

                        creditodata = _conectorBD.ListarCredito(respuesta.ID_CLIENTE);
                        //respuesta.CREDITOS.Clear();
                        //foreach (DataRow cre in creditodata.Rows)
                        //{
                        //    CREDITO credito = new CREDITO();
                        //    credito.DECLARACIONES = new List<DECLARACION_JURADA>();
                        //    credito.DOCUMENTOS_ENTREGADOS = new List<DOCUMENTO_ENTREGADO>();
                        //    credito.AUTORIZACIONES = new List<AUTORIZACION>();

                        //    credito.ID_CREDITO = cre.Field<int>("ID_CREDITO");
                        //    credito.COD_CREDITO = cre.Field<string>("COD_CREDITO").ToString().Trim();
                        //    credito.COD_ANA = string.IsNullOrEmpty(cre.Field<string>("COD_ANA")) ? "" : cre.Field<string>("COD_ANA").ToString().Trim();
                        //    credito.MONTO_CREDITO = cre.Field<decimal>("MONTO_CREDITO");
                        //    credito.TIPO_OPERACION = string.IsNullOrEmpty(cre.Field<string>("TIPO_OPERACION")) ? "" : cre.Field<string>("TIPO_OPERACION").ToString().Trim();
                        //    credito.ESTRATEGIA = string.IsNullOrEmpty(cre.Field<string>("ESTRATEGIA")) ? "" : cre.Field<string>("ESTRATEGIA").ToString().Trim();
                        //    credito.DESTINO = string.IsNullOrEmpty(cre.Field<string>("DESTINO")) ? "" : cre.Field<string>("DESTINO").ToString().Trim();
                        //    credito.DESTINO_CLIENTE = string.IsNullOrEmpty(cre.Field<string>("DESTINO_CLIENTE")) ? "" : cre.Field<string>("DESTINO_CLIENTE").ToString().Trim();
                        //    credito.COMPRA_PASIVO = cre.Field<bool>("COMPRA_PASIVO");
                        //    credito.OBJETO_OPERACION = string.IsNullOrEmpty(cre.Field<string>("OBJETO_OPERACION")) ? "" : cre.Field<string>("OBJETO_OPERACION").ToString().Trim();
                        //    credito.PLAZO = cre.Field<int>("PLAZO");
                        //    credito.DIA_PAGO = cre.Field<int>("DIA_PAGO");
                        //    credito.FECHA_SOLICITUD = cre.Field<DateTime>("FECHA_SOLICITUD");

                        //    declaracionjuradadata = _conectorBD.ListarDeclaracionJurada(credito.ID_CREDITO);
                        //    credito.DECLARACIONES.Clear();
                        //    foreach (DataRow dj in declaracionjuradadata.Rows)
                        //    {
                        //        DECLARACION_JURADA declaracion_jarada = new DECLARACION_JURADA();
                        //        declaracion_jarada.ID_DECLARACION_JURADA = dj.Field<int>("ID_DECLARACION_JURADA");
                        //        declaracion_jarada.TIPO = string.IsNullOrEmpty(dj.Field<string>("TIPO")) ? "" : dj.Field<string>("TIPO").ToString().Trim();
                        //        declaracion_jarada.PATRIMONIO_ACTIVO = dj.Field<int>("PATRIMONIO_ACTIVO");
                        //        declaracion_jarada.PATRIMONIO_PASIVO = dj.Field<int>("PATRIMONIO_PASIVO");
                        //        declaracion_jarada.PERSONAL_OCUPADO = dj.Field<int>("PERSONAL_OCUPADO");
                        //        declaracion_jarada.TOTAL_INGRESO_VENTAS = dj.Field<int>("TOTAL_INGRESO_VENTAS");
                        //        declaracion_jarada.OBSERVACIONES = string.IsNullOrEmpty(dj.Field<string>("OBSERVACIONES")) ? "" : dj.Field<string>("OBSERVACIONES").ToString().Trim();
                        //        declaracion_jarada.USUARIO = string.Empty;
                        //        credito.DECLARACIONES.Add(declaracion_jarada);

                        //    }


                        //    credito.CLIENTE_CPOP = string.IsNullOrEmpty(cre.Field<string>("CLIENTE_CPOP")) ? "" : cre.Field<string>("CLIENTE_CPOP").ToString().Trim();
                        //    credito.NRO_CPOP = cre.Field<int>("NRO_CPOP");
                        //    credito.TIPO_TASA = string.IsNullOrEmpty(cre.Field<string>("TIPO_TASA")) ? "" : cre.Field<string>("TIPO_TASA").ToString().Trim();
                        //    credito.NRO_AUTORIZACION = cre.Field<int>("NRO_AUTORIZACION");
                        //    credito.TASA_SELECCION = string.IsNullOrEmpty(cre.Field<string>("TASA_SELECCION")) ? "" : cre.Field<string>("TASA_SELECCION").ToString().Trim();
                        //    credito.TASA_PP = cre.Field<int>("TASA_PP");
                        //    credito.ESTADO_CREDITO = string.IsNullOrEmpty(cre.Field<string>("ESTADO_CREDITO")) ? "" : cre.Field<string>("ESTADO_CREDITO").ToString().Trim();
                        //    credito.ESTADO_SCI = string.IsNullOrEmpty(cre.Field<string>("ESTADO_SCI")) ? "" : cre.Field<string>("ESTADO_SCI").ToString().Trim();

                        //    documentoentregadodata = _conectorBD.ListarDocumentoEntregado(credito.ID_CREDITO);
                        //    credito.DOCUMENTOS_ENTREGADOS.Clear();
                        //    foreach (DataRow de in documentoentregadodata.Rows)
                        //    {
                        //        DOCUMENTO_ENTREGADO documentos_entregados = new DOCUMENTO_ENTREGADO();
                        //        documentos_entregados.ID_DOCUMENTO_ENTREGADO = de.Field<int>("ID_DOCUMENTO_ENTREGADO");
                        //documentos_entregados.TIPO = string.IsNullOrEmpty(de.Field<string>("TIPO")) ? "" : de.Field<string>("TIPO").ToString().Trim();
                        //        documentos_entregados.EXTENSION = string.IsNullOrEmpty(de.Field<string>("EXTENSION")) ? "" : de.Field<string>("EXTENSION").ToString().Trim();
                        //        documentos_entregados.ARCHIVO = string.IsNullOrEmpty(de.Field<string>("ARCHIVO")) ? "" : de.Field<string>("ARCHIVO").ToString().Trim();
                        //        documentos_entregados.DOCUMENTO = string.IsNullOrEmpty(de.Field<string>("DOCUMENTO")) ? "" : de.Field<string>("DOCUMENTO").ToString().Trim();
                        //        documentos_entregados.ENTREGADO = de.Field<bool>("ENTREGADO");

                        //        credito.DOCUMENTOS_ENTREGADOS.Add(documentos_entregados);

                        //    }

                        //    autorizaciondata = _conectorBD.ListarAutorizacion(credito.ID_CREDITO);
                        //    credito.AUTORIZACIONES.Clear();
                        //    foreach (DataRow auto in autorizaciondata.Rows)
                        //    {
                        //        AUTORIZACION autorizacion = new AUTORIZACION();
                        //        autorizacion.ID_AUTORIZACION = auto.Field<int>("ID_AUTORIZACION");
                        //autorizacion.HABILITADO = auto.Field<bool>("HABILITADO");
                        //        autorizacion.AUTORIZACION_ESPECIAL = string.IsNullOrEmpty(auto.Field<string>("AUTORIZACION_ESPECIAL")) ? "" : auto.Field<string>("AUTORIZACION_ESPECIAL").ToString().Trim();
                        //        autorizacion.DESCRIPCION = string.IsNullOrEmpty(auto.Field<string>("DESCRIPCION")) ? "" : auto.Field<string>("DESCRIPCION").ToString().Trim();
                        //        credito.AUTORIZACIONES.Add(autorizacion);
                        //    }
                        //    credito.USUARIO = String.Empty;
                        //    respuesta.CREDITOS.Add(credito);

                        //}
                    }
                    DataTable creditoresponse = new DataTable();
                    CAMBIOESTADO request = new CAMBIOESTADO();
                    request.TIPO = "ANA";
                    request.ID_CLIENTE = respuesta.ID_CLIENTE;
                    request.NUEVO_ESTADO = "REVISION";

                    creditoresponse = _conectorBD.ClienteCambioEstado(request);

                    if (creditoresponse.Rows.Count != 0)
                    {
                        foreach (DataRow item in creditoresponse.Rows)
                        {

                            if (item.Field<int>("CODIGO") != 1)
                            {
                                respuesta.message = item.Field<string>("MENSAJE");
                                respuesta.code = Configuraciones.GetCode("ERROR_FATAL");
                                respuesta.success = false;
                            }
                        }
                    }
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = Configuraciones.GetMessage("OK");
                    respuesta.success = true;
                }
                else
                {
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = "LISTA DE CREDITOS SE ENCUENTRA VACIO";
                    respuesta.success = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public async Task<CLIENTERESPONSESCI> ListaClienteSCI()
        {
            //ListarCredito ListarCredito = new ListarCredito();
            //ListarCredito.LISTARCREDITOS = new List<CREDITO>();
            //CREDITORESPONSE respuesta = new CREDITORESPONSE();

            DataTable data = new DataTable();
            DataTable personadata = new DataTable();
            DataTable creditodata = new DataTable();
            DataTable telefonodata = new DataTable();
            DataTable direcciondata = new DataTable();
            DataTable documentopersonaldata = new DataTable();
            DataTable actividadeconomicadata = new DataTable();
            DataTable caedecdata = new DataTable();
            DataTable fechaAMdata = new DataTable();
            DataTable direccionAEdata = new DataTable();
            DataTable referenciadata = new DataTable();
            DataTable personareferenciadata = new DataTable();
            DataTable declaracionjuradadata = new DataTable();
            DataTable autorizaciondata = new DataTable();
            DataTable documentoentregadodata = new DataTable();

            CLIENTERESPONSESCI respuesta = new CLIENTERESPONSESCI();

            int ID_CREDITO = 0;

            try
            {
                data = _conectorBD.ListaClienteSCI();

                if (data.Rows.Count!=0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        //CLIENTE respuesta = new CLIENTE();
                        //respuesta.SOLICITANTE = new PERSONA();
                        //respuesta.ACTIVIDADES_ECONOMICAS = new List<ACTIVIDAD_ECONOMICA>();
                        respuesta.REFERENCIAS = new List<REFERENCIA>();


                        //respuesta = null;
                        respuesta.ID_CLIENTE = item.Field<int>("ID_CLIENTE");
                        //respuesta.ESTADO_ANA = item.Field<string>("ESTADO_ANA").ToString().Trim();




                        //personadata = _conectorBD.ListarPersona(respuesta.ID_CLIENTE);

                        //foreach (DataRow per in personadata.Rows)
                        //{
                        //    PERSONA persona = new PERSONA();

                        //    persona.DIRECCIONES_PERS = new List<DIRECCION>();
                        //    persona.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();


                        //    persona.ID_PERSONA = per.Field<int>("ID_PERSONA");
                        //    persona.GENERO = string.IsNullOrEmpty(per.Field<string>("GENERO")) ? "" : per.Field<string>("GENERO").ToString().Trim();
                        //    persona.PRIMER_APELLIDO = string.IsNullOrEmpty(per.Field<string>("PRIMER_APELLIDO")) ? "" : per.Field<string>("PRIMER_APELLIDO").ToString().Trim();
                        //    persona.SEGUNDO_APELLIDO = string.IsNullOrEmpty(per.Field<string>("SEGUNDO_APELLIDO")) ? "" : per.Field<string>("SEGUNDO_APELLIDO").ToString().Trim();
                        //    persona.NOMBRES = string.IsNullOrEmpty(per.Field<string>("NOMBRES")) ? "" : per.Field<string>("NOMBRES").ToString().Trim();
                        //    persona.CASADA_APELLIDO = string.IsNullOrEmpty(per.Field<string>("CASADA_APELLIDO")) ? "" : per.Field<string>("CASADA_APELLIDO").ToString().Trim();
                        //    persona.NOMBRE_COMPLETO = string.IsNullOrEmpty(per.Field<string>("NOMBRE_COMPLETO")) ? "" : per.Field<string>("NOMBRE_COMPLETO").ToString().Trim();

                        //    persona.CORREO = string.IsNullOrEmpty(per.Field<string>("CORREO")) ? "" : per.Field<string>("CORREO").ToString().Trim();
                        //    persona.NIVEL_EDUCACION = string.IsNullOrEmpty(per.Field<string>("NIVEL_EDUCACION")) ? "" : per.Field<string>("NIVEL_EDUCACION").ToString().Trim();
                        //    persona.PROFESION = string.IsNullOrEmpty(per.Field<string>("PROFESION")) ? "" : per.Field<string>("PROFESION").ToString().Trim();
                        //    persona.ESTADO_CIVIL = string.IsNullOrEmpty(per.Field<string>("ESTADO_CIVIL")) ? "" : per.Field<string>("ESTADO_CIVIL").ToString().Trim();
                        //    persona.COD_ANA = string.IsNullOrEmpty(per.Field<string>("COD_ANA")) ? "" : per.Field<string>("COD_ANA").ToString().Trim();
                        //    persona.USUARIO = string.Empty;

                        //    direcciondata = _conectorBD.ListarDireccionPersona(persona.ID_PERSONA);
                        //    persona.DIRECCIONES_PERS.Clear();
                        //    foreach (DataRow dire in direcciondata.Rows)
                        //    {
                        //        DIRECCION direccion = new DIRECCION();
                        //        direccion.TELEFONOS = new List<TELEFONO>();

                        //        direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                        //        direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                        //        direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                        //        direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                        //        direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                        //        direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                        //        direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                        //        direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                        //        direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                        //        direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                        //        direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                        //        direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                        //        direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                        //        direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                        //        direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                        //        direccion.USUARIO = string.Empty;
                        //        telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                        //        direccion.TELEFONOS.Clear();
                        //        foreach (DataRow tele in telefonodata.Rows)
                        //        {
                        //            TELEFONO telefono = new TELEFONO();
                        //            telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                        //            telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                        //            telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                        //            telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                        //            telefono.USUARIO = string.Empty;
                        //            direccion.TELEFONOS.Add(telefono);

                        //        }


                        //        persona.DIRECCIONES_PERS.Add(direccion);
                        //    }
                        //    documentopersonaldata = _conectorBD.ListarDocumentoPersonal(persona.ID_PERSONA);
                        //    persona.DOCUMENTOS.Clear();
                        //    foreach (DataRow doc in documentopersonaldata.Rows)
                        //    {
                        //        DOCUMENTO_PERSONAL documento_personal = new DOCUMENTO_PERSONAL();

                        //        documento_personal.ID_DOCUMENTO_PERSONAL = doc.Field<int>("ID_DOCUMENTO_PERSONAL");
                        //        documento_personal.TIPO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("TIPO_DOCUMENTO")) ? "" : doc.Field<string>("TIPO_DOCUMENTO").ToString().Trim();
                        //        documento_personal.NRO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("NRO_DOCUMENTO")) ? "" : doc.Field<string>("NRO_DOCUMENTO").ToString().Trim();
                        //        documento_personal.COMPLEMENTO = string.IsNullOrEmpty(doc.Field<string>("COMPLEMENTO")) ? "" : doc.Field<string>("COMPLEMENTO").ToString().Trim();
                        //        documento_personal.EXTENSION = string.IsNullOrEmpty(doc.Field<string>("EXTENSION")) ? "" : doc.Field<string>("EXTENSION").ToString().Trim();
                        //        documento_personal.FECHA_VENC = doc.Field<DateTime>("FECHA_VENC");
                        //        documento_personal.ESTADO_CIVIL = string.IsNullOrEmpty(doc.Field<string>("ESTADO_CIVIL")) ? "" : doc.Field<string>("ESTADO_CIVIL").ToString().Trim();
                        //        documento_personal.NACIONALIDAD = string.IsNullOrEmpty(doc.Field<string>("NACIONALIDAD")) ? "" : doc.Field<string>("NACIONALIDAD").ToString().Trim();
                        //        documento_personal.LUGAR_NACIMIENTO = string.IsNullOrEmpty(doc.Field<string>("LUGAR_NACIMIENTO")) ? "" : doc.Field<string>("LUGAR_NACIMIENTO").ToString().Trim();
                        //        documento_personal.FECHA_NACIMIENTO = doc.Field<DateTime>("FECHA_NACIMIENTO");
                        //        documento_personal.USUARIO = string.Empty;
                        //        persona.DOCUMENTOS.Add(documento_personal);

                        //    }

                        //    telefonodata = _conectorBD.ListarTelefonoPersona(persona.ID_PERSONA);
                        //    foreach (DataRow tele in telefonodata.Rows)
                        //    {
                        //        TELEFONO telefono = new TELEFONO();
                        //        telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                        //        telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                        //        telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                        //        telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                        //        telefono.USUARIO = string.Empty;
                        //        persona.CONTACTO = telefono;

                        //    }
                        //    persona.USUARIO = String.Empty;
                        //    //respuesta.SOLICITANTE = persona;
                        //}

                        //actividadeconomicadata = _conectorBD.ListarActividadEconomica(respuesta.ID_CLIENTE);
                        //respuesta.ACTIVIDADES_ECONOMICAS.Clear();
                        //foreach (DataRow ae in actividadeconomicadata.Rows)
                        //{
                        //    ACTIVIDAD_ECONOMICA actividad_economica = new ACTIVIDAD_ECONOMICA();
                        //    actividad_economica.CAE_DEC = new CAEDEC();
                        //    actividad_economica.TIEMPO_EXP = new List<FECHA_A_M>();
                        //    actividad_economica.DIRECCION_ACT_ECO = new DIRECCION();

                        //    actividad_economica.ID_ACTIVIDAD_ECONOMICA = ae.Field<int>("ID_ACTIVIDAD_ECONOMICA");

                        //    caedecdata = _conectorBD.ListarCaedec(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                        //    foreach (DataRow cae in caedecdata.Rows)
                        //    {
                        //        CAEDEC caedec = new CAEDEC();
                        //        caedec.ID_CAEDEC = cae.Field<int>("ID_CAEDEC");
                        //        caedec.COD_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("COD_CAEDEC")) ? "" : cae.Field<string>("COD_CAEDEC").ToString().Trim();
                        //        caedec.ACTIVIDAD_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("ACTIVIDAD_CAEDEC")) ? "" : cae.Field<string>("ACTIVIDAD_CAEDEC").ToString().Trim();
                        //        caedec.GRUPO_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("GRUPO_CAEDEC")) ? "" : cae.Field<string>("GRUPO_CAEDEC").ToString().Trim();
                        //        caedec.SECTOR_ECONOMICO = string.IsNullOrEmpty(cae.Field<string>("SECTOR_ECONOMICO")) ? "" : cae.Field<string>("SECTOR_ECONOMICO").ToString().Trim();
                        //        caedec.USUARIO = string.Empty;
                        //        actividad_economica.CAE_DEC = caedec;

                        //    }

                        //    actividad_economica.NIVEL_LABORAL = string.IsNullOrEmpty(ae.Field<string>("NIVEL_LABORAL")) ? "" : ae.Field<string>("NIVEL_LABORAL").ToString().Trim();
                        //    actividad_economica.NIT = ae.Field<int>("NIT");
                        //    actividad_economica.ACTIVIDAD_DECLARADA = string.IsNullOrEmpty(ae.Field<string>("ACTIVIDAD_DECLARADA")) ? "" : ae.Field<string>("ACTIVIDAD_DECLARADA").ToString().Trim();
                        //    actividad_economica.PRIORIDAD = string.IsNullOrEmpty(ae.Field<string>("PRIORIDAD")) ? "" : ae.Field<string>("PRIORIDAD").ToString().Trim();

                        //    fechaAMdata = _conectorBD.ListarFechaAM(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                        //    actividad_economica.TIEMPO_EXP.Clear();
                        //    foreach (DataRow fec in fechaAMdata.Rows)
                        //    {
                        //        FECHA_A_M fecha_a_m = new FECHA_A_M();
                        //        fecha_a_m.ID_FECHA_A_M = fec.Field<int>("ID_FECHA_A_M");
                        //        fecha_a_m.TIPO = string.IsNullOrEmpty(fec.Field<string>("TIPO")) ? "" : fec.Field<string>("TIPO").ToString().Trim();
                        //        fecha_a_m.ANIO = fec.Field<int>("ANIO");
                        //        fecha_a_m.MES = fec.Field<int>("MES");
                        //        fecha_a_m.USUARIO = string.Empty;
                        //        actividad_economica.TIEMPO_EXP.Add(fecha_a_m);

                        //    }
                        //    actividad_economica.ESTADO = string.IsNullOrEmpty(ae.Field<string>("ESTADO")) ? "" : ae.Field<string>("ESTADO").ToString().Trim();
                        //    actividad_economica.INGRESOS_MENSUALES = string.IsNullOrEmpty(ae.Field<string>("INGRESOS_MENSUALES")) ? "0" : ae.Field<string>("INGRESOS_MENSUALES").ToString().Trim();
                        //    actividad_economica.OTROS_INGRESOS_MENSUALES = ae.Field<int>("OTROS_INGRESOS_MENSUALES");
                        //    actividad_economica.EGRESOS_MENSUALES = ae.Field<int>("EGRESOS_MENSUALES");
                        //    actividad_economica.MARGEN_AHORRO = ae.Field<int>("MARGEN_AHORRO");
                        //    actividad_economica.NOMBRE_EMPRESA = string.IsNullOrEmpty(ae.Field<string>("NOMBRE_EMPRESA")) ? "" : ae.Field<string>("NOMBRE_EMPRESA").ToString().Trim();
                        //    actividad_economica.FECHA_INGRESO = ae.Field<DateTime>("FECHA_INGRESO");
                        //    actividad_economica.USUARIO = string.Empty;
                        //    direccionAEdata = _conectorBD.ListarDireccionActividadEconomica(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                        //    foreach (DataRow dire in direccionAEdata.Rows)
                        //    {
                        //        DIRECCION direccion = new DIRECCION();
                        //        direccion.TELEFONOS = new List<TELEFONO>();
                        //        direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                        //        direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                        //        direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                        //        direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                        //        direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                        //        direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                        //        direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                        //        direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                        //        direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                        //        direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                        //        direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                        //        direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                        //        direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                        //        direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                        //        direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                        //        direccion.USUARIO = string.Empty;
                        //        telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                        //        direccion.TELEFONOS.Clear();
                        //        foreach (DataRow tele in telefonodata.Rows)
                        //        {
                        //            TELEFONO telefono = new TELEFONO();
                        //            telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                        //            telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                        //            telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                        //            telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                        //            telefono.USUARIO = string.Empty;
                        //            direccion.TELEFONOS.Add(telefono);

                        //        }

                        //        actividad_economica.DIRECCION_ACT_ECO = direccion;
                        //    }

                        //    actividad_economica.CARGO = string.IsNullOrEmpty(ae.Field<string>("CARGO")) ? "" : ae.Field<string>("CARGO").ToString().Trim();
                        //    actividad_economica.USUARIO = String.Empty;

                        //    respuesta.ACTIVIDADES_ECONOMICAS.Add(actividad_economica);

                        //}

                        referenciadata = _conectorBD.ListarReferencia(respuesta.ID_CLIENTE);
                        respuesta.REFERENCIAS.Clear();
                        foreach (DataRow refe in referenciadata.Rows)
                        {
                            REFERENCIA referencia = new REFERENCIA();
                            referencia.REFERIDO = new PERSONA();

                            referencia.ID_REFERENCIA = refe.Field<int>("ID_REFERENCIA");
                            referencia.TIPO = string.IsNullOrEmpty(refe.Field<string>("TIPO")) ? "" : refe.Field<string>("TIPO").ToString().Trim();
                            referencia.RELACION = string.IsNullOrEmpty(refe.Field<string>("RELACION")) ? "" : refe.Field<string>("RELACION").ToString().Trim();
                            referencia.OBSERVACION = string.IsNullOrEmpty(refe.Field<string>("OBSERVACION")) ? "" : refe.Field<string>("OBSERVACION").ToString().Trim();
                            referencia.CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("CALIFICACION")) ? "" : refe.Field<string>("CALIFICACION").ToString().Trim();
                            referencia.DESCRIPCION_CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("DESCRIPCION_CALIFICACION")) ? "" : refe.Field<string>("DESCRIPCION_CALIFICACION").ToString().Trim();
                            referencia.CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("CALIFICACION")) ? "" : refe.Field<string>("CALIFICACION").ToString().Trim();
                            referencia.USUARIO = "";

                            personareferenciadata = _conectorBD.ListarPersonaReferencia(referencia.ID_REFERENCIA);
                            foreach (DataRow per in personareferenciadata.Rows)
                            {
                                PERSONA persona = new PERSONA();
                                persona.DIRECCIONES_PERS = new List<DIRECCION>();
                                persona.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();


                                persona.ID_PERSONA = per.Field<int>("ID_PERSONA");
                                persona.GENERO = string.IsNullOrEmpty(per.Field<string>("GENERO")) ? "" : per.Field<string>("GENERO").ToString().Trim();
                                persona.PRIMER_APELLIDO = string.IsNullOrEmpty(per.Field<string>("PRIMER_APELLIDO")) ? "" : per.Field<string>("PRIMER_APELLIDO").ToString().Trim();
                                persona.SEGUNDO_APELLIDO = string.IsNullOrEmpty(per.Field<string>("SEGUNDO_APELLIDO")) ? "" : per.Field<string>("SEGUNDO_APELLIDO").ToString().Trim();
                                persona.NOMBRES = string.IsNullOrEmpty(per.Field<string>("NOMBRES")) ? "" : per.Field<string>("NOMBRES").ToString().Trim();
                                persona.CASADA_APELLIDO = string.IsNullOrEmpty(per.Field<string>("CASADA_APELLIDO")) ? "" : per.Field<string>("CASADA_APELLIDO").ToString().Trim();
                                persona.NOMBRE_COMPLETO = string.IsNullOrEmpty(per.Field<string>("NOMBRE_COMPLETO")) ? "" : per.Field<string>("NOMBRE_COMPLETO").ToString().Trim();
                                persona.CORREO = string.IsNullOrEmpty(per.Field<string>("CORREO")) ? "" : per.Field<string>("CORREO").ToString().Trim();
                                persona.NIVEL_EDUCACION = string.IsNullOrEmpty(per.Field<string>("NIVEL_EDUCACION")) ? "" : per.Field<string>("NIVEL_EDUCACION").ToString().Trim();
                                persona.PROFESION = string.IsNullOrEmpty(per.Field<string>("PROFESION")) ? "" : per.Field<string>("PROFESION").ToString().Trim();
                                persona.ESTADO_CIVIL = string.IsNullOrEmpty(per.Field<string>("ESTADO_CIVIL")) ? "" : per.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                persona.COD_ANA = string.IsNullOrEmpty(per.Field<string>("COD_ANA")) ? "" : per.Field<string>("COD_ANA").ToString().Trim();
                                persona.USUARIO = string.Empty;

                                direcciondata = _conectorBD.ListarDireccionPersona(persona.ID_PERSONA);
                                persona.DIRECCIONES_PERS.Clear();
                                foreach (DataRow dire in direcciondata.Rows)
                                {
                                    DIRECCION direccion = new DIRECCION();
                                    direccion.TELEFONOS = new List<TELEFONO>();
                                    direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");

                                    direccion.PAIS_RESIDENCIA = String.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                    direccion.CIUDAD_RESIDENCIA = String.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                    direccion.TIPO_DIRECCION = String.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                    direccion.DEPARTAMENTO = String.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                    direccion.LOCALIDAD = String.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                                    direccion.ZONA_BARRIO = String.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                    direccion.CALLE_AVENIDA = String.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                    direccion.NRO_PUERTA = String.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                    direccion.NRO_PISO = String.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                                    direccion.NRO_DEPARTAMENTO = String.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                    direccion.NOMBRE_EDIFICIO = String.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                    direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                    direccion.TIPO_VIVIENDA_OFICINA = String.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                    direccion.REFERENCIA = String.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                                    direccion.USUARIO = string.Empty;
                                    telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                    direccion.TELEFONOS.Clear();
                                    foreach (DataRow tele in telefonodata.Rows)
                                    {
                                        TELEFONO telefono = new TELEFONO();
                                        telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                        telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                        telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                        telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                        telefono.USUARIO = string.Empty;
                                        direccion.TELEFONOS.Add(telefono);

                                    }

                                    persona.DIRECCIONES_PERS.Add(direccion);
                                }
                                documentopersonaldata = _conectorBD.ListarDocumentoPersonal(persona.ID_PERSONA);
                                persona.DOCUMENTOS.Clear();
                                foreach (DataRow doc in documentopersonaldata.Rows)
                                {
                                    DOCUMENTO_PERSONAL documento_personal = new DOCUMENTO_PERSONAL();

                                    documento_personal.ID_DOCUMENTO_PERSONAL = doc.Field<int>("ID_DOCUMENTO_PERSONAL");
                                    documento_personal.TIPO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("TIPO_DOCUMENTO")) ? "" : doc.Field<string>("TIPO_DOCUMENTO").ToString().Trim();
                                    documento_personal.NRO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("NRO_DOCUMENTO")) ? "" : doc.Field<string>("NRO_DOCUMENTO").ToString().Trim();
                                    documento_personal.COMPLEMENTO = string.IsNullOrEmpty(doc.Field<string>("COMPLEMENTO")) ? "" : doc.Field<string>("COMPLEMENTO").ToString().Trim();
                                    documento_personal.EXTENSION = string.IsNullOrEmpty(doc.Field<string>("EXTENSION")) ? "" : doc.Field<string>("EXTENSION").ToString().Trim();
                                    documento_personal.FECHA_VENC = doc.Field<DateTime>("FECHA_VENC");
                                    documento_personal.ESTADO_CIVIL = string.IsNullOrEmpty(doc.Field<string>("ESTADO_CIVIL")) ? "" : doc.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                    documento_personal.NACIONALIDAD = string.IsNullOrEmpty(doc.Field<string>("NACIONALIDAD")) ? "" : doc.Field<string>("NACIONALIDAD").ToString().Trim();
                                    documento_personal.LUGAR_NACIMIENTO = string.IsNullOrEmpty(doc.Field<string>("LUGAR_NACIMIENTO")) ? "" : doc.Field<string>("LUGAR_NACIMIENTO").ToString().Trim();
                                    documento_personal.FECHA_NACIMIENTO = doc.Field<DateTime>("FECHA_NACIMIENTO");
                                    documento_personal.USUARIO = string.Empty;
                                    persona.DOCUMENTOS.Add(documento_personal);

                                }
                                persona.USUARIO = String.Empty;
                                referencia.REFERIDO = persona;
                            }

                            respuesta.REFERENCIAS.Add(referencia);

                        }

                        creditodata = _conectorBD.ListarCredito(respuesta.ID_CLIENTE);
                        respuesta.CREDITOS.Clear();
                        foreach (DataRow cre in creditodata.Rows)
                        {
                            CREDITO credito = new CREDITO();
                            credito.DECLARACIONES = new List<DECLARACION_JURADA>();
                            credito.DOCUMENTOS_ENTREGADOS = new List<DOCUMENTO_ENTREGADO>();
                            credito.AUTORIZACIONES = new List<AUTORIZACION>();

                            credito.ID_CREDITO = cre.Field<int>("ID_CREDITO");
                            ID_CREDITO = credito.ID_CREDITO;
                            credito.COD_CREDITO = cre.Field<string>("COD_CREDITO").ToString().Trim();
                            credito.COD_SCI = string.IsNullOrEmpty(cre.Field<string>("COD_SCI")) ? "" : cre.Field<string>("COD_SCI").ToString().Trim();
                            credito.MONTO_CREDITO = cre.Field<decimal>("MONTO_CREDITO");
                            credito.TIPO_OPERACION = string.IsNullOrEmpty(cre.Field<string>("TIPO_OPERACION")) ? "" : cre.Field<string>("TIPO_OPERACION").ToString().Trim();
                            credito.ESTRATEGIA = string.IsNullOrEmpty(cre.Field<string>("ESTRATEGIA")) ? "" : cre.Field<string>("ESTRATEGIA").ToString().Trim();
                            credito.DESTINO = string.IsNullOrEmpty(cre.Field<string>("DESTINO")) ? "" : cre.Field<string>("DESTINO").ToString().Trim();
                            credito.DESTINO_CLIENTE = string.IsNullOrEmpty(cre.Field<string>("DESTINO_CLIENTE")) ? "" : cre.Field<string>("DESTINO_CLIENTE").ToString().Trim();
                            credito.COMPRA_PASIVO = cre.Field<bool>("COMPRA_PASIVO");
                            credito.OBJETO_OPERACION = string.IsNullOrEmpty(cre.Field<string>("OBJETO_OPERACION")) ? "" : cre.Field<string>("OBJETO_OPERACION").ToString().Trim();
                            credito.PLAZO = cre.Field<int>("PLAZO");
                            credito.DIA_PAGO = cre.Field<int>("DIA_PAGO");
                            credito.ANTIGUEDAD_CLIENTE = string.IsNullOrEmpty(cre.Field<string>("ANTIGUEDAD_CLIENTE")) ? "" : cre.Field<string>("ANTIGUEDAD_CLIENTE").ToString().Trim();
                            credito.FECHA_SOLICITUD = cre.Field<DateTime>("FECHA_SOLICITUD");

                            declaracionjuradadata = _conectorBD.ListarDeclaracionJurada(credito.ID_CREDITO);
                            credito.DECLARACIONES.Clear();
                            foreach (DataRow dj in declaracionjuradadata.Rows)
                            {
                                DECLARACION_JURADA declaracion_jarada = new DECLARACION_JURADA();
                                declaracion_jarada.ID_DECLARACION_JURADA = dj.Field<int>("ID_DECLARACION_JURADA");
                                declaracion_jarada.TIPO = string.IsNullOrEmpty(dj.Field<string>("TIPO")) ? "" : dj.Field<string>("TIPO").ToString().Trim();
                                declaracion_jarada.PATRIMONIO_ACTIVO = dj.Field<int>("PATRIMONIO_ACTIVO");
                                declaracion_jarada.PATRIMONIO_PASIVO = dj.Field<int>("PATRIMONIO_PASIVO");
                                declaracion_jarada.PERSONAL_OCUPADO = dj.Field<int>("PERSONAL_OCUPADO");
                                declaracion_jarada.TOTAL_INGRESO_VENTAS = dj.Field<int>("TOTAL_INGRESO_VENTAS");
                                declaracion_jarada.OBSERVACIONES = string.IsNullOrEmpty(dj.Field<string>("OBSERVACIONES")) ? "" : dj.Field<string>("OBSERVACIONES").ToString().Trim();
                                declaracion_jarada.USUARIO = string.Empty;
                                credito.DECLARACIONES.Add(declaracion_jarada);

                            }


                            credito.CLIENTE_CPOP = string.IsNullOrEmpty(cre.Field<string>("CLIENTE_CPOP")) ? "" : cre.Field<string>("CLIENTE_CPOP").ToString().Trim();
                            credito.NRO_CPOP = cre.Field<int>("NRO_CPOP");
                            credito.TIPO_TASA = string.IsNullOrEmpty(cre.Field<string>("TIPO_TASA")) ? "" : cre.Field<string>("TIPO_TASA").ToString().Trim();
                            credito.NRO_AUTORIZACION = cre.Field<int>("NRO_AUTORIZACION");
                            credito.TASA_SELECCION = string.IsNullOrEmpty(cre.Field<string>("TASA_SELECCION")) ? "" : cre.Field<string>("TASA_SELECCION").ToString().Trim();
                            credito.TASA_PP = cre.Field<int>("TASA_PP");
                            credito.ESTADO_CREDITO = string.IsNullOrEmpty(cre.Field<string>("ESTADO_CREDITO")) ? "" : cre.Field<string>("ESTADO_CREDITO").ToString().Trim();
                            credito.ESTADO_SCI = string.IsNullOrEmpty(cre.Field<string>("ESTADO_SCI")) ? "" : cre.Field<string>("ESTADO_SCI").ToString().Trim();

                            documentoentregadodata = _conectorBD.ListarDocumentoEntregado(credito.ID_CREDITO);
                            credito.DOCUMENTOS_ENTREGADOS.Clear();
                            foreach (DataRow de in documentoentregadodata.Rows)
                            {
                                DOCUMENTO_ENTREGADO documentos_entregados = new DOCUMENTO_ENTREGADO();
                                documentos_entregados.ID_DOCUMENTO_ENTREGADO = de.Field<int>("ID_DOCUMENTO_ENTREGADO");
                                documentos_entregados.TIPO = string.IsNullOrEmpty(de.Field<string>("TIPO")) ? "" : de.Field<string>("TIPO").ToString().Trim();
                                documentos_entregados.EXTENSION = string.IsNullOrEmpty(de.Field<string>("EXTENSION")) ? "" : de.Field<string>("EXTENSION").ToString().Trim();
                                documentos_entregados.ARCHIVO = string.IsNullOrEmpty(de.Field<string>("ARCHIVO")) ? "" : de.Field<string>("ARCHIVO").ToString().Trim();
                                documentos_entregados.DOCUMENTO_DESCRIPCION = string.IsNullOrEmpty(de.Field<string>("DOCUMENTO_DESCRIPCION")) ? "" : de.Field<string>("DOCUMENTO_DESCRIPCION").ToString().Trim();
                                documentos_entregados.ENTREGADO = de.Field<bool>("ENTREGADO");
                                documentos_entregados.NOMBRE_ARCHIVO = string.IsNullOrEmpty(de.Field<string>("NOMBRE_ARCHIVO")) ? "" : de.Field<string>("NOMBRE_ARCHIVO").ToString().Trim();

                                credito.DOCUMENTOS_ENTREGADOS.Add(documentos_entregados);

                            }

                            autorizaciondata = _conectorBD.ListarAutorizacion(credito.ID_CREDITO);
                            credito.AUTORIZACIONES.Clear();
                            foreach (DataRow auto in autorizaciondata.Rows)
                            {
                                AUTORIZACION autorizacion = new AUTORIZACION();
                                autorizacion.ID_AUTORIZACION = auto.Field<int>("ID_AUTORIZACION");
                                autorizacion.HABILITADO = auto.Field<bool>("HABILITADO");
                                autorizacion.AUTORIZACION_ESPECIAL = string.IsNullOrEmpty(auto.Field<string>("AUTORIZACION_ESPECIAL")) ? "" : auto.Field<string>("AUTORIZACION_ESPECIAL").ToString().Trim();
                                autorizacion.DESCRIPCION = string.IsNullOrEmpty(auto.Field<string>("DESCRIPCION")) ? "" : auto.Field<string>("DESCRIPCION").ToString().Trim();
                                credito.AUTORIZACIONES.Add(autorizacion);
                            }
                            credito.USUARIO = String.Empty;
                            respuesta.CREDITOS.Add(credito);

                        }
                    }

                    DataTable creditoresponse = new DataTable();
                    CAMBIOESTADO request = new CAMBIOESTADO();
                    request.TIPO = "SCI";
                    request.ID_CLIENTE = respuesta.ID_CLIENTE;
                    request.ID_CREDITO = ID_CREDITO;
                    request.NUEVO_ESTADO = "PROCESANDO";

                    creditoresponse = _conectorBD.CreditoCambioEstado(request);

                    if (creditoresponse.Rows.Count != 0)
                    {
                        foreach (DataRow item in creditoresponse.Rows)
                        {

                            if (item.Field<int>("CODIGO") != 1)
                            {
                                respuesta.message = item.Field<string>("MENSAJE");
                                respuesta.code = Configuraciones.GetCode("ERROR_FATAL");
                                respuesta.success = false;
                            }
                        }
                    }

                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = Configuraciones.GetMessage("OK");
                    respuesta.success = true;
                }
                else
                {
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = "LISTA DE CREDITOS SE ENCUENTRA VACIO";
                    respuesta.success = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public async Task<RespuestaGeneral> ModificarClienteValidar(CLIENTERESPONSEANAVALIDARRESPONSE cliente)
        {
            CLIENTERESPONSE request = new CLIENTERESPONSE();
            request.ID_CLIENTE = cliente.ID_CLIENTE;
            request.ESTADO_ANA = cliente.CLIENTE.ESTADO_ANA;
            request.SOLICITANTE = cliente.CLIENTE.DATOS_GENERALES;
            request.ACTIVIDADES_ECONOMICAS = cliente.CLIENTE.ACTIVIDADES_ECONOMICAS;
            request.REFERENCIAS = cliente.CLIENTE.REFERENCIAS;
            request.USUARIO = cliente.CLIENTE.USUARIO;

            
            RespuestaGeneral response = new RespuestaGeneral();
            RespuestaGenerica clienteresponse = new RespuestaGenerica();
            RespuestaGenerica creditoresponse = new RespuestaGenerica();
            RespuestaGenerica personaresponse = new RespuestaGenerica();
            RespuestaGenerica actividadeconomicaresponse = new RespuestaGenerica();
            RespuestaGenerica referenciaresponse = new RespuestaGenerica();
            RespuestaGenerica direccionresponse = new RespuestaGenerica();
            RespuestaGenerica direccionAEresponse = new RespuestaGenerica();

            DataTable clientedata = new DataTable();
            DataTable creditodata = new DataTable();
            DataTable personadata = new DataTable();
            DataTable telefonodata = new DataTable();
            DataTable direcciondata = new DataTable();
            DataTable documentopersonaldata = new DataTable();
            DataTable actividadeconomicadata = new DataTable();
            DataTable caedecdata = new DataTable();
            DataTable fechaAMdata = new DataTable();
            DataTable direccionAEdata = new DataTable();
            DataTable referenciadata = new DataTable();
            DataTable personareferenciadata = new DataTable();
            DataTable declaracionjuradadata = new DataTable();
            DataTable autorizaciondata = new DataTable();
            DataTable documentoentregadodata = new DataTable();
            DataTable respuestaclientedata = new DataTable();

            DataTable insertar = new DataTable();
            string resultado = string.Empty;

            try
            {
                var borrar = _conectorBD.BorrarClienteTemporal("VALIDAR", request.ID_CLIENTE);
                insertar = _conectorBD.InsertarClienteTemporal(JsonConvert.SerializeObject(request), "VALIDAR", request.ID_CLIENTE, request.USUARIO);
                if (insertar.Rows.Count!=0)
                {
                    foreach (DataRow item in insertar.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }
                        else
                        {
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }
        public async Task<RespuestaGeneral> ModificarClienteActualizar(RESPUESTA_CLIENTE_REQUEST cliente)
        {
            RespuestaGeneral response = new RespuestaGeneral();
            DataTable respuestaclientedata = new DataTable();

            try
            {

                respuestaclientedata = _conectorBD.ModificarRespuestaCliente(cliente,"ACTUALIZAR");
                if (respuestaclientedata.Rows.Count != 0)
                {
                    foreach (DataRow item in respuestaclientedata.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        if (item.Field<int>("CODIGO") == -1)
                        {
                            response.message = item.Field<string>("MENSAJE");
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                        if (item.Field<int>("CODIGO") == -2)
                        {
                            if (!string.IsNullOrEmpty(cliente.MENSAJE) ||
                                !string.IsNullOrEmpty(cliente.CODIGO) ||
                                !string.IsNullOrEmpty(cliente.USUARIO) ||
                                cliente.ID_CLIENTE != 0)
                            {
                                respuestaclientedata = _conectorBD.InsertarRespuestaCliente(cliente,"ACTUALIZAR");
                                if (respuestaclientedata.Rows.Count != 0)
                                {
                                    foreach (DataRow cli in respuestaclientedata.Rows)
                                    {
                                        response.message = cli.Field<string>("MENSAJE");
                                        if (cli.Field<int>("CODIGO") == 1)
                                        {
                                            response.code = Configuraciones.GetCode("OK");
                                            response.success = true;
                                        }
                                        else
                                        {
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                            return response;
                                        }
                                    }
                                }
                            }
                        }
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }
        #endregion

        #region LOGIN
        public ResponseUser Logueo_User(RequestUser request)
        {
            ResponseUser respuesta = new ResponseUser();

            try
            {
                respuesta = _conectorServices.Login(request);

                respuesta.code = Configuraciones.GetCode("OK");
                respuesta.message = "Ejecucion correctamente";
                respuesta.success = true;

            }
            catch (Exception ex)
            {
                throw;
            }
            return respuesta;
        }
        #endregion

        #region CLIENTE
        public async Task<RespuestaGeneral> CreditoCambioEstado(CAMBIOESTADO request)
        {
            RespuestaGeneral response = new RespuestaGeneral();
            DataTable creditoresponse = new DataTable();
            try
            {
                if (!string.IsNullOrEmpty(request.TIPO))
                {
                    if (request.TIPO.Trim().Equals("ANA"))
                    {
                        creditoresponse = _conectorBD.ClienteCambioEstado(request);
                    }
                    else
                    {
                        creditoresponse = _conectorBD.CreditoCambioEstado(request);
                    }
                }
                if (creditoresponse.Rows.Count!=0)
                {
                    foreach (DataRow item in creditoresponse.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }
                        else
                        {
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }
        public async Task<RespuestaGeneral> InsertarCliente(CLIENTE request)
        {
            Dictionary<string, string> entrevista = new Dictionary<string, string>();
            RespuestaGeneral response = new RespuestaGeneral();
            RespuestaGenerica clienteresponse = new RespuestaGenerica();
            RespuestaGenerica creditoresponse = new RespuestaGenerica();
            RespuestaGenerica personaresponse = new RespuestaGenerica();
            RespuestaGenerica actividadeconomicaresponse = new RespuestaGenerica();
            RespuestaGenerica referenciaresponse = new RespuestaGenerica();
            RespuestaGenerica direccionresponse = new RespuestaGenerica();
            RespuestaGenerica direccionAEresponse = new RespuestaGenerica();
            RespuestaGenerica institucionesresponse = new RespuestaGenerica();

            DataTable clientedata = new DataTable();
            DataTable creditodata = new DataTable();
            DataTable personadata = new DataTable();
            DataTable telefonodata = new DataTable();
            DataTable direcciondata = new DataTable();
            DataTable documentopersonaldata = new DataTable();
            DataTable actividadeconomicadata = new DataTable();
            DataTable caedecdata = new DataTable();
            DataTable fechaAMdata = new DataTable();
            DataTable direccionAEdata = new DataTable();
            DataTable referenciadata = new DataTable();
            DataTable personareferenciadata = new DataTable();
            DataTable declaracionjuradadata = new DataTable();
            DataTable autorizaciondata = new DataTable();
            DataTable documentoentregadodata = new DataTable();

            DataTable verificacionclientedata = new DataTable();
            DataTable scripevaluaciondata = new DataTable();
            DataTable instituciondata = new DataTable();
            DataTable segurodesgravamedata = new DataTable();

            string PREGUNTA = string.Empty;
            string RESPUESTA = string.Empty;
            string CONFIRMACION = string.Empty;
            string OBSERVACIONES = string.Empty;
            string INSTITUCIONES = string.Empty;
            string NOMBRE = string.Empty;
            string MONTO = string.Empty;

            try
            {
                clientedata = _conectorBD.InsertarCliente(request);
                if (clientedata.Rows.Count!=0)
                {
                    foreach (DataRow item in clientedata.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        clienteresponse.ID = item.Field<int>("ID");
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }
                        else
                        {
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                    }
                }
                if (request.SOLICITANTE != null)
                {
                    if (!string.IsNullOrEmpty(request.SOLICITANTE.GENERO)
                    || !string.IsNullOrEmpty(request.SOLICITANTE.PRIMER_APELLIDO)
                    || !string.IsNullOrEmpty(request.SOLICITANTE.NOMBRES)
                    || !string.IsNullOrEmpty(request.SOLICITANTE.CORREO)
                    || !string.IsNullOrEmpty(request.SOLICITANTE.NIVEL_EDUCACION)
                    || !string.IsNullOrEmpty(request.SOLICITANTE.PROFESION)
                    || !string.IsNullOrEmpty(request.SOLICITANTE.ESTADO_CIVIL))
                    {
                        if (request.SOLICITANTE.ESTADO_CIVIL.ToUpper().Trim()== "CASADO(A)" && request.SOLICITANTE.GENERO.ToUpper().Trim() == "FEMENINO" && !string.IsNullOrEmpty(request.SOLICITANTE.CASADA_APELLIDO))
                        {
                            if (!entrevista.ContainsKey("¿Podría confirmarme por favor su nombre completo?"))
                                entrevista.Add("¿Podría confirmarme por favor su nombre completo?", request.SOLICITANTE.NOMBRES + " "+request.SOLICITANTE.PRIMER_APELLIDO.Trim()+" "+ request.SOLICITANTE.SEGUNDO_APELLIDO+" De "+request.SOLICITANTE.CASADA_APELLIDO);
                        }
                        else
                        {
                            if (!entrevista.ContainsKey("¿Podría confirmarme por favor su nombre completo?"))
                                entrevista.Add("¿Podría confirmarme por favor su nombre completo?", request.SOLICITANTE.NOMBRES + " " + request.SOLICITANTE.PRIMER_APELLIDO.Trim() + " " + request.SOLICITANTE.SEGUNDO_APELLIDO);
                        }
                        if (!entrevista.ContainsKey("¿Cuál es su estado civil?"))
                            entrevista.Add("¿Cuál es su estado civil?", request.SOLICITANTE.ESTADO_CIVIL);
                        personadata = _conectorBD.InsertarPersona(request.SOLICITANTE, clienteresponse.ID, 0);
                        if (personadata.Rows.Count!=0)
                        {
                            foreach (DataRow item in personadata.Rows)
                            {

                                personaresponse.ID = item.Field<int>("ID");
                                if (item.Field<int>("CODIGO") != 1)
                                {
                                    personaresponse.message = item.Field<string>("MENSAJE");
                                    personaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                    personaresponse.success = false;
                                }
                            }
                        }
                        if (request.SOLICITANTE.DIRECCIONES_PERS != null)
                        {
                            foreach (var item in request.SOLICITANTE.DIRECCIONES_PERS)
                            {
                                if (!string.IsNullOrEmpty(item.PAIS_RESIDENCIA)
                                    || !string.IsNullOrEmpty(item.CIUDAD_RESIDENCIA)
                                    || !string.IsNullOrEmpty(item.TIPO_DIRECCION)
                                    || !string.IsNullOrEmpty(item.DEPARTAMENTO)
                                    || !string.IsNullOrEmpty(item.LOCALIDAD)
                                    || !string.IsNullOrEmpty(item.ZONA_BARRIO)
                                    || !string.IsNullOrEmpty(item.CALLE_AVENIDA)
                                    || !string.IsNullOrEmpty(item.NRO_PUERTA)
                                    || !string.IsNullOrEmpty(item.NRO_PISO)
                                    || !string.IsNullOrEmpty(item.NRO_DEPARTAMENTO)
                                    || !string.IsNullOrEmpty(item.NOMBRE_EDIFICIO)
                                    || !string.IsNullOrEmpty(item.TIPO_VIVIENDA_OFICINA)
                                    || !string.IsNullOrEmpty(item.REFERENCIA)
                                    || item.NRO_CASILLA != 0)
                                {
                                    if (!entrevista.ContainsKey("¿Su ciudad de residencia?"))
                                        entrevista.Add("¿Su ciudad de residencia?", item.CIUDAD_RESIDENCIA);
                                    if ((string.IsNullOrEmpty(item.TIPO_DIRECCION) ? "" : item.TIPO_DIRECCION.ToUpper().Trim()) == "DOMICILIO")
                                    {
                                        if (!entrevista.ContainsKey("¿Podría brindamer la dirección de su domicilio?"))
                                            entrevista.Add("¿Podría brindamer la dirección de su domicilio?", item.CALLE_AVENIDA + " Nro " + item.NRO_PUERTA + " " + item.ZONA_BARRIO);
                                    }
                                    if ((string.IsNullOrEmpty(item.TIPO_DIRECCION) ? "" : item.TIPO_DIRECCION.ToUpper().Trim()) == "LABORAL")
                                    {
                                        if (!entrevista.ContainsKey("¿La dirección de su lugar de trabajo?"))
                                            entrevista.Add("¿La dirección de su lugar de trabajo?", item.CALLE_AVENIDA + " Nro " + item.NRO_PUERTA + " " + item.ZONA_BARRIO);
                                    }
                                    direcciondata = _conectorBD.InsertarDireccion(item, personaresponse.ID, 0);
                                    if (direcciondata.Rows.Count!=0)
                                    {
                                        foreach (DataRow dir in direcciondata.Rows)
                                        {

                                            direccionresponse.ID = dir.Field<int>("ID");
                                            if (dir.Field<int>("CODIGO") != 1)
                                            {
                                                response.message = dir.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }
                                    if (item.TELEFONOS != null)
                                    {
                                        foreach (var telefono in item.TELEFONOS)
                                        {
                                            if (!string.IsNullOrEmpty(telefono.TIPO)
                                                || !string.IsNullOrEmpty(telefono.NUMERO)
                                                || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                            {
                                                telefonodata = _conectorBD.InsertarTelefono(telefono, direccionresponse.ID, 0);
                                                if (telefonodata.Rows.Count!=0)
                                                {
                                                    foreach (DataRow tel in telefonodata.Rows)
                                                    {

                                                        if (tel.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = tel.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (request.SOLICITANTE.DOCUMENTOS != null)
                        {
                            foreach (var item in request.SOLICITANTE.DOCUMENTOS)
                            {
                                if (!string.IsNullOrEmpty(item.TIPO_DOCUMENTO)
                                    || !string.IsNullOrEmpty(item.NRO_DOCUMENTO)
                                    || !string.IsNullOrEmpty(item.EXTENSION)
                                    || !string.IsNullOrEmpty(item.ESTADO_CIVIL)
                                    || !string.IsNullOrEmpty(item.NACIONALIDAD)
                                    || !string.IsNullOrEmpty(item.LUGAR_NACIMIENTO))
                                {
                                    if (!entrevista.ContainsKey("¿Su número de carnet de identidad?"))
                                        entrevista.Add("¿Su número de carnet de identidad?", item.NRO_DOCUMENTO);
                                    documentopersonaldata = _conectorBD.InsertarDocumentoPersonal(item, personaresponse.ID);
                                    if (documentopersonaldata.Rows.Count!=0)
                                    {
                                        foreach (DataRow tel in documentopersonaldata.Rows)
                                        {

                                            if (tel.Field<int>("CODIGO") != 1)
                                            {
                                                response.message = tel.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (request.SOLICITANTE.CONTACTO != null)
                        {
                            telefonodata = _conectorBD.InsertarTelefono(request.SOLICITANTE.CONTACTO, 0, personaresponse.ID);

                            if (telefonodata.Rows.Count!=0)
                            {
                                foreach (DataRow tel in telefonodata.Rows)
                                {

                                    if (tel.Field<int>("CODIGO") != 1)
                                    {
                                        response.message = tel.Field<string>("MENSAJE");
                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                        response.success = false;
                                    }
                                }
                            }
                        }
                    }
                }
                if (request.ACTIVIDADES_ECONOMICAS != null)
                {
                    foreach (var item in request.ACTIVIDADES_ECONOMICAS)
                    {
                        if (!string.IsNullOrEmpty(item.NIVEL_LABORAL)
                            || !string.IsNullOrEmpty(item.ACTIVIDAD_DECLARADA)
                            || !string.IsNullOrEmpty(item.PRIORIDAD)
                            || !string.IsNullOrEmpty(item.ESTADO)
                            || !string.IsNullOrEmpty(item.INGRESOS_MENSUALES)
                            || !string.IsNullOrEmpty(item.NOMBRE_EMPRESA)
                            || !string.IsNullOrEmpty(item.CARGO)
                            || item.NIT != 0
                            || item.OTROS_INGRESOS_MENSUALES != 0
                            || item.EGRESOS_MENSUALES != 0
                            || item.MARGEN_AHORRO != 0)
                        {
                            if (!entrevista.ContainsKey("¿Su lugar o empresa de trabajo?"))
                                entrevista.Add("¿Su lugar o empresa de trabajo?", item.NOMBRE_EMPRESA);
                            if (!entrevista.ContainsKey("¿Cuál es su antigüedad laboral?"))
                                entrevista.Add("¿Cuál es su antigüedad laboral?", DiferenciaFechas(DateTime.Now, item.FECHA_INGRESO));
                            if (!entrevista.ContainsKey("¿Cuál es el cargo que ocupa?"))
                                entrevista.Add("¿Cuál es el cargo que ocupa?", item.CARGO);
                            if (!entrevista.ContainsKey("¿Tipo de contrato?"))
                                entrevista.Add("¿Tipo de contrato?", "1");
                            actividadeconomicadata = _conectorBD.InsertarActividadEconomica(item, clienteresponse.ID);
                            if (actividadeconomicadata.Rows.Count!=0)
                            {
                                foreach (DataRow act in actividadeconomicadata.Rows)
                                {

                                    actividadeconomicaresponse.ID = act.Field<int>("ID");
                                    if (act.Field<int>("CODIGO") != 1)
                                    {
                                        actividadeconomicaresponse.message = act.Field<string>("MENSAJE");
                                        actividadeconomicaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                        actividadeconomicaresponse.success = false;
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(item.CAE_DEC.COD_CAEDEC)
                                || !string.IsNullOrEmpty(item.CAE_DEC.ACTIVIDAD_CAEDEC)
                                || !string.IsNullOrEmpty(item.CAE_DEC.GRUPO_CAEDEC)
                                || !string.IsNullOrEmpty(item.CAE_DEC.SECTOR_ECONOMICO))
                            {
                                caedecdata = _conectorBD.InsertarCaedec(item.CAE_DEC, actividadeconomicaresponse.ID);
                                if (caedecdata.Rows.Count!=0)
                                {
                                    foreach (DataRow cae in caedecdata.Rows)
                                    {
                                        if (cae.Field<int>("CODIGO") != 1)
                                        {
                                            response.message = cae.Field<string>("MENSAJE");
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                        }
                                    }
                                }
                            }

                            if (item.TIEMPO_EXP != null)
                            {
                                foreach (var tem in item.TIEMPO_EXP)
                                {
                                    if (!string.IsNullOrEmpty(tem.TIPO)
                                        || tem.ANIO != 0
                                        || tem.MES != 0
                                        )
                                    {
                                        fechaAMdata = _conectorBD.InsertarFechaAM(tem, actividadeconomicaresponse.ID);

                                        if (fechaAMdata.Rows.Count!=0)
                                        {
                                            foreach (DataRow cae in fechaAMdata.Rows)
                                            {

                                                if (cae.Field<int>("CODIGO") != 1)
                                                {
                                                    response.message = cae.Field<string>("MENSAJE");
                                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                    response.success = false;
                                                }
                                            }
                                        }

                                    }

                                }
                            }


                        }
                        if (item.DIRECCION_ACT_ECO != null)
                        {
                            if (!string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.PAIS_RESIDENCIA)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.CIUDAD_RESIDENCIA)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.TIPO_DIRECCION)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.DEPARTAMENTO)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.LOCALIDAD)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.ZONA_BARRIO)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.CALLE_AVENIDA)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NRO_PUERTA)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NRO_PISO)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NRO_DEPARTAMENTO)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NOMBRE_EDIFICIO)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.TIPO_VIVIENDA_OFICINA)
                                || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.REFERENCIA)
                                || item.DIRECCION_ACT_ECO.NRO_CASILLA != 0)
                            {
                                direccionAEdata = _conectorBD.InsertarDireccion(item.DIRECCION_ACT_ECO, 0, actividadeconomicaresponse.ID);
                                if (direccionAEdata.Rows.Count!=0)
                                {

                                    foreach (DataRow dir in direccionAEdata.Rows)
                                    {

                                        direccionAEresponse.ID = dir.Field<int>("ID");
                                        if (dir.Field<int>("CODIGO") != 1)
                                        {
                                            response.message = dir.Field<string>("MENSAJE");
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                        }
                                    }

                                    if (item.DIRECCION_ACT_ECO.TELEFONOS != null)
                                    {
                                        foreach (var telefono in item.DIRECCION_ACT_ECO.TELEFONOS)
                                        {
                                            if (string.IsNullOrEmpty(telefono.TIPO)
                                                || !string.IsNullOrEmpty(telefono.NUMERO)
                                                || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                            {
                                                telefonodata = _conectorBD.InsertarTelefono(telefono, direccionAEresponse.ID, 0);
                                                if (telefonodata.Rows.Count!=0)
                                                {
                                                    foreach (DataRow tel in telefonodata.Rows)
                                                    {

                                                        if (tel.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = tel.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }

                if (request.REFERENCIAS != null)
                {
                    foreach (var item in request.REFERENCIAS)
                    {
                        if (!string.IsNullOrEmpty(item.TIPO)
                            || !string.IsNullOrEmpty(item.RELACION)
                            || !string.IsNullOrEmpty(item.OBSERVACION)
                            || !string.IsNullOrEmpty(item.CALIFICACION)
                            || !string.IsNullOrEmpty(item.DESCRIPCION_CALIFICACION)
                            )
                        {
                            referenciadata = _conectorBD.InsertarReferencia(item, clienteresponse.ID);
                            if (referenciadata.Rows.Count!=0)
                            {
                                foreach (DataRow act in referenciadata.Rows)
                                {

                                    referenciaresponse.ID = act.Field<int>("ID");
                                    if (act.Field<int>("CODIGO") != 1)
                                    {
                                        referenciaresponse.message = act.Field<string>("MENSAJE");
                                        referenciaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                        referenciaresponse.success = false;
                                    }
                                }
                            }
                            if (item.REFERIDO != null)
                            {
                                if (!string.IsNullOrEmpty(item.REFERIDO.GENERO)
                                || !string.IsNullOrEmpty(item.REFERIDO.PRIMER_APELLIDO)
                                || !string.IsNullOrEmpty(item.REFERIDO.NOMBRES)
                                || !string.IsNullOrEmpty(item.REFERIDO.CORREO)
                                || !string.IsNullOrEmpty(item.REFERIDO.NIVEL_EDUCACION)
                                || !string.IsNullOrEmpty(item.REFERIDO.PROFESION)
                                || !string.IsNullOrEmpty(item.REFERIDO.ESTADO_CIVIL))
                                {
                                    personadata = _conectorBD.InsertarPersona(item.REFERIDO, 0, referenciaresponse.ID);

                                    if (personadata.Rows.Count!=0)
                                    {
                                        foreach (DataRow per in personadata.Rows)
                                        {

                                            personaresponse.ID = per.Field<int>("ID");
                                            if (per.Field<int>("CODIGO") != 1)
                                            {
                                                personaresponse.message = per.Field<string>("MENSAJE");
                                                personaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                                personaresponse.success = false;
                                            }
                                        }
                                    }
                                    if (item.REFERIDO.DIRECCIONES_PERS != null)
                                    {
                                        foreach (var per in item.REFERIDO.DIRECCIONES_PERS)
                                        {
                                            if (!string.IsNullOrEmpty(per.PAIS_RESIDENCIA)
                                                || !string.IsNullOrEmpty(per.CIUDAD_RESIDENCIA)
                                                || !string.IsNullOrEmpty(per.TIPO_DIRECCION)
                                                || !string.IsNullOrEmpty(per.DEPARTAMENTO)
                                                || !string.IsNullOrEmpty(per.LOCALIDAD)
                                                || !string.IsNullOrEmpty(per.ZONA_BARRIO)
                                                || !string.IsNullOrEmpty(per.CALLE_AVENIDA)
                                                || !string.IsNullOrEmpty(per.NRO_PUERTA)
                                                || !string.IsNullOrEmpty(per.NRO_PISO)
                                                || !string.IsNullOrEmpty(per.NRO_DEPARTAMENTO)
                                                || !string.IsNullOrEmpty(per.NOMBRE_EDIFICIO)
                                                || !string.IsNullOrEmpty(per.TIPO_VIVIENDA_OFICINA)
                                                || !string.IsNullOrEmpty(per.REFERENCIA)
                                                || per.NRO_CASILLA != 0)
                                            {
                                                direcciondata = _conectorBD.InsertarDireccion(per, personaresponse.ID, 0);
                                                if (direcciondata.Rows.Count!=0)
                                                {
                                                    foreach (DataRow tel in direcciondata.Rows)
                                                    {
                                                        direccionresponse.ID = tel.Field<int>("ID");
                                                        if (tel.Field<int>("CODIGO") != 1)
                                                        {
                                                            direccionresponse.message = tel.Field<string>("MENSAJE");
                                                            direccionresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            direccionresponse.success = false;
                                                        }
                                                    }

                                                    foreach (var telefono in per.TELEFONOS)
                                                    {
                                                        if (!string.IsNullOrEmpty(telefono.TIPO)
                                                            || !string.IsNullOrEmpty(telefono.NUMERO)
                                                            || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                                        {
                                                            telefonodata = _conectorBD.InsertarTelefono(telefono, direccionresponse.ID, 0);
                                                            if (telefonodata.Rows.Count!=0)
                                                            {
                                                                foreach (DataRow tel in telefonodata.Rows)
                                                                {

                                                                    if (tel.Field<int>("CODIGO") != 1)
                                                                    {
                                                                        response.message = tel.Field<string>("MENSAJE");
                                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                                        response.success = false;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (item.REFERIDO.DOCUMENTOS != null)
                                    {
                                        foreach (var per in item.REFERIDO.DOCUMENTOS)
                                        {
                                            if (!string.IsNullOrEmpty(per.TIPO_DOCUMENTO)
                                                || !string.IsNullOrEmpty(per.NRO_DOCUMENTO)
                                                || !string.IsNullOrEmpty(per.EXTENSION)
                                                || !string.IsNullOrEmpty(per.ESTADO_CIVIL)
                                                || !string.IsNullOrEmpty(per.NACIONALIDAD)
                                                || !string.IsNullOrEmpty(per.LUGAR_NACIMIENTO))
                                            {
                                                documentopersonaldata = _conectorBD.InsertarDocumentoPersonal(per, personaresponse.ID);
                                                if (documentopersonaldata.Rows.Count!=0)
                                                {
                                                    foreach (DataRow tel in documentopersonaldata.Rows)
                                                    {

                                                        if (tel.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = tel.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (item.REFERIDO.CONTACTO != null)
                                    {
                                        telefonodata = _conectorBD.InsertarTelefono(item.REFERIDO.CONTACTO, 0, personaresponse.ID);

                                        if (telefonodata.Rows.Count!=0)
                                        {
                                            foreach (DataRow tel in telefonodata.Rows)
                                            {

                                                if (tel.Field<int>("CODIGO") != 1)
                                                {
                                                    response.message = tel.Field<string>("MENSAJE");
                                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                    response.success = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (request.CREDITOS != null)
                {
                    foreach (var cre in request.CREDITOS)
                    {
                        if (!entrevista.ContainsKey("¿Usted esta solicitando el siguiente monto de financimiento?"))
                            entrevista.Add("¿Usted esta solicitando el siguiente monto de financimiento?", cre.MONTO_CREDITO.ToString());
                        if (!entrevista.ContainsKey("El destino de su crédito es para:"))
                            entrevista.Add("El destino de su crédito es para:", cre.DESTINO);
                        if (!entrevista.ContainsKey("¿El plazo que solicitó?"))
                            entrevista.Add("¿El plazo que solicitó?", cre.PLAZO.ToString());
                        decimal num4 = 0;
                        if (cre.PLAZO != 0)
                        {
                            double num1 = (double)cre.MONTO_CREDITO;
                            double num2 = (double)cre.PLAZO;
                            double num3 = (((num1 * num2 * (0.32 / 12) / 1.74) + num1)) / num2;

                            num4 = decimal.Round((decimal)num3, 2);
                        }
                        if (!entrevista.ContainsKey("Su cuota mensual para el crédito solicitado es de:"))
                            entrevista.Add("Su cuota mensual para el crédito solicitado es de:", num4.ToString());
                        creditodata = _conectorBD.InsertarCredito(cre, clienteresponse.ID);

                        if (creditodata.Rows.Count!=0)
                        {
                            foreach (DataRow item in creditodata.Rows)
                            {

                                creditoresponse.ID = item.Field<int>("ID");
                                if (item.Field<int>("CODIGO") != 1)
                                {
                                    response.message = item.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                            }
                        }

                        if (cre.DECLARACIONES != null)
                        {
                            foreach (var item in cre.DECLARACIONES)
                            {
                                if (!string.IsNullOrEmpty(item.TIPO)
                                    || !string.IsNullOrEmpty(item.OBSERVACIONES)
                                    || item.PATRIMONIO_ACTIVO != 0
                                    || item.PATRIMONIO_PASIVO != 0
                                    || item.PERSONAL_OCUPADO != 0
                                    || item.TOTAL_INGRESO_VENTAS != 0
                                    )
                                {
                                    declaracionjuradadata = _conectorBD.InsertarDeclaracionJurada(item, creditoresponse.ID);
                                    if (declaracionjuradadata.Rows.Count!=0)
                                    {
                                        foreach (DataRow act in declaracionjuradadata.Rows)
                                        {
                                            if (act.Field<int>("CODIGO") == -1)
                                            {
                                                response.message = act.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        if (cre.DOCUMENTOS_ENTREGADOS != null)
                        {
                            foreach (var item in cre.DOCUMENTOS_ENTREGADOS)
                            {
                                if (!string.IsNullOrEmpty(item.DOCUMENTO_DESCRIPCION) || !string.IsNullOrEmpty(item.NOMBRE_ARCHIVO))
                                {
                                    documentoentregadodata = _conectorBD.InsertarDocumentoEntregado(item, creditoresponse.ID);
                                    if (documentoentregadodata.Rows.Count!=0)
                                    {
                                        foreach (DataRow act in documentoentregadodata.Rows)
                                        {
                                            if (act.Field<int>("CODIGO") != 1)
                                            {
                                                response.message = act.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (cre.AUTORIZACIONES != null)
                        {
                            foreach (var item in cre.AUTORIZACIONES)
                            {
                                if (!string.IsNullOrEmpty(item.AUTORIZACION_ESPECIAL)
                                    || !string.IsNullOrEmpty(item.DESCRIPCION)
                                    )
                                {
                                    autorizaciondata = _conectorBD.InsertarAutorizacion(item, creditoresponse.ID);
                                    if (autorizaciondata.Rows.Count!=0)
                                    {
                                        foreach (DataRow act in autorizaciondata.Rows)
                                        {
                                            if (act.Field<int>("CODIGO") != 1)
                                            {
                                                response.message = act.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                PREGUNTA = string.Empty;
                RESPUESTA = string.Empty;
                CONFIRMACION = string.Empty;
                OBSERVACIONES = string.Empty;
                INSTITUCIONES = string.Empty;
                NOMBRE = string.Empty;
                MONTO = string.Empty;

                if (request.ENTREVISTA!=null)
                {
                    if (request.ENTREVISTA.VERIFICACION_CLIENTE.LISTADO_VERIFICACION_CLIENTE != null)
                    {
                        VERIFICACION_CLIENTE_RESPONSE cliente = new VERIFICACION_CLIENTE_RESPONSE();
                        foreach (var item in request.ENTREVISTA.VERIFICACION_CLIENTE.LISTADO_VERIFICACION_CLIENTE)
                        {
                            if (string.IsNullOrEmpty(PREGUNTA))
                            {
                                PREGUNTA = CodigoVeriCliente(item.PREGUNTA);
                                foreach (KeyValuePair<string, string> user in entrevista)
                                {
                                    var otros = item.RESPUESTA;
                                    string aux1 = DescripcionVeriCliente(item.RESPUESTA);
                                    if (!string.IsNullOrEmpty(user.Key))
                                    {
                                        string aux = DescripcionVeriCliente(item.RESPUESTA);
                                        if (user.Key == item.PREGUNTA)
                                        {
                                            RESPUESTA = user.Value;
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(item.RESPUESTA))
                                {
                                    RESPUESTA = "";
                                }

                                //RESPUESTA = String.IsNullOrEmpty(item.RESPUESTA)?"Sin Dato": item.RESPUESTA.Trim();
                                CONFIRMACION = String.IsNullOrEmpty(item.CONFIRMACION)?"Sin Dato": item.CONFIRMACION.Trim();
                                OBSERVACIONES = String.IsNullOrEmpty(item.OBSERVACIONES)?"Sin Dato": item.OBSERVACIONES.Trim();
                            }
                            else
                            {
                                PREGUNTA = PREGUNTA + " | " + CodigoVeriCliente(item.PREGUNTA);
                                foreach (KeyValuePair<string, string> user in entrevista)
                                {
                                    var otros = item.RESPUESTA;
                                    string aux1 = DescripcionVeriCliente(item.RESPUESTA);
                                    if (!string.IsNullOrEmpty(user.Key))
                                    {
                                        string aux = DescripcionVeriCliente(item.RESPUESTA);
                                        if (user.Key == item.PREGUNTA)
                                        {
                                            RESPUESTA = RESPUESTA + " | " + user.Value;
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(item.RESPUESTA))
                                {
                                    RESPUESTA = RESPUESTA + " | " + "";
                                }

                                //RESPUESTA = RESPUESTA + " | " + (String.IsNullOrEmpty(item.RESPUESTA) ? "Sin Dato" : item.RESPUESTA.Trim());
                                CONFIRMACION = CONFIRMACION + " | " + (string.IsNullOrEmpty(item.CONFIRMACION)?"Sin Dato": item.CONFIRMACION.Trim());
                                OBSERVACIONES = OBSERVACIONES + " | " + (string.IsNullOrEmpty(item.OBSERVACIONES)?"Sin Dato": item.OBSERVACIONES.Trim());
                            }
                            cliente.USUARIO = item.USUARIO;
                        }

                        cliente.PREGUNTA = PREGUNTA;
                        cliente.RESPUESTA = RESPUESTA;
                        cliente.CONFIRMACION = CONFIRMACION;
                        cliente.OBSERVACIONES = OBSERVACIONES;
                        
                        verificacionclientedata = _conectorBD.InsertarVerificacionCliente(cliente, clienteresponse.ID);
                        if (verificacionclientedata.Rows.Count!=0)
                        {
                            foreach (DataRow act in verificacionclientedata.Rows)
                            {
                                if (act.Field<int>("CODIGO") == -1)
                                {
                                    response.message = act.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                            }
                        }
                    }

                    PREGUNTA = string.Empty;
                    RESPUESTA = string.Empty;
                    CONFIRMACION = string.Empty;
                    OBSERVACIONES = string.Empty;
                    INSTITUCIONES = string.Empty;
                    NOMBRE = string.Empty;
                    MONTO = string.Empty;
                    if (request.ENTREVISTA.SCRIPT_EVALUACION.LISTADO_SCRIPT_EVALUACION != null)
                    {
                        SCRIPT_EVALUACION_RESPONSE script = new SCRIPT_EVALUACION_RESPONSE();
                        foreach (var item in request.ENTREVISTA.SCRIPT_EVALUACION.LISTADO_SCRIPT_EVALUACION)
                        {
                            INSTITUCION cliente = new INSTITUCION();
                            if (string.IsNullOrEmpty(PREGUNTA))
                            {
                                PREGUNTA = CodigoScripEvaluacion(item.PREGUNTA);
                                RESPUESTA = string.IsNullOrEmpty(item.RESPUESTA) ? "Sin Dato" : item.RESPUESTA.Trim();
                                OBSERVACIONES = string.IsNullOrEmpty(item.OBSERVACIONES)?"Sin Dato": item.OBSERVACIONES.Trim();

                                if (item.INSTITUCIONES.Count!=0)
                                {
                                    foreach (var ins in item.INSTITUCIONES)
                                    {
                                        if (ins.NOMBRE!=null)
                                        {
                                            if (string.IsNullOrEmpty(NOMBRE))
                                            {
                                                NOMBRE = CodigoInstitucion(ins.NOMBRE);
                                                MONTO = ins.MONTO;
                                            }
                                            else
                                            {
                                                NOMBRE = NOMBRE + " | " + CodigoInstitucion(ins.NOMBRE);
                                                MONTO = MONTO + " | " + ins.MONTO;

                                            }
                                        }
                                        
                                    }
                                    cliente.NOMBRE = NOMBRE;
                                    cliente.MONTO = MONTO;
                                    cliente.USUARIO = item.USUARIO;
                                    instituciondata = _conectorBD.InsertarScripEvaluacionInstitucion(cliente, item.TOTAL);
                                    if (instituciondata.Rows.Count!=0)
                                    {
                                        foreach (DataRow insti in instituciondata.Rows)
                                        {

                                            institucionesresponse.ID = insti.Field<int>("ID");
                                            if (insti.Field<int>("CODIGO") != 1)
                                            {
                                                response.message = insti.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }
                                }
                                INSTITUCIONES = institucionesresponse.ID.ToString();
                            }
                            else
                            {
                                PREGUNTA = PREGUNTA + " | " + CodigoScripEvaluacion(item.PREGUNTA);
                                RESPUESTA = RESPUESTA + " | " + (string.IsNullOrEmpty(item.RESPUESTA) ? "Sin Dato" : item.RESPUESTA.Trim());
                                OBSERVACIONES = OBSERVACIONES + " | " + (string.IsNullOrEmpty(item.OBSERVACIONES) ? "Sin Dato" : item.OBSERVACIONES.Trim());

                                
                                if (item.INSTITUCIONES.Count!=0)
                                {
                                    NOMBRE = string.Empty;
                                    MONTO = string.Empty;
                                    foreach (var ins in item.INSTITUCIONES)
                                    {
                                        if (string.IsNullOrEmpty(NOMBRE))
                                        {
                                            if (string.IsNullOrEmpty(CodigoInstitucion(ins.NOMBRE)))
                                            {
                                                var nuevainstitucion = _conectorBD.InsertarInstitucion(ins.NOMBRE, item.USUARIO);
                                                if (nuevainstitucion.Rows.Count!=0)
                                                {
                                                    foreach (DataRow acta in nuevainstitucion.Rows)
                                                    {
                                                        if (acta.Field<int>("CODIGO") == -1)
                                                        {
                                                            response.message = acta.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                            NOMBRE = CodigoInstitucion(ins.NOMBRE);
                                            MONTO = ins.MONTO;
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(CodigoInstitucion(ins.NOMBRE)))
                                            {
                                                var nuevainstitucion = _conectorBD.InsertarInstitucion(ins.NOMBRE, item.USUARIO);
                                                if (nuevainstitucion.Rows.Count!=0)
                                                {
                                                    foreach (DataRow acta in nuevainstitucion.Rows)
                                                    {
                                                        if (acta.Field<int>("CODIGO") == -1)
                                                        {
                                                            response.message = acta.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                            NOMBRE = NOMBRE + " | " + CodigoInstitucion(ins.NOMBRE);
                                            MONTO = MONTO + " | " + ins.MONTO;

                                        }

                                        
                                    }
                                    cliente.NOMBRE = NOMBRE;
                                    cliente.MONTO = MONTO;
                                    cliente.USUARIO = item.USUARIO;
                                    instituciondata = _conectorBD.InsertarScripEvaluacionInstitucion(cliente, item.TOTAL);
                                    if (instituciondata.Rows.Count!=0)
                                    {
                                        foreach (DataRow insti in instituciondata.Rows)
                                        {

                                            institucionesresponse.ID = insti.Field<int>("ID");
                                            if (insti.Field<int>("CODIGO") != 1)
                                            {
                                                response.message = insti.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }

                                }
                                INSTITUCIONES = INSTITUCIONES + " | " + institucionesresponse.ID.ToString();
                            }
                            script.USUARIO = item.USUARIO;
                        }
                        script.PREGUNTA = PREGUNTA;
                        script.RESPUESTA = RESPUESTA;
                        script.OBSERVACIONES = OBSERVACIONES;
                        
                        scripevaluaciondata = _conectorBD.InsertarScripEvaluacion(script, INSTITUCIONES, clienteresponse.ID);

                        if (scripevaluaciondata.Rows.Count!=0)
                        {
                            foreach (DataRow act in scripevaluaciondata.Rows)
                            {
                                if (act.Field<int>("CODIGO") == -1)
                                {
                                    response.message = act.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                            }
                        }
                    }

                    PREGUNTA = string.Empty;
                    RESPUESTA = string.Empty;
                    CONFIRMACION = string.Empty;
                    OBSERVACIONES = string.Empty;
                    INSTITUCIONES = string.Empty;
                    NOMBRE = string.Empty;
                    MONTO = string.Empty;

                    if (request.ENTREVISTA.SEGURO_DESGRAVAME.LISTADO_SEGURO_DESGRAVAME != null)
                    {
                        SEGURO_DESGRAVAME_RESPONSE cliente = new SEGURO_DESGRAVAME_RESPONSE();
                        foreach (var item in request.ENTREVISTA.SEGURO_DESGRAVAME.LISTADO_SEGURO_DESGRAVAME)
                        {
                            if (string.IsNullOrEmpty(PREGUNTA))
                            {
                                PREGUNTA = CodigoSeguroDesgravame(item.PREGUNTA);
                                RESPUESTA = string.IsNullOrEmpty(item.RESPUESTA)?"": item.RESPUESTA.Trim();
                                CONFIRMACION = string.IsNullOrEmpty(item.CONFIRMACION)?"Sin Dato": item.CONFIRMACION.Trim();
                                OBSERVACIONES = string.IsNullOrEmpty(item.OBSERVACIONES)?"Sin Dato": item.OBSERVACIONES.Trim();
                            }
                            else
                            {
                                PREGUNTA = PREGUNTA + " | " + CodigoSeguroDesgravame(item.PREGUNTA);
                                RESPUESTA = RESPUESTA + " | " + (string.IsNullOrEmpty(item.RESPUESTA) ? "Sin Dato" : item.RESPUESTA.Trim());
                                CONFIRMACION = CONFIRMACION + " | " + (string.IsNullOrEmpty(item.CONFIRMACION) ? "Sin Dato" : item.CONFIRMACION.Trim());
                                OBSERVACIONES = OBSERVACIONES + " | " + (string.IsNullOrEmpty(item.OBSERVACIONES) ? "Sin Dato" : item.OBSERVACIONES.Trim());
                            }
                            cliente.USUARIO = item.USUARIO;
                        }

                        cliente.PREGUNTA = PREGUNTA;
                        cliente.RESPUESTA = RESPUESTA;
                        cliente.CONFIRMACION = CONFIRMACION;
                        cliente.OBSERVACIONES = OBSERVACIONES;
                        verificacionclientedata = _conectorBD.InsertarSeguroDesgravame(cliente, clienteresponse.ID);

                        if (verificacionclientedata.Rows.Count!=0)
                        {
                            foreach (DataRow act in verificacionclientedata.Rows)
                            {
                                if (act.Field<int>("CODIGO") == -1)
                                {
                                    response.message = act.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }
        public async Task<RespuestaGeneral> ModificarCliente(CLIENTE request)
        {
            RespuestaGeneral response = new RespuestaGeneral();
            RespuestaGenerica clienteresponse = new RespuestaGenerica();

            RespuestaGenerica creditoresponse = new RespuestaGenerica();
            RespuestaGenerica personaresponse = new RespuestaGenerica();
            RespuestaGenerica actividadeconomicaresponse = new RespuestaGenerica();
            RespuestaGenerica referenciaresponse = new RespuestaGenerica();
            RespuestaGenerica direccionresponse = new RespuestaGenerica();
            RespuestaGenerica direccionAEresponse = new RespuestaGenerica();

            RespuestaGenerica institucionesresponse = new RespuestaGenerica();

            DataTable clientedata = new DataTable();
            DataTable creditodata = new DataTable();
            DataTable personadata = new DataTable();
            DataTable telefonodata = new DataTable();
            DataTable direcciondata = new DataTable();
            DataTable documentopersonaldata = new DataTable();
            DataTable actividadeconomicadata = new DataTable();
            DataTable caedecdata = new DataTable();
            DataTable fechaAMdata = new DataTable();
            DataTable direccionAEdata = new DataTable();
            DataTable referenciadata = new DataTable();
            DataTable personareferenciadata = new DataTable();
            DataTable declaracionjuradadata = new DataTable();
            DataTable autorizaciondata = new DataTable();
            DataTable documentoentregadodata = new DataTable();

            DataTable verificacionclientedata = new DataTable();
            DataTable scripevaluaciondata = new DataTable();
            DataTable instituciondata = new DataTable();
            DataTable segurodesgravamedata = new DataTable();

            DataTable cambioestado = new DataTable();

            string PREGUNTA = string.Empty;
            string RESPUESTA = string.Empty;
            string CONFIRMACION = string.Empty;
            string OBSERVACIONES = string.Empty;
            string INSTITUCIONES = string.Empty;
            string NOMBRE = string.Empty;
            string MONTO = string.Empty;

            CLIENTE clientetemporal = new CLIENTE();
            CLIENTERESPONSE clienteres = new CLIENTERESPONSE();
            BuscarClienteRequest clienterequest = new BuscarClienteRequest();

            CLIENTEROBOT clienterobot = new CLIENTEROBOT();

            CAMBIOESTADOROBOT robot = new CAMBIOESTADOROBOT();

            Dictionary<string, string> entrevista = new Dictionary<string, string>();

            try
            {
                #region TABLA TEMPORAL

                //BORRADO DE DATOS DE LA TABLA TEMPORAL
                var borrarVal = _conectorBD.BorrarClienteTemporal("VALIDAR", request.ID_CLIENTE);
                var borrarAct = _conectorBD.BorrarClienteTemporal("ACTUALIZAR", request.ID_CLIENTE);

                clienterequest.ID_CLIENTE = request.ID_CLIENTE;
                clienteres = await BuscarCliente(clienterequest);

                clienterobot.ID_CLIENTE = request.ID_CLIENTE;

                clienterobot.DATOS_GENERALES = CompararPersona(request.SOLICITANTE, clienteres.SOLICITANTE);

                if (request.SOLICITANTE.DIRECCIONES_PERS != null)
                {
                    foreach (var item in request.SOLICITANTE.DIRECCIONES_PERS)
                    {
                        foreach (var item1 in clienteres.SOLICITANTE.DIRECCIONES_PERS)
                        {
                            DIRECCIONROBOT direccion = new DIRECCIONROBOT();
                            direccion = CompararDireccion(item, item1);
                            foreach (var tel in item.TELEFONOS)
                            {
                                foreach (var tel1 in item1.TELEFONOS)
                                {
                                    TELEFONOROBOT telefono = new TELEFONOROBOT();
                                    telefono = Comparartelefono(tel, tel1);
                                    if (telefono.ID_TELEFONO != 0)
                                    {
                                        direccion.TELEFONOS.Add(telefono);
                                    }
                                }
                            }
                            if (direccion.ID_DIRECCION != 0)
                            {
                                clienterobot.DATOS_GENERALES.DIRECCIONES_PERS.Add(direccion);
                            }
                        }
                    }
                }
                if (request.SOLICITANTE.DOCUMENTOS != null)
                {
                    foreach (var item in request.SOLICITANTE.DOCUMENTOS)
                    {
                        foreach (var item1 in clienteres.SOLICITANTE.DOCUMENTOS)
                        {
                            DOCUMENTO_PERSONALROBOT documentos = new DOCUMENTO_PERSONALROBOT();
                            documentos = CompararDocumentoPersonal(item, item1);
                            if (documentos.ID_DOCUMENTO_PERSONAL != 0)
                            {
                                clienterobot.DATOS_GENERALES.DOCUMENTOS.Add(documentos);
                            }
                        }
                    }
                }

                clienterobot.DATOS_GENERALES.CONTACTO = Comparartelefono(request.SOLICITANTE.CONTACTO, clienteres.SOLICITANTE.CONTACTO);

                if (request.ACTIVIDADES_ECONOMICAS != null)
                {
                    foreach (var item in request.ACTIVIDADES_ECONOMICAS)
                    {
                        foreach (var item1 in clienteres.ACTIVIDADES_ECONOMICAS)
                        {
                            ACTIVIDAD_ECONOMICAROBOT actividad = new ACTIVIDAD_ECONOMICAROBOT();
                            actividad = CompararActividadEconomica(item, item1);

                            actividad.CAE_DEC = CompararCaedec(item.CAE_DEC, item1.CAE_DEC);

                            foreach (var fec in item.TIEMPO_EXP)
                            {
                                foreach (var fec1 in item1.TIEMPO_EXP)
                                {
                                    FECHA_A_MROBOT fecha = new FECHA_A_MROBOT();
                                    fecha = CompararFecha_A_m(fec, fec1);
                                    if (fecha.ID_FECHA_A_M != 0)
                                    {
                                        actividad.TIEMPO_EXP.Add(fecha);
                                    }
                                }
                            }
                            actividad.DIRECCION_ACT_ECO = CompararDireccion(item.DIRECCION_ACT_ECO, item1.DIRECCION_ACT_ECO);

                            if (actividad.ID_ACTIVIDAD_ECONOMICA != 0)
                            {
                                clienterobot.ACTIVIDADES_ECONOMICAS.Add(actividad);
                            }
                        }
                    }

                    foreach (var item in request.REFERENCIAS)
                    {
                        foreach (var item1 in clienteres.REFERENCIAS)
                        {
                            REFERENCIAROBOT referencia = new REFERENCIAROBOT();
                            referencia = CompararReferencia(item, item1);

                            PERSONAROBOT persona = new PERSONAROBOT();
                            persona = CompararPersona(item.REFERIDO, item1.REFERIDO);


                            if (item.REFERIDO.DIRECCIONES_PERS != null)
                            {
                                foreach (var dire in item.REFERIDO.DIRECCIONES_PERS)
                                {
                                    foreach (var dire1 in item1.REFERIDO.DIRECCIONES_PERS)
                                    {
                                        DIRECCIONROBOT direccion = new DIRECCIONROBOT();
                                        direccion = CompararDireccion(dire, dire1);
                                        foreach (var tel in dire.TELEFONOS)
                                        {
                                            foreach (var tel1 in dire1.TELEFONOS)
                                            {
                                                TELEFONOROBOT telefono = new TELEFONOROBOT();
                                                telefono = Comparartelefono(tel, tel1);
                                                if (telefono.ID_TELEFONO != 0)
                                                {
                                                    direccion.TELEFONOS.Add(telefono);
                                                }
                                            }
                                        }
                                        if (direccion.ID_DIRECCION != 0)
                                        {
                                            persona.DIRECCIONES_PERS.Add(direccion);
                                        }
                                    }
                                }
                            }
                            referencia.REFERIDO = persona;


                            if (referencia.ID_REFERENCIA != 0)
                            {
                                clienterobot.REFERENCIAS.Add(referencia);
                            }
                        }
                    }


                }


                var insertar = _conectorBD.InsertarClienteTemporal(JsonConvert.SerializeObject(clienterobot), "ACTUALIZAR", request.ID_CLIENTE, request.USUARIO);

                #endregion

                clientedata = _conectorBD.ModificarCliente(request);
                if (clientedata.Rows.Count != 0)
                {
                    foreach (DataRow item in clientedata.Rows)
                    {
                        response.message = item.Field<string>("MENSAJE");
                        if (item.Field<int>("CODIGO") == 1)
                        {
                            response.code = Configuraciones.GetCode("OK");
                            response.success = true;
                        }
                        else
                        {
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                            return response;
                        }
                    }
                }

                if (request.SOLICITANTE.ESTADO_CIVIL.ToUpper().Trim() == "CASADO(A)" && request.SOLICITANTE.GENERO.ToUpper().Trim() == "FEMENINO" && !string.IsNullOrEmpty(request.SOLICITANTE.CASADA_APELLIDO))
                {
                    if (!entrevista.ContainsKey("¿Podría confirmarme por favor su nombre completo?"))
                        entrevista.Add("¿Podría confirmarme por favor su nombre completo?", request.SOLICITANTE.NOMBRES + " " + request.SOLICITANTE.PRIMER_APELLIDO.Trim() + " " + request.SOLICITANTE.SEGUNDO_APELLIDO + " De " + request.SOLICITANTE.CASADA_APELLIDO);
                }
                else
                {
                    if (!entrevista.ContainsKey("¿Podría confirmarme por favor su nombre completo?"))
                        entrevista.Add("¿Podría confirmarme por favor su nombre completo?", request.SOLICITANTE.NOMBRES + " " + request.SOLICITANTE.PRIMER_APELLIDO.Trim() + " " + request.SOLICITANTE.SEGUNDO_APELLIDO);
                }
                if (!entrevista.ContainsKey("¿Cuál es su estado civil?"))
                    entrevista.Add("¿Cuál es su estado civil?", request.SOLICITANTE.ESTADO_CIVIL);


                personadata = _conectorBD.ModificarPersona(request.SOLICITANTE);
                if (personadata.Rows.Count != 0)
                {
                    foreach (DataRow per in personadata.Rows)
                    {
                        if (per.Field<int>("CODIGO") == -1)
                        {
                            personaresponse.message = per.Field<string>("MENSAJE");
                            personaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                            personaresponse.success = false;
                        }
                        if (per.Field<int>("CODIGO") == -2)
                        {
                            if (!string.IsNullOrEmpty(request.SOLICITANTE.GENERO)
                                || !string.IsNullOrEmpty(request.SOLICITANTE.PRIMER_APELLIDO)
                                || !string.IsNullOrEmpty(request.SOLICITANTE.NOMBRES)
                                || !string.IsNullOrEmpty(request.SOLICITANTE.CORREO)
                                || !string.IsNullOrEmpty(request.SOLICITANTE.NIVEL_EDUCACION)
                                || !string.IsNullOrEmpty(request.SOLICITANTE.PROFESION)
                                || !string.IsNullOrEmpty(request.SOLICITANTE.ESTADO_CIVIL))
                            {
                                personadata = _conectorBD.InsertarPersona(request.SOLICITANTE, request.ID_CLIENTE, 0);
                                if (personadata.Rows.Count != 0)
                                {
                                    foreach (DataRow item in personadata.Rows)
                                    {

                                        personaresponse.ID = item.Field<int>("ID");
                                        if (item.Field<int>("CODIGO") != 1)
                                        {
                                            personaresponse.message = item.Field<string>("MENSAJE");
                                            personaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                            personaresponse.success = false;
                                        }
                                    }
                                }



                                foreach (var item in request.SOLICITANTE.DIRECCIONES_PERS)
                                {
                                    if (!string.IsNullOrEmpty(item.PAIS_RESIDENCIA)
                                        || !string.IsNullOrEmpty(item.CIUDAD_RESIDENCIA)
                                        || !string.IsNullOrEmpty(item.TIPO_DIRECCION)
                                        || !string.IsNullOrEmpty(item.DEPARTAMENTO)
                                        || !string.IsNullOrEmpty(item.LOCALIDAD)
                                        || !string.IsNullOrEmpty(item.ZONA_BARRIO)
                                        || !string.IsNullOrEmpty(item.CALLE_AVENIDA)
                                        || !string.IsNullOrEmpty(item.NRO_PUERTA)
                                        || !string.IsNullOrEmpty(item.NRO_PISO)
                                        || !string.IsNullOrEmpty(item.NRO_DEPARTAMENTO)
                                        || !string.IsNullOrEmpty(item.NOMBRE_EDIFICIO)
                                        || !string.IsNullOrEmpty(item.TIPO_VIVIENDA_OFICINA)
                                        || !string.IsNullOrEmpty(item.REFERENCIA)
                                        || item.NRO_CASILLA != 0)
                                    {
                                        direcciondata = _conectorBD.InsertarDireccion(item, personaresponse.ID, 0);
                                        if (direcciondata.Rows.Count != 0)
                                        {
                                            foreach (DataRow dir in direcciondata.Rows)
                                            {

                                                direccionresponse.ID = dir.Field<int>("ID");
                                                if (dir.Field<int>("CODIGO") != 1)
                                                {
                                                    response.message = dir.Field<string>("MENSAJE");
                                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                    response.success = false;
                                                }
                                            }
                                        }

                                        foreach (var telefono in item.TELEFONOS)
                                        {
                                            if (!string.IsNullOrEmpty(telefono.TIPO)
                                                || !string.IsNullOrEmpty(telefono.NUMERO)
                                                || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                            {
                                                telefonodata = _conectorBD.InsertarTelefono(telefono, direccionresponse.ID, 0);
                                                if (telefonodata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow tel in telefonodata.Rows)
                                                    {

                                                        if (tel.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = tel.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }


                                        }
                                    }

                                }
                                if (request.SOLICITANTE.DOCUMENTOS != null)
                                {
                                    foreach (var item in request.SOLICITANTE.DOCUMENTOS)
                                    {
                                        if (!string.IsNullOrEmpty(item.TIPO_DOCUMENTO)
                                            || !string.IsNullOrEmpty(item.NRO_DOCUMENTO)
                                            || !string.IsNullOrEmpty(item.EXTENSION)
                                            || !string.IsNullOrEmpty(item.ESTADO_CIVIL)
                                            || !string.IsNullOrEmpty(item.NACIONALIDAD)
                                            || !string.IsNullOrEmpty(item.LUGAR_NACIMIENTO))
                                        {
                                            documentopersonaldata = _conectorBD.InsertarDocumentoPersonal(item, personaresponse.ID);
                                            if (documentopersonaldata.Rows.Count != 0)
                                            {
                                                foreach (DataRow tel in documentopersonaldata.Rows)
                                                {

                                                    if (tel.Field<int>("CODIGO") != 1)
                                                    {
                                                        response.message = tel.Field<string>("MENSAJE");
                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                        response.success = false;
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }
                }
                telefonodata = _conectorBD.ModificarTelefono(request.SOLICITANTE.CONTACTO);

                if (telefonodata.Rows.Count != 0)
                {
                    foreach (DataRow tel in telefonodata.Rows)
                    {

                        if (tel.Field<int>("CODIGO") == -1)
                        {
                            response.message = tel.Field<string>("MENSAJE");
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                        if (tel.Field<int>("CODIGO") == -2)
                        {
                            if (!string.IsNullOrEmpty(request.SOLICITANTE.CONTACTO.TIPO)
                                        || !string.IsNullOrEmpty(request.SOLICITANTE.CONTACTO.NUMERO)
                                        || !string.IsNullOrEmpty(request.SOLICITANTE.CONTACTO.DESCRIPCION))
                            {
                                telefonodata = _conectorBD.InsertarTelefono(request.SOLICITANTE.CONTACTO, 0, request.SOLICITANTE.ID_PERSONA);
                                if (telefonodata.Rows.Count != 0)
                                {
                                    foreach (DataRow tele in telefonodata.Rows)
                                    {

                                        if (tele.Field<int>("CODIGO") != 1)
                                        {
                                            response.message = tele.Field<string>("MENSAJE");
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var direc in request.SOLICITANTE.DIRECCIONES_PERS)
                {
                    if (!entrevista.ContainsKey("¿Su ciudad de residencia?"))
                        entrevista.Add("¿Su ciudad de residencia?", direc.CIUDAD_RESIDENCIA);
                    if ((string.IsNullOrEmpty(direc.TIPO_DIRECCION) ? "" : direc.TIPO_DIRECCION.ToUpper().Trim()) == "DOMICILIO")
                    {
                        if (!entrevista.ContainsKey("¿Podría brindamer la dirección de su domicilio?"))
                            entrevista.Add("¿Podría brindamer la dirección de su domicilio?", direc.CALLE_AVENIDA + " Nro " + direc.NRO_PUERTA + " " + direc.ZONA_BARRIO);
                    }
                    if ((string.IsNullOrEmpty(direc.TIPO_DIRECCION) ? "" : direc.TIPO_DIRECCION.ToUpper().Trim()) == "LABORAL")
                    {
                        if (!entrevista.ContainsKey("¿La dirección de su lugar de trabajo?"))
                            entrevista.Add("¿La dirección de su lugar de trabajo?", direc.CALLE_AVENIDA + " Nro " + direc.NRO_PUERTA + " " + direc.ZONA_BARRIO);
                    }
                    direcciondata = _conectorBD.ModificarDireccion(direc);
                    if (direcciondata.Rows.Count != 0)
                    {
                        foreach (DataRow tel in direcciondata.Rows)
                        {

                            if (tel.Field<int>("CODIGO") == -1)
                            {
                                response.message = tel.Field<string>("MENSAJE");
                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                response.success = false;
                            }
                            if (tel.Field<int>("CODIGO") == -2)
                            {
                                if (!string.IsNullOrEmpty(direc.PAIS_RESIDENCIA)
                                        || !string.IsNullOrEmpty(direc.CIUDAD_RESIDENCIA)
                                        || !string.IsNullOrEmpty(direc.TIPO_DIRECCION)
                                        || !string.IsNullOrEmpty(direc.DEPARTAMENTO)
                                        || !string.IsNullOrEmpty(direc.LOCALIDAD)
                                        || !string.IsNullOrEmpty(direc.ZONA_BARRIO)
                                        || !string.IsNullOrEmpty(direc.CALLE_AVENIDA)
                                        || !string.IsNullOrEmpty(direc.NRO_PUERTA)
                                        || !string.IsNullOrEmpty(direc.NRO_PISO)
                                        || !string.IsNullOrEmpty(direc.NRO_DEPARTAMENTO)
                                        || !string.IsNullOrEmpty(direc.NOMBRE_EDIFICIO)
                                        || !string.IsNullOrEmpty(direc.TIPO_VIVIENDA_OFICINA)
                                        || !string.IsNullOrEmpty(direc.REFERENCIA)
                                        || direc.NRO_CASILLA != 0)
                                {
                                    direcciondata = _conectorBD.InsertarDireccion(direc, request.SOLICITANTE.ID_PERSONA, 0);
                                    if (direcciondata.Rows.Count != 0)
                                    {
                                        foreach (DataRow dir in direcciondata.Rows)
                                        {

                                            direccionresponse.ID = dir.Field<int>("ID");
                                            if (dir.Field<int>("CODIGO") != 1)
                                            {
                                                response.message = dir.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }

                                    foreach (var telefono in direc.TELEFONOS)
                                    {
                                        if (!string.IsNullOrEmpty(telefono.TIPO)
                                            || !string.IsNullOrEmpty(telefono.NUMERO)
                                            || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                        {
                                            telefonodata = _conectorBD.InsertarTelefono(telefono, direccionresponse.ID, 0);
                                            if (telefonodata.Rows.Count != 0)
                                            {
                                                foreach (DataRow tele in telefonodata.Rows)
                                                {

                                                    if (tele.Field<int>("CODIGO") != 1)
                                                    {
                                                        response.message = tele.Field<string>("MENSAJE");
                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                        response.success = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (var telefono in direc.TELEFONOS)
                    {
                        telefonodata = _conectorBD.ModificarTelefono(telefono);
                        if (telefonodata.Rows.Count != 0)
                        {
                            foreach (DataRow tel in telefonodata.Rows)
                            {

                                if (tel.Field<int>("CODIGO") == -1)
                                {
                                    response.message = tel.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                                if (tel.Field<int>("CODIGO") == -2)
                                {
                                    if (!string.IsNullOrEmpty(telefono.TIPO)
                                                || !string.IsNullOrEmpty(telefono.NUMERO)
                                                || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                    {
                                        telefonodata = _conectorBD.InsertarTelefono(telefono, direc.ID_DIRECCION, 0);
                                        if (telefonodata.Rows.Count != 0)
                                        {
                                            foreach (DataRow tele in telefonodata.Rows)
                                            {

                                                if (tele.Field<int>("CODIGO") != 1)
                                                {
                                                    response.message = tele.Field<string>("MENSAJE");
                                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                    response.success = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (request.SOLICITANTE.DOCUMENTOS != null)
                {
                    foreach (var item in request.SOLICITANTE.DOCUMENTOS)
                    {
                        if (!entrevista.ContainsKey("¿Su número de carnet de identidad?"))
                            entrevista.Add("¿Su número de carnet de identidad?", item.NRO_DOCUMENTO);
                        documentopersonaldata = _conectorBD.ModificarDocumentoPersonal(item);
                        if (documentopersonaldata.Rows.Count != 0)
                        {
                            foreach (DataRow tel in documentopersonaldata.Rows)
                            {

                                if (tel.Field<int>("CODIGO") == -1)
                                {
                                    response.message = tel.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                                if (tel.Field<int>("CODIGO") == -2)
                                {
                                    if (!string.IsNullOrEmpty(item.TIPO_DOCUMENTO)
                                    || !string.IsNullOrEmpty(item.NRO_DOCUMENTO)
                                    || !string.IsNullOrEmpty(item.EXTENSION)
                                    || !string.IsNullOrEmpty(item.ESTADO_CIVIL)
                                    || !string.IsNullOrEmpty(item.NACIONALIDAD)
                                    || !string.IsNullOrEmpty(item.LUGAR_NACIMIENTO))
                                    {
                                        documentopersonaldata = _conectorBD.InsertarDocumentoPersonal(item, request.SOLICITANTE.ID_PERSONA);
                                        if (documentopersonaldata.Rows.Count != 0)
                                        {
                                            foreach (DataRow tele in documentopersonaldata.Rows)
                                            {

                                                if (tele.Field<int>("CODIGO") != 1)
                                                {
                                                    response.message = tele.Field<string>("MENSAJE");
                                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                    response.success = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (request.ACTIVIDADES_ECONOMICAS != null)
                {
                    foreach (var item in request.ACTIVIDADES_ECONOMICAS)
                    {
                        if (!entrevista.ContainsKey("¿Su lugar o empresa de trabajo?"))
                            entrevista.Add("¿Su lugar o empresa de trabajo?", item.NOMBRE_EMPRESA);
                        if (!entrevista.ContainsKey("¿Cuál es su antigüedad laboral?"))
                            entrevista.Add("¿Cuál es su antigüedad laboral?", DiferenciaFechas(DateTime.Now, item.FECHA_INGRESO));
                        if (!entrevista.ContainsKey("¿Cuál es el cargo que ocupa?"))
                            entrevista.Add("¿Cuál es el cargo que ocupa?", item.CARGO);
                        if (!entrevista.ContainsKey("¿Tipo de contrato?"))
                            entrevista.Add("¿Tipo de contrato?", "1");
                        actividadeconomicadata = _conectorBD.ModificarActividadEconomica(item);
                        if (actividadeconomicadata.Rows.Count != 0)
                        {
                            foreach (DataRow act in actividadeconomicadata.Rows)
                            {
                                if (act.Field<int>("CODIGO") == -1)
                                {
                                    actividadeconomicaresponse.message = act.Field<string>("MENSAJE");
                                    actividadeconomicaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                    actividadeconomicaresponse.success = false;
                                }
                                if (act.Field<int>("CODIGO") == -2)
                                {
                                    if (!string.IsNullOrEmpty(item.NIVEL_LABORAL)
                                        || !string.IsNullOrEmpty(item.ACTIVIDAD_DECLARADA)
                                        || !string.IsNullOrEmpty(item.PRIORIDAD)
                                        || !string.IsNullOrEmpty(item.ESTADO)
                                        || !string.IsNullOrEmpty(item.INGRESOS_MENSUALES)
                                        || !string.IsNullOrEmpty(item.NOMBRE_EMPRESA)
                                        || !string.IsNullOrEmpty(item.CARGO)
                                        || item.NIT != 0
                                        || item.OTROS_INGRESOS_MENSUALES != 0
                                        || item.EGRESOS_MENSUALES != 0
                                        || item.MARGEN_AHORRO != 0)
                                    {
                                        actividadeconomicadata = _conectorBD.InsertarActividadEconomica(item, request.ID_CLIENTE);
                                        if (actividadeconomicadata.Rows.Count != 0)
                                        {
                                            foreach (DataRow acti in actividadeconomicadata.Rows)
                                            {

                                                actividadeconomicaresponse.ID = acti.Field<int>("ID");
                                                if (acti.Field<int>("CODIGO") != 1)
                                                {
                                                    actividadeconomicaresponse.message = act.Field<string>("MENSAJE");
                                                    actividadeconomicaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                                    actividadeconomicaresponse.success = false;
                                                }
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(item.CAE_DEC.COD_CAEDEC)
                                            || !string.IsNullOrEmpty(item.CAE_DEC.ACTIVIDAD_CAEDEC)
                                            || !string.IsNullOrEmpty(item.CAE_DEC.GRUPO_CAEDEC)
                                            || !string.IsNullOrEmpty(item.CAE_DEC.SECTOR_ECONOMICO))
                                        {
                                            caedecdata = _conectorBD.InsertarCaedec(item.CAE_DEC, actividadeconomicaresponse.ID);
                                            if (caedecdata.Rows.Count != 0)
                                            {
                                                foreach (DataRow cae in caedecdata.Rows)
                                                {
                                                    if (cae.Field<int>("CODIGO") != 1)
                                                    {
                                                        response.message = cae.Field<string>("MENSAJE");
                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                        response.success = false;
                                                    }
                                                }
                                            }
                                        }



                                        foreach (var tem in item.TIEMPO_EXP)
                                        {
                                            if (!string.IsNullOrEmpty(tem.TIPO)
                                                || tem.ANIO != 0
                                                || tem.MES != 0
                                                )
                                            {
                                                fechaAMdata = _conectorBD.InsertarFechaAM(tem, actividadeconomicaresponse.ID);

                                                if (fechaAMdata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow cae in fechaAMdata.Rows)
                                                    {

                                                        if (cae.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = cae.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }

                                            }

                                        }
                                    }
                                    if (item.DIRECCION_ACT_ECO != null)
                                    {
                                        if (!string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.PAIS_RESIDENCIA)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.CIUDAD_RESIDENCIA)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.TIPO_DIRECCION)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.DEPARTAMENTO)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.LOCALIDAD)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.ZONA_BARRIO)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.CALLE_AVENIDA)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NRO_PUERTA)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NRO_PISO)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NRO_DEPARTAMENTO)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NOMBRE_EDIFICIO)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.TIPO_VIVIENDA_OFICINA)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.REFERENCIA)
                                            || item.DIRECCION_ACT_ECO.NRO_CASILLA != 0)
                                        {
                                            direccionAEdata = _conectorBD.InsertarDireccion(item.DIRECCION_ACT_ECO, 0, actividadeconomicaresponse.ID);
                                            if (direccionAEdata.Rows.Count != 0)
                                            {

                                                foreach (DataRow dir in direccionAEdata.Rows)
                                                {

                                                    direccionAEresponse.ID = dir.Field<int>("ID");
                                                    if (dir.Field<int>("CODIGO") != 1)
                                                    {
                                                        response.message = dir.Field<string>("MENSAJE");
                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                        response.success = false;
                                                    }
                                                }



                                                foreach (var telefono in item.DIRECCION_ACT_ECO.TELEFONOS)
                                                {
                                                    if (string.IsNullOrEmpty(telefono.TIPO)
                                                        || !string.IsNullOrEmpty(telefono.NUMERO)
                                                        || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                                    {
                                                        telefonodata = _conectorBD.InsertarTelefono(telefono, direccionAEresponse.ID, 0);
                                                        if (telefonodata.Rows.Count != 0)
                                                        {
                                                            foreach (DataRow tel in telefonodata.Rows)
                                                            {

                                                                if (tel.Field<int>("CODIGO") != 1)
                                                                {
                                                                    response.message = tel.Field<string>("MENSAJE");
                                                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                                    response.success = false;
                                                                }
                                                            }
                                                        }
                                                    }

                                                }

                                            }
                                        }
                                    }

                                }
                            }
                        }

                        caedecdata = _conectorBD.ModificarCaedec(item.CAE_DEC);
                        if (caedecdata.Rows.Count != 0)
                        {
                            foreach (DataRow cae in caedecdata.Rows)
                            {
                                if (cae.Field<int>("CODIGO") == -1)
                                {
                                    response.message = cae.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                                if (cae.Field<int>("CODIGO") == -2)
                                {
                                    if (!string.IsNullOrEmpty(item.CAE_DEC.COD_CAEDEC)
                                            || !string.IsNullOrEmpty(item.CAE_DEC.ACTIVIDAD_CAEDEC)
                                            || !string.IsNullOrEmpty(item.CAE_DEC.GRUPO_CAEDEC)
                                            || !string.IsNullOrEmpty(item.CAE_DEC.SECTOR_ECONOMICO))
                                    {
                                        caedecdata = _conectorBD.InsertarCaedec(item.CAE_DEC, item.ID_ACTIVIDAD_ECONOMICA);
                                        if (caedecdata.Rows.Count != 0)
                                        {
                                            foreach (DataRow caedec in caedecdata.Rows)
                                            {
                                                if (caedec.Field<int>("CODIGO") != 1)
                                                {
                                                    response.message = caedec.Field<string>("MENSAJE");
                                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                    response.success = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        foreach (var tem in item.TIEMPO_EXP)
                        {
                            fechaAMdata = _conectorBD.ModificarFechaAM(tem);

                            if (fechaAMdata.Rows.Count != 0)
                            {
                                foreach (DataRow cae in fechaAMdata.Rows)
                                {

                                    if (cae.Field<int>("CODIGO") == -1)
                                    {
                                        response.message = cae.Field<string>("MENSAJE");
                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                        response.success = false;
                                    }
                                    if (cae.Field<int>("CODIGO") == -2)
                                    {
                                        if (!string.IsNullOrEmpty(tem.TIPO)
                                                || tem.ANIO != 0
                                                || tem.MES != 0
                                                )
                                        {
                                            fechaAMdata = _conectorBD.InsertarFechaAM(tem, item.ID_ACTIVIDAD_ECONOMICA);

                                            if (fechaAMdata.Rows.Count != 0)
                                            {
                                                foreach (DataRow caedec in fechaAMdata.Rows)
                                                {

                                                    if (caedec.Field<int>("CODIGO") != 1)
                                                    {
                                                        response.message = caedec.Field<string>("MENSAJE");
                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                        response.success = false;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }

                        direcciondata = _conectorBD.ModificarDireccion(item.DIRECCION_ACT_ECO);
                        if (direcciondata.Rows.Count != 0)
                        {
                            foreach (DataRow dir in direcciondata.Rows)
                            {
                                if (dir.Field<int>("CODIGO") == -1)
                                {
                                    response.message = dir.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                                if (dir.Field<int>("CODIGO") == -2)
                                {
                                    if (!string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.PAIS_RESIDENCIA)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.CIUDAD_RESIDENCIA)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.TIPO_DIRECCION)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.DEPARTAMENTO)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.LOCALIDAD)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.ZONA_BARRIO)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.CALLE_AVENIDA)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NRO_PUERTA)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NRO_PISO)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NRO_DEPARTAMENTO)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.NOMBRE_EDIFICIO)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.TIPO_VIVIENDA_OFICINA)
                                            || !string.IsNullOrEmpty(item.DIRECCION_ACT_ECO.REFERENCIA)
                                            || item.DIRECCION_ACT_ECO.NRO_CASILLA != 0)
                                    {
                                        direccionAEdata = _conectorBD.InsertarDireccion(item.DIRECCION_ACT_ECO, 0, item.ID_ACTIVIDAD_ECONOMICA);
                                        if (direccionAEdata.Rows.Count != 0)
                                        {

                                            foreach (DataRow dire in direccionAEdata.Rows)
                                            {

                                                direccionAEresponse.ID = dire.Field<int>("ID");
                                                if (dire.Field<int>("CODIGO") != 1)
                                                {
                                                    response.message = dire.Field<string>("MENSAJE");
                                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                    response.success = false;
                                                }
                                            }



                                            foreach (var telefono in item.DIRECCION_ACT_ECO.TELEFONOS)
                                            {
                                                if (string.IsNullOrEmpty(telefono.TIPO)
                                                    || !string.IsNullOrEmpty(telefono.NUMERO)
                                                    || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                                {
                                                    telefonodata = _conectorBD.InsertarTelefono(telefono, direccionAEresponse.ID, 0);
                                                    if (telefonodata.Rows.Count != 0)
                                                    {
                                                        foreach (DataRow tele in telefonodata.Rows)
                                                        {

                                                            if (tele.Field<int>("CODIGO") != 1)
                                                            {
                                                                response.message = tele.Field<string>("MENSAJE");
                                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                                response.success = false;
                                                            }
                                                        }
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                            }
                        }

                        foreach (var telefono in item.DIRECCION_ACT_ECO.TELEFONOS)
                        {
                            telefonodata = _conectorBD.ModificarTelefono(telefono);
                            if (telefonodata.Rows.Count != 0)
                            {
                                foreach (DataRow tel in telefonodata.Rows)
                                {

                                    if (tel.Field<int>("CODIGO") == -1)
                                    {
                                        response.message = tel.Field<string>("MENSAJE");
                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                        response.success = false;
                                    }
                                    if (tel.Field<int>("CODIGO") == -2)
                                    {
                                        if (string.IsNullOrEmpty(telefono.TIPO)
                                                    || !string.IsNullOrEmpty(telefono.NUMERO)
                                                    || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                        {
                                            telefonodata = _conectorBD.InsertarTelefono(telefono, item.DIRECCION_ACT_ECO.ID_DIRECCION, 0);
                                            if (telefonodata.Rows.Count != 0)
                                            {
                                                foreach (DataRow tele in telefonodata.Rows)
                                                {

                                                    if (tele.Field<int>("CODIGO") != 1)
                                                    {
                                                        response.message = tele.Field<string>("MENSAJE");
                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                        response.success = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }

                if (request.REFERENCIAS != null)
                {
                    foreach (var item in request.REFERENCIAS)
                    {
                        referenciadata = _conectorBD.ModificarReferencia(item);
                        if (referenciadata.Rows.Count != 0)
                        {
                            foreach (DataRow act in referenciadata.Rows)
                            {
                                if (act.Field<int>("CODIGO") == -1)
                                {
                                    referenciaresponse.message = act.Field<string>("MENSAJE");
                                    referenciaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                    referenciaresponse.success = false;
                                }
                                if (act.Field<int>("CODIGO") == -2)
                                {
                                    if (!string.IsNullOrEmpty(item.TIPO)
                                || !string.IsNullOrEmpty(item.RELACION)
                                || !string.IsNullOrEmpty(item.OBSERVACION)
                                || !string.IsNullOrEmpty(item.CALIFICACION)
                                || !string.IsNullOrEmpty(item.DESCRIPCION_CALIFICACION)
                                )
                                    {
                                        referenciadata = _conectorBD.InsertarReferencia(item, request.ID_CLIENTE);
                                        if (referenciadata.Rows.Count != 0)
                                        {
                                            foreach (DataRow acti in referenciadata.Rows)
                                            {

                                                referenciaresponse.ID = acti.Field<int>("ID");
                                                if (acti.Field<int>("CODIGO") != 1)
                                                {
                                                    referenciaresponse.message = acti.Field<string>("MENSAJE");
                                                    referenciaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                                    referenciaresponse.success = false;
                                                }
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(item.REFERIDO.GENERO)
                                            || !string.IsNullOrEmpty(item.REFERIDO.PRIMER_APELLIDO)
                                            || !string.IsNullOrEmpty(item.REFERIDO.NOMBRES)
                                            || !string.IsNullOrEmpty(item.REFERIDO.CORREO)
                                            || !string.IsNullOrEmpty(item.REFERIDO.NIVEL_EDUCACION)
                                            || !string.IsNullOrEmpty(item.REFERIDO.PROFESION)
                                            || !string.IsNullOrEmpty(item.REFERIDO.ESTADO_CIVIL))
                                        {
                                            personadata = _conectorBD.InsertarPersona(item.REFERIDO, 0, referenciaresponse.ID);

                                            if (personadata.Rows.Count != 0)
                                            {
                                                foreach (DataRow per in personadata.Rows)
                                                {

                                                    personaresponse.ID = per.Field<int>("ID");
                                                    if (per.Field<int>("CODIGO") != 1)
                                                    {
                                                        personaresponse.message = per.Field<string>("MENSAJE");
                                                        personaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                                        personaresponse.success = false;
                                                    }
                                                }
                                            }

                                            foreach (var per in item.REFERIDO.DIRECCIONES_PERS)
                                            {
                                                if (!string.IsNullOrEmpty(per.PAIS_RESIDENCIA)
                                                    || !string.IsNullOrEmpty(per.CIUDAD_RESIDENCIA)
                                                    || !string.IsNullOrEmpty(per.TIPO_DIRECCION)
                                                    || !string.IsNullOrEmpty(per.DEPARTAMENTO)
                                                    || !string.IsNullOrEmpty(per.LOCALIDAD)
                                                    || !string.IsNullOrEmpty(per.ZONA_BARRIO)
                                                    || !string.IsNullOrEmpty(per.CALLE_AVENIDA)
                                                    || !string.IsNullOrEmpty(per.NRO_PUERTA)
                                                    || !string.IsNullOrEmpty(per.NRO_PISO)
                                                    || !string.IsNullOrEmpty(per.NRO_DEPARTAMENTO)
                                                    || !string.IsNullOrEmpty(per.NOMBRE_EDIFICIO)
                                                    || !string.IsNullOrEmpty(per.TIPO_VIVIENDA_OFICINA)
                                                    || !string.IsNullOrEmpty(per.REFERENCIA)
                                                    || per.NRO_CASILLA != 0)
                                                {
                                                    direcciondata = _conectorBD.InsertarDireccion(per, personaresponse.ID, 0);
                                                    if (direcciondata.Rows.Count != 0)
                                                    {
                                                        foreach (DataRow tel in direcciondata.Rows)
                                                        {
                                                            direccionresponse.ID = tel.Field<int>("ID");
                                                            if (tel.Field<int>("CODIGO") != 1)
                                                            {
                                                                direccionresponse.message = tel.Field<string>("MENSAJE");
                                                                direccionresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                                                direccionresponse.success = false;
                                                            }
                                                        }

                                                        foreach (var telefono in per.TELEFONOS)
                                                        {
                                                            if (!string.IsNullOrEmpty(telefono.TIPO)
                                                                || !string.IsNullOrEmpty(telefono.NUMERO)
                                                                || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                                            {
                                                                telefonodata = _conectorBD.InsertarTelefono(telefono, direccionresponse.ID, 0);
                                                                if (telefonodata.Rows.Count != 0)
                                                                {
                                                                    foreach (DataRow tel in telefonodata.Rows)
                                                                    {

                                                                        if (tel.Field<int>("CODIGO") != 1)
                                                                        {
                                                                            response.message = tel.Field<string>("MENSAJE");
                                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                                            response.success = false;
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                        }
                                                    }
                                                }



                                            }

                                            foreach (var per in item.REFERIDO.DOCUMENTOS)
                                            {
                                                documentopersonaldata = _conectorBD.InsertarDocumentoPersonal(per, personaresponse.ID);
                                                if (documentopersonaldata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow tel in documentopersonaldata.Rows)
                                                    {

                                                        if (tel.Field<int>("CODIGO") == -1)
                                                        {
                                                            response.message = tel.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                        if (tel.Field<int>("CODIGO") == -2)
                                                        {

                                                        }
                                                    }
                                                }
                                            }
                                        }


                                    }
                                }
                            }
                        }

                        personadata = _conectorBD.ModificarPersona(item.REFERIDO);

                        if (personadata.Rows.Count != 0)
                        {
                            foreach (DataRow pers in personadata.Rows)
                            {

                                if (pers.Field<int>("CODIGO") == -1)
                                {
                                    response.message = pers.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                                if (pers.Field<int>("CODIGO") == -2)
                                {
                                    if (!string.IsNullOrEmpty(item.REFERIDO.GENERO)
                                            || !string.IsNullOrEmpty(item.REFERIDO.PRIMER_APELLIDO)
                                            || !string.IsNullOrEmpty(item.REFERIDO.NOMBRES)
                                            || !string.IsNullOrEmpty(item.REFERIDO.CORREO)
                                            || !string.IsNullOrEmpty(item.REFERIDO.NIVEL_EDUCACION)
                                            || !string.IsNullOrEmpty(item.REFERIDO.PROFESION)
                                            || !string.IsNullOrEmpty(item.REFERIDO.ESTADO_CIVIL))
                                    {
                                        personadata = _conectorBD.InsertarPersona(item.REFERIDO, 0, item.ID_REFERENCIA);

                                        if (personadata.Rows.Count != 0)
                                        {
                                            foreach (DataRow perso in personadata.Rows)
                                            {

                                                personaresponse.ID = perso.Field<int>("ID");
                                                if (perso.Field<int>("CODIGO") != 1)
                                                {
                                                    personaresponse.message = perso.Field<string>("MENSAJE");
                                                    personaresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                                    personaresponse.success = false;
                                                }
                                            }
                                        }

                                        foreach (var per in item.REFERIDO.DIRECCIONES_PERS)
                                        {
                                            if (!string.IsNullOrEmpty(per.PAIS_RESIDENCIA)
                                            || !string.IsNullOrEmpty(per.CIUDAD_RESIDENCIA)
                                            || !string.IsNullOrEmpty(per.TIPO_DIRECCION)
                                            || !string.IsNullOrEmpty(per.DEPARTAMENTO)
                                            || !string.IsNullOrEmpty(per.LOCALIDAD)
                                            || !string.IsNullOrEmpty(per.ZONA_BARRIO)
                                            || !string.IsNullOrEmpty(per.CALLE_AVENIDA)
                                            || !string.IsNullOrEmpty(per.NRO_PUERTA)
                                            || !string.IsNullOrEmpty(per.NRO_PISO)
                                            || !string.IsNullOrEmpty(per.NRO_DEPARTAMENTO)
                                            || !string.IsNullOrEmpty(per.NOMBRE_EDIFICIO)
                                            || !string.IsNullOrEmpty(per.TIPO_VIVIENDA_OFICINA)
                                            || !string.IsNullOrEmpty(per.REFERENCIA)
                                            || per.NRO_CASILLA != 0)
                                            {
                                                direcciondata = _conectorBD.InsertarDireccion(per, personaresponse.ID, 0);
                                                if (direcciondata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow tel in direcciondata.Rows)
                                                    {
                                                        direccionresponse.ID = tel.Field<int>("ID");
                                                        if (tel.Field<int>("CODIGO") != 1)
                                                        {
                                                            direccionresponse.message = tel.Field<string>("MENSAJE");
                                                            direccionresponse.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            direccionresponse.success = false;
                                                        }
                                                    }

                                                    foreach (var telefono in per.TELEFONOS)
                                                    {
                                                        if (!string.IsNullOrEmpty(telefono.TIPO)
                                                            || !string.IsNullOrEmpty(telefono.NUMERO)
                                                            || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                                        {
                                                            telefonodata = _conectorBD.InsertarTelefono(telefono, direccionresponse.ID, 0);
                                                            if (telefonodata.Rows.Count != 0)
                                                            {
                                                                foreach (DataRow tel in telefonodata.Rows)
                                                                {

                                                                    if (tel.Field<int>("CODIGO") != 1)
                                                                    {
                                                                        response.message = tel.Field<string>("MENSAJE");
                                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                                        response.success = false;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                            }



                                        }

                                        foreach (var per in item.REFERIDO.DOCUMENTOS)
                                        {
                                            documentopersonaldata = _conectorBD.InsertarDocumentoPersonal(per, personaresponse.ID);
                                            if (documentopersonaldata.Rows.Count != 0)
                                            {
                                                foreach (DataRow tel in documentopersonaldata.Rows)
                                                {

                                                    if (tel.Field<int>("CODIGO") != 1)
                                                    {
                                                        response.message = tel.Field<string>("MENSAJE");
                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                        response.success = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }



                        foreach (var per in item.REFERIDO.DIRECCIONES_PERS)
                        {
                            direcciondata = _conectorBD.ModificarDireccion(per);
                            if (direcciondata.Rows.Count != 0)
                            {
                                foreach (DataRow tel in direcciondata.Rows)
                                {

                                    if (tel.Field<int>("CODIGO") == -1)
                                    {
                                        response.message = tel.Field<string>("MENSAJE");
                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                        response.success = false;
                                    }
                                    if (tel.Field<int>("CODIGO") == -2)
                                    {
                                        if (!string.IsNullOrEmpty(per.PAIS_RESIDENCIA)
                                            || !string.IsNullOrEmpty(per.CIUDAD_RESIDENCIA)
                                            || !string.IsNullOrEmpty(per.TIPO_DIRECCION)
                                            || !string.IsNullOrEmpty(per.DEPARTAMENTO)
                                            || !string.IsNullOrEmpty(per.LOCALIDAD)
                                            || !string.IsNullOrEmpty(per.ZONA_BARRIO)
                                            || !string.IsNullOrEmpty(per.CALLE_AVENIDA)
                                            || !string.IsNullOrEmpty(per.NRO_PUERTA)
                                            || !string.IsNullOrEmpty(per.NRO_PISO)
                                            || !string.IsNullOrEmpty(per.NRO_DEPARTAMENTO)
                                            || !string.IsNullOrEmpty(per.NOMBRE_EDIFICIO)
                                            || !string.IsNullOrEmpty(per.TIPO_VIVIENDA_OFICINA)
                                            || !string.IsNullOrEmpty(per.REFERENCIA)
                                            || per.NRO_CASILLA != 0)
                                        {
                                            direcciondata = _conectorBD.InsertarDireccion(per, item.REFERIDO.ID_PERSONA, 0);
                                            if (direcciondata.Rows.Count != 0)
                                            {
                                                foreach (DataRow dir in direcciondata.Rows)
                                                {

                                                    direccionresponse.ID = dir.Field<int>("ID");
                                                    if (dir.Field<int>("CODIGO") != 1)
                                                    {
                                                        response.message = dir.Field<string>("MENSAJE");
                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                        response.success = false;
                                                    }
                                                }
                                            }

                                            foreach (var telefono in per.TELEFONOS)
                                            {
                                                if (!string.IsNullOrEmpty(telefono.TIPO)
                                                    || !string.IsNullOrEmpty(telefono.NUMERO)
                                                    || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                                {
                                                    telefonodata = _conectorBD.InsertarTelefono(telefono, direccionresponse.ID, 0);
                                                    if (telefonodata.Rows.Count != 0)
                                                    {
                                                        foreach (DataRow tele in telefonodata.Rows)
                                                        {

                                                            if (tele.Field<int>("CODIGO") != 1)
                                                            {
                                                                response.message = tele.Field<string>("MENSAJE");
                                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                                response.success = false;
                                                            }
                                                        }
                                                    }
                                                }


                                            }
                                        }
                                    }
                                }
                            }
                            foreach (var telefono in per.TELEFONOS)
                            {
                                telefonodata = _conectorBD.ModificarTelefono(telefono);
                                if (telefonodata.Rows.Count != 0)
                                {
                                    foreach (DataRow tel in telefonodata.Rows)
                                    {

                                        if (tel.Field<int>("CODIGO") == -1)
                                        {
                                            response.message = tel.Field<string>("MENSAJE");
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                        }
                                        if (tel.Field<int>("CODIGO") == -2)
                                        {
                                            if (string.IsNullOrEmpty(telefono.TIPO)
                                                        || !string.IsNullOrEmpty(telefono.NUMERO)
                                                        || !string.IsNullOrEmpty(telefono.DESCRIPCION))
                                            {
                                                telefonodata = _conectorBD.InsertarTelefono(telefono, per.ID_DIRECCION, 0);
                                                if (telefonodata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow tele in telefonodata.Rows)
                                                    {

                                                        if (tele.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = tele.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (item.REFERIDO.DOCUMENTOS != null)
                        {
                            foreach (var per in item.REFERIDO.DOCUMENTOS)
                            {
                                documentopersonaldata = _conectorBD.ModificarDocumentoPersonal(per);
                                if (documentopersonaldata.Rows.Count != 0)
                                {
                                    foreach (DataRow tel in documentopersonaldata.Rows)
                                    {

                                        if (tel.Field<int>("CODIGO") == -1)
                                        {
                                            response.message = tel.Field<string>("MENSAJE");
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                        }
                                        if (tel.Field<int>("CODIGO") == -2)
                                        {
                                            if (!string.IsNullOrEmpty(per.TIPO_DOCUMENTO)
                                                || !string.IsNullOrEmpty(per.NRO_DOCUMENTO)
                                                || !string.IsNullOrEmpty(per.EXTENSION)
                                                || !string.IsNullOrEmpty(per.ESTADO_CIVIL)
                                                || !string.IsNullOrEmpty(per.NACIONALIDAD)
                                                || !string.IsNullOrEmpty(per.LUGAR_NACIMIENTO))
                                            {
                                                documentopersonaldata = _conectorBD.InsertarDocumentoPersonal(per, item.REFERIDO.ID_PERSONA);
                                                if (documentopersonaldata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow tele in documentopersonaldata.Rows)
                                                    {

                                                        if (tele.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = tele.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (item.REFERIDO.CONTACTO != null)
                        {
                            telefonodata = _conectorBD.ModificarTelefono(item.REFERIDO.CONTACTO);

                            if (telefonodata.Rows.Count != 0)
                            {
                                foreach (DataRow tel in telefonodata.Rows)
                                {

                                    if (tel.Field<int>("CODIGO") == -1)
                                    {
                                        response.message = tel.Field<string>("MENSAJE");
                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                        response.success = false;
                                    }
                                    if (tel.Field<int>("CODIGO") == -2)
                                    {
                                        if (!string.IsNullOrEmpty(request.SOLICITANTE.CONTACTO.TIPO)
                                                    || !string.IsNullOrEmpty(request.SOLICITANTE.CONTACTO.NUMERO)
                                                    || !string.IsNullOrEmpty(request.SOLICITANTE.CONTACTO.DESCRIPCION))
                                        {
                                            telefonodata = _conectorBD.InsertarTelefono(request.SOLICITANTE.CONTACTO, 0, request.SOLICITANTE.ID_PERSONA);
                                            if (telefonodata.Rows.Count != 0)
                                            {
                                                foreach (DataRow tele in telefonodata.Rows)
                                                {

                                                    if (tele.Field<int>("CODIGO") != 1)
                                                    {
                                                        response.message = tele.Field<string>("MENSAJE");
                                                        response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                        response.success = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (request.CREDITOS != null)
                {
                    foreach (var cre in request.CREDITOS)
                    {
                        if (!entrevista.ContainsKey("¿Usted esta solicitando el siguiente monto de financimiento?"))
                            entrevista.Add("¿Usted esta solicitando el siguiente monto de financimiento?", cre.MONTO_CREDITO.ToString());
                        if (!entrevista.ContainsKey("El destino de su crédito es para:"))
                            entrevista.Add("El destino de su crédito es para:", cre.DESTINO);
                        if (!entrevista.ContainsKey("¿El plazo que solicitó?"))
                            entrevista.Add("¿El plazo que solicitó?", cre.PLAZO.ToString());
                        decimal num4 = 0;
                        if (cre.PLAZO != 0)
                        {
                            double num1 = (double)cre.MONTO_CREDITO;
                            double num2 = (double)cre.PLAZO;
                            double num3 = (((num1 * num2 * (0.32 / 12) / 1.74) + num1)) / num2;

                            num4 = decimal.Round((decimal)num3, 2);
                        }
                        if (!entrevista.ContainsKey("Su cuota mensual para el crédito solicitado es de:"))
                            entrevista.Add("Su cuota mensual para el crédito solicitado es de:", num4.ToString());

                        creditodata = _conectorBD.ModificarCredito(cre);

                        if (creditodata.Rows.Count != 0)
                        {
                            foreach (DataRow credi in creditodata.Rows)
                            {
                                if (credi.Field<int>("CODIGO") == -1)
                                {
                                    response.message = credi.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }

                                if (credi.Field<int>("CODIGO") == -2)
                                {
                                    creditodata = _conectorBD.InsertarCredito(cre, cre.ID_CREDITO);

                                    if (creditodata.Rows.Count != 0)
                                    {
                                        foreach (DataRow item in creditodata.Rows)
                                        {
                                            creditoresponse.ID = item.Field<int>("ID");
                                            if (item.Field<int>("CODIGO") != 1)
                                            {
                                                response.message = item.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }

                                    if (cre.DECLARACIONES != null)
                                    {
                                        foreach (var item in cre.DECLARACIONES)
                                        {
                                            if (!string.IsNullOrEmpty(item.TIPO)
                                                || !string.IsNullOrEmpty(item.OBSERVACIONES)
                                                || item.PATRIMONIO_ACTIVO != 0
                                                || item.PATRIMONIO_PASIVO != 0
                                                || item.PERSONAL_OCUPADO != 0
                                                || item.TOTAL_INGRESO_VENTAS != 0
                                                )
                                            {
                                                declaracionjuradadata = _conectorBD.InsertarDeclaracionJurada(item, creditoresponse.ID);
                                                if (declaracionjuradadata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow act in declaracionjuradadata.Rows)
                                                    {
                                                        if (act.Field<int>("CODIGO") == -1)
                                                        {
                                                            response.message = act.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }


                                    if (cre.DOCUMENTOS_ENTREGADOS != null)
                                    {
                                        foreach (var item in cre.DOCUMENTOS_ENTREGADOS)
                                        {
                                            if (!string.IsNullOrEmpty(item.DOCUMENTO_DESCRIPCION) || !string.IsNullOrEmpty(item.NOMBRE_ARCHIVO))
                                            {
                                                documentoentregadodata = _conectorBD.InsertarDocumentoEntregado(item, creditoresponse.ID);
                                                if (documentoentregadodata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow act in documentoentregadodata.Rows)
                                                    {
                                                        if (act.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = act.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (cre.AUTORIZACIONES != null)
                                    {
                                        foreach (var item in cre.AUTORIZACIONES)
                                        {
                                            if (!string.IsNullOrEmpty(item.AUTORIZACION_ESPECIAL)
                                                || !string.IsNullOrEmpty(item.DESCRIPCION)
                                                )
                                            {
                                                autorizaciondata = _conectorBD.InsertarAutorizacion(item, creditoresponse.ID);
                                                if (autorizaciondata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow act in autorizaciondata.Rows)
                                                    {
                                                        if (act.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = act.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }


                            }


                        }

                        if (cre.DECLARACIONES != null)
                        {
                            foreach (var item in cre.DECLARACIONES)
                            {
                                declaracionjuradadata = _conectorBD.ModificarDeclaracionJurada(item);
                                if (declaracionjuradadata.Rows.Count != 0)
                                {
                                    foreach (DataRow act in declaracionjuradadata.Rows)
                                    {
                                        if (act.Field<int>("CODIGO") == -1)
                                        {
                                            response.message = act.Field<string>("MENSAJE");
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                        }
                                        if (act.Field<int>("CODIGO") == -2)
                                        {
                                            if (!string.IsNullOrEmpty(item.TIPO)
                                                || !string.IsNullOrEmpty(item.OBSERVACIONES)
                                                || item.PATRIMONIO_ACTIVO != 0
                                                || item.PATRIMONIO_PASIVO != 0
                                                || item.PERSONAL_OCUPADO != 0
                                                || item.TOTAL_INGRESO_VENTAS != 0
                                        )
                                            {
                                                declaracionjuradadata = _conectorBD.InsertarDeclaracionJurada(item, cre.ID_CREDITO);
                                                if (declaracionjuradadata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow acti in declaracionjuradadata.Rows)
                                                    {
                                                        if (acti.Field<int>("CODIGO") == -1)
                                                        {
                                                            response.message = acti.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (cre.DOCUMENTOS_ENTREGADOS != null)
                        {
                            foreach (var item in cre.DOCUMENTOS_ENTREGADOS)
                            {
                                documentoentregadodata = _conectorBD.ModificarDocumentoEntregado(item);
                                if (documentoentregadodata.Rows.Count != 0)
                                {
                                    foreach (DataRow act in documentoentregadodata.Rows)
                                    {
                                        if (act.Field<int>("CODIGO") == -1)
                                        {
                                            response.message = act.Field<string>("MENSAJE");
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                        }
                                        if (act.Field<int>("CODIGO") == -2)
                                        {
                                            if (!string.IsNullOrEmpty(item.DOCUMENTO_DESCRIPCION) || !string.IsNullOrEmpty(item.NOMBRE_ARCHIVO))
                                            {
                                                documentoentregadodata = _conectorBD.InsertarDocumentoEntregado(item, cre.ID_CREDITO);
                                                if (documentoentregadodata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow acti in documentoentregadodata.Rows)
                                                    {
                                                        if (acti.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = acti.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (cre.AUTORIZACIONES != null)
                        {
                            foreach (var item in cre.AUTORIZACIONES)
                            {
                                autorizaciondata = _conectorBD.ModificarAutorizacion(item);
                                if (autorizaciondata.Rows.Count != 0)
                                {
                                    foreach (DataRow act in autorizaciondata.Rows)
                                    {
                                        if (act.Field<int>("CODIGO") == -1)
                                        {
                                            response.message = act.Field<string>("MENSAJE");
                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                            response.success = false;
                                        }
                                        if (act.Field<int>("CODIGO") == -2)
                                        {
                                            if (!string.IsNullOrEmpty(item.AUTORIZACION_ESPECIAL)
                                        || !string.IsNullOrEmpty(item.DESCRIPCION)
                                        )
                                            {
                                                autorizaciondata = _conectorBD.InsertarAutorizacion(item, cre.ID_CREDITO);
                                                if (autorizaciondata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow acti in autorizaciondata.Rows)
                                                    {
                                                        if (acti.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = acti.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }


                    }
                }

                if (request.ENTREVISTA != null)
                {
                    if (request.ENTREVISTA.VERIFICACION_CLIENTE.LISTADO_VERIFICACION_CLIENTE != null)
                    {
                        VERIFICACION_CLIENTE_RESPONSE cliente = new VERIFICACION_CLIENTE_RESPONSE();
                        foreach (var item in request.ENTREVISTA.VERIFICACION_CLIENTE.LISTADO_VERIFICACION_CLIENTE)
                        {
                            if (string.IsNullOrEmpty(PREGUNTA))
                            {
                                PREGUNTA = CodigoVeriCliente(item.PREGUNTA);
                                foreach (KeyValuePair<string, string> user in entrevista)
                                {
                                    var otros = item.RESPUESTA;
                                    string aux1 = DescripcionVeriCliente(item.RESPUESTA);
                                    if (!string.IsNullOrEmpty(user.Key))
                                    {
                                        string aux = DescripcionVeriCliente(item.RESPUESTA);
                                        if (user.Key == item.PREGUNTA)
                                        {
                                            RESPUESTA = user.Value;
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(item.RESPUESTA))
                                {
                                    RESPUESTA = "";
                                }
                                CONFIRMACION = item.CONFIRMACION;
                                OBSERVACIONES = item.OBSERVACIONES;
                            }
                            else
                            {
                                PREGUNTA = PREGUNTA + " | " + CodigoVeriCliente(item.PREGUNTA);
                                foreach (KeyValuePair<string, string> user in entrevista)
                                {
                                    var otros = item.RESPUESTA;
                                    string aux1 = DescripcionVeriCliente(item.RESPUESTA);
                                    if (!string.IsNullOrEmpty(user.Key))
                                    {
                                        string aux = DescripcionVeriCliente(item.RESPUESTA);
                                        if (user.Key == item.PREGUNTA)
                                        {
                                            RESPUESTA = RESPUESTA + " | " + user.Value;
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(item.RESPUESTA))
                                {
                                    RESPUESTA = RESPUESTA + " | " + "";
                                }
                                CONFIRMACION = CONFIRMACION + " | " + item.CONFIRMACION;
                                OBSERVACIONES = OBSERVACIONES + " | " + item.OBSERVACIONES;
                            }
                            cliente.USUARIO = item.USUARIO;
                        }

                        cliente.PREGUNTA = PREGUNTA;
                        cliente.RESPUESTA = RESPUESTA;
                        cliente.CONFIRMACION = CONFIRMACION;
                        cliente.OBSERVACIONES = OBSERVACIONES;

                        verificacionclientedata = _conectorBD.ModificarVerificacionCliente(cliente, request.ENTREVISTA.VERIFICACION_CLIENTE.ID_ENTREVISTA_VERI_CLIENTE, request.ID_CLIENTE);
                        if (verificacionclientedata.Rows.Count != 0)
                        {
                            foreach (DataRow act in verificacionclientedata.Rows)
                            {
                                if (act.Field<int>("CODIGO") == -1)
                                {
                                    response.message = act.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                                if (act.Field<int>("CODIGO") == -2)
                                {
                                    verificacionclientedata = _conectorBD.InsertarVerificacionCliente(cliente, request.ID_CLIENTE);
                                    if (verificacionclientedata.Rows.Count != 0)
                                    {
                                        foreach (DataRow acta in verificacionclientedata.Rows)
                                        {
                                            if (acta.Field<int>("CODIGO") == -1)
                                            {
                                                response.message = acta.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    PREGUNTA = string.Empty;
                    RESPUESTA = string.Empty;
                    CONFIRMACION = string.Empty;
                    OBSERVACIONES = string.Empty;
                    INSTITUCIONES = string.Empty;
                    NOMBRE = string.Empty;
                    MONTO = string.Empty;
                    if (request.ENTREVISTA.SCRIPT_EVALUACION.LISTADO_SCRIPT_EVALUACION != null)
                    {
                        SCRIPT_EVALUACION_RESPONSE script = new SCRIPT_EVALUACION_RESPONSE();
                        foreach (var item in request.ENTREVISTA.SCRIPT_EVALUACION.LISTADO_SCRIPT_EVALUACION)
                        {
                            INSTITUCION cliente = new INSTITUCION();
                            if (string.IsNullOrEmpty(PREGUNTA))
                            {
                                PREGUNTA = CodigoScripEvaluacion(item.PREGUNTA);
                                RESPUESTA = item.RESPUESTA;
                                OBSERVACIONES = item.OBSERVACIONES;

                                if (item.INSTITUCIONES != null)
                                {
                                    foreach (var ins in item.INSTITUCIONES)
                                    {

                                        if (string.IsNullOrEmpty(NOMBRE))
                                        {
                                            if (string.IsNullOrEmpty(CodigoInstitucion(ins.NOMBRE)))
                                            {
                                                var nuevainstitucion = _conectorBD.InsertarInstitucion(ins.NOMBRE, item.USUARIO);
                                                if (nuevainstitucion.Rows.Count != 0)
                                                {
                                                    foreach (DataRow acta in nuevainstitucion.Rows)
                                                    {
                                                        if (acta.Field<int>("CODIGO") == -1)
                                                        {
                                                            response.message = acta.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                            NOMBRE = CodigoInstitucion(ins.NOMBRE);

                                            MONTO = ins.MONTO;
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(CodigoInstitucion(ins.NOMBRE)))
                                            {
                                                var nuevainstitucion = _conectorBD.InsertarInstitucion(ins.NOMBRE, item.USUARIO);
                                                if (nuevainstitucion.Rows.Count != 0)
                                                {
                                                    foreach (DataRow acta in nuevainstitucion.Rows)
                                                    {
                                                        if (acta.Field<int>("CODIGO") == -1)
                                                        {
                                                            response.message = acta.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                            NOMBRE = NOMBRE + " | " + CodigoInstitucion(ins.NOMBRE);
                                            MONTO = MONTO + " | " + ins.MONTO;

                                        }
                                    }
                                    cliente.NOMBRE = NOMBRE;
                                    cliente.MONTO = MONTO;
                                    cliente.USUARIO = item.USUARIO;
                                    instituciondata = _conectorBD.ModificarScripEvaluacionInstitucion(cliente, item.ID_INSTITUCIONES, item.TOTAL);
                                    if (instituciondata.Rows.Count != 0)
                                    {
                                        foreach (DataRow insti in instituciondata.Rows)
                                        {


                                            if (insti.Field<int>("CODIGO") == -1)
                                            {
                                                response.message = insti.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                            if (insti.Field<int>("CODIGO") == -2)
                                            {
                                                instituciondata = _conectorBD.InsertarScripEvaluacionInstitucion(cliente, item.TOTAL);
                                                if (instituciondata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow instis in instituciondata.Rows)
                                                    {

                                                        institucionesresponse.ID = instis.Field<int>("ID");
                                                        if (instis.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = instis.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }

                                INSTITUCIONES = item.ID_INSTITUCIONES.ToString();
                            }
                            else
                            {
                                PREGUNTA = PREGUNTA + " | " + CodigoScripEvaluacion(item.PREGUNTA);
                                RESPUESTA = RESPUESTA + " | " + item.RESPUESTA;
                                OBSERVACIONES = OBSERVACIONES + " | " + item.OBSERVACIONES;


                                if (item.INSTITUCIONES != null)
                                {
                                    NOMBRE = string.Empty;
                                    MONTO = string.Empty;
                                    foreach (var ins in item.INSTITUCIONES)
                                    {
                                        if (string.IsNullOrEmpty(NOMBRE))
                                        {
                                            NOMBRE = CodigoInstitucion(ins.NOMBRE);
                                            MONTO = ins.MONTO;
                                        }
                                        else
                                        {
                                            NOMBRE = NOMBRE + " | " + CodigoInstitucion(ins.NOMBRE);
                                            MONTO = MONTO + " | " + ins.MONTO;

                                        }
                                    }
                                    cliente.NOMBRE = NOMBRE;
                                    cliente.MONTO = MONTO;
                                    cliente.USUARIO = item.USUARIO;
                                    instituciondata = _conectorBD.ModificarScripEvaluacionInstitucion(cliente, item.ID_INSTITUCIONES, item.TOTAL);
                                    if (instituciondata.Rows.Count != 0)
                                    {
                                        foreach (DataRow insti in instituciondata.Rows)
                                        {

                                            if (insti.Field<int>("CODIGO") == -1)
                                            {
                                                response.message = insti.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                            if (insti.Field<int>("CODIGO") == -2)
                                            {
                                                instituciondata = _conectorBD.InsertarScripEvaluacionInstitucion(cliente, item.TOTAL);
                                                if (instituciondata.Rows.Count != 0)
                                                {
                                                    foreach (DataRow instis in instituciondata.Rows)
                                                    {

                                                        institucionesresponse.ID = instis.Field<int>("ID");
                                                        if (instis.Field<int>("CODIGO") != 1)
                                                        {
                                                            response.message = instis.Field<string>("MENSAJE");
                                                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                            response.success = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                                INSTITUCIONES = INSTITUCIONES + " | " + item.ID_INSTITUCIONES.ToString();
                            }
                            script.USUARIO = item.USUARIO;
                        }
                        script.PREGUNTA = PREGUNTA;
                        script.RESPUESTA = RESPUESTA;
                        script.OBSERVACIONES = OBSERVACIONES;

                        scripevaluaciondata = _conectorBD.ModificarScripEvaluacion(script, request.ENTREVISTA.SCRIPT_EVALUACION.ID_SCRIPT_EVALUACION, INSTITUCIONES, request.ID_CLIENTE);

                        if (scripevaluaciondata.Rows.Count != 0)
                        {
                            foreach (DataRow act in scripevaluaciondata.Rows)
                            {
                                if (act.Field<int>("CODIGO") == -1)
                                {
                                    response.message = act.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                                if (act.Field<int>("CODIGO") == -2)
                                {
                                    scripevaluaciondata = _conectorBD.InsertarScripEvaluacion(script, INSTITUCIONES, request.ID_CLIENTE);

                                    if (scripevaluaciondata.Rows.Count != 0)
                                    {
                                        foreach (DataRow acta in scripevaluaciondata.Rows)
                                        {
                                            if (acta.Field<int>("CODIGO") == -1)
                                            {
                                                response.message = acta.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    PREGUNTA = string.Empty;
                    RESPUESTA = string.Empty;
                    CONFIRMACION = string.Empty;
                    OBSERVACIONES = string.Empty;
                    INSTITUCIONES = string.Empty;
                    NOMBRE = string.Empty;
                    MONTO = string.Empty;

                    if (request.ENTREVISTA.SEGURO_DESGRAVAME.LISTADO_SEGURO_DESGRAVAME != null)
                    {
                        SEGURO_DESGRAVAME_RESPONSE cliente = new SEGURO_DESGRAVAME_RESPONSE();
                        foreach (var item in request.ENTREVISTA.SEGURO_DESGRAVAME.LISTADO_SEGURO_DESGRAVAME)
                        {


                            if (string.IsNullOrEmpty(PREGUNTA))
                            {
                                PREGUNTA = CodigoSeguroDesgravame(item.PREGUNTA);
                                RESPUESTA = item.RESPUESTA;
                                CONFIRMACION = item.CONFIRMACION;
                                OBSERVACIONES = item.OBSERVACIONES;
                            }
                            else
                            {
                                PREGUNTA = PREGUNTA + " | " + CodigoSeguroDesgravame(item.PREGUNTA);
                                RESPUESTA = RESPUESTA + " | " + item.RESPUESTA;
                                CONFIRMACION = CONFIRMACION + " | " + item.CONFIRMACION;
                                OBSERVACIONES = OBSERVACIONES + " | " + item.OBSERVACIONES;
                            }
                            cliente.USUARIO = item.USUARIO;
                        }

                        cliente.PREGUNTA = PREGUNTA;
                        cliente.RESPUESTA = RESPUESTA;
                        cliente.CONFIRMACION = CONFIRMACION;
                        cliente.OBSERVACIONES = OBSERVACIONES;
                        verificacionclientedata = _conectorBD.ModificarSeguroDesgravame(cliente, request.ENTREVISTA.SEGURO_DESGRAVAME.ID_SEGURO_DESGRAVAME, request.ID_CLIENTE);

                        if (verificacionclientedata.Rows.Count != 0)
                        {
                            foreach (DataRow act in verificacionclientedata.Rows)
                            {
                                if (act.Field<int>("CODIGO") == -1)
                                {
                                    response.message = act.Field<string>("MENSAJE");
                                    response.code = Configuraciones.GetCode("ERROR_FATAL");
                                    response.success = false;
                                }
                                if (act.Field<int>("CODIGO") == -2)
                                {
                                    verificacionclientedata = _conectorBD.InsertarSeguroDesgravame(cliente, clienteresponse.ID);

                                    if (verificacionclientedata.Rows.Count != 0)
                                    {
                                        foreach (DataRow acta in verificacionclientedata.Rows)
                                        {
                                            if (acta.Field<int>("CODIGO") == -1)
                                            {
                                                response.message = acta.Field<string>("MENSAJE");
                                                response.code = Configuraciones.GetCode("ERROR_FATAL");
                                                response.success = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                robot.TIPO = "ANA";
                robot.ID_CLIENTE = request.ID_CLIENTE;
                robot.ID_CREDITO = 0;
                robot.USUARIO = request.USUARIO;
                robot.NUEVO_ESTADO = "ACTUALIZAR";
                robot.DESCRIPCION = "";

                cambioestado = _conectorBD.ClienteCambioEstadoRobot(robot);

                if (cambioestado.Rows.Count != 0)
                {
                    foreach (DataRow item in cambioestado.Rows)
                    {

                        if (item.Field<int>("CODIGO") != 1)
                        {
                            response.message = item.Field<string>("MENSAJE");
                            response.code = Configuraciones.GetCode("ERROR_FATAL");
                            response.success = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }
        public async Task<LISTARCLIENTE> ListarCliente()
        {
            LISTARCLIENTE ListarCliente = new LISTARCLIENTE();
            ListarCliente.CLIENTES_RESUMEN = new List<CLIENTERESUMEN>();

            //CREDITO respuesta = new CREDITO();


            DataTable data = new DataTable();
            DataTable clientedata = new DataTable();
            DataTable creditodata = new DataTable();
            DataTable personadata = new DataTable();
            DataTable telefonodata = new DataTable();
            DataTable direcciondata = new DataTable();
            DataTable documentopersonaldata = new DataTable();
            DataTable actividadeconomicadata = new DataTable();
            DataTable caedecdata = new DataTable();
            DataTable fechaAMdata = new DataTable();
            DataTable direccionAEdata = new DataTable();
            DataTable referenciadata = new DataTable();
            DataTable personareferenciadata = new DataTable();
            DataTable declaracionjuradadata = new DataTable();
            DataTable autorizaciondata = new DataTable();
            DataTable documentoentregadodata = new DataTable();
            DataTable respuestaclientedata = new DataTable();

            try
            {
                data = _conectorBD.ListarCliente();
                if (data.Rows.Count!=0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        CLIENTERESUMEN respuesta = new CLIENTERESUMEN();
                        //respuesta.SOLICITANTE = new PERSONA();
                        //respuesta.ACTIVIDADES_ECONOMICAS = new List<ACTIVIDAD_ECONOMICA>();
                        //respuesta.REFERENCIAS = new List<REFERENCIA>();


                        //respuesta = null;
                        respuesta.ID_CLIENTE = item.Field<int>("ID_CLIENTE");
                        respuesta.ESTADO_ANA = item.Field<string>("ESTADO_ANA").ToString().Trim();


                        personadata = _conectorBD.ListarPersona(respuesta.ID_CLIENTE);

                        foreach (DataRow per in personadata.Rows)
                        {
                            PERSONA persona = new PERSONA();

                            persona.DIRECCIONES_PERS = new List<DIRECCION>();
                            persona.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();


                            persona.ID_PERSONA = per.Field<int>("ID_PERSONA");
                            //persona.GENERO = string.IsNullOrEmpty(per.Field<string>("GENERO")) ? "" : per.Field<string>("GENERO").ToString().Trim();
                            //persona.PRIMER_APELLIDO = string.IsNullOrEmpty(per.Field<string>("PRIMER_APELLIDO")) ? "" : per.Field<string>("PRIMER_APELLIDO").ToString().Trim();
                            //persona.SEGUNDO_APELLIDO = string.IsNullOrEmpty(per.Field<string>("SEGUNDO_APELLIDO")) ? "" : per.Field<string>("SEGUNDO_APELLIDO").ToString().Trim();
                            //persona.NOMBRES = string.IsNullOrEmpty(per.Field<string>("NOMBRES")) ? "" : per.Field<string>("NOMBRES").ToString().Trim();
                            //persona.CASADA_APELLIDO = string.IsNullOrEmpty(per.Field<string>("CASADA_APELLIDO")) ? "" : per.Field<string>("CASADA_APELLIDO").ToString().Trim();
                            respuesta.NOMBRE_COMPLETO = string.IsNullOrEmpty(per.Field<string>("NOMBRE_COMPLETO")) ? "" : per.Field<string>("NOMBRE_COMPLETO").ToString().Trim();


                            respuesta.CORREO = string.IsNullOrEmpty(per.Field<string>("CORREO")) ? "" : per.Field<string>("CORREO").ToString().Trim();
                            //persona.NIVEL_EDUCACION = string.IsNullOrEmpty(per.Field<string>("NIVEL_EDUCACION")) ? "" : per.Field<string>("NIVEL_EDUCACION").ToString().Trim();
                            //persona.PROFESION = string.IsNullOrEmpty(per.Field<string>("PROFESION")) ? "" : per.Field<string>("PROFESION").ToString().Trim();
                            //persona.ESTADO_CIVIL = string.IsNullOrEmpty(per.Field<string>("ESTADO_CIVIL")) ? "" : per.Field<string>("ESTADO_CIVIL").ToString().Trim();
                            //persona.USUARIO = string.Empty;

                            direcciondata = _conectorBD.ListarDireccionPersona(persona.ID_PERSONA);
                            //persona.DIRECCIONES_PERS.Clear();
                            foreach (DataRow dire in direcciondata.Rows)
                            {
                                DIRECCION direccion = new DIRECCION();
                                direccion.TELEFONOS = new List<TELEFONO>();

                                direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                                direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                                direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                                direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                                direccion.USUARIO = string.Empty;
                                telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                direccion.TELEFONOS.Clear();
                                foreach (DataRow tele in telefonodata.Rows)
                                {
                                    TELEFONO telefono = new TELEFONO();
                                    telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                    telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                    telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                    telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                    telefono.USUARIO = string.Empty;
                                    direccion.TELEFONOS.Add(telefono);

                                }


                                //persona.DIRECCIONES_PERS.Add(direccion);
                            }
                            documentopersonaldata = _conectorBD.ListarDocumentoPersonal(persona.ID_PERSONA);
                            persona.DOCUMENTOS.Clear();
                            foreach (DataRow doc in documentopersonaldata.Rows)
                            {
                                DOCUMENTO_PERSONAL documento_personal = new DOCUMENTO_PERSONAL();

                                documento_personal.ID_DOCUMENTO_PERSONAL = doc.Field<int>("ID_DOCUMENTO_PERSONAL");
                                documento_personal.TIPO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("TIPO_DOCUMENTO")) ? "" : doc.Field<string>("TIPO_DOCUMENTO").ToString().Trim();
                                documento_personal.NRO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("NRO_DOCUMENTO")) ? "" : doc.Field<string>("NRO_DOCUMENTO").ToString().Trim();
                                documento_personal.COMPLEMENTO = string.IsNullOrEmpty(doc.Field<string>("COMPLEMENTO")) ? "" : doc.Field<string>("COMPLEMENTO").ToString().Trim();
                                documento_personal.EXTENSION = string.IsNullOrEmpty(doc.Field<string>("EXTENSION")) ? "" : doc.Field<string>("EXTENSION").ToString().Trim();
                                documento_personal.FECHA_VENC = doc.Field<DateTime>("FECHA_VENC");
                                documento_personal.ESTADO_CIVIL = string.IsNullOrEmpty(doc.Field<string>("ESTADO_CIVIL")) ? "" : doc.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                documento_personal.NACIONALIDAD = string.IsNullOrEmpty(doc.Field<string>("NACIONALIDAD")) ? "" : doc.Field<string>("NACIONALIDAD").ToString().Trim();
                                documento_personal.LUGAR_NACIMIENTO = string.IsNullOrEmpty(doc.Field<string>("LUGAR_NACIMIENTO")) ? "" : doc.Field<string>("LUGAR_NACIMIENTO").ToString().Trim();
                                documento_personal.FECHA_NACIMIENTO = doc.Field<DateTime>("FECHA_NACIMIENTO");
                                documento_personal.USUARIO = string.Empty;
                                respuesta.DOCUMENTOS.Add(documento_personal);
                                persona.DOCUMENTOS.Add(documento_personal);

                            }

                            telefonodata = _conectorBD.ListarTelefonoPersona(persona.ID_PERSONA);
                            foreach (DataRow tele in telefonodata.Rows)
                            {
                                TELEFONO telefono = new TELEFONO();
                                telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                telefono.USUARIO = string.Empty;
                                respuesta.CONTACTO = telefono;

                            }
                            persona.USUARIO = String.Empty;


                        }

                        //actividadeconomicadata = _conectorBD.ListarActividadEconomica(respuesta.ID_CLIENTE);
                        //respuesta.ACTIVIDADES_ECONOMICAS.Clear();
                        //foreach (DataRow ae in actividadeconomicadata.Rows)
                        //{
                        //    ACTIVIDAD_ECONOMICA actividad_economica = new ACTIVIDAD_ECONOMICA();
                        //    actividad_economica.CAE_DEC = new CAEDEC();
                        //    actividad_economica.TIEMPO_EXP = new List<FECHA_A_M>();
                        //    actividad_economica.DIRECCION_ACT_ECO = new DIRECCION();

                        //    actividad_economica.ID_ACTIVIDAD_ECONOMICA = ae.Field<int>("ID_ACTIVIDAD_ECONOMICA");

                        //    caedecdata = _conectorBD.ListarCaedec(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                        //    foreach (DataRow cae in caedecdata.Rows)
                        //    {
                        //        CAEDEC caedec = new CAEDEC();
                        //        caedec.ID_CAEDEC = cae.Field<int>("ID_CAEDEC");
                        //        caedec.COD_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("COD_CAEDEC")) ? "" : cae.Field<string>("COD_CAEDEC").ToString().Trim();
                        //        caedec.ACTIVIDAD_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("ACTIVIDAD_CAEDEC")) ? "" : cae.Field<string>("ACTIVIDAD_CAEDEC").ToString().Trim();
                        //        caedec.GRUPO_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("GRUPO_CAEDEC")) ? "" : cae.Field<string>("GRUPO_CAEDEC").ToString().Trim();
                        //        caedec.SECTOR_ECONOMICO = string.IsNullOrEmpty(cae.Field<string>("SECTOR_ECONOMICO")) ? "" : cae.Field<string>("SECTOR_ECONOMICO").ToString().Trim();
                        //        caedec.USUARIO = string.Empty;
                        //        actividad_economica.CAE_DEC = caedec;

                        //    }

                        //    actividad_economica.NIVEL_LABORAL = string.IsNullOrEmpty(ae.Field<string>("NIVEL_LABORAL")) ? "" : ae.Field<string>("NIVEL_LABORAL").ToString().Trim();
                        //    actividad_economica.NIT = ae.Field<int>("NIT");
                        //    actividad_economica.ACTIVIDAD_DECLARADA = string.IsNullOrEmpty(ae.Field<string>("ACTIVIDAD_DECLARADA")) ? "" : ae.Field<string>("ACTIVIDAD_DECLARADA").ToString().Trim();
                        //    actividad_economica.PRIORIDAD = string.IsNullOrEmpty(ae.Field<string>("PRIORIDAD")) ? "" : ae.Field<string>("PRIORIDAD").ToString().Trim();

                        //    fechaAMdata = _conectorBD.ListarFechaAM(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                        //    actividad_economica.TIEMPO_EXP.Clear();
                        //    foreach (DataRow fec in fechaAMdata.Rows)
                        //    {
                        //        FECHA_A_M fecha_a_m = new FECHA_A_M();
                        //        fecha_a_m.ID_FECHA_A_M = fec.Field<int>("ID_FECHA_A_M");
                        //        fecha_a_m.TIPO = string.IsNullOrEmpty(fec.Field<string>("TIPO")) ? "" : fec.Field<string>("TIPO").ToString().Trim();
                        //        fecha_a_m.ANIO = fec.Field<int>("ANIO");
                        //        fecha_a_m.MES = fec.Field<int>("MES");
                        //        fecha_a_m.USUARIO = string.Empty;
                        //        actividad_economica.TIEMPO_EXP.Add(fecha_a_m);

                        //    }
                        //    actividad_economica.ESTADO = string.IsNullOrEmpty(ae.Field<string>("ESTADO")) ? "" : ae.Field<string>("ESTADO").ToString().Trim();
                        //    actividad_economica.INGRESOS_MENSUALES = string.IsNullOrEmpty(ae.Field<string>("INGRESOS_MENSUALES")) ? "0" : ae.Field<string>("INGRESOS_MENSUALES").ToString().Trim();
                        //    actividad_economica.OTROS_INGRESOS_MENSUALES = ae.Field<int>("OTROS_INGRESOS_MENSUALES");
                        //    actividad_economica.EGRESOS_MENSUALES = ae.Field<int>("EGRESOS_MENSUALES");
                        //    actividad_economica.MARGEN_AHORRO = ae.Field<int>("MARGEN_AHORRO");
                        //    actividad_economica.NOMBRE_EMPRESA = string.IsNullOrEmpty(ae.Field<string>("NOMBRE_EMPRESA")) ? "" : ae.Field<string>("NOMBRE_EMPRESA").ToString().Trim();
                        //    actividad_economica.FECHA_INGRESO = ae.Field<DateTime>("FECHA_INGRESO");
                        //    actividad_economica.USUARIO = string.Empty;
                        //    direccionAEdata = _conectorBD.ListarDireccionActividadEconomica(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                        //    foreach (DataRow dire in direccionAEdata.Rows)
                        //    {
                        //        DIRECCION direccion = new DIRECCION();
                        //        direccion.TELEFONOS = new List<TELEFONO>();
                        //        direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                        //        direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                        //        direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                        //        direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                        //        direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                        //        direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                        //        direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                        //        direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                        //        direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                        //        direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                        //        direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                        //        direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                        //        direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                        //        direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                        //        direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                        //        direccion.USUARIO = string.Empty;
                        //        telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                        //        direccion.TELEFONOS.Clear();
                        //        foreach (DataRow tele in telefonodata.Rows)
                        //        {
                        //            TELEFONO telefono = new TELEFONO();
                        //            telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                        //            telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                        //            telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                        //            telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                        //            telefono.USUARIO = string.Empty;
                        //            direccion.TELEFONOS.Add(telefono);

                        //        }

                        //        actividad_economica.DIRECCION_ACT_ECO = direccion;
                        //    }

                        //    actividad_economica.CARGO = string.IsNullOrEmpty(ae.Field<string>("CARGO")) ? "" : ae.Field<string>("CARGO").ToString().Trim();
                        //    actividad_economica.USUARIO = String.Empty;

                        //    respuesta.ACTIVIDADES_ECONOMICAS.Add(actividad_economica);

                        //}

                        //referenciadata = _conectorBD.ListarReferencia(respuesta.ID_CLIENTE);
                        //respuesta.REFERENCIAS.Clear();
                        //foreach (DataRow refe in referenciadata.Rows)
                        //{
                        //    REFERENCIA referencia = new REFERENCIA();
                        //    referencia.REFERIDO = new PERSONA();

                        //    referencia.ID_REFERENCIA = refe.Field<int>("ID_REFERENCIA");
                        //    referencia.TIPO = string.IsNullOrEmpty(refe.Field<string>("TIPO")) ? "" : refe.Field<string>("TIPO").ToString().Trim();
                        //    referencia.RELACION = string.IsNullOrEmpty(refe.Field<string>("RELACION")) ? "" : refe.Field<string>("RELACION").ToString().Trim();
                        //    referencia.OBSERVACION = string.IsNullOrEmpty(refe.Field<string>("OBSERVACION")) ? "" : refe.Field<string>("OBSERVACION").ToString().Trim();
                        //    referencia.CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("CALIFICACION")) ? "" : refe.Field<string>("CALIFICACION").ToString().Trim();
                        //    referencia.DESCRIPCION_CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("DESCRIPCION_CALIFICACION")) ? "" : refe.Field<string>("DESCRIPCION_CALIFICACION").ToString().Trim();
                        //    referencia.CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("CALIFICACION")) ? "" : refe.Field<string>("CALIFICACION").ToString().Trim();
                        //    referencia.USUARIO = "";

                        //    personareferenciadata = _conectorBD.ListarPersonaReferencia(referencia.ID_REFERENCIA);
                        //    foreach (DataRow per in personareferenciadata.Rows)
                        //    {
                        //        PERSONA persona = new PERSONA();
                        //        persona.DIRECCIONES_PERS = new List<DIRECCION>();
                        //        persona.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();


                        //        persona.ID_PERSONA = per.Field<int>("ID_PERSONA");
                        //        persona.GENERO = string.IsNullOrEmpty(per.Field<string>("GENERO")) ? "" : per.Field<string>("GENERO").ToString().Trim();
                        //        persona.PRIMER_APELLIDO = string.IsNullOrEmpty(per.Field<string>("PRIMER_APELLIDO")) ? "" : per.Field<string>("PRIMER_APELLIDO").ToString().Trim();
                        //        persona.SEGUNDO_APELLIDO = string.IsNullOrEmpty(per.Field<string>("SEGUNDO_APELLIDO")) ? "" : per.Field<string>("SEGUNDO_APELLIDO").ToString().Trim();
                        //        persona.NOMBRES = string.IsNullOrEmpty(per.Field<string>("NOMBRES")) ? "" : per.Field<string>("NOMBRES").ToString().Trim();
                        //        persona.CASADA_APELLIDO = string.IsNullOrEmpty(per.Field<string>("CASADA_APELLIDO")) ? "" : per.Field<string>("CASADA_APELLIDO").ToString().Trim();
                        //        persona.NOMBRE_COMPLETO = string.IsNullOrEmpty(per.Field<string>("NOMBRE_COMPLETO")) ? "" : per.Field<string>("NOMBRE_COMPLETO").ToString().Trim();
                        //        persona.CORREO = string.IsNullOrEmpty(per.Field<string>("CORREO")) ? "" : per.Field<string>("CORREO").ToString().Trim();
                        //        persona.NIVEL_EDUCACION = string.IsNullOrEmpty(per.Field<string>("NIVEL_EDUCACION")) ? "" : per.Field<string>("NIVEL_EDUCACION").ToString().Trim();
                        //        persona.PROFESION = string.IsNullOrEmpty(per.Field<string>("PROFESION")) ? "" : per.Field<string>("PROFESION").ToString().Trim();
                        //        persona.ESTADO_CIVIL = string.IsNullOrEmpty(per.Field<string>("ESTADO_CIVIL")) ? "" : per.Field<string>("ESTADO_CIVIL").ToString().Trim();
                        //        persona.USUARIO = string.Empty;

                        //        direcciondata = _conectorBD.ListarDireccionPersona(persona.ID_PERSONA);
                        //        persona.DIRECCIONES_PERS.Clear();
                        //        foreach (DataRow dire in direcciondata.Rows)
                        //        {
                        //            DIRECCION direccion = new DIRECCION();
                        //            direccion.TELEFONOS = new List<TELEFONO>();
                        //            direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                        //direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                        //direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                        //direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                        //direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                        //direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                        //direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                        //direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                        //direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                        //direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                        //direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                        //direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                        //direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                        //direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                        //direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                        //            direccion.USUARIO = string.Empty;
                        //            telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                        //            direccion.TELEFONOS.Clear();
                        //            foreach (DataRow tele in telefonodata.Rows)
                        //            {
                        //                TELEFONO telefono = new TELEFONO();
                        //                telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                        //                telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                        //                telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                        //                telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                        //                telefono.USUARIO = string.Empty;
                        //                direccion.TELEFONOS.Add(telefono);

                        //            }

                        //            persona.DIRECCIONES_PERS.Add(direccion);
                        //        }
                        //        documentopersonaldata = _conectorBD.ListarDocumentoPersonal(persona.ID_PERSONA);
                        //        persona.DOCUMENTOS.Clear();
                        //        foreach (DataRow doc in documentopersonaldata.Rows)
                        //        {
                        //            DOCUMENTO_PERSONAL documento_personal = new DOCUMENTO_PERSONAL();

                        //            documento_personal.ID_DOCUMENTO_PERSONAL = doc.Field<int>("ID_DOCUMENTO_PERSONAL");
                        //            documento_personal.TIPO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("TIPO_DOCUMENTO")) ? "" : doc.Field<string>("TIPO_DOCUMENTO").ToString().Trim();
                        //            documento_personal.NRO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("NRO_DOCUMENTO")) ? "" : doc.Field<string>("NRO_DOCUMENTO").ToString().Trim();
                        //            documento_personal.COMPLEMENTO = string.IsNullOrEmpty(doc.Field<string>("COMPLEMENTO")) ? "" : doc.Field<string>("COMPLEMENTO").ToString().Trim();
                        //            documento_personal.EXTENSION = string.IsNullOrEmpty(doc.Field<string>("EXTENSION")) ? "" : doc.Field<string>("EXTENSION").ToString().Trim();
                        //            documento_personal.FECHA_VENC = doc.Field<DateTime>("FECHA_VENC");
                        //            documento_personal.ESTADO_CIVIL = string.IsNullOrEmpty(doc.Field<string>("ESTADO_CIVIL")) ? "" : doc.Field<string>("ESTADO_CIVIL").ToString().Trim();
                        //            documento_personal.NACIONALIDAD = string.IsNullOrEmpty(doc.Field<string>("NACIONALIDAD")) ? "" : doc.Field<string>("NACIONALIDAD").ToString().Trim();
                        //            documento_personal.LUGAR_NACIMIENTO = string.IsNullOrEmpty(doc.Field<string>("LUGAR_NACIMIENTO")) ? "" : doc.Field<string>("LUGAR_NACIMIENTO").ToString().Trim();
                        //            documento_personal.FECHA_NACIMIENTO = doc.Field<DateTime>("FECHA_NACIMIENTO");
                        //            documento_personal.USUARIO = string.Empty;
                        //            persona.DOCUMENTOS.Add(documento_personal);

                        //        }
                        //        persona.USUARIO = String.Empty;
                        //        referencia.REFERIDO = persona;
                        //    }

                        //    respuesta.REFERENCIAS.Add(referencia);

                        //}

                        creditodata = _conectorBD.ListarCredito(respuesta.ID_CLIENTE);
                        respuesta.LISTARCREDITOS.Clear();
                        foreach (DataRow cre in creditodata.Rows)
                        {
                            CREDITORESUMEN credito = new CREDITORESUMEN();
                            //credito.DECLARACIONES = new List<DECLARACION_JURADA>();
                            //credito.DOCUMENTOS_ENTREGADOS = new List<DOCUMENTO_ENTREGADO>();
                            //credito.AUTORIZACIONES = new List<AUTORIZACION>();

                            credito.ID_CREDITO = cre.Field<int>("ID_CREDITO");
                            credito.COD_CREDITO = String.IsNullOrEmpty(cre.Field<string>("COD_CREDITO"))?"": cre.Field<string>("COD_CREDITO").ToString().Trim();
                            //credito.COD_ANA = string.IsNullOrEmpty(cre.Field<string>("COD_ANA")) ? "" : cre.Field<string>("COD_ANA").ToString().Trim();
                            //credito.MONTO_CREDITO = cre.Field<decimal>("MONTO_CREDITO");
                            //credito.TIPO_OPERACION = string.IsNullOrEmpty(cre.Field<string>("TIPO_OPERACION")) ? "" : cre.Field<string>("TIPO_OPERACION").ToString().Trim();
                            //credito.ESTRATEGIA = string.IsNullOrEmpty(cre.Field<string>("ESTRATEGIA")) ? "" : cre.Field<string>("ESTRATEGIA").ToString().Trim();
                            //credito.DESTINO = string.IsNullOrEmpty(cre.Field<string>("DESTINO")) ? "" : cre.Field<string>("DESTINO").ToString().Trim();
                            //credito.DESTINO_CLIENTE = string.IsNullOrEmpty(cre.Field<string>("DESTINO_CLIENTE")) ? "" : cre.Field<string>("DESTINO_CLIENTE").ToString().Trim();
                            //credito.COMPRA_PASIVO = cre.Field<bool>("COMPRA_PASIVO");
                            //credito.OBJETO_OPERACION = string.IsNullOrEmpty(cre.Field<string>("OBJETO_OPERACION")) ? "" : cre.Field<string>("OBJETO_OPERACION").ToString().Trim();
                            //credito.PLAZO = cre.Field<int>("PLAZO");
                            //credito.DIA_PAGO = cre.Field<int>("DIA_PAGO");
                            //credito.FECHA_SOLICITUD = cre.Field<DateTime>("FECHA_SOLICITUD");

                            //declaracionjuradadata = _conectorBD.ListarDeclaracionJurada(credito.ID_CREDITO);
                            //credito.DECLARACIONES.Clear();
                            //foreach (DataRow dj in declaracionjuradadata.Rows)
                            //{
                            //    DECLARACION_JURADA declaracion_jarada = new DECLARACION_JURADA();
                            //    declaracion_jarada.ID_DECLARACION_JURADA = dj.Field<int>("ID_DECLARACION_JURADA");
                            //    declaracion_jarada.TIPO = string.IsNullOrEmpty(dj.Field<string>("TIPO")) ? "" : dj.Field<string>("TIPO").ToString().Trim();
                            //    declaracion_jarada.PATRIMONIO_ACTIVO = dj.Field<int>("PATRIMONIO_ACTIVO");
                            //    declaracion_jarada.PATRIMONIO_PASIVO = dj.Field<int>("PATRIMONIO_PASIVO");
                            //    declaracion_jarada.PERSONAL_OCUPADO = dj.Field<int>("PERSONAL_OCUPADO");
                            //    declaracion_jarada.TOTAL_INGRESO_VENTAS = dj.Field<int>("TOTAL_INGRESO_VENTAS");
                            //    declaracion_jarada.OBSERVACIONES = string.IsNullOrEmpty(dj.Field<string>("OBSERVACIONES")) ? "" : dj.Field<string>("OBSERVACIONES").ToString().Trim();
                            //    declaracion_jarada.USUARIO = string.Empty;
                            //    credito.DECLARACIONES.Add(declaracion_jarada);

                            //}


                            //credito.CLIENTE_CPOP = string.IsNullOrEmpty(cre.Field<string>("CLIENTE_CPOP")) ? "" : cre.Field<string>("CLIENTE_CPOP").ToString().Trim();
                            //credito.NRO_CPOP = cre.Field<int>("NRO_CPOP");
                            //credito.TIPO_TASA = string.IsNullOrEmpty(cre.Field<string>("TIPO_TASA")) ? "" : cre.Field<string>("TIPO_TASA").ToString().Trim();
                            //credito.NRO_AUTORIZACION = cre.Field<int>("NRO_AUTORIZACION");
                            //credito.TASA_SELECCION = string.IsNullOrEmpty(cre.Field<string>("TASA_SELECCION")) ? "" : cre.Field<string>("TASA_SELECCION").ToString().Trim();
                            //credito.TASA_PP = cre.Field<int>("TASA_PP");
                            credito.ESTADO_CREDITO = string.IsNullOrEmpty(cre.Field<string>("ESTADO_CREDITO")) ? "" : cre.Field<string>("ESTADO_CREDITO").ToString().Trim();
                            credito.ESTADO_SCI = string.IsNullOrEmpty(cre.Field<string>("ESTADO_SCI")) ? "" : cre.Field<string>("ESTADO_SCI").ToString().Trim();

                            //documentoentregadodata = _conectorBD.ListarDocumentoEntregado(credito.ID_CREDITO);
                            //credito.DOCUMENTOS_ENTREGADOS.Clear();
                            //foreach (DataRow de in documentoentregadodata.Rows)
                            //{
                            //    DOCUMENTO_ENTREGADO documentos_entregados = new DOCUMENTO_ENTREGADO();
                            //    documentos_entregados.ID_DOCUMENTO_ENTREGADO = de.Field<int>("ID_DOCUMENTO_ENTREGADO");
                            //
                            //    documentos_entregados.DOCUMENTO = string.IsNullOrEmpty(de.Field<string>("DOCUMENTO")) ? "" : de.Field<string>("DOCUMENTO").ToString().Trim();
                            //    documentos_entregados.ENTREGADO = de.Field<bool>("ENTREGADO");

                            //    credito.DOCUMENTOS_ENTREGADOS.Add(documentos_entregados);

                            //}

                            //autorizaciondata = _conectorBD.ListarAutorizacion(credito.ID_CREDITO);
                            //credito.AUTORIZACIONES.Clear();
                            //foreach (DataRow auto in autorizaciondata.Rows)
                            //{
                            //    AUTORIZACION autorizacion = new AUTORIZACION();
                            //    autorizacion.ID_AUTORIZACION = auto.Field<int>("ID_AUTORIZACION");
                            //autorizacion.HABILITADO = auto.Field<bool>("HABILITADO");
                            //    autorizacion.AUTORIZACION_ESPECIAL = string.IsNullOrEmpty(auto.Field<string>("AUTORIZACION_ESPECIAL")) ? "" : auto.Field<string>("AUTORIZACION_ESPECIAL").ToString().Trim();
                            //    autorizacion.DESCRIPCION = string.IsNullOrEmpty(auto.Field<string>("DESCRIPCION")) ? "" : auto.Field<string>("DESCRIPCION").ToString().Trim();
                            //    credito.AUTORIZACIONES.Add(autorizacion);
                            //}
                            credito.USUARIO = String.Empty;
                            respuesta.LISTARCREDITOS.Add(credito);

                        }

                        respuestaclientedata = _conectorBD.ListadoHistorialRespuestaCliente(respuesta.ID_CLIENTE);
                        if (respuestaclientedata.Rows.Count != 0)
                        {
                            respuesta.ESTADOS_ROBOT.Clear();
                            foreach (DataRow res in respuestaclientedata.Rows)
                            {
                                RESPUESTA_CLIENTE_RESPONSE clienterespuesta = new RESPUESTA_CLIENTE_RESPONSE();
                                clienterespuesta.ID_RESPUESTA_CLIENTE = res.Field<int>("ID_RESPUESTA_CLIENTE");
                                clienterespuesta.CATEGORIA = string.IsNullOrEmpty(res.Field<string>("CATEGORIA")) ? "" : res.Field<string>("CATEGORIA").ToString().Trim();
                                clienterespuesta.SUCCESS = res.Field<bool>("SUCCESS");
                                clienterespuesta.MENSAJE = string.IsNullOrEmpty(res.Field<string>("MENSAJE")) ? "" : res.Field<string>("MENSAJE").ToString().Trim();
                                clienterespuesta.CODIGO = string.IsNullOrEmpty(res.Field<string>("CODIGO")) ? "" : res.Field<string>("CODIGO").ToString().Trim();
                                respuesta.ESTADOS_ROBOT.Add(clienterespuesta);
                            }
                        }


                        ListarCliente.CLIENTES_RESUMEN.Add(respuesta);



                    }
                    ListarCliente.code = Configuraciones.GetCode("OK");
                    ListarCliente.message = Configuraciones.GetMessage("OK");
                    ListarCliente.success = true;
                }
                else
                {
                    ListarCliente.code = Configuraciones.GetCode("OK");
                    ListarCliente.message = "LISTA DE CREDITOS SE ENCUENTRA VACIO";
                    ListarCliente.success = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ListarCliente;
        }
        public async Task<CLIENTERESPONSE> BuscarCliente(BuscarClienteRequest request)
        {
            //ListarCredito ListarCredito = new ListarCredito();
            //ListarCredito.LISTARCREDITOS = new List<CREDITO>();
            //CREDITORESPONSE respuesta = new CREDITORESPONSE();
            #region VARIALES
            DataTable data = new DataTable();
            DataTable personadata = new DataTable();
            DataTable creditodata = new DataTable();
            DataTable telefonodata = new DataTable();
            DataTable direcciondata = new DataTable();
            DataTable documentopersonaldata = new DataTable();
            DataTable actividadeconomicadata = new DataTable();
            DataTable caedecdata = new DataTable();
            DataTable fechaAMdata = new DataTable();
            DataTable direccionAEdata = new DataTable();
            DataTable referenciadata = new DataTable();
            DataTable personareferenciadata = new DataTable();
            DataTable declaracionjuradadata = new DataTable();
            DataTable autorizaciondata = new DataTable();
            DataTable documentoentregadodata = new DataTable();
            DataTable scoringdata = new DataTable();

            DataTable respuestaclientedata = new DataTable();


            DataTable verificacionclientedata = new DataTable();
            DataTable scripevaluaciondata = new DataTable();
            DataTable instituciondata = new DataTable();
            DataTable segurodesgravamedata = new DataTable();

            string[] CRITERIO;
            string[] RESULTADO;
            string[] PUNTAJE;

            string CRITERIOres = string.Empty;
            string RESULTADOres = string.Empty;
            string PUNTAJEres = string.Empty;


            string[] VerClientePregunta;
            string[] VerClienteRespuesta;
            string[] VerClienteConfirmacion;
            string[] VerClienteInstituciones;
            string[] VerClienteObservacion;

            string[] VerClienteInstitucionNombre;
            string[] VerClienteInstitucionMonto;


            string VerClientePreguntares = string.Empty;
            string VerClienteRespuestares = string.Empty;
            string VerClienteConfirmacionres = string.Empty;
            string VerInstitucion = string.Empty;
            string VerInstitucionNombre = string.Empty;
            string VerInstitucionMonto = string.Empty;
            string VerClienteObservacionres = string.Empty;




            CLIENTERESPONSE respuesta = new CLIENTERESPONSE();
            string datares = string.Empty;
            #endregion 

            try
            {
                data = _conectorBD.BuscarCliente(request);

                if (data.Rows.Count != 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        //CLIENTE respuesta = new CLIENTE();
                        respuesta.SOLICITANTE = new PERSONA();
                        respuesta.ACTIVIDADES_ECONOMICAS = new List<ACTIVIDAD_ECONOMICA>();
                        respuesta.REFERENCIAS = new List<REFERENCIA>();


                        //respuesta = null;
                        respuesta.ID_CLIENTE = item.Field<int>("ID_CLIENTE");
                        respuesta.ESTADO_ANA = string.IsNullOrEmpty(item.Field<string>("ESTADO_ANA")) ? "" : item.Field<string>("ESTADO_ANA").ToString().Trim();



                        personadata = _conectorBD.ListarPersona(respuesta.ID_CLIENTE);

                        foreach (DataRow per in personadata.Rows)
                        {
                            PERSONA persona = new PERSONA();

                            persona.DIRECCIONES_PERS = new List<DIRECCION>();
                            persona.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();


                            persona.ID_PERSONA = per.Field<int>("ID_PERSONA");
                            persona.GENERO = string.IsNullOrEmpty(per.Field<string>("GENERO")) ? "" : per.Field<string>("GENERO").ToString().Trim();
                            persona.PRIMER_APELLIDO = string.IsNullOrEmpty(per.Field<string>("PRIMER_APELLIDO")) ? "" : per.Field<string>("PRIMER_APELLIDO").ToString().Trim();
                            persona.SEGUNDO_APELLIDO = string.IsNullOrEmpty(per.Field<string>("SEGUNDO_APELLIDO")) ? "" : per.Field<string>("SEGUNDO_APELLIDO").ToString().Trim();
                            persona.NOMBRES = string.IsNullOrEmpty(per.Field<string>("NOMBRES")) ? "" : per.Field<string>("NOMBRES").ToString().Trim();
                            persona.CASADA_APELLIDO = string.IsNullOrEmpty(per.Field<string>("CASADA_APELLIDO")) ? "" : per.Field<string>("CASADA_APELLIDO").ToString().Trim();
                            persona.NOMBRE_COMPLETO = string.IsNullOrEmpty(per.Field<string>("NOMBRE_COMPLETO")) ? "" : per.Field<string>("NOMBRE_COMPLETO").ToString().Trim();

                            persona.CORREO = string.IsNullOrEmpty(per.Field<string>("CORREO")) ? "" : per.Field<string>("CORREO").ToString().Trim();
                            persona.NIVEL_EDUCACION = string.IsNullOrEmpty(per.Field<string>("NIVEL_EDUCACION")) ? "" : per.Field<string>("NIVEL_EDUCACION").ToString().Trim();
                            persona.PROFESION = string.IsNullOrEmpty(per.Field<string>("PROFESION")) ? "" : per.Field<string>("PROFESION").ToString().Trim();
                            persona.ESTADO_CIVIL = string.IsNullOrEmpty(per.Field<string>("ESTADO_CIVIL")) ? "" : per.Field<string>("ESTADO_CIVIL").ToString().Trim();
                            persona.COD_ANA = string.IsNullOrEmpty(per.Field<string>("COD_ANA")) ? "" : per.Field<string>("COD_ANA").ToString().Trim();
                            persona.USUARIO = string.Empty;

                            direcciondata = _conectorBD.ListarDireccionPersona(persona.ID_PERSONA);
                            persona.DIRECCIONES_PERS.Clear();
                            foreach (DataRow dire in direcciondata.Rows)
                            {
                                DIRECCION direccion = new DIRECCION();
                                direccion.TELEFONOS = new List<TELEFONO>();

                                direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                                direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                                direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                                direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                                direccion.USUARIO = string.Empty;
                                telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                direccion.TELEFONOS.Clear();
                                foreach (DataRow tele in telefonodata.Rows)
                                {
                                    TELEFONO telefono = new TELEFONO();
                                    telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                    telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                    telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                    telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                    telefono.USUARIO = string.Empty;
                                    direccion.TELEFONOS.Add(telefono);

                                }


                                persona.DIRECCIONES_PERS.Add(direccion);
                            }
                            documentopersonaldata = _conectorBD.ListarDocumentoPersonal(persona.ID_PERSONA);
                            persona.DOCUMENTOS.Clear();
                            foreach (DataRow doc in documentopersonaldata.Rows)
                            {
                                DOCUMENTO_PERSONAL documento_personal = new DOCUMENTO_PERSONAL();

                                documento_personal.ID_DOCUMENTO_PERSONAL = doc.Field<int>("ID_DOCUMENTO_PERSONAL");
                                documento_personal.TIPO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("TIPO_DOCUMENTO")) ? "" : doc.Field<string>("TIPO_DOCUMENTO").ToString().Trim();
                                documento_personal.NRO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("NRO_DOCUMENTO")) ? "" : doc.Field<string>("NRO_DOCUMENTO").ToString().Trim();
                                documento_personal.COMPLEMENTO = string.IsNullOrEmpty(doc.Field<string>("COMPLEMENTO")) ? "" : doc.Field<string>("COMPLEMENTO").ToString().Trim();
                                documento_personal.EXTENSION = string.IsNullOrEmpty(doc.Field<string>("EXTENSION")) ? "" : doc.Field<string>("EXTENSION").ToString().Trim();
                                documento_personal.FECHA_VENC = doc.Field<DateTime>("FECHA_VENC");
                                documento_personal.ESTADO_CIVIL = string.IsNullOrEmpty(doc.Field<string>("ESTADO_CIVIL")) ? "" : doc.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                documento_personal.NACIONALIDAD = string.IsNullOrEmpty(doc.Field<string>("NACIONALIDAD")) ? "" : doc.Field<string>("NACIONALIDAD").ToString().Trim();
                                documento_personal.LUGAR_NACIMIENTO = string.IsNullOrEmpty(doc.Field<string>("LUGAR_NACIMIENTO")) ? "" : doc.Field<string>("LUGAR_NACIMIENTO").ToString().Trim();
                                documento_personal.FECHA_NACIMIENTO = doc.Field<DateTime>("FECHA_NACIMIENTO");
                                documento_personal.USUARIO = string.Empty;
                                persona.DOCUMENTOS.Add(documento_personal);

                            }

                            telefonodata = _conectorBD.ListarTelefonoPersona(persona.ID_PERSONA);
                            foreach (DataRow tele in telefonodata.Rows)
                            {
                                TELEFONO telefono = new TELEFONO();
                                telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                telefono.USUARIO = string.Empty;
                                persona.CONTACTO = telefono;

                            }
                            persona.USUARIO = String.Empty;
                            respuesta.SOLICITANTE = persona;
                        }

                        actividadeconomicadata = _conectorBD.ListarActividadEconomica(respuesta.ID_CLIENTE);
                        respuesta.ACTIVIDADES_ECONOMICAS.Clear();
                        foreach (DataRow ae in actividadeconomicadata.Rows)
                        {
                            ACTIVIDAD_ECONOMICA actividad_economica = new ACTIVIDAD_ECONOMICA();
                            actividad_economica.CAE_DEC = new CAEDEC();
                            actividad_economica.TIEMPO_EXP = new List<FECHA_A_M>();
                            actividad_economica.DIRECCION_ACT_ECO = new DIRECCION();

                            actividad_economica.ID_ACTIVIDAD_ECONOMICA = ae.Field<int>("ID_ACTIVIDAD_ECONOMICA");

                            caedecdata = _conectorBD.ListarCaedec(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                            foreach (DataRow cae in caedecdata.Rows)
                            {
                                CAEDEC caedec = new CAEDEC();
                                caedec.ID_CAEDEC = cae.Field<int>("ID_CAEDEC");
                                caedec.COD_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("COD_CAEDEC")) ? "" : cae.Field<string>("COD_CAEDEC").ToString().Trim();
                                caedec.ACTIVIDAD_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("ACTIVIDAD_CAEDEC")) ? "" : cae.Field<string>("ACTIVIDAD_CAEDEC").ToString().Trim();
                                caedec.GRUPO_CAEDEC = string.IsNullOrEmpty(cae.Field<string>("GRUPO_CAEDEC")) ? "" : cae.Field<string>("GRUPO_CAEDEC").ToString().Trim();
                                caedec.SECTOR_ECONOMICO = string.IsNullOrEmpty(cae.Field<string>("SECTOR_ECONOMICO")) ? "" : cae.Field<string>("SECTOR_ECONOMICO").ToString().Trim();
                                caedec.USUARIO = string.Empty;
                                actividad_economica.CAE_DEC = caedec;

                            }

                            actividad_economica.NIVEL_LABORAL = string.IsNullOrEmpty(ae.Field<string>("NIVEL_LABORAL")) ? "" : ae.Field<string>("NIVEL_LABORAL").ToString().Trim();
                            actividad_economica.NIT = ae.Field<int>("NIT");
                            actividad_economica.ACTIVIDAD_DECLARADA = string.IsNullOrEmpty(ae.Field<string>("ACTIVIDAD_DECLARADA")) ? "" : ae.Field<string>("ACTIVIDAD_DECLARADA").ToString().Trim();
                            actividad_economica.PRIORIDAD = string.IsNullOrEmpty(ae.Field<string>("PRIORIDAD")) ? "" : ae.Field<string>("PRIORIDAD").ToString().Trim();

                            fechaAMdata = _conectorBD.ListarFechaAM(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                            actividad_economica.TIEMPO_EXP.Clear();
                            foreach (DataRow fec in fechaAMdata.Rows)
                            {
                                FECHA_A_M fecha_a_m = new FECHA_A_M();
                                fecha_a_m.ID_FECHA_A_M = fec.Field<int>("ID_FECHA_A_M");
                                fecha_a_m.TIPO = string.IsNullOrEmpty(fec.Field<string>("TIPO")) ? "" : fec.Field<string>("TIPO").ToString().Trim();
                                fecha_a_m.ANIO = fec.Field<int>("ANIO");
                                fecha_a_m.MES = fec.Field<int>("MES");
                                fecha_a_m.USUARIO = string.Empty;
                                actividad_economica.TIEMPO_EXP.Add(fecha_a_m);

                            }
                            actividad_economica.ESTADO = string.IsNullOrEmpty(ae.Field<string>("ESTADO")) ? "" : ae.Field<string>("ESTADO").ToString().Trim();
                            actividad_economica.INGRESOS_MENSUALES = string.IsNullOrEmpty(ae.Field<string>("INGRESOS_MENSUALES")) ? "0" : ae.Field<string>("INGRESOS_MENSUALES").ToString().Trim();
                            actividad_economica.OTROS_INGRESOS_MENSUALES = ae.Field<int>("OTROS_INGRESOS_MENSUALES");
                            actividad_economica.EGRESOS_MENSUALES = ae.Field<int>("EGRESOS_MENSUALES");
                            actividad_economica.MARGEN_AHORRO = ae.Field<int>("MARGEN_AHORRO");
                            actividad_economica.NOMBRE_EMPRESA = string.IsNullOrEmpty(ae.Field<string>("NOMBRE_EMPRESA")) ? "" : ae.Field<string>("NOMBRE_EMPRESA").ToString().Trim();
                            actividad_economica.FECHA_INGRESO = ae.Field<DateTime>("FECHA_INGRESO");
                            actividad_economica.USUARIO = string.Empty;
                            direccionAEdata = _conectorBD.ListarDireccionActividadEconomica(actividad_economica.ID_ACTIVIDAD_ECONOMICA);
                            foreach (DataRow dire in direccionAEdata.Rows)
                            {
                                DIRECCION direccion = new DIRECCION();
                                direccion.TELEFONOS = new List<TELEFONO>();
                                direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                                direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                                direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                                direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                                direccion.USUARIO = string.Empty;
                                telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                direccion.TELEFONOS.Clear();
                                foreach (DataRow tele in telefonodata.Rows)
                                {
                                    TELEFONO telefono = new TELEFONO();
                                    telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                    telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                    telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                    telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                    telefono.USUARIO = string.Empty;
                                    direccion.TELEFONOS.Add(telefono);

                                }

                                actividad_economica.DIRECCION_ACT_ECO = direccion;
                            }

                            actividad_economica.CARGO = string.IsNullOrEmpty(ae.Field<string>("CARGO")) ? "" : ae.Field<string>("CARGO").ToString().Trim();
                            actividad_economica.USUARIO = String.Empty;

                            respuesta.ACTIVIDADES_ECONOMICAS.Add(actividad_economica);

                        }

                        referenciadata = _conectorBD.ListarReferencia(respuesta.ID_CLIENTE);
                        respuesta.REFERENCIAS.Clear();
                        foreach (DataRow refe in referenciadata.Rows)
                        {
                            REFERENCIA referencia = new REFERENCIA();
                            referencia.REFERIDO = new PERSONA();

                            referencia.ID_REFERENCIA = refe.Field<int>("ID_REFERENCIA");
                            referencia.TIPO = string.IsNullOrEmpty(refe.Field<string>("TIPO")) ? "" : refe.Field<string>("TIPO").ToString().Trim();
                            referencia.RELACION = string.IsNullOrEmpty(refe.Field<string>("RELACION")) ? "" : refe.Field<string>("RELACION").ToString().Trim();
                            referencia.OBSERVACION = string.IsNullOrEmpty(refe.Field<string>("OBSERVACION")) ? "" : refe.Field<string>("OBSERVACION").ToString().Trim();
                            referencia.CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("CALIFICACION")) ? "" : refe.Field<string>("CALIFICACION").ToString().Trim();
                            referencia.DESCRIPCION_CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("DESCRIPCION_CALIFICACION")) ? "" : refe.Field<string>("DESCRIPCION_CALIFICACION").ToString().Trim();
                            referencia.CALIFICACION = string.IsNullOrEmpty(refe.Field<string>("CALIFICACION")) ? "" : refe.Field<string>("CALIFICACION").ToString().Trim();
                            referencia.USUARIO = "";

                            personareferenciadata = _conectorBD.ListarPersonaReferencia(referencia.ID_REFERENCIA);
                            foreach (DataRow per in personareferenciadata.Rows)
                            {
                                PERSONA persona = new PERSONA();
                                persona.DIRECCIONES_PERS = new List<DIRECCION>();
                                persona.DOCUMENTOS = new List<DOCUMENTO_PERSONAL>();


                                persona.ID_PERSONA = per.Field<int>("ID_PERSONA");
                                persona.GENERO = string.IsNullOrEmpty(per.Field<string>("GENERO")) ? "" : per.Field<string>("GENERO").ToString().Trim();
                                persona.PRIMER_APELLIDO = string.IsNullOrEmpty(per.Field<string>("PRIMER_APELLIDO")) ? "" : per.Field<string>("PRIMER_APELLIDO").ToString().Trim();
                                persona.SEGUNDO_APELLIDO = string.IsNullOrEmpty(per.Field<string>("SEGUNDO_APELLIDO")) ? "" : per.Field<string>("SEGUNDO_APELLIDO").ToString().Trim();
                                persona.NOMBRES = string.IsNullOrEmpty(per.Field<string>("NOMBRES")) ? "" : per.Field<string>("NOMBRES").ToString().Trim();
                                persona.CASADA_APELLIDO = string.IsNullOrEmpty(per.Field<string>("CASADA_APELLIDO")) ? "" : per.Field<string>("CASADA_APELLIDO").ToString().Trim();
                                persona.NOMBRE_COMPLETO = string.IsNullOrEmpty(per.Field<string>("NOMBRE_COMPLETO")) ? "" : per.Field<string>("NOMBRE_COMPLETO").ToString().Trim();
                                persona.CORREO = string.IsNullOrEmpty(per.Field<string>("CORREO")) ? "" : per.Field<string>("CORREO").ToString().Trim();
                                persona.NIVEL_EDUCACION = string.IsNullOrEmpty(per.Field<string>("NIVEL_EDUCACION")) ? "" : per.Field<string>("NIVEL_EDUCACION").ToString().Trim();
                                persona.PROFESION = string.IsNullOrEmpty(per.Field<string>("PROFESION")) ? "" : per.Field<string>("PROFESION").ToString().Trim();
                                persona.ESTADO_CIVIL = string.IsNullOrEmpty(per.Field<string>("ESTADO_CIVIL")) ? "" : per.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                persona.COD_ANA = string.IsNullOrEmpty(per.Field<string>("COD_ANA")) ? "" : per.Field<string>("COD_ANA").ToString().Trim();
                                persona.USUARIO = string.Empty;

                                direcciondata = _conectorBD.ListarDireccionPersona(persona.ID_PERSONA);
                                persona.DIRECCIONES_PERS.Clear();
                                foreach (DataRow dire in direcciondata.Rows)
                                {
                                    DIRECCION direccion = new DIRECCION();
                                    direccion.TELEFONOS = new List<TELEFONO>();
                                    direccion.ID_DIRECCION = dire.Field<int>("ID_DIRECCION");
                                    direccion.PAIS_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("PAIS_RESIDENCIA")) ? "" : dire.Field<string>("PAIS_RESIDENCIA").ToString().Trim();
                                    direccion.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(dire.Field<string>("CIUDAD_RESIDENCIA")) ? "" : dire.Field<string>("CIUDAD_RESIDENCIA").ToString().Trim();
                                    direccion.TIPO_DIRECCION = string.IsNullOrEmpty(dire.Field<string>("TIPO_DIRECCION")) ? "" : dire.Field<string>("TIPO_DIRECCION").ToString().Trim();
                                    direccion.DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("DEPARTAMENTO")) ? "" : dire.Field<string>("DEPARTAMENTO").ToString().Trim();
                                    direccion.LOCALIDAD = string.IsNullOrEmpty(dire.Field<string>("LOCALIDAD")) ? "" : dire.Field<string>("LOCALIDAD").ToString().Trim();
                                    direccion.ZONA_BARRIO = string.IsNullOrEmpty(dire.Field<string>("ZONA_BARRIO")) ? "" : dire.Field<string>("ZONA_BARRIO").ToString().Trim();
                                    direccion.CALLE_AVENIDA = string.IsNullOrEmpty(dire.Field<string>("CALLE_AVENIDA")) ? "" : dire.Field<string>("CALLE_AVENIDA").ToString().Trim();
                                    direccion.NRO_PUERTA = string.IsNullOrEmpty(dire.Field<string>("NRO_PUERTA")) ? "" : dire.Field<string>("NRO_PUERTA").ToString().Trim();
                                    direccion.NRO_PISO = string.IsNullOrEmpty(dire.Field<string>("NRO_PISO")) ? "" : dire.Field<string>("NRO_PISO").ToString().Trim();
                                    direccion.NRO_DEPARTAMENTO = string.IsNullOrEmpty(dire.Field<string>("NRO_DEPARTAMENTO")) ? "" : dire.Field<string>("NRO_DEPARTAMENTO").ToString().Trim();
                                    direccion.NOMBRE_EDIFICIO = string.IsNullOrEmpty(dire.Field<string>("NOMBRE_EDIFICIO")) ? "" : dire.Field<string>("NOMBRE_EDIFICIO").ToString().Trim();
                                    direccion.NRO_CASILLA = dire.Field<int>("NRO_CASILLA");
                                    direccion.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(dire.Field<string>("TIPO_VIVIENDA_OFICINA")) ? "" : dire.Field<string>("TIPO_VIVIENDA_OFICINA").ToString().Trim();
                                    direccion.REFERENCIA = string.IsNullOrEmpty(dire.Field<string>("REFERENCIA")) ? "" : dire.Field<string>("REFERENCIA").ToString().Trim();
                                    direccion.USUARIO = string.Empty;
                                    telefonodata = _conectorBD.ListarTelefono(direccion.ID_DIRECCION);
                                    direccion.TELEFONOS.Clear();
                                    foreach (DataRow tele in telefonodata.Rows)
                                    {
                                        TELEFONO telefono = new TELEFONO();
                                        telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                        telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                        telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                        telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                        telefono.USUARIO = string.Empty;
                                        direccion.TELEFONOS.Add(telefono);

                                    }

                                    persona.DIRECCIONES_PERS.Add(direccion);
                                }
                                documentopersonaldata = _conectorBD.ListarDocumentoPersonal(persona.ID_PERSONA);
                                persona.DOCUMENTOS.Clear();
                                foreach (DataRow doc in documentopersonaldata.Rows)
                                {
                                    DOCUMENTO_PERSONAL documento_personal = new DOCUMENTO_PERSONAL();

                                    documento_personal.ID_DOCUMENTO_PERSONAL = doc.Field<int>("ID_DOCUMENTO_PERSONAL");
                                    documento_personal.TIPO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("TIPO_DOCUMENTO")) ? "" : doc.Field<string>("TIPO_DOCUMENTO").ToString().Trim();
                                    documento_personal.NRO_DOCUMENTO = string.IsNullOrEmpty(doc.Field<string>("NRO_DOCUMENTO")) ? "" : doc.Field<string>("NRO_DOCUMENTO").ToString().Trim();
                                    documento_personal.COMPLEMENTO = string.IsNullOrEmpty(doc.Field<string>("COMPLEMENTO")) ? "" : doc.Field<string>("COMPLEMENTO").ToString().Trim();
                                    documento_personal.EXTENSION = string.IsNullOrEmpty(doc.Field<string>("EXTENSION")) ? "" : doc.Field<string>("EXTENSION").ToString().Trim();
                                    documento_personal.FECHA_VENC = doc.Field<DateTime>("FECHA_VENC");
                                    documento_personal.ESTADO_CIVIL = string.IsNullOrEmpty(doc.Field<string>("ESTADO_CIVIL")) ? "" : doc.Field<string>("ESTADO_CIVIL").ToString().Trim();
                                    documento_personal.NACIONALIDAD = string.IsNullOrEmpty(doc.Field<string>("NACIONALIDAD")) ? "" : doc.Field<string>("NACIONALIDAD").ToString().Trim();
                                    documento_personal.LUGAR_NACIMIENTO = string.IsNullOrEmpty(doc.Field<string>("LUGAR_NACIMIENTO")) ? "" : doc.Field<string>("LUGAR_NACIMIENTO").ToString().Trim();
                                    documento_personal.FECHA_NACIMIENTO = doc.Field<DateTime>("FECHA_NACIMIENTO");
                                    documento_personal.USUARIO = string.Empty;
                                    persona.DOCUMENTOS.Add(documento_personal);

                                }

                                telefonodata = _conectorBD.ListarTelefonoPersona(persona.ID_PERSONA);
                                foreach (DataRow tele in telefonodata.Rows)
                                {
                                    TELEFONO telefono = new TELEFONO();
                                    telefono.ID_TELEFONO = tele.Field<int>("ID_TELEFONO");
                                    telefono.TIPO = string.IsNullOrEmpty(tele.Field<string>("TIPO")) ? "" : tele.Field<string>("TIPO").ToString().Trim();
                                    telefono.NUMERO = string.IsNullOrEmpty(tele.Field<string>("NUMERO")) ? "0" : tele.Field<string>("NUMERO").ToString().Trim();
                                    telefono.DESCRIPCION = string.IsNullOrEmpty(tele.Field<string>("DESCRIPCION")) ? "" : tele.Field<string>("DESCRIPCION").ToString().Trim();
                                    telefono.USUARIO = string.Empty;
                                    persona.CONTACTO = telefono;
                                }
                                persona.USUARIO = String.Empty;
                                referencia.REFERIDO = persona;
                            }

                            respuesta.REFERENCIAS.Add(referencia);

                        }

                        creditodata = _conectorBD.ListarCredito(respuesta.ID_CLIENTE);
                        respuesta.CREDITOS.Clear();
                        foreach (DataRow cre in creditodata.Rows)
                        {
                            CREDITO credito = new CREDITO();
                            credito.DECLARACIONES = new List<DECLARACION_JURADA>();
                            credito.DOCUMENTOS_ENTREGADOS = new List<DOCUMENTO_ENTREGADO>();
                            credito.AUTORIZACIONES = new List<AUTORIZACION>();

                            credito.ID_CREDITO = cre.Field<int>("ID_CREDITO");
                            credito.COD_CREDITO = String.IsNullOrEmpty(cre.Field<string>("COD_CREDITO")) ? "" : cre.Field<string>("COD_CREDITO").ToString().Trim();
                            credito.COD_SCI = string.IsNullOrEmpty(cre.Field<string>("COD_SCI")) ? "" : cre.Field<string>("COD_SCI").ToString().Trim();
                            credito.MONTO_CREDITO = cre.Field<decimal>("MONTO_CREDITO");
                            credito.TIPO_OPERACION = string.IsNullOrEmpty(cre.Field<string>("TIPO_OPERACION")) ? "" : cre.Field<string>("TIPO_OPERACION").ToString().Trim();
                            credito.ESTRATEGIA = string.IsNullOrEmpty(cre.Field<string>("ESTRATEGIA")) ? "" : cre.Field<string>("ESTRATEGIA").ToString().Trim();
                            credito.DESTINO = string.IsNullOrEmpty(cre.Field<string>("DESTINO")) ? "" : cre.Field<string>("DESTINO").ToString().Trim();
                            credito.DESTINO_CLIENTE = string.IsNullOrEmpty(cre.Field<string>("DESTINO_CLIENTE")) ? "" : cre.Field<string>("DESTINO_CLIENTE").ToString().Trim();
                            credito.COMPRA_PASIVO = cre.Field<bool>("COMPRA_PASIVO");
                            credito.OBJETO_OPERACION = string.IsNullOrEmpty(cre.Field<string>("OBJETO_OPERACION")) ? "" : cre.Field<string>("OBJETO_OPERACION").ToString().Trim();
                            credito.PLAZO = cre.Field<int>("PLAZO");
                            credito.DIA_PAGO = cre.Field<int>("DIA_PAGO");
                            credito.ANTIGUEDAD_CLIENTE = string.IsNullOrEmpty(cre.Field<string>("ANTIGUEDAD_CLIENTE")) ? "" : cre.Field<string>("ANTIGUEDAD_CLIENTE").ToString().Trim();
                            credito.FECHA_SOLICITUD = cre.Field<DateTime>("FECHA_SOLICITUD");

                            declaracionjuradadata = _conectorBD.ListarDeclaracionJurada(credito.ID_CREDITO);
                            credito.DECLARACIONES.Clear();
                            foreach (DataRow dj in declaracionjuradadata.Rows)
                            {
                                DECLARACION_JURADA declaracion_jarada = new DECLARACION_JURADA();
                                declaracion_jarada.ID_DECLARACION_JURADA = dj.Field<int>("ID_DECLARACION_JURADA");
                                declaracion_jarada.TIPO = string.IsNullOrEmpty(dj.Field<string>("TIPO")) ? "" : dj.Field<string>("TIPO").ToString().Trim();
                                declaracion_jarada.PATRIMONIO_ACTIVO = dj.Field<int>("PATRIMONIO_ACTIVO");
                                declaracion_jarada.PATRIMONIO_PASIVO = dj.Field<int>("PATRIMONIO_PASIVO");
                                declaracion_jarada.PERSONAL_OCUPADO = dj.Field<int>("PERSONAL_OCUPADO");
                                declaracion_jarada.TOTAL_INGRESO_VENTAS = dj.Field<int>("TOTAL_INGRESO_VENTAS");
                                declaracion_jarada.OBSERVACIONES = string.IsNullOrEmpty(dj.Field<string>("OBSERVACIONES")) ? "" : dj.Field<string>("OBSERVACIONES").ToString().Trim();
                                declaracion_jarada.USUARIO = string.Empty;
                                credito.DECLARACIONES.Add(declaracion_jarada);

                            }


                            credito.CLIENTE_CPOP = string.IsNullOrEmpty(cre.Field<string>("CLIENTE_CPOP")) ? "" : cre.Field<string>("CLIENTE_CPOP").ToString().Trim();
                            credito.NRO_CPOP = cre.Field<int>("NRO_CPOP");
                            credito.TIPO_TASA = string.IsNullOrEmpty(cre.Field<string>("TIPO_TASA")) ? "" : cre.Field<string>("TIPO_TASA").ToString().Trim();
                            credito.NRO_AUTORIZACION = cre.Field<int>("NRO_AUTORIZACION");
                            credito.TASA_SELECCION = string.IsNullOrEmpty(cre.Field<string>("TASA_SELECCION")) ? "" : cre.Field<string>("TASA_SELECCION").ToString().Trim();
                            credito.TASA_PP = cre.Field<int>("TASA_PP");
                            credito.ESTADO_CREDITO = string.IsNullOrEmpty(cre.Field<string>("ESTADO_CREDITO")) ? "" : cre.Field<string>("ESTADO_CREDITO").ToString().Trim();
                            credito.ESTADO_SCI = string.IsNullOrEmpty(cre.Field<string>("ESTADO_SCI")) ? "" : cre.Field<string>("ESTADO_SCI").ToString().Trim();

                            documentoentregadodata = _conectorBD.ListarDocumentoEntregado(credito.ID_CREDITO);
                            credito.DOCUMENTOS_ENTREGADOS.Clear();
                            foreach (DataRow de in documentoentregadodata.Rows)
                            {
                                DOCUMENTO_ENTREGADO documentos_entregados = new DOCUMENTO_ENTREGADO();
                                documentos_entregados.ID_DOCUMENTO_ENTREGADO = de.Field<int>("ID_DOCUMENTO_ENTREGADO");
                                documentos_entregados.TIPO = string.IsNullOrEmpty(de.Field<string>("TIPO")) ? "" : de.Field<string>("TIPO").ToString().Trim();
                                documentos_entregados.EXTENSION = string.IsNullOrEmpty(de.Field<string>("EXTENSION")) ? "" : de.Field<string>("EXTENSION").ToString().Trim();
                                documentos_entregados.ARCHIVO = string.IsNullOrEmpty(de.Field<string>("ARCHIVO")) ? "" : de.Field<string>("ARCHIVO").ToString().Trim();
                                documentos_entregados.DOCUMENTO_DESCRIPCION = string.IsNullOrEmpty(de.Field<string>("DOCUMENTO_DESCRIPCION")) ? "" : de.Field<string>("DOCUMENTO_DESCRIPCION").ToString().Trim();
                                documentos_entregados.ENTREGADO = de.Field<bool>("ENTREGADO");
                                documentos_entregados.NOMBRE_ARCHIVO = string.IsNullOrEmpty(de.Field<string>("NOMBRE_ARCHIVO")) ? "" : de.Field<string>("NOMBRE_ARCHIVO").ToString().Trim();

                                credito.DOCUMENTOS_ENTREGADOS.Add(documentos_entregados);

                            }

                            autorizaciondata = _conectorBD.ListarAutorizacion(credito.ID_CREDITO);
                            credito.AUTORIZACIONES.Clear();
                            foreach (DataRow auto in autorizaciondata.Rows)
                            {
                                AUTORIZACION autorizacion = new AUTORIZACION();
                                autorizacion.ID_AUTORIZACION = auto.Field<int>("ID_AUTORIZACION");
                                autorizacion.HABILITADO = auto.Field<bool>("HABILITADO");
                                autorizacion.AUTORIZACION_ESPECIAL = string.IsNullOrEmpty(auto.Field<string>("AUTORIZACION_ESPECIAL")) ? "" : auto.Field<string>("AUTORIZACION_ESPECIAL").ToString().Trim();
                                autorizacion.DESCRIPCION = string.IsNullOrEmpty(auto.Field<string>("DESCRIPCION")) ? "" : auto.Field<string>("DESCRIPCION").ToString().Trim();
                                credito.AUTORIZACIONES.Add(autorizacion);
                            }
                            credito.USUARIO = String.Empty;
                            respuesta.CREDITOS.Add(credito);

                        }

                        scoringdata = _conectorBD.ListarScoring(respuesta.ID_CLIENTE);
                        foreach (DataRow scor in scoringdata.Rows)
                        {
                            SCORINGREPONSE scoring = new SCORINGREPONSE();
                            scoring.ID_SCORING = scor.Field<int>("ID_SCORING");
                            scoring.ID_CLIENTE = scor.Field<int>("ID_CLIENTE");
                            scoring.TICKET = string.IsNullOrEmpty(scor.Field<string>("TICKET")) ? "" : scor.Field<string>("TICKET").ToString().Trim();
                            CRITERIOres = string.IsNullOrEmpty(scor.Field<string>("CRITERIO")) ? "" : scor.Field<string>("CRITERIO").ToString().Trim();
                            RESULTADOres = string.IsNullOrEmpty(scor.Field<string>("RESULTADO")) ? "" : scor.Field<string>("RESULTADO").ToString().Trim();
                            PUNTAJEres = string.IsNullOrEmpty(scor.Field<string>("PUNTAJE")) ? "" : scor.Field<string>("PUNTAJE").ToString().Trim();

                            CRITERIO = CRITERIOres.Split('|');
                            RESULTADO = RESULTADOres.Split('|');
                            PUNTAJE = PUNTAJEres.Split('|');
                            for (int i = 0; i < CRITERIO.Length; i++)
                            {
                                Puntaje_Scoring res = new Puntaje_Scoring();
                                string area = string.Empty;
                                string resultado = DescripcionScoring(CRITERIO[i], ref area);
                                res.CRITERIO = resultado.Trim();
                                res.RESULTADO = RESULTADO[i].Trim();
                                res.PUNTAJE = PUNTAJE[i].Trim();
                                switch (area)
                                {
                                    case "CIC":
                                        scoring.CIC.Add(res);
                                        break;
                                    case "Infocred":
                                        scoring.Infocred.Add(res);
                                        break;
                                    case "Hoja Riesgos":
                                        scoring.Hoja_Riesgos.Add(res);
                                        break;
                                    default:
                                        scoring.Puntaje.Add(res);
                                        break;
                                }
                            }
                            respuesta.PUNTAJE_BUROS = scoring;
                        }
                        ENTREVISTA ENTREVISTA = new ENTREVISTA();

                        verificacionclientedata = _conectorBD.BuscarVerificacionClienteXCliente(respuesta.ID_CLIENTE);
                        if (verificacionclientedata.Rows.Count != 0)
                        {
                            foreach (DataRow veri in verificacionclientedata.Rows)
                            {

                                ENTREVISTA_VERIFICACION_CLIENTE veri_cliente = new ENTREVISTA_VERIFICACION_CLIENTE();

                                veri_cliente.ID_ENTREVISTA_VERI_CLIENTE = veri.Field<int>("ID_ENTREVISTA_VERI_CLIENTE");
                                ENTREVISTA.ID_CLIENTE = veri.Field<int>("ID_CLIENTE");
                                VerClientePreguntares = string.IsNullOrEmpty(veri.Field<string>("PREGUNTA")) ? "" : veri.Field<string>("PREGUNTA").ToString().Trim();
                                VerClienteRespuestares = string.IsNullOrEmpty(veri.Field<string>("RESPUESTA")) ? "" : veri.Field<string>("RESPUESTA").ToString().Trim();
                                VerClienteConfirmacionres = string.IsNullOrEmpty(veri.Field<string>("CONFIRMACION")) ? "" : veri.Field<string>("CONFIRMACION").ToString().Trim();
                                VerClienteObservacionres = string.IsNullOrEmpty(veri.Field<string>("OBSERVACIONES")) ? "" : veri.Field<string>("OBSERVACIONES").ToString().Trim();

                                VerClientePregunta = VerClientePreguntares.Split('|');
                                VerClienteRespuesta = VerClienteRespuestares.Split('|');
                                VerClienteConfirmacion = VerClienteConfirmacionres.Split('|');
                                VerClienteObservacion = VerClienteObservacionres.Split('|');
                                veri_cliente.LISTADO_VERIFICACION_CLIENTE.Clear();
                                for (int i = 0; i < VerClientePregunta.Length; i++)
                                {
                                    VERIFICACION_CLIENTE_RESPONSE veri_clienteresponse = new VERIFICACION_CLIENTE_RESPONSE();
                                    veri_clienteresponse.PREGUNTA = DescripcionVeriCliente(VerClientePregunta[i]);
                                    veri_clienteresponse.RESPUESTA = (string.IsNullOrEmpty(VerClienteRespuesta[i]) ? "" : (VerClienteRespuesta[i].Trim() == "Sin dato") ? "" : VerClienteRespuesta[i].Trim());
                                    veri_clienteresponse.CONFIRMACION = (string.IsNullOrEmpty(VerClienteConfirmacion[i]) ? "" : (VerClienteConfirmacion[i].Trim() == "Sin dato") ? "" : VerClienteConfirmacion[i].Trim());
                                    veri_clienteresponse.OBSERVACIONES = (string.IsNullOrEmpty(VerClienteObservacion[i]) ? "" : (VerClienteObservacion[i].Trim() == "Sin dato") ? "" : VerClienteObservacion[i].Trim());
                                    veri_cliente.LISTADO_VERIFICACION_CLIENTE.Add(veri_clienteresponse);

                                }

                                ENTREVISTA.VERIFICACION_CLIENTE = veri_cliente;

                            }
                        }

                        scripevaluaciondata = _conectorBD.BuscarScripEvaluacionXCliente(respuesta.ID_CLIENTE);
                        if (scripevaluaciondata.Rows.Count != 0)
                        {
                            foreach (DataRow veri in scripevaluaciondata.Rows)
                            {

                                ENTREVISTA_SCRIPT_EVALUACION script_evaluacion = new ENTREVISTA_SCRIPT_EVALUACION();
                                script_evaluacion.ID_SCRIPT_EVALUACION = veri.Field<int>("ID_SCRIPT_EVALUACION");
                                ENTREVISTA.ID_CLIENTE = veri.Field<int>("ID_CLIENTE");
                                VerClientePreguntares = string.IsNullOrEmpty(veri.Field<string>("PREGUNTA")) ? "" : veri.Field<string>("PREGUNTA").ToString().Trim();
                                VerClienteRespuestares = string.IsNullOrEmpty(veri.Field<string>("RESPUESTA")) ? "" : veri.Field<string>("RESPUESTA").ToString().Trim();
                                VerInstitucion = string.IsNullOrEmpty(veri.Field<string>("ID_INSTITUCION")) ? "" : veri.Field<string>("ID_INSTITUCION").ToString().Trim();
                                VerClienteObservacionres = string.IsNullOrEmpty(veri.Field<string>("OBSERVACIONES")) ? "" : veri.Field<string>("OBSERVACIONES").ToString().Trim();


                                VerClientePregunta = VerClientePreguntares.Split('|');
                                VerClienteRespuesta = VerClienteRespuestares.Split('|');
                                VerClienteInstituciones = VerInstitucion.Split('|');
                                VerClienteObservacion = VerClienteObservacionres.Split('|');
                                script_evaluacion.LISTADO_SCRIPT_EVALUACION.Clear();
                                for (int i = 0; i < VerClientePregunta.Length; i++)
                                {
                                    SCRIPT_EVALUACION_RESPONSE veri_clienteresponse = new SCRIPT_EVALUACION_RESPONSE();
                                    veri_clienteresponse.PREGUNTA = DescripcionScripEvaluacion(VerClientePregunta[i]);
                                    veri_clienteresponse.RESPUESTA = (string.IsNullOrEmpty(VerClienteRespuesta[i]) ? "" : (VerClienteRespuesta[i].Trim() == "Sin dato") ? "" : VerClienteRespuesta[i].Trim());
                                    veri_clienteresponse.OBSERVACIONES = (string.IsNullOrEmpty(VerClienteObservacion[i]) ? "" : (VerClienteObservacion[i].Trim() == "Sin dato") ? "" : VerClienteObservacion[i].Trim());
                                    instituciondata = _conectorBD.BuscarScripEvaluacionInstitucionXCliente(VerClienteInstituciones[i]);
                                    if (instituciondata.Rows.Count != 0)
                                    {
                                        foreach (DataRow ins in instituciondata.Rows)
                                        {

                                            //VerInstitucionNombre = DescripcionInstitucion(ins.Field<string>("NOMBRE").ToString().Trim());
                                            VerInstitucionNombre = string.IsNullOrEmpty(ins.Field<string>("NOMBRE")) ? "" : ins.Field<string>("NOMBRE").ToString().Trim();
                                            VerInstitucionMonto = string.IsNullOrEmpty(ins.Field<string>("MONTO")) ? "" : ins.Field<string>("MONTO").ToString().Trim();
                                            veri_clienteresponse.TOTAL = string.IsNullOrEmpty(ins.Field<string>("TOTAL")) ? "" : ins.Field<string>("TOTAL").ToString().Trim();
                                            veri_clienteresponse.ID_INSTITUCIONES = ins.Field<int>("ID_INSTITUCIONES");

                                            VerClienteInstitucionNombre = VerInstitucionNombre.Split('|');
                                            VerClienteInstitucionMonto = VerInstitucionMonto.Split('|');
                                            veri_clienteresponse.INSTITUCIONES.Clear();
                                            for (int j = 0; j < VerClienteInstitucionNombre.Length; j++)
                                            {
                                                INSTITUCION institucion = new INSTITUCION();

                                                institucion.NOMBRE = DescripcionInstitucion(VerClienteInstitucionNombre[j]);
                                                institucion.MONTO = string.IsNullOrEmpty(VerClienteInstitucionMonto[j])?"": VerClienteInstitucionMonto[j].ToString().Trim();
                                                veri_clienteresponse.INSTITUCIONES.Add(institucion);
                                            }

                                        }
                                    }



                                    script_evaluacion.LISTADO_SCRIPT_EVALUACION.Add(veri_clienteresponse);
                                }
                                ENTREVISTA.SCRIPT_EVALUACION = script_evaluacion;

                            }
                        }


                        segurodesgravamedata = _conectorBD.BuscarSeguroDesgravameXCliente(respuesta.ID_CLIENTE);
                        if (segurodesgravamedata.Rows.Count != 0)
                        {
                            foreach (DataRow veri in segurodesgravamedata.Rows)
                            {

                                ENTREVISTA_SEGURO_DESGRAVAME veri_cliente = new ENTREVISTA_SEGURO_DESGRAVAME();
                                veri_cliente.ID_SEGURO_DESGRAVAME = veri.Field<int>("ID_SEGURO_DESGRAVAME");
                                ENTREVISTA.ID_CLIENTE = veri.Field<int>("ID_CLIENTE");
                                VerClientePreguntares = string.IsNullOrEmpty(veri.Field<string>("PREGUNTA")) ? "" : veri.Field<string>("PREGUNTA").ToString().Trim();
                                VerClienteRespuestares = string.IsNullOrEmpty(veri.Field<string>("RESPUESTA")) ? "" : veri.Field<string>("RESPUESTA").ToString().Trim();
                                VerClienteConfirmacionres = string.IsNullOrEmpty(veri.Field<string>("CONFIRMACION")) ? "" : veri.Field<string>("CONFIRMACION").ToString().Trim();
                                VerClienteObservacionres = string.IsNullOrEmpty(veri.Field<string>("OBSERVACIONES")) ? "" : veri.Field<string>("OBSERVACIONES").ToString().Trim();

                                VerClientePregunta = VerClientePreguntares.Split('|');
                                VerClienteRespuesta = VerClienteRespuestares.Split('|');
                                VerClienteConfirmacion = VerClienteConfirmacionres.Split('|');
                                VerClienteObservacion = VerClienteObservacionres.Split('|');
                                veri_cliente.LISTADO_SEGURO_DESGRAVAME.Clear();
                                for (int i = 0; i < VerClientePregunta.Length; i++)
                                {
                                    SEGURO_DESGRAVAME_RESPONSE veri_clienteresponse = new SEGURO_DESGRAVAME_RESPONSE();
                                    veri_clienteresponse.PREGUNTA = DescripcionSeguroDesgravame(VerClientePregunta[i]);
                                    veri_clienteresponse.RESPUESTA = (string.IsNullOrEmpty(VerClienteRespuesta[i]) ? "" : (VerClienteRespuesta[i].Trim() == "Sin dato") ? "" : VerClienteRespuesta[i].Trim());
                                    
                                    veri_clienteresponse.CONFIRMACION = (string.IsNullOrEmpty(VerClienteConfirmacion[i]) ? "" : (VerClienteConfirmacion[i].Trim() == "Sin dato") ? "" : VerClienteConfirmacion[i].Trim());
                                    veri_clienteresponse.OBSERVACIONES = (string.IsNullOrEmpty(VerClienteObservacion[i]) ? "" : (VerClienteObservacion[i].Trim() == "Sin dato") ? "" : VerClienteObservacion[i].Trim());
                                    veri_cliente.LISTADO_SEGURO_DESGRAVAME.Add(veri_clienteresponse);

                                }

                                ENTREVISTA.SEGURO_DESGRAVAME = veri_cliente;

                            }
                        }

                        respuesta.ENTREVISTA = ENTREVISTA;

                        respuestaclientedata = _conectorBD.ListadoHistorialRespuestaCliente(respuesta.ID_CLIENTE);
                        if (respuestaclientedata.Rows.Count != 0)
                        {
                            respuesta.ESTADOS_ROBOT.Clear();
                            foreach (DataRow res in respuestaclientedata.Rows)
                            {
                                RESPUESTA_CLIENTE_RESPONSE clienterespuesta = new RESPUESTA_CLIENTE_RESPONSE();
                                clienterespuesta.ID_RESPUESTA_CLIENTE = res.Field<int>("ID_RESPUESTA_CLIENTE");
                                clienterespuesta.CATEGORIA = string.IsNullOrEmpty(res.Field<string>("CATEGORIA")) ? "" : res.Field<string>("CATEGORIA").ToString().Trim();
                                clienterespuesta.SUCCESS = res.Field<bool>("SUCCESS");
                                clienterespuesta.MENSAJE = string.IsNullOrEmpty(res.Field<string>("MENSAJE")) ? "" : res.Field<string>("MENSAJE").ToString().Trim();
                                clienterespuesta.CODIGO = string.IsNullOrEmpty(res.Field<string>("CODIGO")) ? "" : res.Field<string>("CODIGO").ToString().Trim();
                                respuesta.ESTADOS_ROBOT.Add(clienterespuesta);
                            }
                        }

                    }
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = Configuraciones.GetMessage("OK");
                    respuesta.success = true;
                }
                else
                {
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = "LISTA DE CREDITOS SE ENCUENTRA VACIO";
                    respuesta.success = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public async Task<CLIENTERESPONSE> BuscarClienteValidar(BuscarClienteRequest request)
        {
            //ListarCredito ListarCredito = new ListarCredito();
            //ListarCredito.LISTARCREDITOS = new List<CREDITO>();
            //CREDITORESPONSE respuesta = new CREDITORESPONSE();
            #region VARIALES
            DataTable data = new DataTable();
            DataTable personadata = new DataTable();
            DataTable creditodata = new DataTable();
            DataTable telefonodata = new DataTable();
            DataTable direcciondata = new DataTable();
            DataTable documentopersonaldata = new DataTable();
            DataTable actividadeconomicadata = new DataTable();
            DataTable caedecdata = new DataTable();
            DataTable fechaAMdata = new DataTable();
            DataTable direccionAEdata = new DataTable();
            DataTable referenciadata = new DataTable();
            DataTable personareferenciadata = new DataTable();
            DataTable declaracionjuradadata = new DataTable();
            DataTable autorizaciondata = new DataTable();
            DataTable documentoentregadodata = new DataTable();
            DataTable scoringdata = new DataTable();

            DataTable respuestaclientedata = new DataTable();


            DataTable verificacionclientedata = new DataTable();
            DataTable scripevaluaciondata = new DataTable();
            DataTable instituciondata = new DataTable();
            DataTable segurodesgravamedata = new DataTable();

            string[] CRITERIO;
            string[] RESULTADO;
            string[] PUNTAJE;

            string CRITERIOres = string.Empty;
            string RESULTADOres = string.Empty;
            string PUNTAJEres = string.Empty;


            string[] VerClientePregunta;
            string[] VerClienteRespuesta;
            string[] VerClienteConfirmacion;
            string[] VerClienteInstituciones;
            string[] VerClienteObservacion;

            string[] VerClienteInstitucionNombre;
            string[] VerClienteInstitucionMonto;


            string VerClientePreguntares = string.Empty;
            string VerClienteRespuestares = string.Empty;
            string VerClienteConfirmacionres = string.Empty;
            string VerInstitucion = string.Empty;
            string VerInstitucionNombre = string.Empty;
            string VerInstitucionMonto = string.Empty;
            string VerClienteObservacionres = string.Empty;




            CLIENTERESPONSE respuesta = new CLIENTERESPONSE();
            string datares = string.Empty;
            #endregion 

            try
            {
                data = _conectorBD.BuscarClienteTemporal("VALIDAR",request.ID_CLIENTE);

                if (data.Rows.Count!=0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        datares = item.Field<string>("DESCRIPCION");
                    }
                    respuesta = JsonConvert.DeserializeObject<CLIENTERESPONSE>(datares);
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = Configuraciones.GetMessage("OK");
                    respuesta.success = true;
                }
                else
                {
                    respuesta.code = Configuraciones.GetCode("OK");
                    respuesta.message = "LISTA DE CREDITOS SE ENCUENTRA VACIO";
                    respuesta.success = false;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public string CodigoScoring(string AREA,string BUSCAR)
        {
            string resultado = string.Empty;
            DataTable data = new DataTable();
            try
            {
                switch (AREA)
                {
                    case "CIC":
                        data = _conectorBD.BuscarCIC(BUSCAR);
                        if (data.Rows.Count != 0)
                            {
                            foreach (DataRow item in data.Rows)
                            {
                                resultado = string.IsNullOrEmpty(item.Field<string>("CODIGO_CIC")) ? "" : item.Field<string>("CODIGO_CIC").ToString().Trim();
                            }
                        }
                        break;
                    case "Infocred":
                        data = _conectorBD.BuscarINFOCRED(BUSCAR);
                        if (data.Rows.Count != 0)
                        {
                            foreach (DataRow item in data.Rows)
                            {
                                resultado = string.IsNullOrEmpty(item.Field<string>("CODIGO_INFOCRED")) ? "" : item.Field<string>("CODIGO_INFOCRED").ToString().Trim();
                            }
                        }
                        break;
                    case "Hoja Riesgos":
                        data = _conectorBD.BuscarHOJARIESGOS(BUSCAR);
                        if (data.Rows.Count != 0)
                        {
                            foreach (DataRow item in data.Rows)
                            {
                                resultado = string.IsNullOrEmpty(item.Field<string>("CODIGO_HOJA_RIESGOS")) ? "" : item.Field<string>("CODIGO_HOJA_RIESGOS").ToString().Trim();
                            }
                        }
                        break;
                    default:
                        data = _conectorBD.BuscarPUNTAJE(BUSCAR);
                        if (data.Rows.Count != 0)
                        {
                            foreach (DataRow item in data.Rows)
                            {
                                resultado = string.IsNullOrEmpty(item.Field<string>("CODIGO_PUNTAJE")) ? "" : item.Field<string>("CODIGO_PUNTAJE").ToString().Trim();
                            }
                        }
                        break;
                }


            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public string DescripcionScoring(string BUSCAR,ref string AREA)
        {
            string resultado = string.Empty;
            DataTable data = new DataTable();
            try
            {
                data = _conectorBD.BuscarCodigoCIC(BUSCAR);
                if (data.Rows.Count!=0)
                {
                    AREA = "CIC";
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();
                    }
                    return resultado;
                }

                data = _conectorBD.BuscarCodigoINFOCRED(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    AREA = "Infocred";
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();
                    }
                    return resultado;
                }
                data = _conectorBD.BuscarCodigoHOJARIESGOS(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    AREA = "Hoja Riesgos";
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();
                    }
                    return resultado;
                }

                data = _conectorBD.BuscarCodigoPUNTAJE(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    AREA = "Puntaje";
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();
                    }
                    return resultado;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        #endregion

        #region ENTREVISTA

        #region Verificacion Cliente
        public string CodigoVeriCliente(string BUSCAR)
        {
            string resultado = string.Empty;
            DataTable data = new DataTable();
            try
            {
                data = _conectorBD.BuscarVerificacionCliente(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("CODIGO_VERIFICACION_CLIENTE")) ? "" : item.Field<string>("CODIGO_VERIFICACION_CLIENTE").ToString().Trim();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public string DescripcionVeriCliente(string BUSCAR)
        {
            string resultado = string.Empty;
            DataTable data = new DataTable();
            try
            {
                data = _conectorBD.BuscarCodigoVerificacionCliente(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();
                    }
                    return resultado;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        #endregion

        #region Seguro Desgravame
        public string CodigoSeguroDesgravame(string BUSCAR)
        {
            string resultado = string.Empty;
            DataTable data = new DataTable();
            try
            {
                data = _conectorBD.BuscarSeguroDesgravame(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("CODIGO_SEGURO_DESGRAVAME")) ? "" : item.Field<string>("CODIGO_SEGURO_DESGRAVAME").ToString().Trim();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public string DescripcionSeguroDesgravame(string BUSCAR)
        {
            string resultado = string.Empty;
            DataTable data = new DataTable();
            try
            {
                data = _conectorBD.BuscarCodigoSeguroDesgravame(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();
                    }
                    return resultado;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        #endregion

        #region Script Evaluacion
        public string CodigoScripEvaluacion(string BUSCAR)
        {
            string resultado = string.Empty;
            DataTable data = new DataTable();
            try
            {
                data = _conectorBD.BuscarScriptEvaluacion(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("CODIGO_SCRIPT_EVALUACION")) ? "" : item.Field<string>("CODIGO_SCRIPT_EVALUACION").ToString().Trim();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public string DescripcionScripEvaluacion(string BUSCAR)
        {
            string resultado = string.Empty;
            DataTable data = new DataTable();
            try
            {
                data = _conectorBD.BuscarCodigoScriptEvaluacion(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();
                    }
                    return resultado;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        #endregion

        #region Institucion

        public string CodigoInstitucion(string BUSCAR)
        {
            string resultado = string.Empty;
            DataTable data = new DataTable();
            try
            {
                data = _conectorBD.BuscarInstitucion(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("CODIGO_INSTITUCION")) ? "" : item.Field<string>("CODIGO_INSTITUCION").ToString().Trim();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public string DescripcionInstitucion(string BUSCAR)
        {
            string resultado = string.Empty;
            DataTable data = new DataTable();
            try
            {
                data = _conectorBD.BuscarCodigoInstitucion(BUSCAR);
                if (data.Rows.Count != 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = string.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();
                    }
                    return resultado;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        #endregion


        #endregion

        #region COMBOS
        public async Task<ComboCredito>   ComboCredito()
        {
            ComboCredito response = new ComboCredito();
            response.ListaTipoOperacion = new List<TipoOperacionCB>();
            response.ListaEstrategia = new List<EstrategiaCB>();
            response.ListaDestino = new List<DestinoCB>();
            response.ListaObjetoOperacion = new List<ObjetoOperacionCB>();
            response.ListaClienteCPOP = new List<ClienteCPOPCB>();
            response.ListaAntiguedadCliente = new List<AntiguedadClienteCB>();
            response.ListaMoneda = new List<MonedaCB>();
            response.ListaTipoTasaNegocio = new List<TipoTasaNegocioCB>();

            DataTable TipoOperacion = new DataTable();
            DataTable Estrategia = new DataTable();
            DataTable Destino = new DataTable();
            DataTable ObjetoOperacion = new DataTable();
            DataTable ClienteCPOP = new DataTable();
            DataTable AntiguedadCliente = new DataTable();
            DataTable Moneda = new DataTable();
            DataTable TipoTasaNegocio = new DataTable();


            try
            {
                TipoOperacion = _conectorBD.ListaTipoOperacion();
                if (TipoOperacion.Rows.Count!=0)
                {
                    foreach (DataRow item in TipoOperacion.Rows)
                    {
                        TipoOperacionCB data = new TipoOperacionCB();
                        data.ID_TIPO_OPERACION = item.Field<int>("ID_TIPO_OPERACION");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaTipoOperacion.Add(data);
                    }
                }

                Estrategia = _conectorBD.ListaEstrategia();
                if (Estrategia.Rows.Count!=0)
                {
                    foreach (DataRow item in Estrategia.Rows)
                    {
                        EstrategiaCB data = new EstrategiaCB();
                        data.ID_ESTRATEGIA = item.Field<int>("ID_ESTRATEGIA");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaEstrategia.Add(data);
                    }
                }

                Destino = _conectorBD.ListaDestino();
                if (Destino.Rows.Count!=0)
                {
                    foreach (DataRow item in Destino.Rows)
                    {
                        DestinoCB data = new DestinoCB();
                        data.ID_DESTINO = item.Field<int>("ID_DESTINO");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaDestino.Add(data);
                    }
                }

                ObjetoOperacion = _conectorBD.ListaObjetoOperacion();
                if (ObjetoOperacion.Rows.Count!=0)
                {
                    foreach (DataRow item in ObjetoOperacion.Rows)
                    {
                        ObjetoOperacionCB data = new ObjetoOperacionCB();
                        data.ID_OBJETO_OPERACION = item.Field<int>("ID_OBJETO_OPERACION");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaObjetoOperacion.Add(data);
                    }
                }

                ClienteCPOP = _conectorBD.ListaClienteCPOP();
                if (ClienteCPOP.Rows.Count!=0)
                {
                    foreach (DataRow item in ClienteCPOP.Rows)
                    {
                        ClienteCPOPCB data = new ClienteCPOPCB();
                        data.ID_CLIENTE_CPOP = item.Field<int>("ID_CLIENTE_CPOP");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaClienteCPOP.Add(data);
                    }
                }

                AntiguedadCliente = _conectorBD.ListaAntiguedadCliente();
                if (AntiguedadCliente.Rows.Count!=0)
                {
                    foreach (DataRow item in AntiguedadCliente.Rows)
                    {
                        AntiguedadClienteCB data = new AntiguedadClienteCB();
                        data.ID_ANTIGUEDAD_CLIENTE = item.Field<int>("ID_ANTIGUEDAD_CLIENTE");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaAntiguedadCliente.Add(data);
                    }
                }

                Moneda = _conectorBD.ListaMoneda();
                if (Moneda.Rows.Count!=0)
                {
                    foreach (DataRow item in Moneda.Rows)
                    {
                        MonedaCB data = new MonedaCB();
                        data.ID_MONEDA = item.Field<int>("ID_MONEDA");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaMoneda.Add(data);
                    }
                }

                TipoTasaNegocio = _conectorBD.ListaTipoTasaNegocio();
                if (TipoTasaNegocio.Rows.Count!=0)
                {
                    foreach (DataRow item in TipoTasaNegocio.Rows)
                    {
                        TipoTasaNegocioCB data = new TipoTasaNegocioCB();
                        data.ID_TIPO_TASA_NEGOCIACION = item.Field<int>("ID_TIPO_TASA_NEGOCIACION");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaTipoTasaNegocio.Add(data);
                    }
                }



                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboDocumentoPersonal>   ComboDocumentoPersonal()
        {
            ComboDocumentoPersonal response = new ComboDocumentoPersonal();
            response.ListaTipoDocumento = new List<TipoDocumentoCB>();
            response.ListaExtension = new List<ExtensionCB>();
            response.ListaEstadoCivil = new List<EstadoCivilCB>();
            response.ListaNacionalidad = new List<NacionalidadCB>();
            response.ListaPais = new List<PaisCB>();

            DataTable TipoDocumento = new DataTable();
            DataTable Extension = new DataTable();
            DataTable EstadoCivil = new DataTable();
            DataTable Nacionalidad = new DataTable();
            DataTable Pais = new DataTable();


            try
            {
                TipoDocumento = _conectorBD.ListaTipoDocumento();
                if (TipoDocumento.Rows.Count!=0)
                {
                    foreach (DataRow item in TipoDocumento.Rows)
                    {
                        TipoDocumentoCB data = new TipoDocumentoCB();
                        data.ID_TIPO_DOCUMENTO = item.Field<int>("ID_TIPO_DOCUMENTO");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaTipoDocumento.Add(data);
                    }
                }

                Extension = _conectorBD.ListaExtension();
                if (Extension.Rows.Count!=0)
                {
                    foreach (DataRow item in Extension.Rows)
                    {
                        ExtensionCB data = new ExtensionCB();
                        data.ID_EXTENSION = item.Field<int>("ID_EXTENSION");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaExtension.Add(data);
                    }
                }

                EstadoCivil = _conectorBD.ListaEstadoCivil();
                if (EstadoCivil.Rows.Count!=0)
                {
                    foreach (DataRow item in EstadoCivil.Rows)
                    {
                        EstadoCivilCB data = new EstadoCivilCB();
                        data.ID_ESTADO_CIVIL = item.Field<int>("ID_ESTADO_CIVIL");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaEstadoCivil.Add(data);
                    }
                }

                Nacionalidad = _conectorBD.ListaNacionalidad();
                if (Nacionalidad.Rows.Count!=0)
                {
                    foreach (DataRow item in Nacionalidad.Rows)
                    {
                        NacionalidadCB data = new NacionalidadCB();
                        data.ID_NACIONALIDAD = item.Field<int>("ID_NACIONALIDAD");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaNacionalidad.Add(data);
                    }
                }

                Pais = _conectorBD.ListaPais();
                if (Pais.Rows.Count!=0)
                {
                    foreach (DataRow item in Pais.Rows)
                    {
                        PaisCB data = new PaisCB();
                        data.ID_PAIS = item.Field<int>("ID_PAIS");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaPais.Add(data);
                    }
                }


                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboPersona>  ComboPersona()
        {
            ComboPersona response = new ComboPersona();
            response.ListaGenero = new List<GeneroCB>();
            response.ListaNivelEducacion = new List<NivelEducacionCB>();
            response.ListaProfesion = new List<ProfesionCB>();
            response.ListaEstadoCivil = new List<EstadoCivilCB>();

            DataTable Genero = new DataTable();
            DataTable NivelEducacion = new DataTable();
            DataTable Profesion = new DataTable();
            DataTable EstadoCivil = new DataTable();


            try
            {
                Genero = _conectorBD.ListaGenero();
                if (Genero.Rows.Count!=0)
                {
                    foreach (DataRow item in Genero.Rows)
                    {
                        GeneroCB data = new GeneroCB();
                        data.ID_GENERO = item.Field<int>("ID_GENERO");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaGenero.Add(data);
                    }
                }

                NivelEducacion = _conectorBD.ListaNivelEducacion();
                if (NivelEducacion.Rows.Count!=0)
                {
                    foreach (DataRow item in NivelEducacion.Rows)
                    {
                        NivelEducacionCB data = new NivelEducacionCB();
                        data.ID_NIVEL_EDUCACION = item.Field<int>("ID_NIVEL_EDUCACION");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaNivelEducacion.Add(data);
                    }
                }

                Profesion = _conectorBD.ListaProfesion();
                if (Profesion.Rows.Count!=0)
                {
                    foreach (DataRow item in Profesion.Rows)
                    {
                        ProfesionCB data = new ProfesionCB();
                        data.ID_PROFESION = item.Field<int>("ID_PROFESION");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaProfesion.Add(data);
                    }
                }

                EstadoCivil = _conectorBD.ListaEstadoCivil();
                if (EstadoCivil.Rows.Count!=0)
                {
                    foreach (DataRow item in EstadoCivil.Rows)
                    {
                        EstadoCivilCB data = new EstadoCivilCB();
                        data.ID_ESTADO_CIVIL = item.Field<int>("ID_ESTADO_CIVIL");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaEstadoCivil.Add(data);
                    }
                }

                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboDireccion>  ComboDireccion()
        {
            ComboDireccion response = new ComboDireccion();
            response.ListaPaisResidencia = new List<PaisResidenciaCB>();
            response.ListaCiudadResidencia = new List<CiudadResidenciaCB>();
            response.ListaDepartamento = new List<DepartamentoCB>();
            response.ListaLocalidad = new List<LocalidadCB>();
            response.ListaTipoVivienda = new List<TipoViviendaCB>();
            response.ListaTipoDireccion = new List<TipoDireccionCB>();

            DataTable PaisResidencia = new DataTable();
            DataTable CiudadResidencia = new DataTable();
            DataTable Departamento = new DataTable();
            DataTable Localidad = new DataTable();
            DataTable TipoVivienda = new DataTable();
            DataTable TipoDireccion = new DataTable();


            try
            {
                PaisResidencia = _conectorBD.ListaPaisResidencia();
                if (PaisResidencia.Rows.Count!=0)
                {
                    foreach (DataRow item in PaisResidencia.Rows)
                    {
                        PaisResidenciaCB data = new PaisResidenciaCB();
                        data.ID_PAIS_RESIDENCIA = item.Field<int>("ID_PAIS_RESIDENCIA");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaPaisResidencia.Add(data);
                    }
                }

                CiudadResidencia = _conectorBD.ListaCiudadResidencia();
                if (CiudadResidencia.Rows.Count!=0)
                {
                    foreach (DataRow item in CiudadResidencia.Rows)
                    {
                        CiudadResidenciaCB data = new CiudadResidenciaCB();
                        data.ID_CIUDAD_RESIDENCIA = item.Field<int>("ID_CIUDAD_RESIDENCIA");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaCiudadResidencia.Add(data);
                    }
                }

                Departamento = _conectorBD.ListaDepartamento();
                if (Departamento.Rows.Count!=0)
                {
                    foreach (DataRow item in Departamento.Rows)
                    {
                        DepartamentoCB data = new DepartamentoCB();
                        data.ID_DEPARTAMENTO = item.Field<int>("ID_DEPARTAMENTO");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaDepartamento.Add(data);
                    }
                }

                Localidad = _conectorBD.ListaLocalidad();
                if (Localidad.Rows.Count!=0)
                {
                    foreach (DataRow item in Localidad.Rows)
                    {
                        LocalidadCB data = new LocalidadCB();
                        data.ID_LOCALIDAD = item.Field<int>("ID_LOCALIDAD");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaLocalidad.Add(data);
                    }
                }

                TipoVivienda = _conectorBD.ListaTipoVivienda();
                if (TipoVivienda.Rows.Count!=0)
                {
                    foreach (DataRow item in TipoVivienda.Rows)
                    {
                        TipoViviendaCB data = new TipoViviendaCB();
                        data.ID_TIPO_VIVIENDA = item.Field<int>("ID_TIPO_VIVIENDA");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaTipoVivienda.Add(data);
                    }
                }

                TipoDireccion = _conectorBD.ListaTipoDireccion();
                if (TipoDireccion.Rows.Count!=0)
                {
                    foreach (DataRow item in TipoDireccion.Rows)
                    {
                        TipoDireccionCB data = new TipoDireccionCB();
                        data.ID_TIPO_DIRECCION = item.Field<int>("ID_TIPO_DIRECCION");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaTipoDireccion.Add(data);
                    }
                }


                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboActividadEconomica>  ComboActividadEconomica()
        {
            ComboActividadEconomica response = new ComboActividadEconomica();
            response.ListaNivelLaboral = new List<NivelLaboralCB>();
            response.ListaIngresosMensuales = new List<IngresosMensualesCB>();
            response.ListaEstadoActividad = new List<EstadoActividadCB>();
            response.ListaSectorEconomico = new List<SectorEconomicoCB>();

            DataTable NivelLaboral = new DataTable();
            DataTable IngresosMensuales = new DataTable();
            DataTable EstadoActividad = new DataTable();
            DataTable SectorEconomico = new DataTable();



            try
            {
                NivelLaboral = _conectorBD.ListaNivelLaboral();
                if (NivelLaboral.Rows.Count!=0)
                {
                    foreach (DataRow item in NivelLaboral.Rows)
                    {
                        NivelLaboralCB data = new NivelLaboralCB();
                        data.ID_NIVEL_LABORAL = item.Field<int>("ID_NIVEL_LABORAL");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaNivelLaboral.Add(data);
                    }
                }

                IngresosMensuales = _conectorBD.ListaIngresosMensuales();
                if (IngresosMensuales.Rows.Count!=0)
                {
                    foreach (DataRow item in IngresosMensuales.Rows)
                    {
                        IngresosMensualesCB data = new IngresosMensualesCB();
                        data.ID_INGRESOS_MENSUALES = item.Field<int>("ID_INGRESOS_MENSUALES");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaIngresosMensuales.Add(data);
                    }
                }

                EstadoActividad = _conectorBD.ListaEstadoActividad();
                if (EstadoActividad.Rows.Count!=0)
                {
                    foreach (DataRow item in EstadoActividad.Rows)
                    {
                        EstadoActividadCB data = new EstadoActividadCB();
                        data.ID_ESTADO_ACTIVIDAD = item.Field<int>("ID_ESTADO_ACTIVIDAD");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaEstadoActividad.Add(data);
                    }
                }

                SectorEconomico = _conectorBD.ListaSectorEconomico();
                if (SectorEconomico.Rows.Count!=0)
                {
                    foreach (DataRow item in SectorEconomico.Rows)
                    {
                        SectorEconomicoCB data = new SectorEconomicoCB();
                        data.ID_SECTOR_ECONOMICO = item.Field<int>("ID_SECTOR_ECONOMICO");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaSectorEconomico.Add(data);
                    }
                }


                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboReferencia>  ComboReferencia()
        {
            ComboReferencia response = new ComboReferencia();
            response.ListaRelacion = new List<RelacionCB>();
            response.ListaCalificacion = new List<CalificacionCB>();

            DataTable Relacion = new DataTable();
            DataTable Calificacion = new DataTable();



            try
            {
                Relacion = _conectorBD.ListaRelacion();
                if (Relacion.Rows.Count!=0)
                {
                    foreach (DataRow item in Relacion.Rows)
                    {
                        RelacionCB data = new RelacionCB();
                        data.ID_RELACION = item.Field<int>("ID_RELACION");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaRelacion.Add(data);
                    }
                }

                Calificacion = _conectorBD.ListaCalificacion();
                if (Calificacion.Rows.Count!=0)
                {
                    foreach (DataRow item in Calificacion.Rows)
                    {
                        CalificacionCB data = new CalificacionCB();
                        data.ID_CALIFICACION = item.Field<int>("ID_CALIFICACION");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaCalificacion.Add(data);
                    }
                }

                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboTelefono>   ComboTelefono()
        {
            ComboTelefono response = new ComboTelefono();
            response.ListaTipoTelefono = new List<TipoTelefonoCB>();

            DataTable TipoTelefono = new DataTable();



            try
            {
                TipoTelefono = _conectorBD.ListaTipoTelefono();
                if (TipoTelefono.Rows.Count!=0)
                {
                    foreach (DataRow item in TipoTelefono.Rows)
                    {
                        TipoTelefonoCB data = new TipoTelefonoCB();
                        data.ID_TIPO_TELEFONO = item.Field<int>("ID_TIPO_TELEFONO");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaTipoTelefono.Add(data);
                    }
                }

                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboCaedec>   ComboCaedec()
        {
            ComboCaedec response = new ComboCaedec();
            response.ListaCaedec = new List<CaedecCB>();

            DataTable Caedec = new DataTable();



            try
            {
                Caedec = _conectorBD.ListaCaedec();
                if (Caedec.Rows.Count!=0)
                {
                    foreach (DataRow item in Caedec.Rows)
                    {
                        CaedecCB data = new CaedecCB();
                        data.ID_ACTIVIDAD_CAEDEC = item.Field<int>("ID_ACTIVIDAD_CAEDEC");
                        data.COD_CAEDEC = String.IsNullOrEmpty(item.Field<string>("COD_CAEDEC"))?"": item.Field<string>("COD_CAEDEC").ToString().Trim();
                        data.ACTIVIDAD_CAEDEC = String.IsNullOrEmpty(item.Field<string>("ACTIVIDAD_CAEDEC"))?"": item.Field<string>("ACTIVIDAD_CAEDEC").ToString().Trim();
                        data.COD_GRUPO_CAEDEC = String.IsNullOrEmpty(item.Field<string>("COD_GRUPO_CAEDEC"))?"": item.Field<string>("COD_GRUPO_CAEDEC").ToString().Trim();
                        data.GRUPO_CAEDEC = String.IsNullOrEmpty(item.Field<string>("GRUPO_CAEDEC"))?"": item.Field<string>("GRUPO_CAEDEC").ToString().Trim();
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaCaedec.Add(data);
                    }
                }

                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboAutorizacionesEspeciales>   ComboAutorizacionesEspeciales()
        {
            ComboAutorizacionesEspeciales response = new ComboAutorizacionesEspeciales();
            response.ListaAutorizacionesEspeciales = new List<AutorizacionesEspecialesCB>();

            DataTable AutorizacionesEspeciales = new DataTable();



            try
            {
                AutorizacionesEspeciales = _conectorBD.ListaAutorizacionesEspeciales();
                if (AutorizacionesEspeciales.Rows.Count!=0)
                {
                    foreach (DataRow item in AutorizacionesEspeciales.Rows)
                    {
                        AutorizacionesEspecialesCB data = new AutorizacionesEspecialesCB();
                        data.ID_AUTORIZACIONES_ESPECIALES = item.Field<int>("ID_AUTORIZACIONES_ESPECIALES");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaAutorizacionesEspeciales.Add(data);
                    }
                }

                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboTipoFechaAM>   ComboTipoFechaAM()
        {
            ComboTipoFechaAM response = new ComboTipoFechaAM();
            response.ListaTipoFechaAM = new List<TipoFechaAMCB>();

            DataTable TipoFechaAM = new DataTable();



            try
            {
                TipoFechaAM = _conectorBD.ListaTipoFechaAM();
                if (TipoFechaAM.Rows.Count!=0)
                {
                    foreach (DataRow item in TipoFechaAM.Rows)
                    {
                        TipoFechaAMCB data = new TipoFechaAMCB();
                        data.ID_TIPO_FECHA_A_M = item.Field<int>("ID_TIPO_FECHA_A_M");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaTipoFechaAM.Add(data);
                    }
                }

                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboDocumentosEntregados>   ComboDocumentosEntregados()
        {
            ComboDocumentosEntregados response = new ComboDocumentosEntregados();
            response.ListaDocumentosEntregados = new List<DocumentosEntregadosCB>();

            DataTable DocumentosEntregados = new DataTable();



            try
            {
                DocumentosEntregados = _conectorBD.ListaDocumentosEntregados();
                if (DocumentosEntregados.Rows.Count!=0)
                {
                    foreach (DataRow item in DocumentosEntregados.Rows)
                    {
                        DocumentosEntregadosCB data = new DocumentosEntregadosCB();
                        data.ID_DOCUMENTOS_ENTREGADOS = item.Field<int>("ID_DOCUMENTOS_ENTREGADOS");
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaDocumentosEntregados.Add(data);
                    }
                }

                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboScoring>   ComboScoring()
        {
            ComboScoring response = new ComboScoring();
            response.ListaCriterio = new List<CriterioCB>();

            DataTable Criterio = new DataTable();
            try
            {
                Criterio = _conectorBD.ListaCriterio();
                if (Criterio.Rows.Count!=0)
                {
                    foreach (DataRow item in Criterio.Rows)
                    {
                        CriterioCB data = new CriterioCB();
                        data.ID_CRITERIO = item.Field<int>("ID_CRITERIO");
                        data.AREA = String.IsNullOrEmpty(item.Field<string>("AREA"))?"": item.Field<string>("AREA").ToString().Trim();
                        data.ID_AREA = String.IsNullOrEmpty(item.Field<string>("ID_AREA"))?"": item.Field<string>("ID_AREA").ToString().Trim();
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaCriterio.Add(data);
                    }
                }

                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public async Task<ComboCaedec>   BusquedaCaedec(BusquedaCaedecRequest request)
        {
            ComboCaedec response = new ComboCaedec();
            response.ListaCaedec = new List<CaedecCB>();

            DataTable Caedec = new DataTable();



            try
            {
                Caedec = _conectorBD.BusquedaCaedec(request);
                if (Caedec.Rows.Count!=0)
                {
                    foreach (DataRow item in Caedec.Rows)
                    {
                        CaedecCB data = new CaedecCB();
                        data.ID_ACTIVIDAD_CAEDEC = item.Field<int>("ID_ACTIVIDAD_CAEDEC");
                        data.COD_CAEDEC = String.IsNullOrEmpty(item.Field<string>("COD_CAEDEC"))?"":item.Field<string>("COD_CAEDEC").ToString().Trim();
                        data.ACTIVIDAD_CAEDEC = String.IsNullOrEmpty(item.Field<string>("ACTIVIDAD_CAEDEC"))?"": item.Field<string>("ACTIVIDAD_CAEDEC").ToString().Trim();
                        data.COD_GRUPO_CAEDEC = String.IsNullOrEmpty(item.Field<string>("COD_GRUPO_CAEDEC"))?"": item.Field<string>("COD_GRUPO_CAEDEC").ToString().Trim();
                        data.GRUPO_CAEDEC = String.IsNullOrEmpty(item.Field<string>("GRUPO_CAEDEC"))?"": item.Field<string>("GRUPO_CAEDEC").ToString().Trim();
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION"))?"": item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaCaedec.Add(data);
                    }
                }

                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        public int BuscarPersonaCliente(string NRO_DOCUMENTO, string COMPLEMENTO, string EXTENSION)
        {
            int resultado = 0;
            DataTable data = new DataTable();
            try
            {
                data = _conectorBD.BuscarPersonaCliente(NRO_DOCUMENTO, COMPLEMENTO, EXTENSION);
                if (data.Rows.Count!=0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        resultado = item.Field<int>("ID_CLIENTE");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public async Task<ComboEntrevista>  ComboEntrevista()
        {
            ComboEntrevista response = new ComboEntrevista();
            response.ListaVerificacionCliente = new List<VerificacionClienteCB>();
            response.ListaScriptEvaluacion = new List<ScriptEvaluacionCB>();
            response.ListaInstitucion = new List<InstitucionCB>();
            response.ListaSeguroDesgravame = new List<SeguroDesgravameCB>();

            DataTable VerificacionCliente = new DataTable();
            DataTable ScriptEvaluacion = new DataTable();
            DataTable Institucion = new DataTable();
            DataTable SeguroDesgravame = new DataTable();


            try
            {
                VerificacionCliente = _conectorBD.ListaVerificacionCliente();
                if (VerificacionCliente.Rows.Count!=0)
                {
                    foreach (DataRow item in VerificacionCliente.Rows)
                    {
                        VerificacionClienteCB data = new VerificacionClienteCB();
                        data.ID_VERIFICACION_CLIENTE = item.Field<int>("ID_VERIFICACION_CLIENTE");
                        data.CODIGO_VERIFICACION_CLIENTE = String.IsNullOrEmpty(item.Field<string>("CODIGO_VERIFICACION_CLIENTE"))?"": item.Field<string>("CODIGO_VERIFICACION_CLIENTE").ToString().Trim();
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaVerificacionCliente.Add(data);
                    }
                }

                ScriptEvaluacion = _conectorBD.ListaScriptEvaluacion();
                if (ScriptEvaluacion.Rows.Count!=0)
                {
                    foreach (DataRow item in ScriptEvaluacion.Rows)
                    {
                        ScriptEvaluacionCB data = new ScriptEvaluacionCB();
                        data.ID_SCRIPT_EVALUACION = item.Field<int>("ID_SCRIPT_EVALUACION");
                        data.CODIGO_SCRIPT_EVALUACION = String.IsNullOrEmpty(item.Field<string>("CODIGO_SCRIPT_EVALUACION")) ? "" : item.Field<string>("CODIGO_SCRIPT_EVALUACION").ToString().Trim();
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();
                        response.ListaScriptEvaluacion.Add(data);
                    }
                }

                Institucion = _conectorBD.ListaInstitucion();
                if (Institucion.Rows.Count!=0)
                {
                    foreach (DataRow item in Institucion.Rows)
                    {
                        InstitucionCB data = new InstitucionCB();
                        data.ID_INSTITUCION = item.Field<int>("ID_INSTITUCION");
                        data.CODIGO_INSTITUCION = String.IsNullOrEmpty(item.Field<string>("CODIGO_INSTITUCION")) ? "" : item.Field<string>("CODIGO_INSTITUCION").ToString().Trim();
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaInstitucion.Add(data);
                    }
                }

                SeguroDesgravame = _conectorBD.ListaSeguroDesgravame();
                if (SeguroDesgravame.Rows.Count!=0)
                {
                    foreach (DataRow item in SeguroDesgravame.Rows)
                    {
                        SeguroDesgravameCB data = new SeguroDesgravameCB();
                        data.ID_SEGURO_DESGRAVAME = item.Field<int>("ID_SEGURO_DESGRAVAME");
                        data.CODIGO_SEGURO_DESGRAVAME = String.IsNullOrEmpty(item.Field<string>("CODIGO_SEGURO_DESGRAVAME")) ? "" : item.Field<string>("CODIGO_SEGURO_DESGRAVAME").ToString().Trim();
                        data.DESCRIPCION = String.IsNullOrEmpty(item.Field<string>("DESCRIPCION")) ? "" : item.Field<string>("DESCRIPCION").ToString().Trim();

                        response.ListaSeguroDesgravame.Add(data);
                    }
                }

                response.code = Configuraciones.GetCode("OK");
                response.message = Configuraciones.GetMessage("OK");
                response.success = true;

            }
            catch (Exception ex)
            {
                response.code = Configuraciones.GetCode("ERROR_FATAL");
                response.message = ex.Message;
                response.success = true;
            }
            return response;
        }
        #endregion

        #region OTROS

        public CLIENTEROBOT CompararCliente(CLIENTE A, CLIENTE B)
        {
            CLIENTEROBOT respuesta = new CLIENTEROBOT();
            try
            {
                if (A.ID_CLIENTE == B.ID_CLIENTE)
                {
                    respuesta.ID_CLIENTE = A.ID_CLIENTE;
                    respuesta.ESTADO_ANA = "";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public PERSONAROBOT CompararPersona(PERSONA A,PERSONA B)
        {
            PERSONAROBOT PERSONA = new PERSONAROBOT();
            try
            {
                if (A.ID_PERSONA == B.ID_PERSONA)
                {
                    if ((string.IsNullOrEmpty(A.GENERO)?"": A.GENERO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.GENERO)?"": B.GENERO.ToUpper().Trim()))
                        PERSONA.GENERO = string.IsNullOrEmpty(A.GENERO) ? "" : A.GENERO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.PRIMER_APELLIDO)?"": A.PRIMER_APELLIDO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.PRIMER_APELLIDO)?"": B.PRIMER_APELLIDO.ToUpper().Trim()))
                        PERSONA.PRIMER_APELLIDO = string.IsNullOrEmpty(A.PRIMER_APELLIDO) ? "" : A.PRIMER_APELLIDO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.SEGUNDO_APELLIDO) ? "" : A.SEGUNDO_APELLIDO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.SEGUNDO_APELLIDO)?"": B.SEGUNDO_APELLIDO.ToUpper().Trim()))
                        PERSONA.SEGUNDO_APELLIDO = string.IsNullOrEmpty(A.SEGUNDO_APELLIDO) ? "" : A.SEGUNDO_APELLIDO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.NOMBRES)?"": A.NOMBRES.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NOMBRES)?"": B.NOMBRES.ToUpper().Trim()))
                        PERSONA.NOMBRES = string.IsNullOrEmpty(A.NOMBRES) ? "" : A.NOMBRES.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.CASADA_APELLIDO)?"": A.CASADA_APELLIDO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.CASADA_APELLIDO)?"": B.CASADA_APELLIDO.ToUpper().Trim()))
                        PERSONA.CASADA_APELLIDO = string.IsNullOrEmpty(A.CASADA_APELLIDO) ? "" : A.CASADA_APELLIDO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.NOMBRE_COMPLETO)?"": A.NOMBRE_COMPLETO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NOMBRE_COMPLETO)?"": B.NOMBRE_COMPLETO.ToUpper().Trim()))
                        PERSONA.NOMBRE_COMPLETO = string.IsNullOrEmpty(A.NOMBRE_COMPLETO) ? "" : A.NOMBRE_COMPLETO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.CORREO)?"": A.CORREO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.CORREO)?"": B.CORREO.ToUpper().Trim()))
                        PERSONA.CORREO = string.IsNullOrEmpty(A.CORREO) ? "" : A.CORREO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.NIVEL_EDUCACION)?"": A.NIVEL_EDUCACION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NIVEL_EDUCACION)?"": B.NIVEL_EDUCACION.ToUpper().Trim()))
                        PERSONA.NIVEL_EDUCACION = string.IsNullOrEmpty(A.NIVEL_EDUCACION) ? "" : A.NIVEL_EDUCACION.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.PROFESION)?"": A.PROFESION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.PROFESION)?"": B.PROFESION.ToUpper().Trim()))
                        PERSONA.PROFESION = string.IsNullOrEmpty(A.PROFESION) ? "" : A.PROFESION.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.ESTADO_CIVIL)?"": A.ESTADO_CIVIL.ToUpper().Trim()) != (string.IsNullOrEmpty(B.ESTADO_CIVIL)?"": B.ESTADO_CIVIL.ToUpper().Trim()))
                        PERSONA.ESTADO_CIVIL = string.IsNullOrEmpty(A.ESTADO_CIVIL) ? "" : A.ESTADO_CIVIL.ToUpper().Trim();
                    PERSONA.COD_ANA = A.COD_ANA;
                    PERSONA.ID_PERSONA = A.ID_PERSONA;
                    

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return PERSONA;
        }
        public DIRECCIONROBOT CompararDireccion(DIRECCION A, DIRECCION B)
        {
            DIRECCIONROBOT respuesta = new DIRECCIONROBOT();
            try
            {
                if (A.ID_DIRECCION == B.ID_DIRECCION)
                {
                    if ((string.IsNullOrEmpty(A.PAIS_RESIDENCIA)?"": A.PAIS_RESIDENCIA.ToUpper().Trim()) != (string.IsNullOrEmpty(B.PAIS_RESIDENCIA)?"": B.PAIS_RESIDENCIA.ToUpper().Trim()))
                        respuesta.PAIS_RESIDENCIA = string.IsNullOrEmpty(A.PAIS_RESIDENCIA) ? "" : A.PAIS_RESIDENCIA.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.CIUDAD_RESIDENCIA)?"": A.CIUDAD_RESIDENCIA.ToUpper().Trim()) != (string.IsNullOrEmpty(B.CIUDAD_RESIDENCIA)?"": B.CIUDAD_RESIDENCIA.ToUpper().Trim()))
                        respuesta.CIUDAD_RESIDENCIA = string.IsNullOrEmpty(A.CIUDAD_RESIDENCIA) ? "" : A.CIUDAD_RESIDENCIA.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.TIPO_DIRECCION)?"": A.TIPO_DIRECCION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TIPO_DIRECCION)?"": B.TIPO_DIRECCION.ToUpper().Trim()))
                        respuesta.TIPO_DIRECCION = string.IsNullOrEmpty(A.TIPO_DIRECCION) ? "" : A.TIPO_DIRECCION.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.DEPARTAMENTO)?"": A.DEPARTAMENTO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.DEPARTAMENTO)?"": B.DEPARTAMENTO.ToUpper().Trim()))
                        respuesta.DEPARTAMENTO = string.IsNullOrEmpty(A.DEPARTAMENTO) ? "" : A.DEPARTAMENTO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.LOCALIDAD)?"": A.LOCALIDAD.ToUpper().Trim()) != (string.IsNullOrEmpty(B.LOCALIDAD)?"": B.LOCALIDAD.ToUpper().Trim()))
                        respuesta.LOCALIDAD = string.IsNullOrEmpty(A.LOCALIDAD) ? "" : A.LOCALIDAD.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.ZONA_BARRIO)?"": A.ZONA_BARRIO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.ZONA_BARRIO)?"": B.ZONA_BARRIO.ToUpper().Trim()))
                        respuesta.ZONA_BARRIO = string.IsNullOrEmpty(A.ZONA_BARRIO) ? "" : A.ZONA_BARRIO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.CALLE_AVENIDA)?"": A.CALLE_AVENIDA.ToUpper().Trim()) != (string.IsNullOrEmpty(B.CALLE_AVENIDA)?"": B.CALLE_AVENIDA.ToUpper().Trim()))
                        respuesta.CALLE_AVENIDA = string.IsNullOrEmpty(A.CALLE_AVENIDA) ? "" : A.CALLE_AVENIDA.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.NRO_PUERTA)?"": A.NRO_PUERTA.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NRO_PUERTA)?"": B.NRO_PUERTA.ToUpper().Trim()))
                        respuesta.NRO_PUERTA = string.IsNullOrEmpty(A.NRO_PUERTA) ? "" : A.NRO_PUERTA.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.NRO_PISO)?"": A.NRO_PISO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NRO_PISO)?"": B.NRO_PISO.ToUpper().Trim()))
                        respuesta.NRO_PISO = string.IsNullOrEmpty(A.NRO_PISO) ? "" : A.NRO_PISO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.NRO_DEPARTAMENTO)?"": A.NRO_DEPARTAMENTO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NRO_DEPARTAMENTO)?"": B.NRO_DEPARTAMENTO.ToUpper().Trim()))
                        respuesta.NRO_DEPARTAMENTO = string.IsNullOrEmpty(A.NRO_DEPARTAMENTO) ? "" : A.NRO_DEPARTAMENTO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.NOMBRE_EDIFICIO)?"": A.NOMBRE_EDIFICIO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NOMBRE_EDIFICIO)?"": B.NOMBRE_EDIFICIO.ToUpper().Trim()))
                        respuesta.NOMBRE_EDIFICIO = string.IsNullOrEmpty(A.NOMBRE_EDIFICIO) ? "" : A.NOMBRE_EDIFICIO.ToUpper().Trim();
                    if (A.NRO_CASILLA != B.NRO_CASILLA)
                        respuesta.NRO_CASILLA = A.NRO_CASILLA.ToString();
                    if ((string.IsNullOrEmpty(A.TIPO_VIVIENDA_OFICINA)?"": A.TIPO_VIVIENDA_OFICINA.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TIPO_VIVIENDA_OFICINA)?"": B.TIPO_VIVIENDA_OFICINA.ToUpper().Trim()))
                        respuesta.TIPO_VIVIENDA_OFICINA = string.IsNullOrEmpty(A.TIPO_VIVIENDA_OFICINA) ? "" : A.TIPO_VIVIENDA_OFICINA.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.REFERENCIA)?"": A.REFERENCIA.ToUpper().Trim()) != (string.IsNullOrEmpty(B.REFERENCIA)?"": B.REFERENCIA.ToUpper().Trim()))
                        respuesta.REFERENCIA = string.IsNullOrEmpty(A.REFERENCIA) ? "" : A.REFERENCIA.ToUpper().Trim();
                    respuesta.ID_DIRECCION = A.ID_DIRECCION;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public TELEFONOROBOT Comparartelefono(TELEFONO A, TELEFONO B)
        {
            TELEFONOROBOT respuesta = new TELEFONOROBOT();
            try
            {
                if (A.ID_TELEFONO == B.ID_TELEFONO)
                {
                    if ((string.IsNullOrEmpty(A.TIPO)?"": A.TIPO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TIPO)?"": B.TIPO.ToUpper().Trim()))
                        respuesta.TIPO = string.IsNullOrEmpty(A.TIPO) ? "" : A.TIPO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.NUMERO)?"": A.NUMERO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NUMERO)?"": B.NUMERO.ToUpper().Trim()))
                        respuesta.NUMERO = string.IsNullOrEmpty(A.NUMERO) ? "" : A.NUMERO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.DESCRIPCION)?"": A.DESCRIPCION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.DESCRIPCION)?"": B.DESCRIPCION.ToUpper().Trim()))
                        respuesta.DESCRIPCION = string.IsNullOrEmpty(A.DESCRIPCION) ? "" : A.DESCRIPCION.ToUpper().Trim();
                    respuesta.ID_TELEFONO = A.ID_TELEFONO;                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public CREDITOROBOT CompararCredito(CREDITO A, CREDITO B)
        {
            CREDITOROBOT respuesta = new CREDITOROBOT();
            try
            {
                if (A.ID_CREDITO == B.ID_CREDITO)
                {
                    if (A.MONTO_CREDITO != B.MONTO_CREDITO)
                        respuesta.MONTO_CREDITO = A.MONTO_CREDITO.ToString();
                    if ((string.IsNullOrEmpty(A.TIPO_OPERACION)?"": A.TIPO_OPERACION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TIPO_OPERACION)?"": B.TIPO_OPERACION.ToUpper().Trim()))
                        respuesta.TIPO_OPERACION = string.IsNullOrEmpty(A.TIPO_OPERACION) ? "" : A.TIPO_OPERACION.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.DESTINO)?"": A.DESTINO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.DESTINO)?"": B.DESTINO.ToUpper().Trim()))
                        respuesta.DESTINO = string.IsNullOrEmpty(A.DESTINO) ? "" : A.DESTINO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.DESTINO_CLIENTE)?"": A.DESTINO_CLIENTE.ToUpper().Trim()) != (string.IsNullOrEmpty(B.DESTINO_CLIENTE)?"": B.DESTINO_CLIENTE.ToUpper().Trim()))
                        respuesta.DESTINO_CLIENTE = string.IsNullOrEmpty(A.DESTINO_CLIENTE) ? "" : A.DESTINO_CLIENTE.ToUpper().Trim();
                    if (A.COMPRA_PASIVO != B.COMPRA_PASIVO)
                        respuesta.COMPRA_PASIVO = A.COMPRA_PASIVO.ToString();
                    if ((string.IsNullOrEmpty(A.OBJETO_OPERACION)?"": A.OBJETO_OPERACION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.OBJETO_OPERACION) ? "" : B.OBJETO_OPERACION.ToUpper().Trim()))
                        respuesta.OBJETO_OPERACION = string.IsNullOrEmpty(A.OBJETO_OPERACION) ? "" : A.OBJETO_OPERACION.ToUpper().Trim();
                    if (A.PLAZO != B.PLAZO)
                        respuesta.PLAZO = A.PLAZO.ToString();
                    if (A.DIA_PAGO != B.DIA_PAGO)
                        respuesta.DIA_PAGO = A.DIA_PAGO.ToString();
                    if ((string.IsNullOrEmpty(A.ANTIGUEDAD_CLIENTE)?"": A.ANTIGUEDAD_CLIENTE.ToUpper().Trim()) != (string.IsNullOrEmpty(B.ANTIGUEDAD_CLIENTE)?"": B.ANTIGUEDAD_CLIENTE.ToUpper().Trim()))
                        respuesta.ANTIGUEDAD_CLIENTE = string.IsNullOrEmpty(A.ANTIGUEDAD_CLIENTE) ? "" : A.ANTIGUEDAD_CLIENTE.ToUpper().Trim();
                    if (A.FECHA_SOLICITUD != B.FECHA_SOLICITUD)
                        respuesta.FECHA_SOLICITUD = A.FECHA_SOLICITUD.ToString();
                    if ((string.IsNullOrEmpty(A.CLIENTE_CPOP)?"": A.CLIENTE_CPOP.ToUpper().Trim()) != (string.IsNullOrEmpty(B.CLIENTE_CPOP)?"": B.CLIENTE_CPOP.ToUpper().Trim()))
                        respuesta.CLIENTE_CPOP = string.IsNullOrEmpty(A.CLIENTE_CPOP) ? "" : A.CLIENTE_CPOP.ToUpper().Trim();
                    if (A.NRO_CPOP != B.NRO_CPOP)
                        respuesta.NRO_CPOP = A.NRO_CPOP.ToString();
                    if ((string.IsNullOrEmpty(A.TIPO_TASA)?"": A.TIPO_TASA.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TIPO_TASA.ToUpper().Trim())?"": B.TIPO_TASA.ToUpper().Trim()))
                        respuesta.TIPO_TASA = string.IsNullOrEmpty(A.TIPO_TASA) ? "" : A.TIPO_TASA.ToUpper().Trim();
                    if (A.NRO_AUTORIZACION != B.NRO_AUTORIZACION)
                        respuesta.NRO_AUTORIZACION = A.NRO_AUTORIZACION.ToString();
                    if ((string.IsNullOrEmpty(A.TASA_SELECCION)?"": A.TASA_SELECCION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TASA_SELECCION)?"": B.TASA_SELECCION.ToUpper().Trim()))
                        respuesta.TASA_SELECCION = string.IsNullOrEmpty(A.TASA_SELECCION) ? "" : A.TASA_SELECCION.ToUpper().Trim();
                    if (A.TASA_PP != B.TASA_PP)
                        respuesta.TASA_PP = A.TASA_PP.ToString();
                    if ((string.IsNullOrEmpty(A.ESTADO_CREDITO)?"": A.ESTADO_CREDITO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.ESTADO_CREDITO)?"": B.ESTADO_CREDITO.ToUpper().Trim()))
                        respuesta.ESTADO_CREDITO = string.IsNullOrEmpty(A.ESTADO_CREDITO) ? "" : A.ESTADO_CREDITO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.ESTADO_SCI)?"": A.ESTADO_SCI.ToUpper().Trim()) != (string.IsNullOrEmpty(B.ESTADO_SCI)?"": B.ESTADO_SCI.ToUpper().Trim()))
                        respuesta.ESTADO_SCI = string.IsNullOrEmpty(A.ESTADO_SCI) ? "" : A.ESTADO_SCI.ToUpper().Trim();
                    respuesta.COD_CREDITO = "";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public DOCUMENTO_PERSONALROBOT CompararDocumentoPersonal(DOCUMENTO_PERSONAL A, DOCUMENTO_PERSONAL B)
        {
            DOCUMENTO_PERSONALROBOT respuesta = new DOCUMENTO_PERSONALROBOT();
            try
            {
                if (A.ID_DOCUMENTO_PERSONAL == B.ID_DOCUMENTO_PERSONAL)
                {
                    if ((string.IsNullOrEmpty(A.TIPO_DOCUMENTO)?"": A.TIPO_DOCUMENTO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TIPO_DOCUMENTO) ? "" : B.TIPO_DOCUMENTO.ToUpper().Trim()))
                        respuesta.TIPO_DOCUMENTO = string.IsNullOrEmpty(A.TIPO_DOCUMENTO) ? "" : A.TIPO_DOCUMENTO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.NRO_DOCUMENTO) ? "" : A.NRO_DOCUMENTO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NRO_DOCUMENTO) ? "" : B.NRO_DOCUMENTO.ToUpper().Trim()))
                        respuesta.NRO_DOCUMENTO = string.IsNullOrEmpty(A.NRO_DOCUMENTO) ? "" : A.NRO_DOCUMENTO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.COMPLEMENTO) ? "" : A.COMPLEMENTO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.COMPLEMENTO) ? "" : B.COMPLEMENTO.ToUpper().Trim()))
                        respuesta.COMPLEMENTO = string.IsNullOrEmpty(A.COMPLEMENTO) ? "" : A.COMPLEMENTO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.EXTENSION) ? "" : A.EXTENSION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.EXTENSION) ? "" : B.EXTENSION.ToUpper().Trim()))
                        respuesta.EXTENSION = string.IsNullOrEmpty(A.EXTENSION) ? "" : A.EXTENSION.ToUpper().Trim();
                    if (A.FECHA_VENC != B.FECHA_VENC)
                        respuesta.FECHA_VENC = A.FECHA_VENC.ToString();
                    if ((string.IsNullOrEmpty(A.ESTADO_CIVIL) ? "" : A.ESTADO_CIVIL.ToUpper().Trim()) != (string.IsNullOrEmpty(B.ESTADO_CIVIL) ? "" : B.ESTADO_CIVIL.ToUpper().Trim()))
                        respuesta.ESTADO_CIVIL = string.IsNullOrEmpty(A.ESTADO_CIVIL) ? "" : A.ESTADO_CIVIL.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.NACIONALIDAD)?"": A.NACIONALIDAD.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NACIONALIDAD)?"": B.NACIONALIDAD.ToUpper().Trim()))
                        respuesta.NACIONALIDAD = string.IsNullOrEmpty(A.NACIONALIDAD) ? "" : A.NACIONALIDAD.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.LUGAR_NACIMIENTO)?"": A.LUGAR_NACIMIENTO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.LUGAR_NACIMIENTO) ? "" : B.LUGAR_NACIMIENTO.ToUpper().Trim()))
                        respuesta.LUGAR_NACIMIENTO = string.IsNullOrEmpty(A.LUGAR_NACIMIENTO) ? "" : A.LUGAR_NACIMIENTO.ToUpper().Trim();
                    if (A.FECHA_NACIMIENTO != B.FECHA_NACIMIENTO)
                        respuesta.FECHA_NACIMIENTO = A.FECHA_NACIMIENTO.ToString();
                    respuesta.ID_DOCUMENTO_PERSONAL = A.ID_DOCUMENTO_PERSONAL;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public ACTIVIDAD_ECONOMICAROBOT CompararActividadEconomica(ACTIVIDAD_ECONOMICA A, ACTIVIDAD_ECONOMICA B)
        {
            ACTIVIDAD_ECONOMICAROBOT respuesta = new ACTIVIDAD_ECONOMICAROBOT();
            try
            {
                if (A.ID_ACTIVIDAD_ECONOMICA == B.ID_ACTIVIDAD_ECONOMICA)
                {
                    if ((string.IsNullOrEmpty(A.NIVEL_LABORAL) ? "" : A.NIVEL_LABORAL.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NIVEL_LABORAL) ? "" : B.NIVEL_LABORAL.ToUpper().Trim()))
                        respuesta.NIVEL_LABORAL = string.IsNullOrEmpty(A.NIVEL_LABORAL) ? "" : A.NIVEL_LABORAL.ToUpper().Trim();
                    if (A.NIT != B.NIT)
                        respuesta.NIT = A.NIT.ToString();
                    if ((string.IsNullOrEmpty(A.ACTIVIDAD_DECLARADA) ? "" : A.ACTIVIDAD_DECLARADA.ToUpper().Trim()) != (string.IsNullOrEmpty(B.ACTIVIDAD_DECLARADA) ? "" : B.ACTIVIDAD_DECLARADA.ToUpper().Trim()))
                        respuesta.ACTIVIDAD_DECLARADA = string.IsNullOrEmpty(A.ACTIVIDAD_DECLARADA) ? "" : A.ACTIVIDAD_DECLARADA.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.PRIORIDAD) ? "" : A.PRIORIDAD.ToUpper().Trim()) != (string.IsNullOrEmpty(B.PRIORIDAD) ? "" : B.PRIORIDAD.ToUpper().Trim()))
                        respuesta.PRIORIDAD = string.IsNullOrEmpty(A.PRIORIDAD) ? "" : A.PRIORIDAD.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.ESTADO) ? "" : A.ESTADO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.ESTADO) ? "" : B.ESTADO.ToUpper().Trim()))
                        respuesta.ESTADO = string.IsNullOrEmpty(A.ESTADO) ? "" : A.ESTADO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.INGRESOS_MENSUALES) ? "" : A.INGRESOS_MENSUALES.ToUpper().Trim()) != (string.IsNullOrEmpty(B.INGRESOS_MENSUALES) ? "" : B.INGRESOS_MENSUALES.ToUpper().Trim()))
                        respuesta.INGRESOS_MENSUALES = string.IsNullOrEmpty(A.INGRESOS_MENSUALES) ? "" : A.INGRESOS_MENSUALES.ToUpper().Trim();
                    if (A.OTROS_INGRESOS_MENSUALES != B.OTROS_INGRESOS_MENSUALES)
                        respuesta.OTROS_INGRESOS_MENSUALES = A.OTROS_INGRESOS_MENSUALES.ToString();
                    if (A.EGRESOS_MENSUALES != B.EGRESOS_MENSUALES)
                        respuesta.EGRESOS_MENSUALES = A.EGRESOS_MENSUALES.ToString();
                    if (A.MARGEN_AHORRO != B.MARGEN_AHORRO)
                        respuesta.MARGEN_AHORRO = A.MARGEN_AHORRO.ToString();
                    if ((string.IsNullOrEmpty(A.NOMBRE_EMPRESA) ? "" : A.NOMBRE_EMPRESA.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NOMBRE_EMPRESA) ? "" : B.NOMBRE_EMPRESA.ToUpper().Trim()))
                        respuesta.NOMBRE_EMPRESA = string.IsNullOrEmpty(A.NOMBRE_EMPRESA) ? "" : A.NOMBRE_EMPRESA.ToUpper().Trim();
                    if (A.FECHA_INGRESO != B.FECHA_INGRESO)
                        respuesta.FECHA_INGRESO = A.FECHA_INGRESO.ToString();
                    if ((string.IsNullOrEmpty(A.CARGO) ? "" : A.CARGO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.CARGO) ? "" : B.CARGO.ToUpper().Trim()))
                        respuesta.CARGO = string.IsNullOrEmpty(A.CARGO) ? "" : A.CARGO.ToUpper().Trim();
                    respuesta.ID_ACTIVIDAD_ECONOMICA = A.ID_ACTIVIDAD_ECONOMICA;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public REFERENCIAROBOT CompararReferencia(REFERENCIA A, REFERENCIA B)
        {
            REFERENCIAROBOT respuesta = new REFERENCIAROBOT();
            try
            {
                if (A.ID_REFERENCIA == B.ID_REFERENCIA)
                {
                    if ((string.IsNullOrEmpty(A.TIPO) ? "" : A.TIPO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TIPO) ? "" : B.TIPO.ToUpper().Trim()))
                        respuesta.TIPO = string.IsNullOrEmpty(A.TIPO) ? "" : A.TIPO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.RELACION) ? "" : A.RELACION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.RELACION) ? "" : B.RELACION.ToUpper().Trim()))
                        respuesta.RELACION = string.IsNullOrEmpty(A.RELACION) ? "" : A.RELACION.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.OBSERVACION) ? "" : A.OBSERVACION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.OBSERVACION) ? "" : B.OBSERVACION.ToUpper().Trim()))
                        respuesta.OBSERVACION = string.IsNullOrEmpty(A.OBSERVACION) ? "" : A.OBSERVACION.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.CALIFICACION) ? "" : A.CALIFICACION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.CALIFICACION) ? "" : B.CALIFICACION.ToUpper().Trim()))
                        respuesta.CALIFICACION = string.IsNullOrEmpty(A.CALIFICACION) ? "" : A.CALIFICACION.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.DESCRIPCION_CALIFICACION) ? "" : A.DESCRIPCION_CALIFICACION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.DESCRIPCION_CALIFICACION) ? "" : B.DESCRIPCION_CALIFICACION.ToUpper().Trim()))
                        respuesta.DESCRIPCION_CALIFICACION = string.IsNullOrEmpty(A.DESCRIPCION_CALIFICACION) ? "" : A.DESCRIPCION_CALIFICACION.ToUpper().Trim();
                    respuesta.ID_REFERENCIA = A.ID_REFERENCIA;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public CAEDECROBOT CompararCaedec(CAEDEC A, CAEDEC B)
        {
            CAEDECROBOT respuesta = new CAEDECROBOT();
            try
            {
                if (A.ID_CAEDEC == B.ID_CAEDEC)
                {
                    if ((string.IsNullOrEmpty(A.COD_CAEDEC) ? "" : A.COD_CAEDEC.ToUpper().Trim()) != (string.IsNullOrEmpty(B.COD_CAEDEC) ? "" : B.COD_CAEDEC.ToUpper().Trim()))
                        respuesta.COD_CAEDEC = string.IsNullOrEmpty(A.COD_CAEDEC) ? "" : A.COD_CAEDEC.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.ACTIVIDAD_CAEDEC) ? "" : A.ACTIVIDAD_CAEDEC.ToUpper().Trim()) != (string.IsNullOrEmpty(B.ACTIVIDAD_CAEDEC) ? "" : B.ACTIVIDAD_CAEDEC.ToUpper().Trim()))
                        respuesta.ACTIVIDAD_CAEDEC = string.IsNullOrEmpty(A.ACTIVIDAD_CAEDEC) ? "" : A.ACTIVIDAD_CAEDEC.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.GRUPO_CAEDEC) ? "" : A.GRUPO_CAEDEC.ToUpper().Trim()) != (string.IsNullOrEmpty(B.GRUPO_CAEDEC) ? "" : B.GRUPO_CAEDEC.ToUpper().Trim()))
                        respuesta.GRUPO_CAEDEC = string.IsNullOrEmpty(A.GRUPO_CAEDEC) ? "" : A.GRUPO_CAEDEC.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.SECTOR_ECONOMICO) ? "" : A.SECTOR_ECONOMICO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.SECTOR_ECONOMICO) ? "" : B.SECTOR_ECONOMICO.ToUpper().Trim()))
                        respuesta.SECTOR_ECONOMICO = string.IsNullOrEmpty(A.SECTOR_ECONOMICO) ? "" : A.SECTOR_ECONOMICO.ToUpper().Trim();
                    respuesta.ID_CAEDEC = A.ID_CAEDEC;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public DECLARACION_JURADAROBOT CompararDeclaracionJurada(DECLARACION_JURADA A, DECLARACION_JURADA B)
        {
            DECLARACION_JURADAROBOT respuesta = new DECLARACION_JURADAROBOT();
            try
            {
                if (A.ID_DECLARACION_JURADA == B.ID_DECLARACION_JURADA)
                {
                    if ((string.IsNullOrEmpty(A.TIPO) ? "" : A.TIPO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TIPO) ? "" : B.TIPO.ToUpper().Trim()))
                        respuesta.TIPO = string.IsNullOrEmpty(A.TIPO) ? "" : A.TIPO.ToUpper().Trim();
                    if (A.PATRIMONIO_ACTIVO != B.PATRIMONIO_ACTIVO)
                        respuesta.PATRIMONIO_ACTIVO = A.PATRIMONIO_ACTIVO.ToString();
                    if (A.PATRIMONIO_PASIVO != B.PATRIMONIO_PASIVO)
                        respuesta.PATRIMONIO_PASIVO = A.PATRIMONIO_PASIVO.ToString();
                    if (A.PERSONAL_OCUPADO != B.PERSONAL_OCUPADO)
                        respuesta.PERSONAL_OCUPADO = A.PERSONAL_OCUPADO.ToString();
                    if (A.TOTAL_INGRESO_VENTAS != B.TOTAL_INGRESO_VENTAS)
                        respuesta.TOTAL_INGRESO_VENTAS = A.TOTAL_INGRESO_VENTAS.ToString();
                    if ((string.IsNullOrEmpty(A.OBSERVACIONES) ? "" : A.OBSERVACIONES.ToUpper().Trim()) != (string.IsNullOrEmpty(B.OBSERVACIONES) ? "" : B.OBSERVACIONES.ToUpper().Trim()))
                        respuesta.OBSERVACIONES = string.IsNullOrEmpty(A.OBSERVACIONES) ? "" : A.OBSERVACIONES.ToUpper().Trim();
                    respuesta.ID_DECLARACION_JURADA = A.ID_DECLARACION_JURADA;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public DOCUMENTO_ENTREGADOROBOT CompararDocumentoEntregado(DOCUMENTO_ENTREGADO A, DOCUMENTO_ENTREGADO B)
        {
            DOCUMENTO_ENTREGADOROBOT respuesta = new DOCUMENTO_ENTREGADOROBOT();
            try
            {
                if (A.ID_DOCUMENTO_ENTREGADO == B.ID_DOCUMENTO_ENTREGADO)
                {
                    if ((string.IsNullOrEmpty(A.TIPO) ? "" : A.TIPO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TIPO) ? "" : B.TIPO.ToUpper().Trim()))
                        respuesta.TIPO = string.IsNullOrEmpty(A.TIPO) ? "" : A.TIPO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.EXTENSION) ? "" : A.EXTENSION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.EXTENSION) ? "" : B.EXTENSION.ToUpper().Trim()))
                        respuesta.EXTENSION = string.IsNullOrEmpty(A.EXTENSION) ? "" : A.EXTENSION.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.ARCHIVO) ? "" : A.ARCHIVO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.ARCHIVO) ? "" : B.ARCHIVO.ToUpper().Trim()))
                        respuesta.ARCHIVO = string.IsNullOrEmpty(A.ARCHIVO) ? "" : A.ARCHIVO.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.DOCUMENTO_DESCRIPCION) ? "" : A.DOCUMENTO_DESCRIPCION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.DOCUMENTO_DESCRIPCION) ? "" : B.DOCUMENTO_DESCRIPCION.ToUpper().Trim()))
                        respuesta.DOCUMENTO_DESCRIPCION = string.IsNullOrEmpty(A.DOCUMENTO_DESCRIPCION) ? "" : A.DOCUMENTO_DESCRIPCION.ToUpper().Trim();
                    if (A.ENTREGADO != B.ENTREGADO)
                        respuesta.ENTREGADO = A.ENTREGADO.ToString();
                    if ((string.IsNullOrEmpty(A.NOMBRE_ARCHIVO) ? "" : A.NOMBRE_ARCHIVO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.NOMBRE_ARCHIVO) ? "" : B.NOMBRE_ARCHIVO.ToUpper().Trim()))
                        respuesta.NOMBRE_ARCHIVO = string.IsNullOrEmpty(A.NOMBRE_ARCHIVO) ? "" : A.NOMBRE_ARCHIVO.ToUpper().Trim();
                    respuesta.ID_DOCUMENTO_ENTREGADO = A.ID_DOCUMENTO_ENTREGADO;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public AUTORIZACIONROBOT CompararAutorizacion(AUTORIZACION A, AUTORIZACION B)
        {
            AUTORIZACIONROBOT respuesta = new AUTORIZACIONROBOT();
            try
            {
                if (A.ID_AUTORIZACION == B.ID_AUTORIZACION)
                {
                    if ((string.IsNullOrEmpty(A.AUTORIZACION_ESPECIAL) ? "" : A.AUTORIZACION_ESPECIAL.ToUpper().Trim()) != (string.IsNullOrEmpty(B.AUTORIZACION_ESPECIAL) ? "" : B.AUTORIZACION_ESPECIAL.ToUpper().Trim()))
                        respuesta.AUTORIZACION_ESPECIAL = string.IsNullOrEmpty(A.AUTORIZACION_ESPECIAL) ? "" : A.AUTORIZACION_ESPECIAL.ToUpper().Trim();
                    if ((string.IsNullOrEmpty(A.DESCRIPCION) ? "" : A.DESCRIPCION.ToUpper().Trim()) != (string.IsNullOrEmpty(B.DESCRIPCION) ? "" : B.DESCRIPCION.ToUpper().Trim()))
                        respuesta.DESCRIPCION = string.IsNullOrEmpty(A.DESCRIPCION) ? "" : A.DESCRIPCION.ToUpper().Trim();
                    respuesta.ID_AUTORIZACION = A.ID_AUTORIZACION;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }
        public FECHA_A_MROBOT CompararFecha_A_m(FECHA_A_M A, FECHA_A_M B)
        {
            FECHA_A_MROBOT respuesta = new FECHA_A_MROBOT();
            try
            {
                if (A.ID_FECHA_A_M == B.ID_FECHA_A_M)
                {
                    if ((string.IsNullOrEmpty(A.TIPO) ? "" : A.TIPO.ToUpper().Trim()) != (string.IsNullOrEmpty(B.TIPO) ? "" : B.TIPO.ToUpper().Trim()))
                        respuesta.TIPO = string.IsNullOrEmpty(A.TIPO) ? "" : A.TIPO.ToUpper().Trim();
                    if (A.ANIO != B.ANIO)
                        respuesta.ANIO = A.ANIO.ToString();
                    if (A.MES != B.MES)
                        respuesta.MES = A.MES.ToString();
                    respuesta.ID_FECHA_A_M = A.ID_FECHA_A_M;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }

        public string EntrevistaPregunta(string pregunta,int ID_CLIENTE,int ID)
        {
            string respuesta = string.Empty;
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return respuesta;
        }
        #endregion


        private String DiferenciaFechas(DateTime newdt, DateTime olddt)
        {
            Int32 anios;
            Int32 meses;
            Int32 dias;
            String str = "";

            anios = (newdt.Year - olddt.Year);
            meses = (newdt.Month - olddt.Month);
            dias = (newdt.Day - olddt.Day);

            if (meses < 0)
            {
                anios -= 1;
                meses += 12;
            }
            if (dias < 0)
            {
                meses -= 1;
                dias += DateTime.DaysInMonth(newdt.Year, newdt.Month);
            }

            if (anios < 0)
            {
                return "Fecha Invalida";
            }
            if (anios > 0)
            {
                if (anios==1)
                    str = str + anios.ToString() + " año ";
                else
                    str = str + anios.ToString() + " años ";
            }
                
            if (meses > 0)
            {
                if (meses==1)
                    str = str + meses.ToString() + " mes ";
                else
                    str = str + meses.ToString() + " meses ";

            }
            if (dias > 0)
            {
                if (dias==1)
                    str = str + dias.ToString() + " dia ";
                else
                    str = str + dias.ToString() + " dias ";
            }
                

            return str;
        }

    }
}
