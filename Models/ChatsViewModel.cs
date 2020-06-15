using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundoCanjeWeb.Models
{
    public class ChatsViewModel
    {
        public int Id { get; set; }
        public int Usuario_Emisor { get; set; }
        public string NombreUsuarioEmisor { get; set; }
        public string ImgUsuarioEmisor { get; set; }
        public int Usuario_Receptor { get; set; }
        public string NombreUsuarioReceptor { get; set; }
        public string ImgUsuarioReceptor { get; set; }
        public string Ultimo_Mensaje { get; set; }
        public DateTime Fecha_Ultimo_Mensaje { get; set; }
        
    }

    public class ChatsDetallesViewModel
    {
        public int Id { get; set; }
        public int Usuario_Emisor { get; set; }
        public string NombreUsuarioEmisor { get; set; }
        public int Usuario_Receptor { get; set; }
        public string NombreUsuarioReceptor { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha_Mensaje { get; set; }
        public int IdChat { get; set; }
    }

    


}