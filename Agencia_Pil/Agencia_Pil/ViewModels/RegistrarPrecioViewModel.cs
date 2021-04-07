using Agencia_Pil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.ViewModels
{
    class RegistrarPrecioViewModel
    {
        public List<Precios_Venta> Productos_con_Precio { get; set; }
        public List<Producto> Productos_sin_Precio { get; set; }
        public RegistrarPrecioViewModel()
        {
            Productos_sin_Precio = new List<Producto>();
            Productos_con_Precio = new List<Precios_Venta>();
            using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                conn.CreateTable<Producto>();
                conn.CreateTable<Precio>();
                //Precio pr = new Precio() { id_producto = "lpb10", estado = 1, fechaini = DateTime.Now, precio = 5.5M };
                //conn.Insert(pr);
                var sx=conn.Table<Precio>().ToList();
                Productos_sin_Precio = conn.Query<Producto>("SELECT p.* FROM Producto p WHERE p.codigo NOT IN ( SELECT p.codigo FROM Precio pr, Producto p WHERE pr.id_producto = p.codigo and pr.estado=1)");
                SQLiteCommand comm = new SQLiteCommand(conn);
                Productos_con_Precio = conn.Query<Precios_Venta>("SELECT p.nombre, p.codigo, p.imagen, pr.precio FROM precio pr, producto p WHERE p.codigo = pr.id_producto and pr.estado=1");
            }
        }
    }
}
