using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    public class Pedido
    {[PrimaryKey, AutoIncrement]
        public int id_pedido { get; set; }
        public DateTime fecha_Peticion { get; set; }
        public int Estado { get; set; }
        
        public DateTime fecha_Recepcion { get; set; }
        public int ci_usuario { get; set; }
        public decimal  Importe_Total { get; set; }
        public int Cantidad_Total { get; set; }


    }
}
