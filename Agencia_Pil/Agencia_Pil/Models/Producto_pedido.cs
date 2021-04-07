using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    class Producto_pedido
    {
        public int numero { get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal precio_Total { get; set; }
    }
}
