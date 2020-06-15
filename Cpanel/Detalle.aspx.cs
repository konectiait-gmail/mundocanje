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
using System.IO;
using Newtonsoft.Json.Converters;

namespace MundoCanjeWeb.Cpanel
{
    public partial class Detalle : System.Web.UI.Page
    {
        public void IniciarControles()
        {
            HdnIdCanje.Value = "0";


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
                    string idPedido = Request.QueryString["id"];
                    string idProducto = Request.QueryString["idProd"];
                    
                    IniciarControles();

                    if (idPedido != null)
                    {
                        GetDetallePedido(Convert.ToInt32(idPedido));
                    }
                    if (idProducto != null)
                    {
                        GetDetalleProducto(Convert.ToInt32(idProducto));
                    }

                }
                catch (Exception ex)
                {
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
        }

        public void GetDetallePedido(int idPed)
        {
            
            ApiServices objApi = new ApiServices();
            string Request = "{}";
            
            HttpResponseMessage response=objApi.CallService("Pedidos/"+ idPed, Request, ApiServices.TypeMethods.GET).Result;
            
            if (response.IsSuccessStatusCode)
            {
                string Respuesta = response.Content.ReadAsStringAsync().Result;
                var format = "dd/MM/yyyy"; // your datetime format
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
                Models.Pedidos obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Pedidos>(Respuesta, dateTimeConverter);
                //Models.Pedidos obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Pedidos>(Respuesta);

                ////////////////////////
                if (obj != null)
                {
                    HttpResponseMessage respProd = objApi.CallService("Productos/" + obj.IdProducto, Request, ApiServices.TypeMethods.GET).Result;
                    HttpResponseMessage respUsuario = objApi.CallService("Usuarios/" + obj.IdUsuario, Request, ApiServices.TypeMethods.GET).Result;
                    if (respProd.IsSuccessStatusCode && respUsuario.IsSuccessStatusCode)
                    {
                        string RespProd = respProd.Content.ReadAsStringAsync().Result;
                        Models.Productos objProd = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Productos>(RespProd);
                        string RespUsuario = respUsuario.Content.ReadAsStringAsync().Result;
                        Models.Usuarios objUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Usuarios>(RespUsuario);


                        LblTitulo.Text = "Detalle de Canje #" + obj.Id.ToString();
                        LblUsuarioVendedor.Text = objUsuario.Nombre.ToString();
                        LblFechaAltaCanje.Text = obj.FechaPedido.Value.ToShortDateString();
                        LblImporte.Text = "$" + objProd.Importe.ToString();
                        imgUsuario.ImageUrl = objUsuario.Imagen;

                        string ImagenProducto = "";
                        ImagenProducto = objProd.Imagen;

                        string data = "";
                        data += "<div class='row mt-3'> ";
                        data += "<div class='col-6 pr-1'> ";
                        data += "<img src='" + ImagenProducto + "' class='mb-2 mw-100 w-100 rounded' alt='image'> ";
                        if (objProd.Imagen2 != null)
                            data += "<img src='" + objProd.Imagen2 + "' class='mw-100 w-100 rounded' alt='image'> ";

                        data += "</div> ";
                        data += "</div> ";

                        LitImgCanje.Text = data;

                        ///////////////
                        string dataProd = "";
                        dataProd += "<div class='d-flex mt-5 align-items-top'> ";
                        //dataProd += "<img src='"+obj.Usuarios.Imagen+"' class='img-sm rounded-circle mr-3' alt='image'> ";
                        dataProd += "<div class='mb-0 flex-grow'> ";
                        dataProd += "<h5 class='mr-2 mb-2'>" + objProd.Nombre + "</h5> ";
                        dataProd += "<p class='mb-0 font-weight-light'>" + objProd.Descripcion + "</p> ";
                        dataProd += "</div></div> ";

                        LitDetalleCanje.Text = dataProd;

                        if (obj.IdPedido_Estado > 3)
                        {
                            DivMatch.Visible = true;
                            GetDatosComprador(obj, objUsuario);
                        }
                        else
                        {
                            DivMatch.Visible = false;
                        }
                    }
                }
                    ////////////////

                
            }
            else
            {
                string RespuestaService = response.Content.ReadAsStringAsync().Result;
                ApiServices.Response obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiServices.Response>(RespuestaService);
                RespuestaService = response.StatusCode + " - " + obj.Error.message;
            }
            //return ListaOrdenes;
        }

        public void GetDetalleProducto(int idProd)
        {

            ApiServices objApi = new ApiServices();
            string Request = "{}";
            HttpResponseMessage response = objApi.CallService("Productos/" + idProd, Request, ApiServices.TypeMethods.GET).Result;

            if (response.IsSuccessStatusCode)
            {
                string Respuesta = response.Content.ReadAsStringAsync().Result;
                Models.ItemVM obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.ItemVM>(Respuesta);

                LblTitulo.Text = "Detalle de Canje #" + obj.Id.ToString();
                LblUsuarioVendedor.Text = obj.Usuario.ToString();
                LblFechaAltaCanje.Text = obj.Fecha_Publicacion.Value.ToShortDateString();
                LblImporte.Text = "$" + obj.Importe.ToString();
                imgUsuario.ImageUrl = obj.UsuarioImagen;

                string data = "";
                data += "<div class='row mt-3'> ";
                data += "<div class='col-6 pr-1'> ";
                data += "<img src='" + obj.Imagen + "' class='mb-2 mw-100 w-100 rounded' alt='image'> ";
                //if (obj.im != null)
                //    data += "<img src='" + obj.Imagen2 + "' class='mw-100 w-100 rounded' alt='image'> ";

                data += "</div> ";
                data += "</div> ";

                LitImgCanje.Text = data;

                ///////////////
                string dataProd = "";
                dataProd += "<div class='d-flex mt-5 align-items-top'> ";
                //dataProd += "<img src='"+obj.Usuarios.Imagen+"' class='img-sm rounded-circle mr-3' alt='image'> ";
                dataProd += "<div class='mb-0 flex-grow'> ";
                dataProd += "<h5 class='mr-2 mb-2'>" + obj.Nombre + "</h5> ";
                dataProd += "<p class='mb-0 font-weight-light'>" + obj.Descripcion + "</p> ";
                dataProd += "</div></div> ";

                LitDetalleCanje.Text = dataProd;
                DivMatch.Visible = false;
            }
            else
            {
                string RespuestaService = response.Content.ReadAsStringAsync().Result;
                ApiServices.Response obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiServices.Response>(RespuestaService);
                RespuestaService = response.StatusCode + " - " + obj.Error.message;
            }
            //return ListaOrdenes;
        }
        public void GetDatosComprador(Models.Pedidos obj, Models.Usuarios objUsuario)
        {

            LblUsuarioComprador.Text = objUsuario.Nombre.ToString();
            LblFechaEntrega.Text = (obj.FechaEntrega!=null) ? obj.FechaEntrega.Value.ToShortDateString():"";
            LblImporteDiferencia.Text = "Diferencia $" + obj.ImporteDiferencia.ToString();
            imgUsuarioComprador.ImageUrl = objUsuario.Imagen;

            string data = "";
            data += "<div class='row mt-3'> ";
            data += "<div class='col-6 pr-1'> ";
            data += "<img src='" + obj.ImagenMatch + "' class='mb-2 mw-100 w-100 rounded' alt='image'> ";
            
            data += "</div> ";
            data += "</div> ";

            LitImgCanjeComprador.Text = data;

            ///////////////
            string dataProd = "";
            dataProd += "<div class='d-flex mt-5 align-items-top'> ";
            dataProd += "<div class='mb-0 flex-grow'> ";
            dataProd += "<h5 class='mr-2 mb-2'>" + obj.ProductoNombreMatch + "</h5> ";
            dataProd += "<p class='mb-0 font-weight-light'>" + obj.ProductoDescripcionMatch + "</p> ";
            dataProd += "</div></div> ";

            LitDetalleCanjeComprador.Text = dataProd;
        }
        
    }
}