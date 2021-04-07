using Agencia_Pil.Models;
using AppCotizaciones.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.ViewModels
{
    public class VentasViewModel
    {
       
        public int nroVenta { get; set; }
        public List<string> opcionesDeBusqueda { get; set; }
        public List<producto_precio_cantidad> productos { get; set; }
        public VentasViewModel()
        {
            nroVenta = ObtenerNroVenta();
            productos = new List<producto_precio_cantidad>();
            opcionesDeBusqueda = new List<string>() { "Nombre", "Categoria", "Tipo", "Peso/Volumen", "Presentacion" };
            using (SQLiteConnection con=new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                productos = con.Query<producto_precio_cantidad>("SELECT p.nombre, p.codigo, p.imagen, pr.precio, i.cantidad FROM producto p, precio pr, inventario i WHERE p.codigo = pr.id_producto and p.codigo = i.id_producto and pr.estado=1");
            }

        }

        private int ObtenerNroVenta()
        {
            int roventa = 0;
            using (SQLiteConnection conn=new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                conn.CreateTable<Venta>();
                var e=conn.Query<Venta>("SELECT * FROM Venta");
                if (e.Count>0)
                {
                    roventa=e.Count+1;
                }
            }
            return roventa;
        }

        internal void buscarPor(string tipoBusqueda, string newTextValue)
        {
            string query = "";
            switch (tipoBusqueda)
            {
                case "Nombre":
                default:
                    query = "SELECT * FROM producto WHERE nombre like '%" + newTextValue + "%'";
                    break;
                case "Tipo":
                    query = "SELECT * FROM producto WHERE tipo like '%" + newTextValue + "%'";
                    break;
                case "Categoria":
                    query = "SELECT * FROM producto WHERE categoria like '%" + newTextValue + "%'";
                    break;
                case "Peso/Volumen":
                    query = "SELECT * FROM producto WHERE detalle like '%" + newTextValue + "%'";
                    break;
                case "Presentacion":
                    query = "SELECT * FROM producto WHERE Presentacion like '%" + newTextValue + "%'";
                    break;

            }
            using (SQLiteConnection con=new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
              var prod=con.Query<producto_precio_cantidad>(query);
                productos = prod;
            }
        }

        internal int VreificarCantidad(producto_precio_cantidad prod)
        { int Cantidad = 0;
            using (SQLiteConnection conn=new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                conn.CreateTable<Inventario>();
                string query = "SELECT * FROM Inventario WHERE id_producto like '"+prod.codigo+"'";
                var inv_prod=conn.Query<Inventario>(query);
                if (inv_prod.Count>0)
                {
                    Cantidad=inv_prod[0].cantidad;

                }
            }
            return Cantidad;
        }
    }
}
