<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/Principal.Master" AutoEventWireup="true" CodeBehind="ListadoPaises.aspx.cs" Inherits="MundoCanjeWeb.Cpanel.ListadoPaises" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentBeginAndEndHandler" runat="server">
    <script type="text/javascript" language="javascript">
        function BeginRequestHandler() {
            //Antes de ejecutar la peticion de AJAX ASP.Net                      
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                if ($("[id$=HdnIdPais]").val() == "0") {
                    $("#myModalABM").modal('hide');
                }
                //$('#example1').DataTable();
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CphBody" runat="server">
    <asp:HiddenField ID="HdnIdPais" runat="server" Value="0" />
    <asp:HiddenField ID="HdnEsNuevo" runat="server" Value="0" />
    
          <div class="content-wrapper">
            
            <div class="page-header">
              <h3 class="page-title">
                <span class="page-title-icon bg-gradient-primary text-white mr-2">
                  <i class="mdi mdi-home"></i>
                </span> Paises </h3>
              
            </div>
            <div class="row">
              <div class="col-md-4 stretch-card grid-margin">
                <div class="card bg-gradient-danger card-img-holder text-white">
                  <div class="card-body">
                    <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                    <h4 class="font-weight-normal mb-3">Paises Totales <i class="mdi mdi-chart-line mdi-24px float-right"></i>
                    </h4>
                    <h2 class="CCantPaises mb-1">0</h2>
        
                  </div>
                </div>
              </div>
              
            </div>
            <div class="row">
              <div class="col-12 grid-margin">
                <div class="card">
                  <div class="card-body">
                      <div class="row">
                          <div class="col-8">
                              <h4 class="card-title">Listado de Paises</h4>
                          </div>
                          <div class="col-4 text-right">
                              <button type="button" onclick='NuevaPais();return false' class="btn btn-gradient-info btn-icon-text"> Nueva Pais <i class="mdi mdi-folder-plus btn-icon-append"></i></button>
                          </div>
                       </div>
                    
                      
                    <div class="table-responsive">
                      <table class="table">
                        <thead>
                          <tr>
                            <th> Id </th>
                            <th> Nombre </th>                            
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
            <h4 class="modal-title"><i class="fa fa-pencil"></i> Edición de la Pais: <asp:Label ID="LblIdPais" runat="server" Text="0"></asp:Label></h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal form-label-left">                    
                    <div class="form-group">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Nombre</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:TextBox class="form-control" ClientIDMode="Static" placeholder="Nombre" ID="TxbNombre" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>                                       
                </div>
            </div>
            <div class="modal-footer">                
                <button type="button" ID="BtnCancelar" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                <asp:Button ID="BtnAceptar" class="btn btn-primary" runat="server" Text="Aceptar" OnClientClick="GrabarPais(); return false"  />                
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
                        $(".CCantPaises").html(Resp.CantidadPaises);
                    }

                },
                error: function (result) {
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }
        

        function NuevaPais() {
            $("[id$=HdnEsNuevo]").val('1');
            $("[id$=HdnIdPais]").val(0);
            $("[id$=LblIdPais]").text(0);
            $("#TxbNombre").val('');
            $("#TxbProvincia").val('');

            $("#myModalABM").modal('show');
        }
        function SetDeleteId(id) {
            $("[id$=HdnIdPais]").val(id);
            $("#modalDelete").modal('show');
        }

        function GetEditId(id) {
            $("[id$=HdnIdPais]").val(id);
            $.ajax({
                type: "POST",
                url: "ListadoPaises.aspx/IniModalEdit",
                data: "{Id:'" + id + "'}",
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var ListaMC = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    if (ListaMC == null) {
                        //alert('No exiten datos');                        
                        $("[id$=HdnIdPais]").val(0);
                        $("[id$=LblIdPais]").text(0);
                        $("#TxbNombre").val('');
                        $("#TxbProvincia").val('');
                        $("#myModalABM").modal('show');

                    }
                    else {
                        $("[id$=LblIdPais]").text(ListaMC[0].Id);
                        $("#TxbNombre").val(ListaMC[0].Nombre);
                        $("#TxbProvincia").val(ListaMC[0].Provincia);
                        $("#myModalABM").modal('show');
                    }
                },
                error: function (result) {
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }
        function GrabarPais() {
            var vIdCat = $("[id$=HdnIdPais]").val();
            var vNombre = $("#TxbNombre").val();                        
            var EsNuevo = $("[id$=HdnEsNuevo]").val();

            var local = { Id: vIdCat, Nombre: vNombre };

            $.ajax({
                type: "POST",
                url: "ListadoPaises.aspx/Grabar",
                data: JSON.stringify({ 'Pais': local, 'EsNuevo': EsNuevo }),
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var resp = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    if (resp == 1) {
                        $('#myModalABM').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();
                        //RefrescarUpdatePanel();
                        window.location.href = "ListadoPaises.aspx";
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

        function DeleteById() {
            var vIdLocal = $("[id$=HdnIdPais]").val();
            $.ajax({
                type: "POST",
                url: "ListadoPaises.aspx/Eliminar",
                data: JSON.stringify({ 'IdPais': vIdLocal }),
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var resp = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    if (resp == 1) {
                        $('#modalDelete').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();
                        //RefrescarUpdatePanel();
                        window.location.href = "ListadoPaises.aspx";
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
