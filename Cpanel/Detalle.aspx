<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/Principal.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="MundoCanjeWeb.Cpanel.Detalle" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentBeginAndEndHandler" runat="server">
    <script type="text/javascript" language="javascript">
        function BeginRequestHandler() {
            //Antes de ejecutar la peticion de AJAX ASP.Net                      
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                if ($("[id$=HdnIdUsuario]").val() == "0") {
                    $("#myModalABM").modal('hide');
                }
                //$('#example1').DataTable();
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CphBody" runat="server">
    <asp:HiddenField ID="HdnIdCanje" runat="server" Value="0" />

    
          <div class="content-wrapper">
            
            <div class="page-header">
              <h3 class="page-title">
                <span class="page-title-icon bg-gradient-primary text-white mr-2">
                  <i class="mdi mdi-home"></i>
                </span>  
                  <asp:Label ID="LblTitulo" runat="server" Text="Detalle de Canje"></asp:Label>
              </h3>
              
            </div>
            <div class="row">
              <div class="col-12 grid-margin stretch-card">
                <div class="card">
                  <div class="card-body">
                    <%--<h4 class="card-title"># <asp:Label ID="LblIdCanje" runat="server" Text="30"></asp:Label></h4>--%>
                    <div class="row">
                        <div class="col-12">
                            <h4 class="card-title">Detalle del Producto</h4>
                            <br />
                        </div>                        
                    </div>
                    <div class="d-flex">
                      <div class="d-flex align-items-center mr-4 text-muted font-weight-light">
                        <asp:Image ID="imgUsuario" class="img-sm rounded-circle mr-3" runat="server"></asp:Image>
                        <span>
                            <asp:Label ID="LblUsuarioVendedor" runat="server" Text=""></asp:Label>
                        </span>
                      </div>
                      <div class="d-flex align-items-center mr-4 text-muted font-weight-light">
                        <i class="mdi mdi-clock icon-sm mr-2"></i>
                        <span>
                            <asp:Label ID="LblFechaAltaCanje" runat="server" Text=""></asp:Label>
                        </span>
                      </div>
                      <div class="d-flex align-items-center mr-4 text-muted font-weight-light">
                        <i class="mdi mdi-cash-multiple icon-sm mr-2"></i>
                        <span>
                            <asp:Label ID="LblImporte" runat="server" Text=""></asp:Label>
                        </span>
                      </div>
                    </div>
                        <asp:Literal ID="LitImgCanje" runat="server"></asp:Literal>
                        <asp:Literal ID="LitDetalleCanje" runat="server"></asp:Literal>
                    
                  </div>
                </div>
              </div>
            </div>

              <%--//Detalle del Interesado o MATCH--%>
             <div class="row" id="DivMatch"  runat="server">
              <div class="col-12 grid-margin stretch-card">
                <div class="card">
                  <div class="card-body">
                    <div class="row">
                        <div class="col-12">
                            <h4 class="card-title">Detalle del Interesado</h4>
                            <br />
                        </div>                        
                    </div>
                    <div class="d-flex">
                      <div class="d-flex align-items-center mr-4 text-muted font-weight-light">
                        <asp:Image ID="imgUsuarioComprador" class="img-sm rounded-circle mr-3" runat="server"></asp:Image>
                        <span>
                            <asp:Label ID="LblUsuarioComprador" runat="server" Text=""></asp:Label>
                        </span>
                      </div>
                      <div class="d-flex align-items-center mr-4 text-muted font-weight-light">
                        <i class="mdi mdi-clock icon-sm mr-2"></i>
                        <span>
                            <asp:Label ID="LblFechaEntrega" runat="server" Text=""></asp:Label>
                        </span>
                      </div>
                      <div class="d-flex align-items-center mr-4 text-muted font-weight-light">
                        <i class="mdi mdi-cash-multiple icon-sm mr-2"></i>
                        <span>
                            <asp:Label ID="LblImporteDiferencia" runat="server" Text=""></asp:Label>
                        </span>
                      </div>
                    </div>
                        <asp:Literal ID="LitImgCanjeComprador" runat="server"></asp:Literal>
                        <asp:Literal ID="LitDetalleCanjeComprador" runat="server"></asp:Literal>
                    
                  </div>
                </div>
              </div>
            </div>

            
          </div>
          
  


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphJsInferior" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            console.log("ready!");
           // GetContadores();

        });

        
        //function GetContadores() {
        //    $.ajax({
        //        type: "GET",
        //        url: "../api/Pedidos/PedidosCount",
        //        data: "{}",
        //        traditional: true,
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (response) {
        //            var Resp = (typeof response) == 'string' ? eval('(' + response + ')') : response;
        //            if (Resp != null) {
        //                $(".CCanjesPend").html(Resp.CanjesPendientes);
        //                $(".CCanjesIni").html(Resp.CanjesIniciados);
        //                $(".CCanjesConfirm").html(Resp.CanjesConfirmados);
        //                $(".CCanjesCancel").html(Resp.CanjesCancelados);
        //            }

        //        },
        //        error: function (result) {
        //            alert('ERROR ' + result.status + ' ' + result.statusText);
        //        }
        //    });
        //}
        
      
    </script>
</asp:Content>
