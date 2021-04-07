using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    class Precio_Mayor
    {
        public string id_producto { get; set; }
        public decimal precio { get; set; }
        public DateTime fechaini { get; set; }
        public int estado { get; set; }
    }
}
