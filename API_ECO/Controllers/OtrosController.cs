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
    public class OtrosController : Controller
    {
        private string _operacion;
        private readonly ILogger _logger;
        private IBussinessPendientes _business;
        public OtrosController(ILogger logger, IBussinessPendientes business)
        {
            this._logger = logger;
            this._business = business;
        }
        [HttpPost("BusquedaCaedec")]
        public async Task<IActionResult> BusquedaCaedec([FromBody] BusquedaCaedecRequest request)
        {
            this._operacion = ManagerOperation.GenerateOperation("");
            ComboCaedec _response = new ComboCaedec();
            try
            {
                _response = await _business.BusquedaCaedec(request);
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
