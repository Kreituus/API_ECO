using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Common_Eco
{
    public class Configuraciones
    {
        private static Configuraciones _appSettings;
        public string appSettingValue { get; set; }

        //public static string ObtieneParametroConfig(string Name)
        //{
        //    string resultado = string.Empty;
        //    try
        //    {
        //        resultado = ConfigurationManager.AppSettings.Get(Name);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return resultado;
        //}

        public static string AppSetting(string ruta,string Key)
        {
            _appSettings = GetCurrentSettings(ruta,Key);
            return _appSettings.appSettingValue;
        }

        public Configuraciones(IConfiguration config, string Key)
        {
            this.appSettingValue = config.GetValue<string>(Key);
        }
        public static Configuraciones GetCurrentSettings(string ruta,string Key)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var settings = new Configuraciones(configuration.GetSection(ruta), Key);

            return settings;
        }
        public static string ObtieneAppSettings(string ruta,string Name)
        {
            string resultado = string.Empty;
            try
            {

                _appSettings = GetCurrentSettings(ruta,Name);
                resultado = _appSettings.appSettingValue;

            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public static string GetCode(string typeIN)
        {
            string result = string.Empty;
            if (typeIN.Equals("OK"))
                result = "200";
            else if (typeIN.Equals("CREATED"))
                result = "201";
            else if (typeIN.Equals("NO_CONTENT"))
                result = "203";
            else if (typeIN.Equals("NO_MODIFIED"))
                result = "304";
            else if (typeIN.Equals("NO_INSTRUC"))
                result = "305";
            else if (typeIN.Equals("SUP_INTENTOS"))
                result = "306";
            else if (typeIN.Equals("UNAUTHORIZED"))
                result = "401";
            else if (typeIN.Equals("UNPROCESABLE_ENTITY"))
                result = "422";
            else if (typeIN.Equals("ERROR_FATAL"))
                result = "500";
            else if (typeIN.Equals("SERVICE_UNAVARIABLE"))
                result = "503";
            else
                result = "500";
            return result;
        }
        public static string GetMessage(string typeIN)
        {
            string result = string.Empty;
            if (typeIN.Equals("OK"))
                result = "La solicitud se ejecutó correctamente.";
            else if (typeIN.Equals("CREATED"))
                result = "El registro  se creó correctamente.";
            else if (typeIN.Equals("NO_CONTENT"))
                result = "La solicitud se ejecutó correctamente pero no afecto ningún registro.";
            else if (typeIN.Equals("NO_MODIFIED"))
                result = "No se pudo actualizar el registro solicitado.";
            else if (typeIN.Equals("UNAUTHORIZED"))
                result = "Canal no autorizado.";
            else if (typeIN.Equals("UNPROCESABLE_ENTITY"))
                result = "La información enviada no puede ser procesada.";
            else if (typeIN.Equals("ERROR_FATAL"))
                result = "Ocurrió un error comuníquese con el administrador del sistema.";
            else if (typeIN.Equals("SERVICE_UNAVARIABLE"))
                result = "El servicio se encuentra temporalmente fuera de servicio.";
            else
                result = "Ocurrió un error comuníquese con el administrador del sistema.";
            return result;
        }
    }
}
