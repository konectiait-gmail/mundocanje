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
    public partial class ListadoCanjes : System.Web.UI.Page
    {
        public void IniciarControles()
        {
            HdnIdUsuario.Value = "0";


        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    try
                    {
                        string idEstado = Request.QueryString["Est"];
                        IniciarControles();

                        if (idEstado != null)
                        {
                            GetDetalleGrilla(Convert.ToInt32(idEstado));
                        }
                        else
                            GetDetalleGrilla(1);
                    }
                    catch (Exception ex)
                    {
                        //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }
            }
        }

        public void GetDetalleGrilla(int idEstado)
        {
            switch (idEstado)
            {
                case 1: //Pendiente                        
                    LblTituloGrilla.Text = "Listado de Canjes Pendientes";
                    break;
                case 2: //Cancelados                       
                    LblTituloGrilla.Text = "Listado de Canjes Cancelados";
                    break;
                case 3: //Iniciados                  
                    LblTituloGrilla.Text = "Listado de Canjes Iniciados";
                    break;
                case 4: //Confirmados                        
                    LblTituloGrilla.Text = "Listado de Canjes Confirmados";
                    break;
            }

            ApiServices objApi = new ApiServices();
            string Request = "{}";
            HttpResponseMessage response=objApi.CallService("Pedidos/CanjesByState/" + idEstado, Request, ApiServices.TypeMethods.GET).Result;
            
            if (response.IsSuccessStatusCode)
            {
                string Respuesta = response.Content.ReadAsStringAsync().Result;
                List<Models.PedidoViewModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.PedidoViewModel>>(Respuesta);
                string data = "";
                
                foreach (var item in obj)
                {
                    
                    data += "<tr> ";
                    data += "<td> <img src='" + item.Img_Usuario + "'> " + item.Nombre_Usuario + " </td>  ";

                    data += "<td> " + item.Nombre_Producto + " </td> ";
                    data += "<td><label class='badge badge-gradient-success'>" + item.Desc_Estado+ "</label></td>  ";
                    data += "<td> " + item.FechaPedido + " </td> ";
                    data += "<td> " + item.Id + " </td> ";
                    data += "<td style='font-size: x-large'>  ";
                    if(idEstado==1)
                        data += "<a style='cursor:pointer' onclick='VerDetalleProducto(" + item.Id + ");return false' ><i class='mdi mdi-magnify'></i><span class='count-symbol bg-warning'></span></a> ";
                    else
                        data += "<a style='cursor:pointer' onclick='VerDetalle(" + item.Id + ");return false' ><i class='mdi mdi-magnify'></i><span class='count-symbol bg-warning'></span></a> ";

                    data += "</td></td>	</tr> ";

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
        public static List<Models.Usuarios> IniModalEdit(string Id)
        {
            List<Models.Usuarios> lista = new List<Models.Usuarios>();
            try
            {
                if (Id != "0")
                {
                    Int64 IdUsuario = Convert.ToInt64(Id);
                    ApiServices objApi = new ApiServices();
                    string Request = "{}";
                    HttpResponseMessage response = objApi.CallService("usuarios/"+ IdUsuario, Request, ApiServices.TypeMethods.GET).Result;

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
                                Imagen = obj.Imagen,
                                token=obj.token,
                                IdTipo=obj.IdTipo,
                                Estado=obj.Estado,
                                Lat=obj.Lat,
                                Long=obj.Long,
                                Puntuacion=obj.Puntuacion,
                                IdPlan= obj.IdPlan
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
        public static int Grabar(Models.Usuarios usuario, int EsNuevo)
        {
            try
            {
                if (usuario != null)
                {
                    ApiServices objApi = new ApiServices();
                    HttpResponseMessage response = null;
                    string Request = Newtonsoft.Json.JsonConvert.SerializeObject(usuario);
                    if(EsNuevo==0)
                    {
                        response = objApi.CallService("usuarios/"+ usuario.Id, Request, ApiServices.TypeMethods.PUT).Result;
                    }
                    else
                    {
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
        public static int Eliminar(int IdUsuario)
        {
            try
            {
                if (IdUsuario > 0)
                {
                    ApiServices objApi = new ApiServices();
                    HttpResponseMessage response = null;
                    string Request = "{}";
                    response = objApi.CallService("usuarios/" + IdUsuario, Request, ApiServices.TypeMethods.DELETE).Result;

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
        public static int GrabarCambioEstado(int IdUsuario, int OnOff)
        {
            try
            {
                if (IdUsuario > 0)
                {
                    ApiServices objApi = new ApiServices();
                    string Request = "{}";
                    HttpResponseMessage response = objApi.CallService("usuarios/" + IdUsuario, Request, ApiServices.TypeMethods.GET).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string Respuesta = response.Content.ReadAsStringAsync().Result;
                        Models.Usuarios objUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Usuarios>(Respuesta);
                        if (objUsuario != null)
                        {
                            objUsuario.Estado = OnOff;
                            response = null;
                            
                            string Request2 = Newtonsoft.Json.JsonConvert.SerializeObject(objUsuario, new Newtonsoft.Json.JsonSerializerSettings()
                            {
                                PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects,
                                Formatting = Newtonsoft.Json.Formatting.Indented
                            });
                            response = objApi.CallService("usuarios/" + objUsuario.Id, Request2, ApiServices.TypeMethods.PUT).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                return 1;
                            }                            
                        }
                        return 0;
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

        protected void DlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}