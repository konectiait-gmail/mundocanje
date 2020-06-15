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
    [EnableCors(origins: "http://mundocanje.tk,http://localhost:51199,http://localhost:8100,http://localhost:8000", headers: "*", methods: "*")]
    //[EnableCors(origins: "http://w1021886.ferozo.com", headers: "*", methods: "*")]
    public class NotificationPushController : ApiController
    {
        


        // GET: api/NotificationPush
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/NotificationPush/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/NotificationPush
        public IHttpActionResult Post(BodyNotificationPush objToken)
        {
            try
            {
                string URL = "https://fcm.googleapis.com/fcm/send";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=AIzaSyAqMuAUGZnw2s72jvQ_HJEVnVkXx-jBeRM");

                ReqNotificationPush.Notification objNot = new ReqNotificationPush.Notification()
                {
                    body = objToken.Titulo,
                    title = objToken.Descripcion,
                    sound = "default",
                    badge = "8",
                    color = "green",
                    icon = "https://www.e3ecommerce.com.ar/e3ecommerce.png"
                };
                ReqNotificationPush.Data objData = new ReqNotificationPush.Data()
                {
                    body = "Body of Your Notification in Data",
                    title = "Title of Your Notification in Title",
                    content_available = true,
                    key_1 = "Value for key_1",
                    key_2 = "Value for key_2",
                };

                ReqNotificationPush objNotificationPush = new ReqNotificationPush()
                {
                    to = "/topics/mundocanje",
                    collapse_key = "type_a",
                    notification = objNot,
                    data = objData

                };

                string Request = Newtonsoft.Json.JsonConvert.SerializeObject(objNotificationPush);

                HttpContent content = new StringContent(Request, UTF8Encoding.UTF8, "application/json");                
                HttpRequestMessage reqMessage = null;
                HttpResponseMessage response = client.PostAsync(new Uri(URL), content).Result;




                if (response.IsSuccessStatusCode)
                {

                    string Respuesta = response.Content.ReadAsStringAsync().Result;
                    RespNotificationPush obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RespNotificationPush>(Respuesta);
                    return CreatedAtRoute("DefaultApi", new { id = 1 }, obj);
                    //return Ok(obj);

                }
                else
                {
                    return BadRequest("INSERTION ERROR");
                }


            }
            catch (System.Exception ex)
            {
                return null;
                //throw ex;
            }
        }

        // PUT: api/NotificationPush/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/NotificationPush/5
        public void Delete(int id)
        {
        }
    }

    public class BodyNotificationPush
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }        
    }
    public class ReqNotificationPush
    {
        public string to { get; set; }
        public string collapse_key { get; set; }
        public Notification notification { get; set; }
        public Data data { get; set; }

        public class Notification
        {
            public string body { get; set; }
            public string title { get; set; }
            public string sound { get; set; }
            public string badge { get; set; }
            public string color { get; set; }
            public string icon { get; set; }
        }
        public class Data
        {
            public string body { get; set; }
            public string title { get; set; }
            public bool content_available { get; set; }
            public string key_1 { get; set; }
            public string key_2 { get; set; }
        }
    }
    public class RespNotificationPush
    {
        public string message_id { get; set; }
        
    }
}
