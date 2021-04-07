using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    public class Venta
    {
        [PrimaryKey]
        public int id_venta { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
        public int id_cliente { get; set; }
        public int ci_usuario { get; set; }
        public decimal Monto_total { get; set; }
        public int cantidad_productos { get; set; }
    }
}
