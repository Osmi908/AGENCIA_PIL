using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    public class producto_precio_cantidad
    {
        public string nombre { get; set; }
        public string codigo { get; set; }
        public string imagen { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal precioMayor { get; set; }
    }
}
