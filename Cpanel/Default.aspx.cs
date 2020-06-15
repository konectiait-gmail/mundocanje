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
    public partial class Default : System.Web.UI.Page
    {
        public void IniciarControles()
        {
            //HdnId.Value = "0";
            

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

                        IniciarControles();
                        GetGrillaUltCanjes();
                        GetGrillaUltCupones();
                        GetGrillaUltUsuarios();
                    }
                    catch (Exception ex)
                    {
                        //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }
            }
        }
        public void GetGrillaUltCanjes()
        {

            ApiServices objApi = new ApiServices();
            string Request = "{}";
            HttpResponseMessage response = objApi.CallService("Pedidos/UltimosCanjes/"+ 4, Request, ApiServices.TypeMethods.GET).Result;

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
                    if(item.IdPedido_Estado==1)
                        data += "<td><label class='badge badge-gradient-success'>" + item.Desc_Estado + "</label></td>  ";
                    else
                        data += "<td><label class='badge badge-gradient-warning'>" + item.Desc_Estado + "</label></td>  ";

                    data += "<td> " + item.FechaPedido + " </td> ";
                    data += "<td> " + item.Id + " </td> ";
                    data += "	</tr> ";

                }
                LitGrillaUltCanjes.Text = data;
            }
            else
            {
                string RespuestaService = response.Content.ReadAsStringAsync().Result;
                ApiServices.Response obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiServices.Response>(RespuestaService);
                RespuestaService = response.StatusCode + " - " + obj.Error.message;
            }
            //return ListaOrdenes;
        }

        public void GetGrillaUltCupones()
        {

            ApiServices objApi = new ApiServices();
            string Request = "{}";
            HttpResponseMessage response = objApi.CallService("Pedidos/UltDescuentosDescargados/" + 4, Request, ApiServices.TypeMethods.GET).Result;

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
                    data += "<td> " + item.FechaPedido + " </td> ";
                    data += "<td> <label class='badge badge-gradient-info'>"+ item.CodigoDescuento+ "</label> </td> ";
                    data += "	</tr> ";

                }
                LitUltCupones.Text = data;
            }
            else
            {
                string RespuestaService = response.Content.ReadAsStringAsync().Result;
                ApiServices.Response obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiServices.Response>(RespuestaService);
                RespuestaService = response.StatusCode + " - " + obj.Error.message;
            }
            //return ListaOrdenes;
        }
        public void GetGrillaUltUsuarios()
        {

            ApiServices objApi = new ApiServices();
            string Request = "{}";
            HttpResponseMessage response = objApi.CallService("usuarios/UltimosUsuarios/" + 6, Request, ApiServices.TypeMethods.GET).Result;

            if (response.IsSuccessStatusCode)
            {
                string Respuesta = response.Content.ReadAsStringAsync().Result;
                List<Models.Usuarios> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Usuarios>>(Respuesta);
                string data = "";

                foreach (var item in obj)
                {

                    data += "<tr> ";
                    data += "<td> " + item.Id + " </td> ";
                    data += "<td> " + item.Nombre + " </td> ";
                    data += "<td> " + item.Fecha_Alta.Value.ToShortDateString() + " </td> ";
                    data += "<td> <div class='progress'><div class='progress-bar bg-gradient-success' role='progressbar' style='width: 100%' aria-valuenow='100' aria-valuemin='0' aria-valuemax='100'></div> </div> </td> ";
                    data += "	</tr> ";
                }
                LitUltUsuarios.Text = data;
            }
            else
            {
                string RespuestaService = response.Content.ReadAsStringAsync().Result;
                ApiServices.Response obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiServices.Response>(RespuestaService);
                RespuestaService = response.StatusCode + " - " + obj.Error.message;
            }
            //return ListaOrdenes;
        }
    }
}