<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/Principal.Master" AutoEventWireup="true" CodeBehind="ListadoLocalidades.aspx.cs" Inherits="MundoCanjeWeb.Cpanel.ListadoLocalidades" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentBeginAndEndHandler" runat="server">
    <script type="text/javascript" language="javascript">
        function BeginRequestHandler() {
            //Antes de ejecutar la peticion de AJAX ASP.Net                      
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                if ($("[id$=HdnIdLocalidad]").val() == "0") {
                    $("#myModalABM").modal('hide');
                }
                //$('#example1').DataTable();
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="CphBody" runat="server">
    <asp:HiddenField ID="HdnIdLocalidad" runat="server" Value="0" />
    <asp:HiddenField ID="HdnEsNuevo" runat="server" Value="0" />
    
          <div class="content-wrapper">
            
            <div class="page-header">
              <h3 class="page-title">
                <span class="page-title-icon bg-gradient-primary text-white mr-2">
                  <i class="mdi mdi-home"></i>
                </span> Localidades </h3>
              
            </div>
            <div class="row">
              <div class="col-md-4 stretch-card grid-margin">
                <div class="card bg-gradient-danger card-img-holder text-white">
                  <div class="card-body">
                    <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                    <h4 class="font-weight-normal mb-3">Ciudades Totales <i class="mdi mdi-chart-line mdi-24px float-right"></i>
                    </h4>
                    <h2 class="CCantLocalidades mb-1">0</h2>
        
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
                              <h4 class="card-title">Listado de Ciudades</h4>
                          </div>
                          <div class="col-4 text-right">
                              <button type="button" onclick='NuevaLocalidad();return false' class="btn btn-gradient-info btn-icon-text"> Nueva Ciudad <i class="mdi mdi-folder-plus btn-icon-append"></i></button>
                          </div>
                       </div>
                    
                      
                    <div class="table-responsive">
                      <table class="table">
                        <thead>
                          <tr>
                            <th> Id </th>
                            <th> Nombre </th>
                            <th> Provincia </th>
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
            <h4 class="modal-title"><i class="fa fa-pencil"></i> Edición de la Ciudad: <asp:Label ID="LblIdLocalidad" runat="server" Text="0"></asp:Label></h4>
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
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Pais</label>
                        <div class="col-md-12 col-sm-12 col-xs-12">                            
                            <asp:DropDownList id="DlPais"  AutoPostBack="False" OnSelectedIndexChanged="Pais_SelectedIndexChanged" runat="server"/>
                        </div>
                    </div>                      
                </div>
            </div>
            <div class="modal-footer">                
                <button type="button" ID="BtnCancelar" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                <asp:Button ID="BtnAceptar" class="btn btn-primary" runat="server" Text="Aceptar" OnClientClick="GrabarLocalidad(); return false"  />                
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
                        $(".CCantLocalidades").html(Resp.CantidadLocalidades);
                    }

                },
                error: function (result) {
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }
        

        function NuevaLocalidad() {
            $("[id$=HdnEsNuevo]").val('1');
            $("[id$=HdnIdLocalidad]").val(0);
            $("[id$=LblIdLocalidad]").text(0);
            $("#TxbNombre").val('');

            $("#myModalABM").modal('show');
        }
        function SetDeleteId(id) {
            $("[id$=HdnIdLocalidad]").val(id);
            $("#modalDelete").modal('show');
        }

        function GetEditId(id) {
            $("[id$=HdnIdLocalidad]").val(id);
            $.ajax({
                type: "POST",
                url: "ListadoLocalidades.aspx/IniModalEdit",
                data: "{Id:'" + id + "'}",
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var ListaMC = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    if (ListaMC == null) {
                        //alert('No exiten datos');                        
                        $("[id$=HdnIdLocalidad]").val(0);
                        $("[id$=LblIdLocalidad]").text(0);
                        $("#TxbNombre").val('');
                        $("#myModalABM").modal('show');

                    }
                    else {
                        $("[id$=LblIdLocalidad]").text(ListaMC[0].Id);
                        $("#TxbNombre").val(ListaMC[0].Nombre);
                        $("[id$=DlPais]").val(ListaMC[0].IdPais);
                        
                        $("#myModalABM").modal('show');
                    }
                },
                error: function (result) {
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }
        function GrabarLocalidad() {
            var vIdCat = $("[id$=HdnIdLocalidad]").val();
            var vNombre = $("#TxbNombre").val();                        
            var vIdPais = $("[id$=DlPais]").val();
            var EsNuevo = $("[id$=HdnEsNuevo]").val();

            var local = { Id: vIdCat, Nombre: vNombre, IdPais: vIdPais };
            

            $.ajax({
                type: "POST",
                url: "ListadoLocalidades.aspx/Grabar",
                data: JSON.stringify({ 'localidad': local, 'EsNuevo': EsNuevo }),
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var resp = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    if (resp == 1) {
                        $('#myModalABM').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();
                        //RefrescarUpdatePanel();
                        window.location.href = "ListadoLocalidades.aspx";
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
            var vIdLocal = $("[id$=HdnIdLocalidad]").val();
            $.ajax({
                type: "POST",
                url: "ListadoLocalidades.aspx/Eliminar",
                data: JSON.stringify({ 'IdLocalidad': vIdLocal }),
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var resp = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    if (resp == 1) {
                        $('#modalDelete').modal('hide'); $('body').removeClass('modal-open'); $('.modal-backdrop').remove();
                        //RefrescarUpdatePanel();
                        window.location.href = "ListadoLocalidades.aspx";
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
