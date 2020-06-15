using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundoCanjeWeb.Models
{
    public class PedidoViewModel
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public int IdPedido_Estado { get; set; }
        public string Desc_Estado { get; set; }
        public string Nombre_Producto { get; set; }
        public string Desc_Producto { get; set; }
        public string Img_Usuario { get; set; }
        public string Nombre_Usuario { get; set; }
        public string FechaPedido { get; set; }
        public string Fecha_Entrega { get; set; }
        public Decimal Importe { get; set; }
        public string CodigoDescuento { get; set; }
        public string Img_Comercio { get; set; }
        public string Nombre_Comercio { get; set; }
        public string IdProductoMatch { get; set; }
        public string ProductoNombreMatch { get; set; }
        public string ProductoDescripcionMatch { get; set; }
        public string ImagenMatch { get; set; }
        public string CategoriaMatch { get; set; }
        public string Comentarios { get; set; }
        public int Ult_Dias { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string IdProductoUsuario { get; set; }
        
    }
}