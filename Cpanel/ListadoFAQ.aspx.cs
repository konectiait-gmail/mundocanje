using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MundoCanjeWeb.Cpanel.Clases;
using System.Threading.Tasks;
using System.Web.Services;

namespace MundoCanjeWeb.Cpanel
{
    public partial class ListadoFAQ : System.Web.UI.Page
    {
        public void IniciarControles()
        {
            HdnIdFAQ.Value = "0";
            

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                try
                {
                    IniciarControles();
                    GetDetalleGrilla();                    
                }
                catch (Exception ex)
                {
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
        }
        
        public void GetDetalleGrilla() 
        {
            ApiServices objApi = new ApiServices();
            string Request = "{}";
            HttpResponseMessage response=objApi.CallService("Preguntas_Frecuentes", Request, ApiServices.TypeMethods.GET).Result;
            
            if (response.IsSuccessStatusCode)
            {
                //resp = await response.Content.ReadAsAsync();
                string Respuesta = response.Content.ReadAsStringAsync().Result;
                List<Models.Preguntas_Frecuentes> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Preguntas_Frecuentes>>(Respuesta);
                string data = "";
                foreach (var item in obj)
                {

                    data += "<tr> ";
                    data += "<td> " + item.Id + " </td>  ";
                    data += "<td> " + item.Pregunta + " </td>  ";
                    data += "<td> " + item.Orden + " </td> ";
                    data += "<td style='font-size: x-large'>  ";
                    //data += "<a style='cursor:pointer' onclick='GetEditId(\"" + item.Pregunta + "\");return false' ><i class='mdi mdi-lead-pencil'></i><span class='count-symbol bg-warning'></span></a> ";
                    data += "<a style='cursor:pointer' onclick='GetEditId("+ item.Id + ");return false' ><i class='mdi mdi-lead-pencil'></i><span class='count-symbol bg-warning'></span></a> ";
                    data += "<a style='cursor:pointer' onclick='SetDeleteId(" + item.Id + ");return false' ><i class='mdi mdi-delete-outline'></i><span class='count-symbol bg-warning'></span></a> ";
                    data += "</td>	</tr> ";

                }
                LitFAQ.Text = data;
            }
            else
            {
                string RespuestaService = response.Content.ReadAsStringAsync().Result;
                ApiServices.Response obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiServices.Response>(RespuestaService);
                RespuestaService = response.StatusCode + " - " + obj.Error.message;
            }
            //return ListaOrdenes;
        }

        [WebMethod]
        public static List<Models.Preguntas_Frecuentes> IniModalEdit(int Id)
        {
            List<Models.Preguntas_Frecuentes> lista = new List<Models.Preguntas_Frecuentes>();
            try
            {
                if (Id>0)
                {
                    //Int64 IdFAQ = Convert.ToInt64(Id);
                    ApiServices objApi = new ApiServices();
                    string Request = "{}";
                    HttpResponseMessage response = objApi.CallService("Preguntas_Frecuentes/" + Id, Request, ApiServices.TypeMethods.GET).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //resp = await response.Content.ReadAsAsync();
                        string Respuesta = response.Content.ReadAsStringAsync().Result;
                        Models.Preguntas_Frecuentes obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Preguntas_Frecuentes>(Respuesta);
                        if (obj != null)
                        {
                            lista.Add(new Models.Preguntas_Frecuentes
                            {
                                Id = obj.Id,
                                Orden = obj.Orden,
                                Pregunta = obj.Pregunta,
                                Respuesta = obj.Respuesta
                            });
                        }
                        
                    }

                }
            }
            catch
            {
                int sss = 0;
            }
            return lista;


        }

        [WebMethod]
        public static int Grabar(Models.Preguntas_Frecuentes preg, int EsNuevo)
        {
            try
            {
                if (preg != null)
                {
                    ApiServices objApi = new ApiServices();
                    HttpResponseMessage response = null;
                    string Request = Newtonsoft.Json.JsonConvert.SerializeObject(preg);
                    if(EsNuevo==0)
                    {
                        response = objApi.CallService("Preguntas_Frecuentes/" + preg.Id, Request, ApiServices.TypeMethods.PUT).Result;
                    }
                    else
                    {
                        response = objApi.CallService("Preguntas_Frecuentes", Request, ApiServices.TypeMethods.POST).Result;
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                    
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return 0;
            }

        }

        [WebMethod]
        public static int Eliminar(string idPreg)
        {
            try
            {
                if (!string.IsNullOrEmpty(idPreg))
                {
                    ApiServices objApi = new ApiServices();
                    HttpResponseMessage response = null;
                    string Request = "{}";
                    response = objApi.CallService("Preguntas_Frecuentes/" + idPreg, Request, ApiServices.TypeMethods.DELETE).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return 0;
            }

        }
    }

    
}