using Common_Eco;
using Entidades_Eco;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Eco
{
    public class DataConsultaServicio
    {
        public ResponseUser Login(RequestUser request)
        {
            ResponseUser respuesta = new ResponseUser();
            User user = new User();
            respuesta.Usuario = new User();
            string endpoint = Configuraciones.ObtieneAppSettings("ApplicationSettings", "LOGUEO") + request.Usuario;
            //respuesta = ManagerJson.DeserializeStream<ResponseUser>(ManagerJson.sendGenericPOST(endpoint, ""));
            //respuesta = ManagerJson.DeserializeStream<ResponseUser>(ManagerJson.sendGET(endpoint));
            //user = ManagerJson.DeserializeStream<User>(ManagerJson.sendGET(endpoint));
            respuesta.Usuario.codigoAgenda = 869572;
            respuesta.Usuario.codigoSFI = 0;
            respuesta.Usuario.numDocIdentidad = "4797064";
            respuesta.Usuario.expedido = "";
            respuesta.Usuario.apellidoPaterno = "NAVARRO";
            respuesta.Usuario.apellidoMaterno = "VALENZUELA";
            respuesta.Usuario.nombres = "PAMELA ALEJANDRA";
            respuesta.Usuario.genero = "FEMENINO";
            respuesta.Usuario.fechaNacimiento = "27/09/1984";
            respuesta.Usuario.nacionalidad = "";
            respuesta.Usuario.fechaIngreso = "14/07/2021";
            respuesta.Usuario.cargo = "ANALISTA DE PROYECTOS";
            respuesta.Usuario.sucursal = "OFICINA NACIONAL";
            respuesta.Usuario.agencia = "OFICINA NACIONAL";
            respuesta.Usuario.areaOrganizacional = null;
            respuesta.Usuario.estado = "ACTIVO";
            return respuesta;
        }

        //public ModeloFuncionario ObtieneUsuarios(RequestUser request)
        //{
        //    string endpoint = Configuraciones.ObtieneAppSettings("LOGUEO") + request.Usuario;
        //    var funcionario = new RestClient(endpoint + request.Usuario);
        //    funcionario.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        //    funcionario.Timeout = -1;
        //    var obtiene = new RestRequest(Method.GET);
        //    IRestResponse resp = funcionario.Execute(obtiene);
        //    var json = JsonConvert.DeserializeObject<ModeloFuncionario>(resp.Content.Substring(1, resp.Content.Length - 2));
        //    return json;
        //}
    }
}
