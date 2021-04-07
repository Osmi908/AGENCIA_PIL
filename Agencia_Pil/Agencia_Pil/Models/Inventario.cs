using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    class Inventario
    {
        [PrimaryKey,AutoIncrement]
        public int id_inventario { get; set; }
        public int cantidad { get; set; }
        public string id_producto { get; set; }
        public string detalle { get; set; }

    }
}
