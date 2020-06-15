<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/Principal.Master" AutoEventWireup="true" CodeBehind="ListadoCanjes.aspx.cs" Inherits="MundoCanjeWeb.Cpanel.ListadoCanjes" %>

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
    <asp:HiddenField ID="HdnIdUsuario" runat="server" Value="0" />
    <asp:HiddenField ID="HdnEsNuevo" runat="server" Value="0" />
    <asp:HiddenField ID="HdnOnOffUsuario" runat="server" Value="0" />
    
          <div class="content-wrapper">
            
            <div class="page-header">
              <h3 class="page-title">
                <span class="page-title-icon bg-gradient-primary text-white mr-2">
                  <i class="mdi mdi-home"></i>
                </span> Canjes </h3>
              
            </div>
            <div class="row">
              <div class="col-md-3 stretch-card grid-margin">
                <div class="card bg-gradient-danger card-img-holder text-white">
                    <a href="ListadoCanjes.aspx?est=1" style="color: white;">
                        <div class="card-body">
                    <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                    <h4 class="font-weight-normal mb-3">Pendientes <i class="mdi mdi-comment-alert-outline mdi-24px float-right"></i>
                    </h4>
                    <h2 class="CCanjesPend mb-1">0</h2>
        
                  </div>
                    </a>
                </div>
              </div>
                <div class="col-md-3 stretch-card grid-margin">
                    <div class="card bg-gradient-info card-img-holder text-white">
                        <a href="ListadoCanjes.aspx?est=3" style="color: white;">
                            <div class="card-body">
                        <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                        <h4 class="font-weight-normal mb-3">Iniciados <i class="mdi mdi-comment-check-outline mdi-24px float-right"></i>
                        </h4>
                        <h2 class=CCanjesIni "mb-1">0</h2>
                      </div>
                        </a>
                    </div>
                  </div>
                  <div class="col-md-3 stretch-card grid-margin">
                    <div class="card bg-gradient-success card-img-holder text-white">
                        <a href="ListadoCanjes.aspx?est=4" style="color: white;">
                            <div class="card-body">
                        <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                        <h4 class="font-weight-normal mb-3">Confirmados <i class="mdi mdi-comment-plus-outline mdi-24px float-right"></i>
                        </h4>
                        <h2 class="CCanjesConfirm mb-1">0</h2>
                      </div>
                        </a>
                    </div>
                  </div>
                
                <div class="col-md-3 stretch-card grid-margin">                    
                <div class="card bg-gradient-danger card-img-holder text-white">
                    <a href="ListadoCanjes.aspx?est=2" style="color: white;">
                        <div class="card-body">
                    <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                    <h4 class="font-weight-normal mb-3">Cancelados <i class="mdi mdi-comment-remove-outline mdi-24px float-right"></i>
                    </h4>
                    <h2 class="CCanjesCancel mb-1">0</h2>
        
                  </div>
                    </a>
                </div>
                    
              </div>
               
            </div>
            <div class="row">
              <div class="col-12 grid-margin">
                <div class="card">
                  <div class="card-body">
                      <div class="row">
                          <div class="col-8">
                              <h4 class="card-title">
                                  <asp:Label ID="LblTituloGrilla" runat="server" Text="Listado de Canjes"></asp:Label>
                              </h4>
                          </div>
                          <div class="col-4 text-right">
                             <%-- <button type="button" onclick='NuevoRegistro();return false' class="btn btn-gradient-info btn-icon-text"> Nuevo Usuario <i class="mdi mdi-folder-plus btn-icon-append"></i></button>--%>
                          </div>
                       </div>
                    
                      
                    <div class="table-responsive">
                      <table class="table">
                        <thead>
                          <tr>
                            <th> Usuario </th>
                            <th> Canje </th>
                            <th> Estado </th>
                            <th> Fecha </th>
                            <th> Id del Canje </th>
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
            <h4 class="modal-title"><i class="fa fa-pencil"></i> Edición del Usuario: <asp:Label ID="LblIdUsuario" runat="server" Text="0"></asp:Label></h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal form-label-left">                    
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Nombre</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Nombre" ID="TxbNombre" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Telefono</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Telefono" ID="TXbTelefono" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">E-Mail</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="E-Mail" ID="TXbMail" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Dirección</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Direccion" ID="TxbDireccion" runat="server" MaxLength="150"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Estado</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Estado" ID="TxbEstado" runat="server" MaxLength="150"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Plan</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Plan" ID="TxbPlan" runat="server" MaxLength="150"></asp:TextBox>
                            <%--<asp:DropDownList id="DlPlan"  AutoPostBack="False" OnSelectedIndexChanged="DlPlan_SelectedIndexChanged" runat="server"/>--%>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Imagen</label>
                        <div class="col-md-9 col-sm-9 col-xs-12">                                                
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Ruta Imagen" ID="TxbImagen" runat="server"></asp:TextBox>
                            <img id="myUploadedImg" alt="Imagen" style="width:100px;" />
                            <br />
                            <input type="file" class="upload"  id="f_UploadImage">                            
                        </div>                        
                    </div>                      
                </div>
            </div>
            <div class="modal-footer">                
                <button type="button" ID="BtnCancelar" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                <button type="button" ID="BtnAceptar" class="btn btn-primary" onclick="GrabarUsuario(); return false" >Aceptar</button>
                <%--<asp:Button ID="BtnAceptar" class="btn btn-primary" runat="server" Text="Aceptar" OnClientClick="GrabarUsuario(); return false"  />   --%>             
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
                <button type="button" onclick='DeleteById();return false' class="btn btn-primary">Si</button>
                  <button type="button" class="btn btn-danger" data-dismiss="modal">No</button>
                
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>

    <div class="modal fade" id="modalOnOffUsser" style="display: none;">
          <div class="modal-dialog">
            <div class="modal-content" style="width: 70%;">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">×</span></button>
                <h4 class="modal-title">
                    <asp:Label ID="LblTituloOnOff" runat="server" Text=""></asp:Label>
                </h4>
              </div>
              <div class="modal-body">
                <p><asp:Label ID="LblDescOnOff" runat="server" Text=""></asp:Label></p>
              </div>
              <div class="modal-footer">
                <button type="button" onclick='SetOnOffUser();return false' class="btn btn-primary">Si</button>
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
                        $(".CCanjesPend").html(Resp.CanjesPendientes);
                        $(".CCanjesIni").html(Resp.CanjesIniciados);
                        $(".CCanjesConfirm").html(Resp.CanjesConfirmados);
                        $(".CCanjesCancel").html(Resp.CanjesCancelados);
                    }

                },
                error: function (result) {
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }
        function VerDetalle(Id) {
            window.location.href = "Detalle.aspx?id=" + Id;
        }
        function VerDetalleProducto(Id) {
            window.location.href = "Detalle.aspx?idProd=" + Id;
        }
        
      
    </script>
</asp:Content>
