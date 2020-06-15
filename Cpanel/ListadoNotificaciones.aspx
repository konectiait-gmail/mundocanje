<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/Principal.Master" AutoEventWireup="true" CodeBehind="ListadoNotificaciones.aspx.cs" Inherits="MundoCanjeWeb.Cpanel.ListadoNotificaciones" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentBeginAndEndHandler" runat="server">
    <script type="text/javascript" language="javascript">
        function BeginRequestHandler() {
            //Antes de ejecutar la peticion de AJAX ASP.Net                      
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                if ($("[id$=HdnId]").val() == "0") {
                    $("#myModalABM").modal('hide');
                }
                //$('#example1').DataTable();
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CphBody" runat="server">
    <asp:HiddenField ID="HdnId" runat="server" Value="0" />
    <asp:HiddenField ID="HdnEsNuevo" runat="server" Value="0" />
    
          <div class="content-wrapper">
            
            <div class="page-header">
              <h3 class="page-title">
                <span class="page-title-icon bg-gradient-primary text-white mr-2">
                  <i class="mdi mdi-home"></i>
                </span> Notificaciones </h3>
              
            </div>
            <div class="row">
              <div class="col-md-6 stretch-card grid-margin">
                <div class="card bg-gradient-danger card-img-holder text-white">
                    <a href="ListadoDescuentos.aspx?est=1" style="color: white;">
                        <div class="card-body">
                    <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                    <h4 class="font-weight-normal mb-3">Notificaciones Enviadas <i class="mdi mdi-comment-alert-outline mdi-24px float-right"></i>
                    </h4>                    
                    <h2 class="CNotifEnviadas mb-1">0</h2>
                  </div>
                    </a>
                </div>
              </div>
            <%--    <div class="col-md-6 stretch-card grid-margin">
                    <div class="card bg-gradient-info card-img-holder text-white">
                        <a href="ListadoDescuentos.aspx?est=3" style="color: white;">
                            <div class="card-body">
                        <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                        <h4 class="font-weight-normal mb-3">Descargados <i class="mdi mdi-comment-check-outline mdi-24px float-right"></i>
                        </h4>
                        <h2 class="CDescuentosConfirm mb-1">0</h2>
                      </div>
                        </a>
                    </div>
                  </div>
              
               --%>
            </div>
            <div class="row">
              <div class="col-12 grid-margin">
                <div class="card">
                  <div class="card-body">
                      <div class="row">
                          <div class="col-8">
                              <h4 class="card-title">
                                  <asp:Label ID="LblTituloGrilla" runat="server" Text="Listado de Notificaciones"></asp:Label>
                              </h4>
                          </div>
                          <div class="col-4 text-right">
                              <button type="button" onclick='NuevoRegistro();return false' class="btn btn-gradient-info btn-icon-text"> Nueva Notificacion <i class="mdi mdi-folder-plus btn-icon-append"></i></button>
                          </div>
                       </div>
                    
                      
                    <div class="table-responsive">
                      <table class="table">
                        <thead>
                          <tr>
                            <th> Id </th>
                            <th> Notificacion </th>
                            <th> Fecha </th>
                            <th> Estado </th>                            
                            <th> Acciones </th>
                          </tr>
                        </thead>
                        <tbody>
                          
                            <asp:Literal ID="LitGrilla" runat="server"></asp:Literal>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            
          </div>
          
    
    <div class="modal fade" id="myModalABM"  data-backdrop="static"  data-keyboard="false" aria-hidden="true" role="dialog">    
        <div class="modal-dialog">
        <div class="modal-content">
       
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title"><i class="fa fa-pencil"></i> Nueva Notificacion</h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal form-label-left">                    
                    
                    
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Titulo</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Titulo de Notificacion" ID="TxbTitulo" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Descripcion</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Descripcion de Notificacion" ID="TxbDescripcion" runat="server" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div> 
                    
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Fecha de Envío</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Fecha" ID="TXbFechaEnvio" runat="server" MaxLength="10" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                                        
                </div>
            </div>
            <div class="modal-footer">                
                <button type="button" ID="BtnCancelar" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                <button type="button" ID="BtnAceptar" class="btn btn-primary" onclick="GrabarNotificacion(); return false" >Aceptar</button>
            </div>
        </div>
        <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    <div class="modal fade" id="modalDelete" style="display: none;">
          <div class="modal-dialog">
            <div class="modal-content" style="width: 70%;">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">×</span></button>
                <h4 class="modal-title">Confirmar Eliminación</h4>
              </div>
              <div class="modal-body">
                <p>¿Está seguro de eliminar el registro?</p>
              </div>
              <div class="modal-footer">
                <button type="button" onclick='EliminarDesc();return false' class="btn btn-primary">Si</button>
                  <button type="button" class="btn btn-danger" data-dismiss="modal">No</button>
                
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>

    




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphJsInferior" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            console.log("ready!");
            GetContadores();

        });
        function GetContadores() {
            $.ajax({
                type: "GET",
                url: "../api/Pedidos/PedidosCount",
                data: "{}",
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var Resp = (typeof response) == 'string' ? eval('(' + response + ')') : response;
                    if (Resp != null) {
                        $(".CNotifEnviadas").html(Resp.CantNotificacEnviadas);
                    }

                },
                error: function (result) {
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }

        function NuevoRegistro() {
            $("[id$=HdnEsNuevo]").val('1');
            $("#myModalABM").modal('show');
        }
        
        function GrabarNotificacion() {
            var vTitulo = $("#TxbTitulo").val();
            var vDescripcion = $("#TxbDescripcion").val();
            var vFecha = $("#TXbFechaEnvio").val();
            $.ajax({
                type: "POST",
                url: "ListadoNotificaciones.aspx/Grabar",
                data: JSON.stringify({ 'Titulo': vTitulo, 'Desc': vDescripcion, 'FechaEnvio': vFecha}),
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var resp = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    if (resp == 1) {
                        $('#myModalABM').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();
                        //RefrescarUpdatePanel();
                        window.location.href = "ListadoNotificaciones.aspx";
                    }
                    else {
                        AlertError();
                    }
                },
                error: function (result) {
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }
        
      
    </script>
</asp:Content>
