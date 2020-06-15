//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MundoCanjeWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pedidos
    {
        public int Id { get; set; }
        public Nullable<int> IdProducto { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdPedido_Estado { get; set; }
        public Nullable<System.DateTime> FechaPedido { get; set; }
        public Nullable<System.DateTime> FechaEntrega { get; set; }
        public string Comentarios { get; set; }
        public Nullable<decimal> Importe { get; set; }
        public string ImagenMatch { get; set; }
        public Nullable<decimal> ImporteDiferencia { get; set; }
        public string ProductoNombreMatch { get; set; }
        public string ProductoDescripcionMatch { get; set; }
        public string CategoriaMatch { get; set; }
        public Nullable<int> IdProductoMatch { get; set; }
    
        public virtual Pedidos_Estados Pedidos_Estados { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        public virtual Productos Productos { get; set; }
    }
}
