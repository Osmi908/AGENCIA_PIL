using Agencia_Pil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.ViewModels
{
    public class RegistrarPedidoViemModel
    {
        public decimal VentasMes { get; set; }
        public int nroVenta { get; set; }
        public List<string> opcionesDeBusqueda { get; set; }
        public List<Producto> productos { get; set; }
        public RegistrarPedidoViemModel()
        {
            nroVenta = ObtenerNroRegProd();
            productos = new List<Producto>();
            opcionesDeBusqueda = new List<string>() { "Nombre", "Categoria", "Tipo", "Peso/Volumen", "Presentacion" };
            using (SQLiteConnection con = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                
                productos = con.Table<Producto>().ToList();

            }
        }


        public int ObtenerNroRegProd()
        {
            int roventa = 0;
            using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                conn.CreateTable<Pedido>();
                List<Pedido> res = new List<Pedido>();
                res =conn.Table<Pedido>().ToList();
                roventa = res.Count+1;
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
            using (SQLiteConnection con = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                var prod = con.Query<Producto>(query);
                productos = prod;
            }
        }

        internal int VreificarCantidad(Producto prod)
        {
            int Cantidad = 0;
            using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                conn.CreateTable<Inventario>();
                string query = "SELECT * FROM Inventario WHERE id_producto=" + prod.id_producto + "";
                var inv_prod = conn.Query<Inventario>(query);
                if (inv_prod.Count > 0)
                {
                    Cantidad = inv_prod[0].cantidad;

                }
            }
            return Cantidad;
        }

        internal int ObtenerIdMax()
        {
            int resultado = 0;
            string query = "SELECT MAX(id_pedido) FROM pedido";
            using (SQLiteConnection conn=new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                var res=conn.Query<int>(query);
                if (res.Count>0)
                {
                    resultado = res[0];
                }

            }
            return resultado;

        }
    }
}
