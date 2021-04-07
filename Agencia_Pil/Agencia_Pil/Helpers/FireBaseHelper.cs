
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Agencia_Pil.Models;
using Agencia_Pil;
using SQLite;

namespace Agencia_Pil.Helpers
{

    public class FirebaseHelper
    {
        public static string urlP = "https://agenciapil-f8472-default-rtdb.firebaseio.com/";
        public int CantidadTotal { get; set; }
        public decimal MontoTotal { get; set; }


        public List<Venta> GetAllArticulos()
        {
            List<Venta> ventas = new List<Venta>();
            List<Detalle_Venta> Detalleventas = new List<Detalle_Venta>();
            using (SQLiteConnection conn =new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                CantidadTotal = 0;
                MontoTotal = 0;
                var usuarios=conn.Table<Usuario>().ToList();
                for (int i = 0; i < usuarios.Count; i++)
                {
                    string uri = urlP + usuarios[i].ci_usuario;
                    FirebaseClient firebase = new FirebaseClient(uri);
                    var ex = firebase.Child("Ventas").OnceAsync<Venta>().Result;
                    var est = ex.Select(item => new Venta
                    {
                        id_cliente = item.Object.id_cliente,
                        id_venta = item.Object.id_venta,
                        fecha = item.Object.fecha,
                        ci_usuario = item.Object.ci_usuario,
                        Monto_total = item.Object.Monto_total,
                        cantidad_productos = item.Object.cantidad_productos,
                        estado = item.Object.estado
                        
                    }
                        ).ToList();
                    for (int j  = 0; j < est.Count; j++)
                    {

                        CantidadTotal += est[j].cantidad_productos;
                        MontoTotal += est[j].Monto_total;
                        ventas.Add(est[j]);
                    }
                    var det = firebase.Child("Detalle_Ventas").OnceAsync<Detalle_Venta>().Result;
                    var edtalle = det.Select(item => new Detalle_Venta
                    {
                        cantidad = item.Object.cantidad,
                        codigo_producto = item.Object.codigo_producto,
                        id_detalle_Venta = item.Object.id_detalle_Venta,
                        id_venta = item.Object.id_venta,
                        precio_total = item.Object.precio_total,
                        precio_unitario = item.Object.precio_unitario

                    }
                        ).ToList();
                    for (int k = 0; k < Detalleventas.Count; k++)
                    {
                        Detalleventas.Add(edtalle[k]);
                    }

                }

            }
            
            return ventas;

        }

        internal string GetCantidadTotal()
        {
            return CantidadTotal.ToString();
        }

        internal string GetMontoTotal()
        {
            return MontoTotal.ToString();
        }
    }
}


       




