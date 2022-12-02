using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common_Eco
{
    public static class ManagerJson
    {
        public static void SaveJson(string path, string jsonIN)
        {
            System.IO.File.WriteAllText(path, jsonIN);
        }

        public static string GetJson(Object objectIN)
        {

            MemoryStream strmemo = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Object));
            serializer.WriteObject(strmemo, objectIN);
            string aux = Encoding.Default.GetString(strmemo.ToArray());
            return aux;
        }

        private static T Deserialize<T>(string json)
        {
            var instance = Activator.CreateInstance<T>();
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(instance.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        public static string SerializeObject<T>(this T objectIN)
        {
            using (var ms = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(objectIN.GetType());
                serializer.WriteObject(ms, objectIN);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static Stream SerializeObjectStream<T>(this T objectRequest)
        {
            using (var ms = new MemoryStream())
            {
                DataContractJsonSerializer serializerGen = new DataContractJsonSerializer(objectRequest.GetType());
                serializerGen.WriteObject(ms, objectRequest);
                return ms;
            }
        }


        public static Stream sendGenericPOST<T>(string strEndPointIN, T objectRequest)
        {
            using (var client = new WebClient())
            {
                using (var ms = new MemoryStream())
                {
                    DataContractJsonSerializer serializerGen = new DataContractJsonSerializer(objectRequest.GetType());
                    serializerGen.WriteObject(ms, objectRequest);
                    client.Headers["Content-type"] = "application/json";

                    


                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.UseDefaultCredentials = true;
                    
                    byte[] byteResult = client.UploadData(strEndPointIN, "POST", ms.ToArray());
                    Stream objectRsponse = new MemoryStream(byteResult);
                    return objectRsponse;
                }
            }


        }


        public static Stream sendPOST(string strEndPointIN, MemoryStream objectRequest)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) =>
            {
                return true;
            };


            using (var client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                byte[] byteResult = client.UploadData(strEndPointIN, "POST", objectRequest.ToArray());
                Stream objectRsponse = new MemoryStream(byteResult);
                return objectRsponse;
            }
        }

        public static Stream sendPOSTSinRequests(string strEndPointIN)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) =>
            {
                return true;
            };


            using (var client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                byte[] byteResult = client.DownloadData(strEndPointIN);
                Stream objectRsponse = new MemoryStream(byteResult);
                return objectRsponse;
            }
        }

        public static Stream sendPOST_Auto(string strEndPointIN, MemoryStream objectRequest, string Token)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) =>
            {
                return true;
            };


            using (var client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Headers["clientId"] = Configuraciones.ObtieneAppSettings("ApplicationSettings", "CLIENT_ID");
                client.Headers["secretId"] = Configuraciones.ObtieneAppSettings("ApplicationSettings", "SECRED_ID");
                client.Headers["refreshToken"] = Token;
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                byte[] byteResult = client.UploadData(strEndPointIN, "POST", objectRequest.ToArray());
                Stream objectRsponse = new MemoryStream(byteResult);
                return objectRsponse;
            }
        }

        public static Stream sendPOST_Token(string strEndPointIN, MemoryStream objectRequest, string Token)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) =>
            {
                return true;
            };


            using (var client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";

                client.Headers["access-token"] = Token;
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                byte[] byteResult = client.UploadData(strEndPointIN, "POST", objectRequest.ToArray());
                Stream objectRsponse = new MemoryStream(byteResult);
                return objectRsponse;
            }
        }



        public static Stream sendGET(string strEndPointIN)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };

            using (var client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                //ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyError) { return true; };
                byte[] byteResult = client.DownloadData(strEndPointIN);
                Stream objectRsponse = new MemoryStream(byteResult);
                return objectRsponse;
            }
        }
        public static Stream sendPOSTAut(string strEndPointIN, MemoryStream objectRequest, string User, string Pass, int timeout)
        {
            using (var client = new GenericWebClient())
            {
                client.Timeout = timeout;
                client.Headers["Content-type"] = "application/json";
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(User + ":" + Pass));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                byte[] byteResult = client.UploadData(strEndPointIN, "POST", objectRequest.ToArray());
                Stream objectRsponse = new MemoryStream(byteResult);
                return objectRsponse;
            }
        }
        public static T DeserializeStream<T>(Stream json)
        {
            var instance = Activator.CreateInstance<T>();
            var serializer = new DataContractJsonSerializer(instance.GetType());
            return (T)serializer.ReadObject(json);
        }

        public static Stream sendPOSTwithToken(string strEndPoint, MemoryStream objectRequest, string Token)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };
            using (var client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json";
                client.Headers["token"] = Token;
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UseDefaultCredentials = true;
                ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyError) { return true; };
                byte[] byteResult = client.UploadData(strEndPoint, "POST", objectRequest.ToArray());
                Stream objectRsponse = new MemoryStream(byteResult);
                return objectRsponse;
            }
        }
    }
    public class GenericWebClient : WebClient
    {
        #region Fields
        private int timeout;
        private int readWriteTimeout;
        #endregion

        #region Properties
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }
        #endregion

        #region Constructors
        public GenericWebClient()
        {
            this.timeout = Convert.ToInt32(TimeSpan.FromSeconds(30).TotalMilliseconds);
            this.readWriteTimeout = Convert.ToInt32(TimeSpan.FromSeconds(60).TotalMilliseconds);
        }

        public GenericWebClient(int timeout, int readWriteTimeout)
        {
            this.timeout = Convert.ToInt32(TimeSpan.FromSeconds(timeout).TotalMilliseconds);
            this.readWriteTimeout = Convert.ToInt32(TimeSpan.FromSeconds(readWriteTimeout).TotalMilliseconds);
        }
        #endregion

        #region Overloads
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            request.Timeout = this.timeout;

            if (request is HttpWebRequest)
            {
                ((HttpWebRequest)request).ReadWriteTimeout = this.readWriteTimeout;
            }

            return request;
        }
        #endregion
    }
}
