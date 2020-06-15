<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/Principal.Master" AutoEventWireup="true" CodeBehind="ListadoDescuentos.aspx.cs" Inherits="MundoCanjeWeb.Cpanel.ListadoDescuentos" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentBeginAndEndHandler" runat="server">
    <script type="text/javascript" language="javascript">
        function BeginRequestHandler() {
            //Antes de ejecutar la peticion de AJAX ASP.Net                      
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                if ($("[id$=HdnIdDescuento]").val() == "0") {
                    $("#myModalABM").modal('hide');
                }
                //$('#example1').DataTable();
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CphBody" runat="server">
    <asp:HiddenField ID="HdnIdDescuento" runat="server" Value="0" />
    <asp:HiddenField ID="HdnEsNuevo" runat="server" Value="0" />
    
          <div class="content-wrapper">
            
            <div class="page-header">
              <h3 class="page-title">
                <span class="page-title-icon bg-gradient-primary text-white mr-2">
                  <i class="mdi mdi-home"></i>
                </span> Canjes </h3>
              
            </div>
            <div class="row">
              <div class="col-md-6 stretch-card grid-margin">
                <div class="card bg-gradient-danger card-img-holder text-white">
                    <a href="ListadoDescuentos.aspx?est=1" style="color: white;">
                        <div class="card-body">
                    <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                    <h4 class="font-weight-normal mb-3">Generados <i class="mdi mdi-comment-alert-outline mdi-24px float-right"></i>
                    </h4>                    
                    <h2 class="CDescuentosPend mb-1">0</h2>
                  </div>
                    </a>
                </div>
              </div>
                <div class="col-md-6 stretch-card grid-margin">
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
              
               
            </div>
            <div class="row">
              <div class="col-12 grid-margin">
                <div class="card">
                  <div class="card-body">
                      <div class="row">
                          <div class="col-8">
                              <h4 class="card-title">
                                  <asp:Label ID="LblTituloGrilla" runat="server" Text="Listado de Descuentos"></asp:Label>
                              </h4>
                          </div>
                          <div class="col-4 text-right">
                              <button type="button" onclick='NuevoRegistro();return false' class="btn btn-gradient-info btn-icon-text"> Generar Descuento <i class="mdi mdi-folder-plus btn-icon-append"></i></button>
                          </div>
                       </div>
                    
                      
                    <div class="table-responsive">
                      <table class="table">
                        <thead>
                          <tr>
                            <th> Comercio </th>
                            <th> Canje </th>
                            <th> Estado </th>
                            <th> Fecha </th>
                            <th> Id del Canje </th>
                            <th> Codigo </th>
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
            <h4 class="modal-title"><i class="fa fa-pencil"></i> Nuevo Cupon de Descuento</h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal form-label-left">                    
                    
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Comercio</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:DropDownList id="DlComercio"  AutoPostBack="False" OnSelectedIndexChanged="DlComercio_SelectedIndexChanged" runat="server"/>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Nombre Descuento</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Nombre Descuento" ID="TxbNombreDescuento" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Descripcion Descuento</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Descripcion del Descuento" ID="TxbDescDescuento" runat="server" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div> 
                    
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Código</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Codigo" ID="TxbCodigo" runat="server" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Cantidad</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Cantidad" ID="TXbCantidad" runat="server" TextMode="Number"></asp:TextBox>
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
                <button type="button" ID="BtnAceptar" class="btn btn-primary" onclick="GrabarDescuento(); return false" >Aceptar</button>
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
        var _URL = window.URL || window.webkitURL;
        $("#f_UploadImage").on('change', function () {
            var data = new FormData();
            var files = $("#f_UploadImage").get(0).files;
            console.log("El file es: " + files);

            if (files.length > 0) {
                data.append("UploadedImage", files[0]);
            }
            var ajaxRequest = $.ajax({
                type: "POST",
                url: "/api/image/SaveImage/Productos",
                contentType: false,
                processData: false,
                data: data
            });

            ajaxRequest.done(function (xhr, textStatus) {
                // Do other operation
                console.log("OK");
                $("#myUploadedImg").attr("src", xhr.Message);
            });
            ajaxRequest.fail(function () {
                console.log("Error al devolver el POST");
            });
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
                        $(".CDescuentosPend").html(Resp.DescuentosPendientes);
                        $(".CDescuentosConfirm").html(Resp.DescuentosConfirmados);
                        $(".CDescuentosCancelados").html(Resp.DescuentosCancelados);
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
        function SetDeleteId(id) {
            $("[id$=HdnIdDescuento]").val(id);
            $("#modalDelete").modal('show');
        }

        function EliminarDesc() {
            var vIdDesc = $("[id$=HdnIdDescuento]").val();
            $.ajax({
                type: "POST",
                url: "ListadoDescuentos.aspx/Eliminar",
                data: JSON.stringify({ 'idDesc': vIdDesc }),
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var resp = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    if (resp == 1) {
                        $('#modalDelete').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();
                        //RefrescarUpdatePanel();
                        window.location.href = "ListadoDescuentos.aspx";
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
        function NuevoRegistro() {
            $("[id$=HdnEsNuevo]").val('1');
            $("#myModalABM").modal('show');
        }
        
        function GrabarDescuento() {
            var vIdUsuario = $("[id$=DlComercio]").val();;
            var vNombre = $("#TxbNombreDescuento").val();
            var vDescripcion = $("#TxbDescDescuento").val();
            var vCodigo = $("#TxbCodigo").val();
            var vCantidad = $("#TXbCantidad").val();
            var vImagen = $("#myUploadedImg").attr("src");
            $.ajax({
                type: "POST",
                url: "ListadoDescuentos.aspx/Grabar",
                data: JSON.stringify({ 'Nombre': vNombre, 'Desc': vDescripcion, 'Codigo': vCodigo, 'Imagen': vImagen, 'IdUsuario': vIdUsuario, 'Cantidad': vCantidad }),
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var resp = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    if (resp == 1) {
                        $('#myModalABM').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();
                        //RefrescarUpdatePanel();
                        window.location.href = "ListadoDescuentos.aspx";
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
