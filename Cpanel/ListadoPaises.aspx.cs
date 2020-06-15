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
    public partial class ListadoPaises : System.Web.UI.Page
    {
        public void IniciarControles()
        {
            HdnIdPais.Value = "0";
            
            //LblIdPais.Text = "";
            //TxbDescripcion.Text = "";
            //TxbDirecGMap.Text = "";
            //TxbProvincia.Text = "";

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
            HttpResponseMessage response=objApi.CallService("Paises", Request, ApiServices.TypeMethods.GET).Result;
            
            if (response.IsSuccessStatusCode)
            {
                string RespuestaVtex = response.Content.ReadAsStringAsync().Result;
                List<Models.Paises> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Paises>>(RespuestaVtex);
                string data = "";
                foreach (var item in obj)
                {

                    data += "<tr> ";
                    data += "<td> " + item.Id + " </td> ";
                    data += "<td> " + item.Nombre + " </td>  ";                    
                    data += "<td style='font-size: x-large'>  ";
                    data += "<a style='cursor:pointer' onclick='GetEditId(" + item.Id + ");return false' ><i class='mdi mdi-lead-pencil'></i><span class='count-symbol bg-warning'></span></a> ";
                    data += "<a style='cursor:pointer' onclick='SetDeleteId(" + item.Id + ");return false' ><i class='mdi mdi-delete-outline'></i><span class='count-symbol bg-warning'></span></a> ";
                    data += "</td>	</tr> ";

                }
                LitGrilla.Text = data;
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
        public static List<Models.Paises> IniModalEdit(string Id)
        {
            List<Models.Paises> lista = new List<Models.Paises>();
            try
            {
                if (Id != "0")
                {
                    Int64 IdPais = Convert.ToInt64(Id);
                    ApiServices objApi = new ApiServices();
                    string Request = "{}";
                    HttpResponseMessage response = objApi.CallService("Paises/"+ IdPais, Request, ApiServices.TypeMethods.GET).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //resp = await response.Content.ReadAsAsync();
                        string Respuesta = response.Content.ReadAsStringAsync().Result;
                        Models.Paises obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Paises>(Respuesta);
                        if (obj != null)
                        {
                            lista.Add(new Models.Paises
                            {
                                Id = obj.Id,
                                Nombre = obj.Nombre
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
        public static int Grabar(Models.Paises Pais, int EsNuevo)
        {
            try
            {
                if (Pais != null)
                {
                    ApiServices objApi = new ApiServices();
                    HttpResponseMessage response = null;
                    string Request = Newtonsoft.Json.JsonConvert.SerializeObject(Pais);
                    if(EsNuevo==0)
                    {
                        response = objApi.CallService("Paises/"+ Pais.Id, Request, ApiServices.TypeMethods.PUT).Result;
                    }
                    else
                    {
                        response = objApi.CallService("Paises", Request, ApiServices.TypeMethods.POST).Result;
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
        public static int Eliminar(int IdPais)
        {
            try
            {
                if (IdPais > 0)
                {
                    ApiServices objApi = new ApiServices();
                    HttpResponseMessage response = null;
                    string Request = "{}";
                    response = objApi.CallService("Paises/" + IdPais, Request, ApiServices.TypeMethods.DELETE).Result;

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