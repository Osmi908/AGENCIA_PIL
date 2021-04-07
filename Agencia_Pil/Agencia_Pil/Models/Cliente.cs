using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    public class Cliente
    {[PrimaryKey, AutoIncrement]
        public int id_cliente { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int Enviar_mensaje { get; set; }
        public string nit { get; set; }
        public string nombre { get; set; }
        public int telefono { get; set; }
    }
}
