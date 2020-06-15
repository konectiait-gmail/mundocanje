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
    public partial class ListadoTerminos : System.Web.UI.Page
    {
        public void IniciarControles()
        {
            HdnIdTerminos.Value = "0";
            

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
            HttpResponseMessage response=objApi.CallService("Terminos", Request, ApiServices.TypeMethods.GET).Result;
            
            if (response.IsSuccessStatusCode)
            {
                //resp = await response.Content.ReadAsAsync();
                string Descripcion = response.Content.ReadAsStringAsync().Result;
                List<Models.Terminos> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Terminos>>(Descripcion);
                string data = "";
                foreach (var item in obj)
                {

                    data += "<tr> ";
                    data += "<td> " + item.Id + " </td>  ";
                    data += "<td> " + item.Titulo + " </td>  ";
                    if(item.Descripcion.Length>50)
                        data += "<td> " + item.Descripcion.Substring(0, 50)  + "... </td> ";
                    else
                        data += "<td> " + item.Descripcion+ "</td> ";

                    data += "<td style='font-size: x-large'>  ";
                    //data += "<a style='cursor:pointer' onclick='GetEditId(\"" + item.Titulo + "\");return false' ><i class='mdi mdi-lead-pencil'></i><span class='count-symbol bg-warning'></span></a> ";
                    data += "<a style='cursor:pointer' onclick='GetEditId("+ item.Id + ");return false' ><i class='mdi mdi-lead-pencil'></i><span class='count-symbol bg-warning'></span></a> ";
                    data += "<a style='cursor:pointer' onclick='SetDeleteId(" + item.Id + ");return false' ><i class='mdi mdi-delete-outline'></i><span class='count-symbol bg-warning'></span></a> ";
                    data += "</td>	</tr> ";

                }
                LitTerminos.Text = data;
            }
            else
            {
                string DescripcionService = response.Content.ReadAsStringAsync().Result;
                ApiServices.Response obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiServices.Response>(DescripcionService);
                DescripcionService = response.StatusCode + " - " + obj.Error.message;
            }
            //return ListaOrdenes;
        }

        [WebMethod]
        public static List<Models.Terminos> IniModalEdit(int Id)
        {
            List<Models.Terminos> lista = new List<Models.Terminos>();
            try
            {
                if (Id>0)
                {
                    //Int64 IdFAQ = Convert.ToInt64(Id);
                    ApiServices objApi = new ApiServices();
                    string Request = "{}";
                    HttpResponseMessage response = objApi.CallService("Terminos/" + Id, Request, ApiServices.TypeMethods.GET).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //resp = await response.Content.ReadAsAsync();
                        string Descripcion = response.Content.ReadAsStringAsync().Result;
                        Models.Terminos obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Terminos>(Descripcion);
                        if (obj != null)
                        {
                            lista.Add(new Models.Terminos
                            {
                                Id = obj.Id,
                                Titulo = obj.Titulo,
                                Descripcion = obj.Descripcion
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
        public static int Grabar(Models.Terminos preg, int EsNuevo)
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
                        response = objApi.CallService("Terminos/" + preg.Id, Request, ApiServices.TypeMethods.PUT).Result;
                    }
                    else
                    {
                        response = objApi.CallService("Terminos", Request, ApiServices.TypeMethods.POST).Result;
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
                    response = objApi.CallService("Terminos/" + idPreg, Request, ApiServices.TypeMethods.DELETE).Result;

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