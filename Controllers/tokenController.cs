using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MundoCanjeWeb.Cpanel.Clases;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace MundoCanjeWeb.Controllers
{
    //[EnableCors(origins: "http://mundocanje.tk,http://localhost:51199,http://localhost:8100,http://localhost:8000", headers: "*", methods: "*")]
    [EnableCors(origins: "http://w1021886.ferozo.com", headers: "*", methods: "*")]
    //[EnableCors(origins: "https://w1021886.ferozo.com,http://w1021886.ferozo.com,http://localhost:51199,http://localhost:8100,http://localhost:8000", headers: "*", methods: "*")]
    public class tokenController : ApiController
    {
        // GET: api/token
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/token/5
        public string Get(int id)
        {
            return "value";
        }

        

        // POST: api/token
        public IHttpActionResult Post(ReqToken objToken)
        {
            try
            {
                string URL = "http://devsiempre.e3ecommerce.com/Api/token";
                
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("X-VTEX-API-AppToken", Token);
                //client.DefaultRequestHeaders.Add("X-VTEX-API-AppKey", Key);
                /*
                ReqToken objToken = new ReqToken()
                {
                    username = "leonardo@e3ecommerce.com.ar",
                    password = "123456",
                    grant_type = "siemprebien"
                };
                */
                string Request = Newtonsoft.Json.JsonConvert.SerializeObject(objToken);
                
                HttpContent content = new StringContent(Request, UTF8Encoding.UTF8, "application/json");
                
                HttpRequestMessage reqMessage = null;
                //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Ssl3;


                HttpResponseMessage response = client.PostAsync(new Uri(URL), content).Result;

                if (response.IsSuccessStatusCode)
                {
                    
                    string Respuesta = response.Content.ReadAsStringAsync().Result;
                    RespToken obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RespToken>(Respuesta);
                    return Ok(obj);
                    
                }
                else
                {                    
                    return BadRequest("LOGIN INCORRECT");
                }
                    

            }
            catch (System.Exception ex)
            {
                return null;
                //throw ex;
            }
        }

        // PUT: api/token/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/token/5
        public void Delete(int id)
        {
        }
    }

    public class ReqToken
    {
        public string username { get; set; }
        public string password { get; set; }
        public string grant_type { get; set; }
    }
    public class RespToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }


    
   
}
