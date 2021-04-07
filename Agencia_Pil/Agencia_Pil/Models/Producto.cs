using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    public class Producto
    {
        [PrimaryKey, AutoIncrement]
        public int id_producto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string detalle { get; set; }
        public int lote { get; set; }
        public string tipo { get; set; }
        public string Presentacion { get; set; }
        public string marca { get; set; }
        public string imagen { get; set; }
        public string categoria { get; set; }


    }
}
