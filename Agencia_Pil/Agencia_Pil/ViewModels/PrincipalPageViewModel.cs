using Agencia_Pil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.ViewModels
{
    public class PrincipalPageViewModel
    {
        public decimal ComprasDia { get; set; }
        public decimal ComprasMes { get; set; }
        public decimal ComprasAño { get; set; }
        public decimal VentasDia { get; set; }
        public decimal VentasAño { get; set; }
        public decimal VentasMes { get; set; }
        public List<Pedido> ListaPedidosMes { get; set; }
        public List<inventario_vista> productos_inventario { get; set; }
        public List<Cliente> clientes { get; set; }
        public List<Producto> productos { get; set; }
        public PrincipalPageViewModel()
        {
            ListaPedidosMes = new List<Pedido>();
            clientes = new List<Cliente>();
            productos_inventario = new List<inventario_vista>();
            using (SQLiteConnection con = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                con.CreateTable<Pedido>();
                con.CreateTable<Producto>();
                con.CreateTable<Cliente>();
                clientes = con.Table<Cliente>().ToList();
                productos = con.Table<Producto>().ToList();
                var elem = con.Query<inventario_vista>("Select p.nombre, p.codigo, i.cantidad, p.categoria,p.tipo From producto p, inventario i Where p.codigo = i.id_producto order by p.categoria");
                productos_inventario = elem;
                //Lista de pedidos al mes
                int mes = DateTime.Now.Month;
                var pedodos=con.Table<Pedido>().ToList();
                for (int i = 0; i < pedodos.Count; i++)
                {
                    if (pedodos[i].fecha_Recepcion.Month==mes)
                    {
                        ListaPedidosMes.Add(pedodos[i]);
                    }
                }
                //Ventas del dia, mes , año
                var ventas = con.Table<Venta>().ToList();
                var mess = DateTime.Now.Month;
                var dia = DateTime.Now.Day;
                var año = DateTime.Now.Year;
                for (int i = 0; i < ventas.Count; i++)
                {
                    if (ventas[i].fecha.Month == mess && ventas[i].fecha.Year==año)
                    {
                        VentasMes = VentasMes + ventas[i].Monto_total;
                    }
                    if (ventas[i].fecha.Day==dia && ventas[i].fecha.Month == mess && ventas[i].fecha.Year == año)
                    {
                        VentasDia += ventas[i].Monto_total;
                    }
                    if (ventas[i].fecha.Year == año)
                    {
                        VentasAño += ventas[i].Monto_total;
                    }
                }
                //Compras del dia, mes. año

                var compras = con.Table<Pedido>().ToList();
                for (int i = 0; i < compras.Count; i++)
                {
                    if (compras[i].fecha_Recepcion.Month == mess && compras[i].fecha_Recepcion.Year == año)
                    {
                        ComprasMes = ComprasMes + compras[i].Importe_Total;
                    }
                    if (compras[i].fecha_Recepcion.Day == dia && compras[i].fecha_Recepcion.Month == mess && compras[i].fecha_Recepcion.Year == año)
                    {
                        ComprasDia += compras[i].Importe_Total;
                    }
                    if (ventas[i].fecha.Year == año)
                    {
                        ComprasAño += compras[i].Importe_Total;
                    }
                }

            }
        }
    }
}
