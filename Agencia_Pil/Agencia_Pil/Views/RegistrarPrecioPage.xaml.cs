using Agencia_Pil.Models;
using Agencia_Pil.ViewModels;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
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
    public partial class RegistrarPrecioPage : TabbedPage
    {
        public RegistrarPrecioPage()
        {
            InitializeComponent();
            this.BindingContext = new RegistrarPrecioViewModel();
        }

        private async void listaProd_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PreciosPopopPage((Producto)e.Item));
            //var prod = (Precios_Venta)e.Item;
            //var resp=await DisplayPromptAsync("Precio", "Precio del producto " + prod.nombre, "OK", "Cancelar", "precio de venta");
            //try
            //{
            //    using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            //    {
            //        conn.Execute("Update precio Set estado = 0 Where id_producto like '" + prod.codigo + "'");
            //        conn.Execute("INSERT into precio Values('" + prod.codigo + "'," + resp + ",'" + DateTime.Now.ToString() + "',1)");
            //    }

            //}
            //catch (Exception)
            //{

            //    await DisplayAlert("Error", "Inserte los datos correctos Ej 5.50","OK");
            //}
            
        }

        private async void listaprod_sin_precio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var producto = (Producto)e.Item;

                var animationPopup = new PreciosPopopPage(producto);

                var scaleAnimation = new ScaleAnimation
                {
                    PositionIn = MoveAnimationOptions.Top,
                    PositionOut = MoveAnimationOptions.Bottom,
                    ScaleIn = 1.2,
                    ScaleOut = 0.8,
                    DurationIn = 400,
                    DurationOut = 800,
                    EasingIn = Easing.BounceIn,
                    EasingOut = Easing.CubicOut,
                    HasBackgroundAnimation = false
                };

                animationPopup.Animation = scaleAnimation;
            await PopupNavigation.Instance.PushAsync(new PreciosPopopPage(producto));
            //var prod = (Producto)e.Item;
            //var resp = await DisplayPromptAsync("Precio", "Precio del producto " + prod.nombre, "OK", "Cancelar", "precio de venta");
            //try
            //{
            //    using (SQLiteConnection conn = new SQLiteConnection(App.ArchivoDBAgenciaPil))
            //    {
            //        Precio pr = new Precio() {
            //            id_producto = prod.codigo,
            //            precio = decimal.Parse(resp),
            //            fechaini = DateTime.Now,
            //            estado = 1
            //        };
            //        conn.Insert(pr);
            //    }

            //}
            //catch (Exception ex)
            //{


            //    await DisplayAlert(ex.Message, "Inserte los datos correctos Ej 5.50", "OK");
            //}
        }
    }
}