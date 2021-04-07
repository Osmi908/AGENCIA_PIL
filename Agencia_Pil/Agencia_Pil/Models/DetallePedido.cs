using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    class DetallePedido
    {
        public int id_pedido { get; set; }
        public string codigo_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio_Unitario { get; set; }
        public decimal precio_total { get; set; }
    }
}
