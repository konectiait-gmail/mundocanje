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
    public partial class ListadoDescuentos : System.Web.UI.Page
    {
        public void IniciarControles()
        {
            HdnIdDescuento.Value = "0";
            #region CargarCombo

            ApiServices objApi = new ApiServices();
            string Request = "{}";
            HttpResponseMessage response = objApi.CallService("usuarios/GetUsuariosByTipo/2", Request, ApiServices.TypeMethods.GET).Result;

            if (response.IsSuccessStatusCode)
            {
                string Respuesta = response.Content.ReadAsStringAsync().Result;
                List<Models.Usuarios> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Usuarios>>(Respuesta);
                DlComercio.DataSource = obj;
                DlComercio.DataTextField = "Nombre";
                DlComercio.DataValueField = "Id";
                DlComercio.DataBind();
            }
            else
            {
                string RespuestaService = response.Content.ReadAsStringAsync().Result;
                ApiServices.Response obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiServices.Response>(RespuestaService);
                RespuestaService = response.StatusCode + " - " + obj.Error.message;
            }
            #endregion

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
                    LblTituloGrilla.Text = "Listado de Descuentos Pendientes";
                    break;
                case 4: //Confirmados                        
                    LblTituloGrilla.Text = "Listado de Descuentos Confirmados";
                    break;
            }

            ApiServices objApi = new ApiServices();
            string Request = "{}";
            HttpResponseMessage response=objApi.CallService("Pedidos/DescuentosByState/" + idEstado, Request, ApiServices.TypeMethods.GET).Result;
            
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
                    data += "<td> " + item.CodigoDescuento + " </td> ";
                    data += "<td style='font-size: x-large'>  ";
                    if (idEstado > 1)
                        data += "<a style='cursor:pointer' onclick='VerDetalle(" + item.Id + ");return false' ><i class='mdi mdi-magnify'></i><span class='count-symbol bg-warning'></span></a> ";
                    if(idEstado==1)
                        data += "<a style='cursor:pointer' onclick='SetDeleteId(" + item.Id + ");return false' ><i class='mdi mdi-delete-outline'></i><span class='count-symbol bg-warning'></span></a> ";

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
        public static int Grabar(string Nombre, string Desc, string Codigo, string Imagen, string IdUsuario, string Cantidad)
        {
            try
            {

                string CodDescuento = (string.IsNullOrEmpty(Codigo)) ? new Models.FuncGrales().GenerateRandom(8) : Codigo;
                Models.Productos objProd = new Models.Productos()
                {
                    Nombre = Nombre,
                    Descripcion = Desc,
                    CodigoDescuento = CodDescuento,
                    Imagen = Imagen,
                    IdUsuario = Convert.ToInt32(IdUsuario),
                    Cantidad = Convert.ToInt32(Cantidad),
                    IdTipo = 2,
                    IdEstado = 1,
                    Importe = 0,
                    Fecha_Publicacion = DateTime.Now,
                    TipoDespublicacion = 1,
                    IdCategoria = 1
                };

                ApiServices objApi = new ApiServices();
                HttpResponseMessage response = null;
                string Request = Newtonsoft.Json.JsonConvert.SerializeObject(objProd);
                response = objApi.CallService("productos", Request, ApiServices.TypeMethods.POST).Result;

                if (response.IsSuccessStatusCode)
                {
                    return 1;
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
        public static int Eliminar(int idDesc)
        {
            try
            {
                if (idDesc > 0)
                {
                    ApiServices objApi = new ApiServices();
                    HttpResponseMessage response = null;
                    string Request = "{}";
                    response = objApi.CallService("productos/" + idDesc, Request, ApiServices.TypeMethods.DELETE).Result;

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

        protected void DlComercio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}