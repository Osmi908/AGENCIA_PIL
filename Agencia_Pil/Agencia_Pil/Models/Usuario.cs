using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil
{
    public class Usuario
    {
        [PrimaryKey]
        public int ci_usuario { get; set; }
        public DateTime fecha_inicio { get; set; }
        public string estado { get; set; }
        public string rol { get; set; }
        public string nombre { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string direccion { get; set; }
        public int telefono { get; set; }
    }
}
