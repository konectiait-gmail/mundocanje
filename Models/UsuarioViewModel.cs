using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundoCanjeWeb.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Telefono { get; set; }
        public string Mail { get; set; }
        public string Direccion { get; set; }
        public string token { get; set; }
        public int Estado { get; set; }
        public int IdTipo { get; set; }
        public string Cuit { get; set; }
        public string Razon_Social { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public int Puntuacion { get; set; }
        public string Imagen { get; set; }
        public int IdPlan { get; set; }
        public int IdLocalidad { get; set; }
        public DateTime Fecha_Alta { get; set; }
    }

    


}