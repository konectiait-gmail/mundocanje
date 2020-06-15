using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundoCanjeWeb.Models
{
    public class ProductoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdTipo { get; set; }
        public Nullable<int> IdEstado { get; set; }
        public Nullable<decimal> Importe { get; set; }
        public Nullable<System.DateTime> Fecha_Publicacion { get; set; }
        public Nullable<int> TipoDespublicacion { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public string Imagen { get; set; }
        public Nullable<int> Ult_Dias { get; set; }
        public string Subtitulo { get; set; }
    }

    public class ContadoresProductos
    {
        public string CanjesPendientes { get; set; }
        public string CanjesCancelados { get; set; }
        public string CanjesIniciados { get; set; }
        public string CanjesConfirmados { get; set; }
        public string DescuentosPendientes { get; set; }
        public string DescuentosCancelados { get; set; }
        public string DescuentosIniciados { get; set; }
        public string DescuentosConfirmados { get; set; }
        public string CantidadUsuarios { get; set; }
        public string CantTerminosYCondiciones { get; set; }
        public string CantidadCategorias { get; set; }
        public string CantidadComercios { get; set; }
        public string CantidadFAQ { get; set; }
        public string CantidadLocalidades { get; set; }
        public string CantidadPaises { get; set; }
        
        public string CantNotificacEnviadas { get; set; }


    }

    public class ContadoresHomeWeb
    {
        public string PlanVendidoCant { get; set; }
        public string PlanVendidoPorc { get; set; }
        public string CanjesRealizadosCant { get; set; }
        public string CanjesRealizadosPorc { get; set; }
        public string UsuariosActivosCant { get; set; }
        public string UsuariosActivosPorc { get; set; }
    }
}