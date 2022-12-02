using Business_Eco;
using Common_Eco;
using Entidades_Eco;
using Logs_Eco;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ECO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombosController : Controller
    {
        private string _operacion;
        private readonly ILogger _logger;
        private IBussinessPendientes _business;
        public CombosController(ILogger logger, IBussinessPendientes business)
        {
            this._logger = logger;
            this._business = business;
        }

        [HttpGet("ComboCredito")]
        public async Task<IActionResult> ComboCredito()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboCredito _response = new ComboCredito();
            try
            {
                _response = await _business.ComboCredito();

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

        [HttpGet("ComboDocumentoPersonal")]
        public async Task<IActionResult> ComboDocumentoPersonal()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboDocumentoPersonal _response = new ComboDocumentoPersonal();
            try
            {

                _response = await _business.ComboDocumentoPersonal();

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

        [HttpGet("ComboPersona")]
        public async Task<IActionResult> ComboPersona()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboPersona _response = new ComboPersona();
            try
            {

                _response = await _business.ComboPersona();

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

        [HttpGet("ComboDireccion")]
        public async Task<IActionResult> ComboDireccion()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboDireccion _response = new ComboDireccion();
            try
            {

                _response = await _business.ComboDireccion();

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

        [HttpGet("ComboActividadEconomica")]
        public async Task<IActionResult> ComboActividadEconomica()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboActividadEconomica _response = new ComboActividadEconomica();
            try
            {

                _response = await _business.ComboActividadEconomica();

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

        [HttpGet("ComboReferencia")]
        public async Task<IActionResult> ComboReferencia()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboReferencia _response = new ComboReferencia();
            try
            {

                _response = await _business.ComboReferencia();

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

        [HttpGet("ComboTelefono")]
        public async Task<IActionResult> ComboTelefono()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboTelefono _response = new ComboTelefono();
            try
            {

                _response = await _business.ComboTelefono();

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

        [HttpGet("ComboCaedec")]
        public async Task<IActionResult> ComboCaedec()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboCaedec _response = new ComboCaedec();
            try
            {

                _response = await _business.ComboCaedec();

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

        [HttpGet("ComboAutorizacionesEspeciales")]
        public async Task<IActionResult> ComboAutorizacionesEspeciales()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboAutorizacionesEspeciales _response = new ComboAutorizacionesEspeciales();
            try
            {

                _response = await _business.ComboAutorizacionesEspeciales();

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

        [HttpGet("ComboTipoFechaAM")]
        public async Task<IActionResult> ComboTipoFechaAM()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboTipoFechaAM _response = new ComboTipoFechaAM();
            try
            {

                _response = await _business.ComboTipoFechaAM();

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

        [HttpGet("ComboDocumentosEntregados")]
        public async Task<IActionResult> ComboDocumentosEntregados()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboDocumentosEntregados _response = new ComboDocumentosEntregados();
            try
            {

                _response = await _business.ComboDocumentosEntregados();

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

        [HttpGet("ComboScoring")]
        public async Task<IActionResult> ComboScoring()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboScoring _response = new ComboScoring();
            try
            {

                _response = await _business.ComboScoring();

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

        [HttpGet("ComboDocumentoEntrevista")]
        public async Task<IActionResult> ComboEntrevista()
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboEntrevista _response = new ComboEntrevista();
            try
            {

                _response = await _business.ComboEntrevista();

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
