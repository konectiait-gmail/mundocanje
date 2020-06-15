using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundoCanjeWeb.Models
{
    public class HomeViewModel
    {
        public List<ItemVM> Banners { get; set; }
        public List<ItemVM> Canjes { get; set; }
        public List<ItemVM> Descuentos { get; set; }
    }

    public class ItemVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdTipo { get; set; }

        public Nullable<System.DateTime> Fecha_Publicacion { get; set; }
        public Nullable<int> Ult_Dias { get; set; }
        public string Imagen { get; set; }
        public string Categoria { get; set; }
        public string IdCategoria { get; set; }
        public string Importe { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string NroSolicitudes { get; set; }
        public string Usuario { get; set; }
        public string UsuarioImagen { get; set; }
        public string IdProductoUsuario { get; set; }

    }


}