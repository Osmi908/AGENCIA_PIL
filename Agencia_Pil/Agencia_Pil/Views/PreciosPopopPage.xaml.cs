using Agencia_Pil.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agencia_Pil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreciosPopopPage : PopupPage
    {
        FirebaseClient firebase = new FirebaseClient("https://agenciapil-f8472-default-rtdb.firebaseio.com/" + App.usuario.ci_usuario);

        public string[] TipoPrecio = { "POR UNIDAD", "POR MAYOR" };
        public Producto Pproducto { get; set; }
        public PreciosPopopPage(Producto producto)
        {
            Pproducto = producto;
            InitializeComponent();
            lblNombreProd.Text = producto.nombre;
            
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            int sw = 0;
            string precioMayor = txtPrecioMayor.Text;
            string precioMenor = txtprecioMenor.Text;
            if (!precioMayor.Equals(""))
            {
                try
                {
                    decimal mayor = decimal.Parse(precioMayor);
                    using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
                    {
                        conn.CreateTable<Precio_Mayor>();
                        conn.Execute("Update Precio_Mayor Set estado = 0 Where id_producto like '" + Pproducto.codigo + "'");
                        conn.Execute("INSERT into precio Values('" + Pproducto.codigo + "'," + precioMayor + ",'" + DateTime.Now.ToString() + "',1)");
                        await firebase
                          .Child("Precios_Mayor")
                          .PostAsync(new Precio() {precio=mayor,estado=1,fechaini=DateTime.Now,id_producto=Pproducto.codigo });
                    }
                    sw ++;
                }
                catch (Exception)
                {

                    await DisplayAlert("Error", "Inserte los datos correctos em Precio por Mayor Ej 5.50", "OK");
                }
                
            }
            if (precioMenor.Equals(""))
            {
                try
                {
                    decimal menor = decimal.Parse(precioMenor);
                    using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
                    {
                        conn.CreateTable<Precio>();
                        conn.Execute("Update precio Set estado = 0 Where id_producto like '" + Pproducto.codigo + "'");
                        conn.Execute("INSERT into precio Values('" + Pproducto.codigo + "'," + menor + ",'" + DateTime.Now.ToString() + "',1)");
                        await firebase
                            .Child("Precios")
                            .PostAsync(new Precio() { precio = menor, estado = 1, fechaini = DateTime.Now, id_producto = Pproducto.codigo });
                    }
                    sw +=2;
                }
                catch (Exception)
                {

                    await DisplayAlert("Error", "Inserte los datos correctos en Precio por Menor Ej 5.50", "OK");
                }
                
            }
            if (sw == 0) 
            {
                await DisplayAlert("sin Datos", "Los campos no fueron llenados", "OK");
            }
            else
            {
                if (sw==1)
                {
                    await DisplayAlert("Listo","Se agrego el precio por mayor.","Listo");
                }
                else
                {
                    if (sw == 2)
                    {
                        await DisplayAlert("Listo", "Se agrego el precio por menor.", "Listo");
                    }
                    else
                    {
                        await DisplayAlert("Listo", "Se agrgaron lo precios por mayor y menor.", "Listo");
                    }
                }
            }
        }
    }
}