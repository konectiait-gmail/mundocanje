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
    public class deviceController : ApiController
    {
        // GET: api/device
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/device/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/device
        //public IHttpActionResult Post(string hash, [FromBody]ReqDevice objDevice)
        public IHttpActionResult Post(string hash, string OS, string DeviceId)
        {
            try
            {
                //string URL = "http://devsiempre.e3ecommerce.com/Api/device?hash=" + hash;
                string URL = "http://devsiempre.e3ecommerce.com/Api/device?hash="+ hash + "&OS="+ OS+"&DeviceId="+ DeviceId;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //ReqDevice objDevice = new ReqDevice();
                //Hardcodeo para probar
                //objDevice.DeviceId = "qwewqeqwewq";
                //objDevice.OS = "IOS";
                //string Request = Newtonsoft.Json.JsonConvert.SerializeObject(objDevice);
                //HttpContent content = new StringContent(Request, UTF8Encoding.UTF8, "application/json");
                //HttpResponseMessage response = client.PostAsync(new Uri(URL), content).Result;
                HttpResponseMessage response = client.GetAsync(new Uri(URL)).Result;

                if (response.IsSuccessStatusCode)
                {

                    string Respuesta = response.Content.ReadAsStringAsync().Result;
                    RespToken obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RespToken>(Respuesta);
                    return Ok(obj);

                }
                else
                {
                    return BadRequest("DeviceId not set.");
                }


            }
            catch (System.Exception ex)
            {
                return null;
                //throw ex;
            }
        }

        // PUT: api/device/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/device/5
        public void Delete(int id)
        {
        }
    }

    public class ReqDevice
    {
        public string OS { get; set; }
        public string DeviceId { get; set; }
    }
    public class RespDevice
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
