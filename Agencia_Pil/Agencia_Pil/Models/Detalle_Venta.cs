using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    class Detalle_Venta
    {
        [PrimaryKey, AutoIncrement]
        public int id_detalle_Venta { get; set; }
        public int id_venta { get; set; }
        public string codigo_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio_total { get; set; }
        public decimal precio_unitario { get; set; }
    }
}
