using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_Eco
{
    public class UserModel
    {
        
    }
    
    public class RequestUser
    {
        public string Usuario { get; set; }
    }
    public class ResponseUser  : RespuestaGenerica
    {
        public User Usuario { get; set; }
    }
    public class User
    {
        public int codigoAgenda { get; set; }
        public int codigoSFI { get; set; }
        public string numDocIdentidad { get; set; }
        public string expedido { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string nombres { get; set; }
        public string genero { get; set; }
        public string fechaNacimiento { get; set; }
        public string nacionalidad { get; set; }
        public string fechaIngreso { get; set; }
        public string cargo { get; set; }
        public string sucursal { get; set; }
        public string agencia { get; set; }
        public string areaOrganizacional { get; set; }
        public string estado { get; set; }
    }
}
