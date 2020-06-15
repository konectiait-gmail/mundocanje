<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/Principal.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MundoCanjeWeb.Cpanel.Default" %>


    <asp:Content ID="Content1" ContentPlaceHolderID="CphBody" runat="server">
    
              <div class="content-wrapper">
            
                <div class="page-header">
                  <h3 class="page-title">
                    <span class="page-title-icon bg-gradient-primary text-white mr-2">
                      <i class="mdi mdi-home"></i>
                    </span> Dashboard </h3>
              
                </div>
                <div class="row">
                  <div class="col-md-4 stretch-card grid-margin">
                    <div class="card bg-gradient-danger card-img-holder text-white">
                      <div class="card-body">
                        <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                        <h4 class="font-weight-normal mb-3">Planes Vendidos <i class="mdi mdi-chart-line mdi-24px float-right"></i>
                        </h4>
                        <h2 class="CPlanesVendidos mb-5">$ 0</h2>
                        <%--<h6 class="CPlanVendidoPorc card-text">Aumento del 0%</h6>--%>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-4 stretch-card grid-margin">
                    <div class="card bg-gradient-info card-img-holder text-white">
                      <div class="card-body">
                        <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                        <h4 class="font-weight-normal mb-3">Canjes Realizados <i class="mdi mdi-bookmark-outline mdi-24px float-right"></i>
                        </h4>
                        <h2 class="CCanjesRealizados mb-5">0</h2>
                        <%--<h6 class="CCanjeAumPorc card-text">Aumento del 0%</h6>--%>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-4 stretch-card grid-margin">
                    <div class="card bg-gradient-success card-img-holder text-white">
                      <div class="card-body">
                        <img src="assets/images/dashboard/circle.svg" class="card-img-absolute" alt="circle-image" />
                        <h4 class="font-weight-normal mb-3">Usuarios Activos <i class="mdi mdi-diamond mdi-24px float-right"></i>
                        </h4>
                        <h2 class="CUsuariosActivos mb-5">0</h2>
                        <%--<h6 class="CUsuariosActivosPorc card-text">Aumento del 0%</h6>--%>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-7 grid-margin stretch-card">
                    <div class="card">
                      <div class="card-body">
                        <div class="clearfix">
                          <h4 class="card-title float-left">Usuarios y Ventas</h4>
                          <div id="visit-sale-chart-legend" class="rounded-legend legend-horizontal legend-top-right float-right"></div>
                        </div>
                        <canvas id="visit-sale-chart" class="mt-4"></canvas>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-5 grid-margin stretch-card">
                    <div class="card">
                      <div class="card-body">
                        <h4 class="card-title">Origen del Tráfico</h4>
                        <canvas id="traffic-chart"></canvas>
                        <div id="traffic-chart-legend" class="rounded-legend legend-vertical legend-bottom-left pt-4"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-12 grid-margin">
                    <div class="card">
                      <div class="card-body">
                        <h4 class="card-title">Ultimos Canjes</h4>
                        <div class="table-responsive">
                          <table class="table">
                            <thead>
                              <tr>
                                <th> Usuario </th>
                                <th> Canje </th>
                                <th> Estado </th>
                                <th> Fecha </th>
                                <th> ID del Canje </th>
                              </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="LitGrillaUltCanjes" runat="server"></asp:Literal>
                           
                            </tbody>
                          </table>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-6 grid-margin stretch-card">
                    <div class="card">
                      <div class="card-body">
                        <h4 class="card-title">Últimos Usuarios registrados</h4>
                        <div class="table-responsive">
                          <table class="table">
                            <thead>
                              <tr>
                                <th> # </th>
                                <th> Nombre </th>
                                <th> Fecha </th>
                                <th> Progreso </th>
                              </tr>
                            </thead>
                            <tbody>
                             <asp:Literal ID="LitUltUsuarios" runat="server"></asp:Literal>
                            </tbody>
                          </table>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-6 grid-margin stretch-card">
                    <div class="card">
                      <div class="card-body">
                        <h4 class="card-title">Últimos Cupones Descargados</h4>
                        <div class="table-responsive">
                          <table class="table">
                            <thead>
                              <tr>
                                <th> Usuario </th>
                                <th> Canje </th>
                                <th> Fecha </th>
                                <th> ID del Cupón </th>
                              </tr>
                            </thead>
                            <tbody>
                              <asp:Literal ID="LitUltCupones" runat="server"></asp:Literal>
                            </tbody>
                          </table>
                        </div>
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
            SetChartVisit();
            SetChartTraffic();
            GetContadores();
        });
        function GetContadores() {
            $.ajax({
                type: "GET",
                url: "../api/Pedidos/HomeWebCount",
                data: "{}",
                traditional: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var Resp = (typeof response) == 'string' ? eval('(' + response + ')') : response;
                    if (Resp != null) {                        
                        $(".CPlanesVendidos").html(Resp.PlanVendidoCant);
                        $(".CCanjesRealizados").html(Resp.CanjesRealizadosCant);
                        $(".CUsuariosActivos").html(Resp.UsuariosActivosCant);
                    }

                },
                error: function (result) {
                    alert('ERROR ' + result.status + ' ' + result.statusText);
                }
            });
        }

        function SetChartVisit() {
            if ($("#visit-sale-chart").length) {
                Chart.defaults.global.legend.labels.usePointStyle = true;
                var ctx = document.getElementById('visit-sale-chart').getContext("2d");

                var gradientStrokeViolet = ctx.createLinearGradient(0, 0, 0, 181);
                gradientStrokeViolet.addColorStop(0, 'rgba(218, 140, 255, 1)');
                gradientStrokeViolet.addColorStop(1, 'rgba(154, 85, 255, 1)');
                var gradientLegendViolet = 'linear-gradient(to right, rgba(218, 140, 255, 1), rgba(154, 85, 255, 1))';

                var gradientStrokeBlue = ctx.createLinearGradient(0, 0, 0, 360);
                gradientStrokeBlue.addColorStop(0, 'rgba(54, 215, 232, 1)');
                gradientStrokeBlue.addColorStop(1, 'rgba(177, 148, 250, 1)');
                var gradientLegendBlue = 'linear-gradient(to right, rgba(54, 215, 232, 1), rgba(177, 148, 250, 1))';

                var gradientStrokeRed = ctx.createLinearGradient(0, 0, 0, 300);
                gradientStrokeRed.addColorStop(0, 'rgba(255, 191, 150, 1)');
                gradientStrokeRed.addColorStop(1, 'rgba(254, 112, 150, 1)');
                var gradientLegendRed = 'linear-gradient(to right, rgba(255, 191, 150, 1), rgba(254, 112, 150, 1))';

                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JUL', 'AUG'],
                        datasets: [
                            {
                                label: "CHN",
                                borderColor: gradientStrokeViolet,
                                backgroundColor: gradientStrokeViolet,
                                hoverBackgroundColor: gradientStrokeViolet,
                                legendColor: gradientLegendViolet,
                                pointRadius: 0,
                                fill: false,
                                borderWidth: 1,
                                fill: 'origin',
                                data: [20, 40, 15, 35, 25, 50, 30, 20]
                            },
                            {
                                label: "USA",
                                borderColor: gradientStrokeRed,
                                backgroundColor: gradientStrokeRed,
                                hoverBackgroundColor: gradientStrokeRed,
                                legendColor: gradientLegendRed,
                                pointRadius: 0,
                                fill: false,
                                borderWidth: 1,
                                fill: 'origin',
                                data: [40, 30, 20, 10, 50, 15, 35, 40]
                            },
                            {
                                label: "UK",
                                borderColor: gradientStrokeBlue,
                                backgroundColor: gradientStrokeBlue,
                                hoverBackgroundColor: gradientStrokeBlue,
                                legendColor: gradientLegendBlue,
                                pointRadius: 0,
                                fill: false,
                                borderWidth: 1,
                                fill: 'origin',
                                data: [70, 10, 30, 40, 25, 50, 15, 30]
                            }
                        ]
                    },
                    options: {
                        responsive: true,
                        legend: false,
                        legendCallback: function (chart) {
                            var text = [];
                            text.push('<ul>');
                            for (var i = 0; i < chart.data.datasets.length; i++) {
                                text.push('<li><span class="legend-dots" style="background:' +
                                    chart.data.datasets[i].legendColor +
                                    '"></span>');
                                if (chart.data.datasets[i].label) {
                                    text.push(chart.data.datasets[i].label);
                                }
                                text.push('</li>');
                            }
                            text.push('</ul>');
                            return text.join('');
                        },
                        scales: {
                            yAxes: [{
                                ticks: {
                                    display: false,
                                    min: 0,
                                    stepSize: 20,
                                    max: 80
                                },
                                gridLines: {
                                    drawBorder: false,
                                    color: 'rgba(235,237,242,1)',
                                    zeroLineColor: 'rgba(235,237,242,1)'
                                }
                            }],
                            xAxes: [{
                                gridLines: {
                                    display: false,
                                    drawBorder: false,
                                    color: 'rgba(0,0,0,1)',
                                    zeroLineColor: 'rgba(235,237,242,1)'
                                },
                                ticks: {
                                    padding: 20,
                                    fontColor: "#9c9fa6",
                                    autoSkip: true,
                                },
                                categoryPercentage: 0.5,
                                barPercentage: 0.5
                            }]
                        }
                    },
                    elements: {
                        point: {
                            radius: 0
                        }
                    }
                })
                $("#visit-sale-chart-legend").html(myChart.generateLegend());
            }

        }
        function SetChartTraffic() {
            if ($("#traffic-chart").length) {
                var ctx = document.getElementById('visit-sale-chart').getContext("2d");
                var gradientStrokeBlue = ctx.createLinearGradient(0, 0, 0, 181);
                gradientStrokeBlue.addColorStop(0, 'rgba(54, 215, 232, 1)');
                gradientStrokeBlue.addColorStop(1, 'rgba(177, 148, 250, 1)');
                var gradientLegendBlue = 'linear-gradient(to right, rgba(54, 215, 232, 1), rgba(177, 148, 250, 1))';

                var gradientStrokeRed = ctx.createLinearGradient(0, 0, 0, 50);
                gradientStrokeRed.addColorStop(0, 'rgba(255, 191, 150, 1)');
                gradientStrokeRed.addColorStop(1, 'rgba(254, 112, 150, 1)');
                var gradientLegendRed = 'linear-gradient(to right, rgba(255, 191, 150, 1), rgba(254, 112, 150, 1))';

                //var gradientStrokeGreen = ctx.createLinearGradient(0, 0, 0, 300);
                //gradientStrokeGreen.addColorStop(0, 'rgba(6, 185, 157, 1)');
                //gradientStrokeGreen.addColorStop(1, 'rgba(132, 217, 210, 1)');
                //var gradientLegendGreen = 'linear-gradient(to right, rgba(6, 185, 157, 1), rgba(132, 217, 210, 1))';

                var trafficChartData = {
                    datasets: [{
                        //data: [30, 30, 40],
                        data: [30, 70],
                        backgroundColor: [
                            gradientStrokeBlue,
                            //gradientStrokeGreen,
                            gradientStrokeRed
                        ],
                        hoverBackgroundColor: [
                            gradientStrokeBlue,
                            //gradientStrokeGreen,
                            gradientStrokeRed
                        ],
                        borderColor: [
                            gradientStrokeBlue,
                            //gradientStrokeGreen,
                            gradientStrokeRed
                        ],
                        legendColor: [
                            gradientLegendBlue,
                            //gradientLegendGreen,
                            gradientLegendRed
                        ]
                    }],

                    // These labels appear in the legend and in the tooltips when hovering different arcs
                    labels: [
                        'IOS OS',
                        //'Direct Click',
                        'Android OS',
                    ]
                };
                var trafficChartOptions = {
                    responsive: true,
                    animation: {
                        animateScale: true,
                        animateRotate: true
                    },
                    legend: false,
                    legendCallback: function (chart) {
                        var text = [];
                        text.push('<ul>');
                        for (var i = 0; i < trafficChartData.datasets[0].data.length; i++) {
                            text.push('<li><span class="legend-dots" style="background:' +
                                trafficChartData.datasets[0].legendColor[i] +
                                '"></span>');
                            if (trafficChartData.labels[i]) {
                                text.push(trafficChartData.labels[i]);
                            }
                            text.push('<span class="float-right">' + trafficChartData.datasets[0].data[i] + "%" + '</span>')
                            text.push('</li>');
                        }
                        text.push('</ul>');
                        return text.join('');
                    }
                };
                var trafficChartCanvas = $("#traffic-chart").get(0).getContext("2d");
                var trafficChart = new Chart(trafficChartCanvas, {
                    type: 'doughnut',
                    data: trafficChartData,
                    options: trafficChartOptions
                });
                $("#traffic-chart-legend").html(trafficChart.generateLegend());
            }
        }
        
    </script>
</asp:Content>
