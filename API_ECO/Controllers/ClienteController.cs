using Business_Eco;
using Common_Eco;
using Entidades_Eco;
using Logs_Eco;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILogger = Logs_Eco.ILogger;


namespace API_ECO.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {

        private string _operacion;
        private readonly ILogger _logger;
        private IBussinessPendientes _business;
        public ClienteController(ILogger logger, IBussinessPendientes business)
        {
            this._logger = logger;
            this._business = business;
        }

        [HttpPost("InsertarCliente")]
        public async Task<IActionResult> InsertarCliente([FromBody] CLIENTE request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            RespuestaGeneral _response = new RespuestaGeneral();
            try
            {
                _response = await _business.InsertarCliente(request);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.code = Configuraciones.GetCode("ERROR_FATAL");
                _response.message = "ERROR INESPERADO EN LA PETICION";
                _logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializeObject(ex));
                return BadRequest(_response);
            }
            finally
            {
                _logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializeObject(_response));
            }

        }

        [HttpPut("ModificarCliente")]
        public async Task<IActionResult> ModificarCliente([FromBody] CLIENTE request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            RespuestaGeneral _response = new RespuestaGeneral();
            try
            {
                _response = await _business.ModificarCliente(request);
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.code = Configuraciones.GetCode("ERROR_FATAL");
                _response.message = "ERROR INESPERADO EN LA PETICION";
                _logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializeObject(ex));
                return BadRequest(_response);
            }
            finally
            {
                _logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializeObject(_response));
            }

        }

        [HttpPost("BusquedaCliente")]
        public async Task<IActionResult> BusquedaCredito(BuscarClienteRequest request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            CLIENTERESPONSE _response = new CLIENTERESPONSE();
            try
            {
                _response = await _business.BuscarCliente(request);
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.code = Configuraciones.GetCode("ERROR_FATAL");
                _response.message = "ERROR INESPERADO EN LA PETICION";
                _logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializeObject(ex));
                return BadRequest(_response);
            }
            finally
            {
                _logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializeObject(_response));
            }

        }

        [HttpPost("CambioEstado")]
        public async Task<IActionResult> CambioEstado(CAMBIOESTADO request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            RespuestaGeneral _response = new RespuestaGeneral();
            try
            {
                _response = await _business.CreditoCambioEstado(request);
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.code = Configuraciones.GetCode("ERROR_FATAL");
                _response.message = "ERROR INESPERADO EN LA PETICION";
                _logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializeObject(ex));
                return BadRequest(_response);
            }
            finally
            {
                _logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializeObject(_response));
            }

        }

        [HttpGet("ListarCliente")]
        public async Task<IActionResult> ListarCliente()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            LISTARCLIENTE _response = new LISTARCLIENTE();
            try
            {
                _response = await _business.ListarCliente();
                if (_response.CLIENTES_RESUMEN.Count == 0)
                {
                    _response.code = Configuraciones.GetCode("OK");
                    _response.message = "LA LISTA SE ENCUENTRA VACIO";
                    _response.success = false;

                }
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.code = Configuraciones.GetCode("ERROR_FATAL");
                _response.message = "ERROR INESPERADO EN LA PETICION";
                _logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializeObject(ex));
                return BadRequest(_response);
            }
            finally
            {
                _logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializeObject(_response));
            }

        }

        [HttpPost("BusquedaClienteValidar")]
        public async Task<IActionResult> BusquedaCreditovalidar(BuscarClienteRequest request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            CLIENTERESPONSE _response = new CLIENTERESPONSE();
            try
            {
                _response = await _business.BuscarClienteValidar(request);
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.code = Configuraciones.GetCode("ERROR_FATAL");
                _response.message = "ERROR INESPERADO EN LA PETICION";
                _logger.Error("[{0}] ->    ERROR: {1} , Exception: {2}", this._operacion, ex.Message, ManagerJson.SerializeObject(ex));
                return BadRequest(_response);
            }
            finally
            {
                _logger.Debug("[{0}] -> RESPONSE: {1}", this._operacion, ManagerJson.SerializeObject(_response));
            }

        }

    }
}
