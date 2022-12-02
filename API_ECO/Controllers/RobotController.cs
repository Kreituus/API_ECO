using Business_Eco;
using Common_Eco;
using Entidades_Eco;
using Logs_Eco;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API_ECO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobotController : Controller
    {
        private string _operacion;
        private readonly ILogger _logger;
        private IBussinessPendientes _business;
        public RobotController(ILogger logger, IBussinessPendientes business)
        {
            this._logger = logger;
            this._business = business;
        }

        [HttpGet("Pendiente/ANA/Request/InsertarCliente")]
        public async Task<IActionResult> InsertarClienteAnaPendiente()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            CLIENTERESPONSEANA _response = new CLIENTERESPONSEANA();
            try
            {
                _response = await _business.ListaClienteAna("PENDIENTE");
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

        [HttpPost("Pendiente/ANA/Response/InsertarCliente")]
        public async Task<IActionResult> CambioEstadoAna(CAMBIOESTADOANA request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            RespuestaGeneral _response = new RespuestaGeneral();
            try
            {

                _response = await _business.CambioEstadoAna(request);
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

        [HttpGet("Validar/ANA/Cliente/Request")]
        public async Task<IActionResult> ClienteAnaValidar()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            CLIENTERESPONSEANAVALIDAR _response = new CLIENTERESPONSEANAVALIDAR();
            try
            {

                _response = await _business.ListaClienteAnaValidar("VALIDAR");
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

        [HttpPost("Validar/ANA/Cliente/Response")]
        public async Task<IActionResult> ClienteAnaValidarResponse(CLIENTERESPONSEANAVALIDARRESPONSE request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            RespuestaGeneral _response = new RespuestaGeneral();
            try
            {

                _response = await _business.ModificarClienteValidar(request);
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


        [HttpGet("Actualizar/ANA/Cliente/Request")]
        public async Task<IActionResult> InsertarClienteAnaActualizar()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            CLIENTEROBOT _response = new CLIENTEROBOT();
            try
            {

                _response = await _business.ListaClienteAnaRobot("ACTUALIZAR");
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

        [HttpPost("Actualizar/ANA/Cliente/Response")]
        public async Task<IActionResult> InsertarClienteAnaActualizarRespuesta(RESPUESTA_CLIENTE_REQUEST request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            RespuestaGeneral _response = new RespuestaGeneral();
            try
            {
                _response = await _business.ModificarClienteActualizar(request);
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

        [HttpGet("Pendiente/SCI/ListarCredito")]
        public async Task<IActionResult> InsertarCreditoSCI()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            CLIENTERESPONSESCI _response = new CLIENTERESPONSESCI();
            try
            {

                _response = await _business.ListaClienteSCI();
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

        [HttpPost("Pendiente/SCI/CambioEstado")]
        public async Task<IActionResult> CambioEstadoSCI(CAMBIOESTADOSCI request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            RespuestaGeneral _response = new RespuestaGeneral();
            try
            {

                _response = await _business.CambioEstadoSCI(request);
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


        [HttpPost("ANA/CambioEstadoProblemas")]
        public async Task<IActionResult> CambioEstadoAnaProblemas(CAMBIOESTADOANAPROBLEMAS request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            RespuestaGeneral _response = new RespuestaGeneral();
            try
            {

                _response = await _business.CambioEstadoAnaProblemas(request);
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


        [HttpPost("SCI/CambioEstadoProblemas")]
        public async Task<IActionResult> CambioEstadoSCIProblemas(CAMBIOESTADOSCIPROBLEMAS request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            RespuestaGeneral _response = new RespuestaGeneral();
            try
            {
                _response = await _business.CambioEstadoSCIProblemas(request);
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


        [HttpPost("Scoring/Insertar")]
        public async Task<IActionResult> InsertarScoring(SCORINGREQUEST request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            RespuestaGeneral _response = new RespuestaGeneral();
            try
            {
                _response = await _business.InsertarScoring(request);
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
