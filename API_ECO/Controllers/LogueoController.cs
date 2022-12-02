using Business_Eco;
using Common_Eco;
using Entidades_Eco;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ECO.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LogueoController : Controller
    {
        [HttpPost("LogueoUser")]
        public ResponseUser InsertContrato(RequestUser request)
        {
            BussinessPendientes _conector = new BussinessPendientes();
            ResponseUser _response = new ResponseUser();
            try
            {

                _response = _conector.Logueo_User(request);

                return _response;

            }
            catch (Exception ex)
            {
                _response.code = Configuraciones.GetCode("ERROR_FATAL");
                _response.message = "ERROR INESPERADO EN LA PETICION";
            }
            return _response;
        }
    }
}
