using Entidades_Eco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_Eco
{
    public class AccessBD
    {
        string strConexion = Conection.Conexion();

        #region ROBOT

        public DataTable InsertarClienteTemporal(string DESCRIPCION,string CATEGORIA,int ID_CLIENTE,string USUARIO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_TEMPORAL_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@DESCRIPCION", DESCRIPCION, Direccion.Input);
                storeProcedure.AgregarParametro("@CATEGORIA", CATEGORIA, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BorrarClienteTemporal(string CATEGORIA, int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_TEMPORAL_DELETE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CATEGORIA", CATEGORIA, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarClienteTemporal(string CATEGORIA, int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_TEMPORAL_BUSCAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CATEGORIA", CATEGORIA, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable InsertarRespuestaCliente(RESPUESTA_CLIENTE_REQUEST data,string CATEGORIA)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[RESPUESTA_CLIENTE_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CATEGORIA", CATEGORIA, Direccion.Input);
                storeProcedure.AgregarParametro("@CODIGO_R", data.CODIGO, Direccion.Input);
                storeProcedure.AgregarParametro("@MENSAJE_R", data.MENSAJE, Direccion.Input);
                storeProcedure.AgregarParametro("@SUCCESS", data.SUCCESS, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data.ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarRespuestaCliente(RESPUESTA_CLIENTE_REQUEST data,string CATEGORIA)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[RESPUESTA_CLIENTE_MODIFICAR_CLIENTE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CATEGORIA", CATEGORIA, Direccion.Input);
                storeProcedure.AgregarParametro("@CODIGO_R", data.CODIGO, Direccion.Input);
                storeProcedure.AgregarParametro("@MENSAJE_R", data.MENSAJE, Direccion.Input);
                storeProcedure.AgregarParametro("@SUCCESS", data.SUCCESS, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data.ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarRespuestaCliente(int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[RESPUESTA_CLIENTE_BUSCAR_CLIENTE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }
        public DataTable CreditoCambioEstadoRobot(CAMBIOESTADOROBOT data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CREDITO_CAMBIO_ESTADO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CREDITO", data.ID_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                storeProcedure.AgregarParametro("@NUEVO_ESTADO", data.NUEVO_ESTADO, Direccion.Input);
                storeProcedure.AgregarParametro("@DESCRIPCION", data.DESCRIPCION, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ClienteCambioEstadoRobot(CAMBIOESTADOROBOT data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_CAMBIO_ESTADO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data.ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                storeProcedure.AgregarParametro("@NUEVO_ESTADO", data.NUEVO_ESTADO, Direccion.Input);
                storeProcedure.AgregarParametro("@DESCRIPCION", data.DESCRIPCION, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaClienteAna(string ESTADO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_LISTAR_ANA]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ESTADO", ESTADO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable ActualizarCodigoAnaPersona(CAMBIOESTADOANA data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[PERSONA_ACTUALIZAR_ANA]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data.ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@COD_ANA", data.COD_ANA, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.ROBOT, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ActualizarCodigoSCICredito(CAMBIOESTADOSCI data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CREDITO_ACTUALIZAR_SCI]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CREDITO", data.ID_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@COD_SCI", data.COD_SCI, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.ROBOT, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaClienteSCI()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_LISTAR_SCI]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable InsertarScoring(SCORINGRESPONSE data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[SCORING_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CRITERIO", data.CRITERIO, Direccion.Input);
                storeProcedure.AgregarParametro("@RESULTADO", data.RESULTADO, Direccion.Input);
                storeProcedure.AgregarParametro("@PUNTAJE", data.PUNTAJE, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data.ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@TICKET", data.TICKET, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarScoring(int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[SCORING_BUSCAR_CLIENTE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        #endregion

        #region CAMBIO DE ESTADO
        public DataTable CreditoCambioEstado(CAMBIOESTADO data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CREDITO_CAMBIO_ESTADO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CREDITO", data.ID_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                storeProcedure.AgregarParametro("@NUEVO_ESTADO", data.NUEVO_ESTADO, Direccion.Input);
                storeProcedure.AgregarParametro("@DESCRIPCION", "", Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ClienteCambioEstado(CAMBIOESTADO data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_CAMBIO_ESTADO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data.ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                storeProcedure.AgregarParametro("@NUEVO_ESTADO", data.NUEVO_ESTADO, Direccion.Input);
                storeProcedure.AgregarParametro("@DESCRIPCION", "", Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }
        #endregion

        #region CREDITO
        public DataTable InsertarCredito(CREDITO data, int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CREDITO_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@COD_SCI", data.COD_SCI, Direccion.Input);
                storeProcedure.AgregarParametro("@MONTO_CREDITO", data.MONTO_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO_OPERACION", data.TIPO_OPERACION, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTRATEGIA", data.ESTRATEGIA, Direccion.Input);
                storeProcedure.AgregarParametro("@DESTINO", data.DESTINO, Direccion.Input);
                storeProcedure.AgregarParametro("@DESTINO_CLIENTE", data.DESTINO_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@COMPRA_PASIVO", data.COMPRA_PASIVO, Direccion.Input);
                storeProcedure.AgregarParametro("@OBJETO_OPERACION", data.OBJETO_OPERACION, Direccion.Input);
                storeProcedure.AgregarParametro("@PLAZO", data.PLAZO, Direccion.Input);
                storeProcedure.AgregarParametro("@DIA_PAGO", data.DIA_PAGO, Direccion.Input);
                storeProcedure.AgregarParametro("@ANTIGUEDAD_CLIENTE", data.ANTIGUEDAD_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@FECHA_SOLICITUD", data.FECHA_SOLICITUD, Direccion.Input);
                storeProcedure.AgregarParametro("@CLIENTE_CPOP", data.CLIENTE_CPOP, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_CPOP", data.NRO_CPOP, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO_TASA", data.TIPO_TASA, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_AUTORIZACION", data.NRO_AUTORIZACION, Direccion.Input);
                storeProcedure.AgregarParametro("@TASA_SELECCION", data.TASA_SELECCION, Direccion.Input);
                storeProcedure.AgregarParametro("@TASA_PP", data.TASA_PP, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO_CREDITO", data.ESTADO_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO_SCI", data.ESTADO_SCI, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable ModificarCredito(CREDITO data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CREDITO_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CREDITO", data.ID_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@COD_SCI", data.COD_SCI, Direccion.Input);
                storeProcedure.AgregarParametro("@MONTO_CREDITO", data.MONTO_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO_OPERACION", data.TIPO_OPERACION, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTRATEGIA", data.ESTRATEGIA, Direccion.Input);
                storeProcedure.AgregarParametro("@DESTINO", data.DESTINO, Direccion.Input);
                storeProcedure.AgregarParametro("@DESTINO_CLIENTE", data.DESTINO_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@COMPRA_PASIVO", data.COMPRA_PASIVO, Direccion.Input);
                storeProcedure.AgregarParametro("@OBJETO_OPERACION", data.OBJETO_OPERACION, Direccion.Input);
                storeProcedure.AgregarParametro("@PLAZO", data.PLAZO, Direccion.Input);
                storeProcedure.AgregarParametro("@DIA_PAGO", data.DIA_PAGO, Direccion.Input);
                storeProcedure.AgregarParametro("@ANTIGUEDAD_CLIENTE", data.ANTIGUEDAD_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@FECHA_SOLICITUD", data.FECHA_SOLICITUD, Direccion.Input);
                storeProcedure.AgregarParametro("@CLIENTE_CPOP", data.CLIENTE_CPOP, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_CPOP", data.NRO_CPOP, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO_TASA", data.TIPO_TASA, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_AUTORIZACION", data.NRO_AUTORIZACION, Direccion.Input);
                storeProcedure.AgregarParametro("@TASA_SELECCION", data.TASA_SELECCION, Direccion.Input);
                storeProcedure.AgregarParametro("@TASA_PP", data.TASA_PP, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO_CREDITO", data.ESTADO_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO_SCI", data.ESTADO_SCI, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarCredito(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CREDITO_LISTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        #endregion

        #region CLIENTE

        public DataTable InsertarCliente(CLIENTE data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO_ANA", data.ESTADO_ANA, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarCliente(CLIENTE data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data.ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO_ANA", data.ESTADO_ANA, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarCliente(BuscarClienteRequest data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_BUSCAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data.ID_CLIENTE, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarCliente()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CLIENTE_LISTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }
        #endregion

        #region PERSONA
        public DataTable InsertarPersona(PERSONA data, int ID_CLIENTE, int ID_REFERENCIA)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[PERSONA_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@GENERO", data.GENERO, Direccion.Input);
                storeProcedure.AgregarParametro("@PRIMER_APELLIDO", data.PRIMER_APELLIDO, Direccion.Input);
                storeProcedure.AgregarParametro("@SEGUNDO_APELLIDO", data.SEGUNDO_APELLIDO, Direccion.Input);
                storeProcedure.AgregarParametro("@NOMBRES", data.NOMBRES, Direccion.Input);
                storeProcedure.AgregarParametro("@CASADA_APELLIDO", data.CASADA_APELLIDO, Direccion.Input);
                storeProcedure.AgregarParametro("@CORREO", data.CORREO, Direccion.Input);
                storeProcedure.AgregarParametro("@NIVEL_EDUCACION", data.NIVEL_EDUCACION, Direccion.Input);
                storeProcedure.AgregarParametro("@PROFESION", data.PROFESION, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO_CIVIL", data.ESTADO_CIVIL, Direccion.Input);
                storeProcedure.AgregarParametro("@COD_ANA", data.COD_ANA, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_REFERENCIA", ID_REFERENCIA, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable ModificarPersona(PERSONA data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[PERSONA_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_PERSONA", data.ID_PERSONA, Direccion.Input);
                storeProcedure.AgregarParametro("@GENERO", data.GENERO, Direccion.Input);
                storeProcedure.AgregarParametro("@PRIMER_APELLIDO", data.PRIMER_APELLIDO, Direccion.Input);
                storeProcedure.AgregarParametro("@SEGUNDO_APELLIDO", data.SEGUNDO_APELLIDO, Direccion.Input);
                storeProcedure.AgregarParametro("@NOMBRES", data.NOMBRES, Direccion.Input);
                storeProcedure.AgregarParametro("@CASADA_APELLIDO", data.CASADA_APELLIDO, Direccion.Input);
                storeProcedure.AgregarParametro("@CORREO", data.CORREO, Direccion.Input);
                storeProcedure.AgregarParametro("@NIVEL_EDUCACION", data.NIVEL_EDUCACION, Direccion.Input);
                storeProcedure.AgregarParametro("@PROFESION", data.PROFESION, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO_CIVIL", data.ESTADO_CIVIL, Direccion.Input);
                storeProcedure.AgregarParametro("@COD_ANA", data.COD_ANA, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable ListarPersona(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[PERSONA_BUSCAR_CLIENTE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarPersonaCliente(string NRO_DOCUMENTO,string COMPLEMENTO,string EXTENSION)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[PERSONA_BUSCAR_NRO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@NRO_DOCUMENTO", NRO_DOCUMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@COMPLEMENTO", COMPLEMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@EXTENSION", EXTENSION, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarPersonaReferencia(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[PERSONA_BUSCAR_REFERENCIA]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_REFERENCIA", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region TELEFONO

        public DataTable InsertarTelefono(TELEFONO data, int ID_DIRECCION, int ID_PERSONA)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[TELEFONO_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@NUMERO", data.NUMERO, Direccion.Input);
                storeProcedure.AgregarParametro("@DESCRIPCION", data.DESCRIPCION, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_DIRECCION", ID_DIRECCION, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_PERSONA", ID_PERSONA, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable ModificarTelefono(TELEFONO data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[TELEFONO_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_TELEFONO", data.ID_TELEFONO, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@NUMERO", data.NUMERO, Direccion.Input);
                storeProcedure.AgregarParametro("@DESCRIPCION", data.DESCRIPCION, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarTelefono(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[TELEFONO_BUSCAR_DIRECCION]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_DIRECCION", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarTelefonoPersona(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[TELEFONO_BUSCAR_PERSONA]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_PERSONA", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region DIRECCION
        public DataTable InsertarDireccion(DIRECCION data, int ID_PERSONA, int ID_ACTIVIDAD_ECONOMICA)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DIRECCION_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@PAIS_RESIDENCIA", data.PAIS_RESIDENCIA, Direccion.Input);
                storeProcedure.AgregarParametro("@CIUDAD_RESIDENCIA", data.CIUDAD_RESIDENCIA, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO_DIRECCION", data.TIPO_DIRECCION, Direccion.Input);
                storeProcedure.AgregarParametro("@DEPARTAMENTO", data.DEPARTAMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@LOCALIDAD", data.LOCALIDAD, Direccion.Input);
                storeProcedure.AgregarParametro("@ZONA_BARRIO", data.ZONA_BARRIO, Direccion.Input);
                storeProcedure.AgregarParametro("@CALLE_AVENIDA", data.CALLE_AVENIDA, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_PUERTA", data.NRO_PUERTA, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_PISO", data.NRO_PISO, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_DEPARTAMENTO", data.NRO_DEPARTAMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@NOMBRE_EDIFICIO", data.NOMBRE_EDIFICIO, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_CASILLA", data.NRO_CASILLA, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO_VIVIENDA_OFICINA", data.TIPO_VIVIENDA_OFICINA, Direccion.Input);
                storeProcedure.AgregarParametro("@REFERENCIA", data.REFERENCIA, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_PERSONA", ID_PERSONA, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_ACTIVIDAD_ECONOMICA", ID_ACTIVIDAD_ECONOMICA, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable ModificarDireccion(DIRECCION data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DIRECCION_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_DIRECCION", data.ID_DIRECCION, Direccion.Input);
                storeProcedure.AgregarParametro("@PAIS_RESIDENCIA", data.PAIS_RESIDENCIA, Direccion.Input);
                storeProcedure.AgregarParametro("@CIUDAD_RESIDENCIA", data.CIUDAD_RESIDENCIA, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO_DIRECCION", data.TIPO_DIRECCION, Direccion.Input);
                storeProcedure.AgregarParametro("@DEPARTAMENTO", data.DEPARTAMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@LOCALIDAD", data.LOCALIDAD, Direccion.Input);
                storeProcedure.AgregarParametro("@ZONA_BARRIO", data.ZONA_BARRIO, Direccion.Input);
                storeProcedure.AgregarParametro("@CALLE_AVENIDA", data.CALLE_AVENIDA, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_PUERTA", data.NRO_PUERTA, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_PISO", data.NRO_PISO, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_DEPARTAMENTO", data.NRO_DEPARTAMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@NOMBRE_EDIFICIO", data.NOMBRE_EDIFICIO, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_CASILLA", data.NRO_CASILLA, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO_VIVIENDA_OFICINA", data.TIPO_VIVIENDA_OFICINA, Direccion.Input);
                storeProcedure.AgregarParametro("@REFERENCIA", data.REFERENCIA, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarDireccionPersona(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DIRECCION_BUSCAR_PERSONA]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_PERSONA", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarDireccionActividadEconomica(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DIRECCION_BUSCAR_ACTIVIDAD_ECONOMICA]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_ACTIVIDAD_ECONOMICA", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region DOCUMENTO PERSONAL
        public DataTable InsertarDocumentoPersonal(DOCUMENTO_PERSONAL data, int ID_PERSONA)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DOCUMENTO_PERSONAL_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@TIPO_DOCUMENTO", data.TIPO_DOCUMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_DOCUMENTO", data.NRO_DOCUMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@COMPLEMENTO", data.COMPLEMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@EXTENSION", data.EXTENSION, Direccion.Input);
                storeProcedure.AgregarParametro("@FECHA_VENC", data.FECHA_VENC, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO_CIVIL", data.ESTADO_CIVIL, Direccion.Input);
                storeProcedure.AgregarParametro("@NACIONALIDAD", data.NACIONALIDAD, Direccion.Input);
                storeProcedure.AgregarParametro("@LUGAR_NACIMIENTO", data.LUGAR_NACIMIENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@FECHA_NACIMIENTO", data.FECHA_NACIMIENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_PERSONA", ID_PERSONA, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable ModificarDocumentoPersonal(DOCUMENTO_PERSONAL data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DOCUMENTO_PERSONAL_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_DOCUMENTO_PERSONAL", data.ID_DOCUMENTO_PERSONAL, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO_DOCUMENTO", data.TIPO_DOCUMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@NRO_DOCUMENTO", data.NRO_DOCUMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@COMPLEMENTO", data.COMPLEMENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@EXTENSION", data.EXTENSION, Direccion.Input);
                storeProcedure.AgregarParametro("@FECHA_VENC", data.FECHA_VENC, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO_CIVIL", data.ESTADO_CIVIL, Direccion.Input);
                storeProcedure.AgregarParametro("@NACIONALIDAD", data.NACIONALIDAD, Direccion.Input);
                storeProcedure.AgregarParametro("@LUGAR_NACIMIENTO", data.LUGAR_NACIMIENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@FECHA_NACIMIENTO", data.FECHA_NACIMIENTO, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarDocumentoPersonal(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DOCUMENTO_PERSONAL_BUSCAR_PERSONA]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_PERSONA", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region ACTIVIDAD ECONOMICA
        public DataTable InsertarActividadEconomica(ACTIVIDAD_ECONOMICA data, int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ACTIVIDAD_ECONOMICA_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@NIVEL_LABORAL", data.NIVEL_LABORAL, Direccion.Input);
                storeProcedure.AgregarParametro("@NIT", data.NIT, Direccion.Input);
                storeProcedure.AgregarParametro("@ACTIVIDAD_DECLARADA", data.ACTIVIDAD_DECLARADA, Direccion.Input);
                storeProcedure.AgregarParametro("@PRIORIDAD", data.PRIORIDAD, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO", data.ESTADO, Direccion.Input);
                storeProcedure.AgregarParametro("@INGRESOS_MENSUALES", data.INGRESOS_MENSUALES, Direccion.Input);
                storeProcedure.AgregarParametro("@OTROS_INGRESOS_MENSUALES", data.OTROS_INGRESOS_MENSUALES, Direccion.Input);
                storeProcedure.AgregarParametro("@EGRESOS_MENSUALES", data.EGRESOS_MENSUALES, Direccion.Input);
                storeProcedure.AgregarParametro("@MARGEN_AHORRO", data.MARGEN_AHORRO, Direccion.Input);
                storeProcedure.AgregarParametro("@NOMBRE_EMPRESA", data.NOMBRE_EMPRESA, Direccion.Input);
                storeProcedure.AgregarParametro("@FECHA_INGRESO", data.FECHA_INGRESO, Direccion.Input);
                storeProcedure.AgregarParametro("@CARGO", data.CARGO, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarActividadEconomica(ACTIVIDAD_ECONOMICA data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ACTIVIDAD_ECONOMICA_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_ACTIVIDAD_ECONOMICA", data.ID_ACTIVIDAD_ECONOMICA, Direccion.Input);
                storeProcedure.AgregarParametro("@NIVEL_LABORAL", data.NIVEL_LABORAL, Direccion.Input);
                storeProcedure.AgregarParametro("@NIT", data.NIT, Direccion.Input);
                storeProcedure.AgregarParametro("@ACTIVIDAD_DECLARADA", data.ACTIVIDAD_DECLARADA, Direccion.Input);
                storeProcedure.AgregarParametro("@PRIORIDAD", data.PRIORIDAD, Direccion.Input);
                storeProcedure.AgregarParametro("@ESTADO", data.ESTADO, Direccion.Input);
                storeProcedure.AgregarParametro("@INGRESOS_MENSUALES", data.INGRESOS_MENSUALES, Direccion.Input);
                storeProcedure.AgregarParametro("@OTROS_INGRESOS_MENSUALES", data.OTROS_INGRESOS_MENSUALES, Direccion.Input);
                storeProcedure.AgregarParametro("@EGRESOS_MENSUALES", data.EGRESOS_MENSUALES, Direccion.Input);
                storeProcedure.AgregarParametro("@MARGEN_AHORRO", data.MARGEN_AHORRO, Direccion.Input);
                storeProcedure.AgregarParametro("@NOMBRE_EMPRESA", data.NOMBRE_EMPRESA, Direccion.Input);
                storeProcedure.AgregarParametro("@FECHA_INGRESO", data.FECHA_INGRESO, Direccion.Input);
                storeProcedure.AgregarParametro("@CARGO", data.CARGO, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarActividadEconomica(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ACTIVIDAD_ECONOMICA_BUSCAR_CLIENTE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }




        #endregion

        #region CAEDEC
        public DataTable InsertarCaedec(CAEDEC data, int ID_ACTIVIDAD_ECONOMICA)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CAEDEC_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@COD_CAEDEC", data.COD_CAEDEC, Direccion.Input);
                storeProcedure.AgregarParametro("@ACTIVIDAD_CAEDEC", data.ACTIVIDAD_CAEDEC, Direccion.Input);
                storeProcedure.AgregarParametro("@GRUPO_CAEDEC", data.GRUPO_CAEDEC, Direccion.Input);
                storeProcedure.AgregarParametro("@SECTOR_ECONOMICO", data.SECTOR_ECONOMICO, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_ACTIVIDAD_ECONOMICA", ID_ACTIVIDAD_ECONOMICA, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarCaedec(CAEDEC data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CAEDEC_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CAEDEC", data.ID_CAEDEC, Direccion.Input);
                storeProcedure.AgregarParametro("@COD_CAEDEC", data.COD_CAEDEC, Direccion.Input);
                storeProcedure.AgregarParametro("@ACTIVIDAD_CAEDEC", data.ACTIVIDAD_CAEDEC, Direccion.Input);
                storeProcedure.AgregarParametro("@GRUPO_CAEDEC", data.GRUPO_CAEDEC, Direccion.Input);
                storeProcedure.AgregarParametro("@SECTOR_ECONOMICO", data.SECTOR_ECONOMICO, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable ListarCaedec(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CAEDEC_BUSCAR_ACTIVIDAD_ECONOMICA]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_ACTIVIDAD_ECONOMICA", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BusquedaCaedec(BusquedaCaedecRequest data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_CAEDEC_BUSQUEDA]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@COD_CAEDEC", data.COD_CAEDEC, Direccion.Input);
                storeProcedure.AgregarParametro("@ACTIVIDAD_CAEDEC", data.ACTIVIDAD_CAEDEC, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region REFERENCIA
        public DataTable InsertarReferencia(REFERENCIA data, int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[REFERENCIA_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@RELACION", data.RELACION, Direccion.Input);
                storeProcedure.AgregarParametro("@OBSERVACION", data.OBSERVACION, Direccion.Input);
                storeProcedure.AgregarParametro("@CALIFICACION", data.CALIFICACION, Direccion.Input);
                storeProcedure.AgregarParametro("@DESCRIPCION_CALIFICACION", data.DESCRIPCION_CALIFICACION, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarReferencia(REFERENCIA data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[REFERENCIA_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_REFERENCIA", data.ID_REFERENCIA, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@RELACION", data.RELACION, Direccion.Input);
                storeProcedure.AgregarParametro("@OBSERVACION", data.OBSERVACION, Direccion.Input);
                storeProcedure.AgregarParametro("@CALIFICACION", data.CALIFICACION, Direccion.Input);
                storeProcedure.AgregarParametro("@DESCRIPCION_CALIFICACION", data.DESCRIPCION_CALIFICACION, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }



        public DataTable ListarReferencia(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[REFERENCIA_BUSCAR_CLIENTE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region FECHA_A_M
        public DataTable InsertarFechaAM(FECHA_A_M data, int ID_ACTIVIDAD_ECONOMICA)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[FECHA_A_M_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@ANIO", data.ANIO, Direccion.Input);
                storeProcedure.AgregarParametro("@MES", data.MES, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_ACTIVIDAD_ECONOMICA", ID_ACTIVIDAD_ECONOMICA, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarFechaAM(FECHA_A_M data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[FECHA_A_M_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_FECHA_A_M", data.ID_FECHA_A_M, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@ANIO", data.ANIO, Direccion.Input);
                storeProcedure.AgregarParametro("@MES", data.MES, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarFechaAM(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[FECHA_A_M_BUSCAR_ACTIVIDAD_ECONOMICA]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_ACTIVIDAD_ECONOMICA", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region DECLARACION JURADA
        public DataTable InsertarDeclaracionJurada(DECLARACION_JURADA data, int ID_CREDITO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DECLARACION_JURADA_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@PATRIMONIO_ACTIVO", data.PATRIMONIO_ACTIVO, Direccion.Input);
                storeProcedure.AgregarParametro("@PATRIMONIO_PASIVO", data.PATRIMONIO_PASIVO, Direccion.Input);
                storeProcedure.AgregarParametro("@PERSONAL_OCUPADO", data.PERSONAL_OCUPADO, Direccion.Input);
                storeProcedure.AgregarParametro("@TOTAL_INGRESO_VENTAS", data.TOTAL_INGRESO_VENTAS, Direccion.Input);
                storeProcedure.AgregarParametro("@OBSERVACIONES", data.OBSERVACIONES, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CREDITO", ID_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarDeclaracionJurada(DECLARACION_JURADA data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DECLARACION_JURADA_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_DECLARACION_JURADA", data.ID_DECLARACION_JURADA, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@PATRIMONIO_ACTIVO", data.PATRIMONIO_ACTIVO, Direccion.Input);
                storeProcedure.AgregarParametro("@PATRIMONIO_PASIVO", data.PATRIMONIO_PASIVO, Direccion.Input);
                storeProcedure.AgregarParametro("@PERSONAL_OCUPADO", data.PERSONAL_OCUPADO, Direccion.Input);
                storeProcedure.AgregarParametro("@TOTAL_INGRESO_VENTAS", data.TOTAL_INGRESO_VENTAS, Direccion.Input);
                storeProcedure.AgregarParametro("@OBSERVACIONES", data.OBSERVACIONES, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarDeclaracionJurada(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DECLARACION_JURADA_BUSCAR_CREDITO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CREDITO", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region DOCUMENTO ENTREGADO
        public DataTable InsertarDocumentoEntregado(DOCUMENTO_ENTREGADO data, int ID_CREDITO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DOCUMENTO_ENTREGADO_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@EXTENSION", data.EXTENSION, Direccion.Input);
                storeProcedure.AgregarParametro("@ARCHIVO", data.ARCHIVO, Direccion.Input);
                storeProcedure.AgregarParametro("@DOCUMENTO_DESCRIPCION", data.DOCUMENTO_DESCRIPCION, Direccion.Input);
                storeProcedure.AgregarParametro("@ENTREGADO", data.ENTREGADO, Direccion.Input);
                storeProcedure.AgregarParametro("@NOMBRE_ARCHIVO", data.NOMBRE_ARCHIVO, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CREDITO", ID_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarDocumentoEntregado(DOCUMENTO_ENTREGADO data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DOCUMENTO_ENTREGADO_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_DOCUMENTO_ENTREGADO", data.ID_DOCUMENTO_ENTREGADO, Direccion.Input);
                storeProcedure.AgregarParametro("@TIPO", data.TIPO, Direccion.Input);
                storeProcedure.AgregarParametro("@EXTENSION", data.EXTENSION, Direccion.Input);
                storeProcedure.AgregarParametro("@ARCHIVO", data.ARCHIVO, Direccion.Input);
                storeProcedure.AgregarParametro("@DOCUMENTO_DESCRIPCION", data.DOCUMENTO_DESCRIPCION, Direccion.Input);
                storeProcedure.AgregarParametro("@ENTREGADO", data.ENTREGADO, Direccion.Input);
                storeProcedure.AgregarParametro("@NOMBRE_ARCHIVO", data.NOMBRE_ARCHIVO, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarDocumentoEntregado(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[DOCUMENTO_ENTREGADO_BUSCAR_CREDITO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CREDITO", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region AUTORIZACION
        public DataTable InsertarAutorizacion(AUTORIZACION data, int ID_CREDITO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[AUTORIZACION_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@HABILITADO", data.HABILITADO, Direccion.Input);
                storeProcedure.AgregarParametro("@AUTORIZACION_ESPECIAL", data.AUTORIZACION_ESPECIAL, Direccion.Input);
                storeProcedure.AgregarParametro("@DESCRIPCION", data.DESCRIPCION, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CREDITO", ID_CREDITO, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarAutorizacion(AUTORIZACION data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[AUTORIZACION_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_AUTORIZACION", data.ID_AUTORIZACION, Direccion.Input);
                storeProcedure.AgregarParametro("@HABILITADO", data.HABILITADO, Direccion.Input);
                storeProcedure.AgregarParametro("@AUTORIZACION_ESPECIAL", data.AUTORIZACION_ESPECIAL, Direccion.Input);
                storeProcedure.AgregarParametro("@DESCRIPCION", data.DESCRIPCION, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListarAutorizacion(int data)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[AUTORIZACION_BUSCAR_CREDITO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CREDITO", data, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region COMBOS

        public DataTable ListaAntiguedadCliente()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_ANTIGUEDAD_CLIENTE_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaAutorizacionesEspeciales()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_AUTORIZACIONES_ESPECIALES_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaCaedec()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_CAEDEC_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaCalificacion()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_CALIFICACION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaCiudadResidencia()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_CIUDAD_RESIDENCIA_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaEstadoActividad()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_ESTADO_ACTIVIDAD_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaDepartamento()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_DEPARTAMENTO_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaDestino()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_DESTINO_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaDocumentosEntregados()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_DOCUMENTOS_ENTREGADOS_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaEstadoCivil()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_ESTADO_CIVIL_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaEstrategia()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_ESTRATEGIA_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }
        public DataTable ListaExtension()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_EXTENSION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaGenero()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_GENERO_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaIngresosMensuales()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_INGRESOS_MENSUALES_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaLocalidad()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_LOCALIDAD_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaMoneda()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_MONEDA_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaNacionalidad()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_NACIONALIDAD_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaNivelEducacion()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_NIVEL_EDUCACION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaObjetoOperacion()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_OBJETO_OPERACION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaPaisResidencia()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_PAIS_RESIDENCIA_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaPais()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_PAIS_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaParentesco()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_PAIS_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaProfesion()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_PROFESION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaRelacion()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_RELACION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaTipoDireccion()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_TIPO_DIRECCION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaTipoDocumento()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_TIPO_DOCUMENTO_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }
        public DataTable ListaTipoOperacion()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_TIPO_OPERACION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaTipoTasaNegocio()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_TIPO_TASA_NEGOCIACION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaTipoTelefono()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_TIPO_TELEFONO_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }
        public DataTable ListaTipoVivienda()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_TIPO_VIVIENDA_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaTipoFechaAM()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_TIPO_FECHA_A_M_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaNivelLaboral()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_NIVEL_LABORAL_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaClienteCPOP()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_CLIENTE_CPOP]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaSectorEconomico()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_SECTOR_ECONOMICO_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaCriterio()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_CRITERIO_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaVerificacionCliente()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_ENTREVISTA_VERIFICACION_CLIENTE_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaScriptEvaluacion()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_ENTREVISTA_SCRIPT_EVALUACION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaInstitucion()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_ENTREVISTA_INSTITUCION_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ListaSeguroDesgravame()
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[BD_ENTREVISTA_SEGURO_DESGRAVAME_SELECT]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region SCORING
        public DataTable BuscarCIC(string BUSCAR)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_CIC_BUSCAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@BUSCAR", BUSCAR, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarHOJARIESGOS(string BUSCAR)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_HOJA_RIESGOS_BUSCAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@BUSCAR", BUSCAR, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarINFOCRED(string BUSCAR)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_INFOCRED_BUSCAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@BUSCAR", BUSCAR, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarPUNTAJE(string BUSCAR)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_PUNTAJE_BUSCAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@BUSCAR", BUSCAR, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarCodigoCIC(string CODIGO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_CIC_CODIGO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CODIGO", CODIGO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarCodigoHOJARIESGOS(string CODIGO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_HOJA_RIESGOS_CODIGO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CODIGO", CODIGO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarCodigoINFOCRED(string CODIGO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_INFOCRED_CODIGO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CODIGO", CODIGO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarCodigoPUNTAJE(string CODIGO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_PUNTAJE_CODIGO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CODIGO", CODIGO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }



        #endregion

        #region ENTREVISTA

        #region Script Evaluacion

        public DataTable BuscarScriptEvaluacion(string BUSCAR)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_ENTREVISTA_SCRIPT_EVALUACION_BUSCAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@BUSCAR", BUSCAR, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarCodigoScriptEvaluacion(string CODIGO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_ENTREVISTA_SCRIPT_EVALUACION_CODIGO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CODIGO", CODIGO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region SEGURO DESGRAVAME

        public DataTable BuscarSeguroDesgravame(string BUSCAR)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_ENTREVISTA_SEGURO_DESGRAVAME_BUSCAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@BUSCAR", BUSCAR, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarCodigoSeguroDesgravame(string CODIGO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_ENTREVISTA_SEGURO_DESGRAVAME_CODIGO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CODIGO", CODIGO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region Verificacion Cliente

        public DataTable BuscarVerificacionCliente(string BUSCAR)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_ENTREVISTA_VERIFICACION_CLIENTE_BUSCAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@BUSCAR", BUSCAR, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarCodigoVerificacionCliente(string CODIGO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_ENTREVISTA_VERIFICACION_CLIENTE_CODIGO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CODIGO", CODIGO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region Institucion

        public DataTable BuscarInstitucion(string BUSCAR)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_ENTREVISTA_INSTITUCION_BUSCAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@BUSCAR", BUSCAR, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarCodigoInstitucion(string CODIGO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_ENTREVISTA_INSTITUCION_CODIGO]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@CODIGO", CODIGO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable InsertarInstitucion(string DESCRIPCION,string USUARIO)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[CB_ENTREVISTA_INSTITUCION_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@DESCRIPCION", DESCRIPCION, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }
        #endregion


        public DataTable BuscarVerificacionClienteXCliente(int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_VERI_CLIENTE_BUSCAR_CLIENTE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable InsertarVerificacionCliente(VERIFICACION_CLIENTE_RESPONSE data,int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_VERI_CLIENTE_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@PREGUNTA", data.PREGUNTA, Direccion.Input);
                storeProcedure.AgregarParametro("@RESPUESTA", data.RESPUESTA, Direccion.Input);
                storeProcedure.AgregarParametro("@CONFIRMACION", data.CONFIRMACION, Direccion.Input);
                storeProcedure.AgregarParametro("@OBSERVACIONES", data.OBSERVACIONES, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarVerificacionCliente(VERIFICACION_CLIENTE_RESPONSE data,int ID_ENTREVISTA_VERI_CLIENTE, int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_VERI_CLIENTE_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_ENTREVISTA_VERI_CLIENTE", ID_ENTREVISTA_VERI_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@PREGUNTA", data.PREGUNTA, Direccion.Input);
                storeProcedure.AgregarParametro("@RESPUESTA", data.RESPUESTA, Direccion.Input);
                storeProcedure.AgregarParametro("@CONFIRMACION", data.CONFIRMACION, Direccion.Input);
                storeProcedure.AgregarParametro("@OBSERVACIONES", data.OBSERVACIONES, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable BuscarScripEvaluacionXCliente(int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_SCRIPT_EVALUACION_BUSCAR_CLIENTE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable InsertarScripEvaluacion(SCRIPT_EVALUACION_RESPONSE data,string ID_INSTITUCION, int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_SCRIPT_EVALUACION_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@PREGUNTA", data.PREGUNTA, Direccion.Input);
                storeProcedure.AgregarParametro("@RESPUESTA", data.RESPUESTA, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_INSTITUCION", ID_INSTITUCION, Direccion.Input);
                storeProcedure.AgregarParametro("@OBSERVACIONES", data.OBSERVACIONES, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarScripEvaluacion(SCRIPT_EVALUACION_RESPONSE data,int ID_SCRIPT_EVALUACION, string ID_INSTITUCION, int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_SCRIPT_EVALUACION_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_SCRIPT_EVALUACION", ID_SCRIPT_EVALUACION, Direccion.Input);
                storeProcedure.AgregarParametro("@PREGUNTA", data.PREGUNTA, Direccion.Input);
                storeProcedure.AgregarParametro("@RESPUESTA", data.RESPUESTA, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_INSTITUCION", ID_INSTITUCION, Direccion.Input);
                storeProcedure.AgregarParametro("@OBSERVACIONES", data.OBSERVACIONES, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable BuscarScripEvaluacionInstitucionXCliente(string ID_INSTITUCIONES)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_SCRIPT_EVALUACION_INSTITUCION_BUSCAR_CLIENTE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_INSTITUCIONES", ID_INSTITUCIONES, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable InsertarScripEvaluacionInstitucion(INSTITUCION data,string TOTAL)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_SCRIPT_EVALUACION_INSTITUCION_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@NOMBRE", data.NOMBRE, Direccion.Input);
                storeProcedure.AgregarParametro("@MONTO", data.MONTO, Direccion.Input);
                storeProcedure.AgregarParametro("@TOTAL", TOTAL, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarScripEvaluacionInstitucion(INSTITUCION data,int ID_INSTITUCIONES, string TOTAL)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_SCRIPT_EVALUACION_INSTITUCION_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_INSTITUCIONES", ID_INSTITUCIONES, Direccion.Input);
                storeProcedure.AgregarParametro("@NOMBRE", data.NOMBRE, Direccion.Input);
                storeProcedure.AgregarParametro("@MONTO", data.MONTO, Direccion.Input);
                storeProcedure.AgregarParametro("@TOTAL", TOTAL, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }


        public DataTable BuscarSeguroDesgravameXCliente(int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_SEGURO_DESGRAVAME_BUSCAR_CLIENTE]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable InsertarSeguroDesgravame(SEGURO_DESGRAVAME_RESPONSE data, int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_SEGURO_DESGRAVAME_INSERTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@PREGUNTA", data.PREGUNTA, Direccion.Input);
                storeProcedure.AgregarParametro("@RESPUESTA", data.RESPUESTA, Direccion.Input);
                storeProcedure.AgregarParametro("@CONFIRMACION", data.CONFIRMACION, Direccion.Input);
                storeProcedure.AgregarParametro("@OBSERVACIONES", data.OBSERVACIONES, Direccion.Input);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        public DataTable ModificarSeguroDesgravame(SEGURO_DESGRAVAME_RESPONSE data,int ID_SEGURO_DESGRAVAME, int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[ENTREVISTA_SEGURO_DESGRAVAME_MODIFICAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_SEGURO_DESGRAVAME", ID_SEGURO_DESGRAVAME, Direccion.Input);
                storeProcedure.AgregarParametro("@PREGUNTA", data.PREGUNTA, Direccion.Input);
                storeProcedure.AgregarParametro("@RESPUESTA", data.RESPUESTA, Direccion.Input);
                storeProcedure.AgregarParametro("@CONFIRMACION", data.CONFIRMACION, Direccion.Input);
                storeProcedure.AgregarParametro("@OBSERVACIONES", data.OBSERVACIONES, Direccion.Input);
                storeProcedure.AgregarParametro("@USUARIO", data.USUARIO, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion

        #region HISTORIAL_RESPUESTA_CLIENTE

        public DataTable ListadoHistorialRespuestaCliente(int ID_CLIENTE)
        {
            DataTable Get = new DataTable();
            try
            {
                string nombreSP = "[dbo].[HISTORIAL_RESPUESTA_CLIENTE_LISTAR]";
                StoreProcedure storeProcedure = new StoreProcedure(nombreSP);
                storeProcedure.AgregarParametro("@ID_CLIENTE", ID_CLIENTE, Direccion.Input);
                Get = storeProcedure.RealizarConsulta(strConexion);
                if (storeProcedure.Error != String.Empty)
                {
                    throw new Exception("procedimiento Almacenado: " + nombreSP + " , Descripción: " + storeProcedure.Error.Trim());
                }
            }
            catch (Exception ex)
            {

            }
            return Get;
        }

        #endregion
    }
}
