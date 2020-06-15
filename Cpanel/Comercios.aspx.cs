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
    public partial class Comercios : System.Web.UI.Page
    {
        public void IniciarControles()
        {
            HdnIdComercio.Value = "0";           

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
            HttpResponseMessage response=objApi.CallService("usuarios/GetUsuariosByTipo/2", Request, ApiServices.TypeMethods.GET).Result;
            
            if (response.IsSuccessStatusCode)
            {
                string Respuesta = response.Content.ReadAsStringAsync().Result;
                List<Models.Usuarios> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Usuarios>>(Respuesta);
                string data = "";
                foreach (var item in obj)
                {

                    data += "<tr> ";
                    data += "<td> " + item.Id + " </td> ";
                    data += "<td> " + item.Nombre + " </td>  ";
                    data += "<td> " + item.Direccion + " </td>  ";
                    data += "<td><img src='"+ item.Imagen+ "' style='width:200px !important;border-radius:0px !important;height: auto !important;' ></td> ";
                    data += "<td style='font-size: x-large'>  ";
                    data += "<a style='cursor:pointer' onclick='GetEditId(" + item.Id + ");return false' ><i class='mdi mdi-lead-pencil'></i><span class='count-symbol bg-warning'></span></a> ";
                    data += "<a style='cursor:pointer' onclick='SetDeleteId(" + item.Id + ");return false' ><i class='mdi mdi-delete-outline'></i><span class='count-symbol bg-warning'></span></a> ";
                    data += "</td>	</tr> ";

                }
                LitComercios.Text = data;
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
        public static List<Models.Usuarios> IniModalEdit(string Id)
        {
            List<Models.Usuarios> lista = new List<Models.Usuarios>();
            try
            {
                if (Id != "0")
                {
                    Int64 IdComercio = Convert.ToInt64(Id);
                    ApiServices objApi = new ApiServices();
                    string Request = "{}";
                    HttpResponseMessage response = objApi.CallService("usuarios/"+ IdComercio, Request, ApiServices.TypeMethods.GET).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //resp = await response.Content.ReadAsAsync();
                        string Respuesta = response.Content.ReadAsStringAsync().Result;
                        Models.Usuarios obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Usuarios>(Respuesta);
                        if (obj != null)
                        {
                            lista.Add(new Models.Usuarios
                            {
                                Id = obj.Id,
                                Nombre = obj.Nombre,
                                Direccion=obj.Direccion,
                                Telefono=obj.Telefono,
                                Mail=obj.Mail,
                                Cuit=obj.Cuit,
                                Razon_Social=obj.Razon_Social,
                                Imagen = obj.Imagen,
                                token=obj.token,
                                IdTipo=obj.IdTipo,
                                Estado=obj.Estado,
                                Lat=obj.Lat,
                                Long=obj.Long,
                                Puntuacion=obj.Puntuacion
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
        public static int Grabar(Models.Usuarios comercio, int EsNuevo)
        //public static int Grabar(int EsNuevo)
        //public static int Grabar(string Nombre, string Direccion, int Telefono, string Mail, Int64 Cuit, string RazonSocial, string Imagen, int EsNuevo)
        {

           

            //Models.Usuarios comercio = new Models.Usuarios();
            try
            {
                if (comercio != null)
                {
                    ApiServices objApi = new ApiServices();
                    HttpResponseMessage response = null;                    
                    if(EsNuevo==0)
                    {
                        string Request = Newtonsoft.Json.JsonConvert.SerializeObject(comercio);
                        response = objApi.CallService("usuarios/"+ comercio.Id, Request, ApiServices.TypeMethods.PUT).Result;
                    }
                    else
                    {
                        comercio.Fecha_Alta = DateTime.Now;
                        string Request = Newtonsoft.Json.JsonConvert.SerializeObject(comercio);
                        response = objApi.CallService("usuarios", Request, ApiServices.TypeMethods.POST).Result;
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
        public static int Eliminar(int idComercio)
        {
            try
            {
                if (idComercio > 0)
                {
                    ApiServices objApi = new ApiServices();
                    HttpResponseMessage response = null;
                    string Request = "{}";
                    response = objApi.CallService("usuarios/" + idComercio, Request, ApiServices.TypeMethods.DELETE).Result;

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