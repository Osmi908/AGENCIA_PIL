using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.Models
{
    public class Producto_Precio
    {
        public string nombre_producto { get; set; }
        public string cod_producto { get; set; }
        public string imagen_producto { get; set; }
        public decimal precio_unitario { get ; set ; }
        public int cantidad { get; set; }
        public decimal precio_cantidad { get; set; }
        public Producto_Precio()
        {

        }
        public Producto_Precio(producto_precio_cantidad prod, int cantidad)
        {
            this.nombre_producto = prod.nombre;
            this.imagen_producto = prod.imagen;
            this.cod_producto = prod.codigo;
            this.precio_unitario = ObtenerPrecio(prod.codigo);
            this.cantidad = cantidad;
            this.precio_cantidad = CalculaPrecioTotal(precio_unitario, cantidad);
        }

        public decimal CalculaPrecioTotal(decimal precio_unitario, int cantidad)
        {
            return precio_unitario * cantidad;
        }

        public decimal ObtenerPrecio(string id_producto)
        {
            decimal precio = 0;
            using (SQLiteConnection conn=new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                conn.CreateTable<Precio>();
                var precios=conn.Query<Precio>("SELECT * FROM Precio WHERE estado=1 and id_producto like '" + id_producto + "'");
                if (precios.Count>0)
                {
                    precio = precios[0].precio;
                }
            }
            return precio;

        }
    }
}
