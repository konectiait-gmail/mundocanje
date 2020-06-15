using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MundoCanjeWeb.Cpanel.Clases
{
    
    public class ApiServices
    {
        public class Response
        {
            public ErrorResponse Error { get; set; }

        }
        public class ErrorResponse
        {
            public string code { get; set; }
            public string message { get; set; }
        }

        public enum TypeMethods
        {
            GET,            
            POST,
            PUT,
            DELETE
        }


        public async Task<HttpResponseMessage> CallService(string Service, string Request, TypeMethods Method)
        {
            try
            {
                string URL = System.Configuration.ConfigurationManager.AppSettings["APIUrl"] + Service;
                //string Token = DatosFranquicia.Token;
                //string Key = DatosFranquicia.Key;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("X-VTEX-API-AppToken", Token);
                //client.DefaultRequestHeaders.Add("X-VTEX-API-AppKey", Key);
                HttpContent content = new StringContent(Request, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                HttpRequestMessage reqMessage = null;
                //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Ssl3;

                switch (Method)
                {
                    case TypeMethods.GET:
                        response = await client.GetAsync(new Uri(URL), HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                        break;                    
                   
                    case TypeMethods.POST:
                        response = client.PostAsync(new Uri(URL), content).Result;
                        break;                    
                    case TypeMethods.PUT:
                        response = client.PutAsync(new Uri(URL), content).Result;

                        break;                    
                    case TypeMethods.DELETE:                        
                        response = client.DeleteAsync(new Uri(URL)).Result;
                        break;
                    default:
                        break;
                }

                return response;

            }
            catch (System.Exception ex)
            {
                return null;
                //throw ex;
            }

        }

       


    }

    
}