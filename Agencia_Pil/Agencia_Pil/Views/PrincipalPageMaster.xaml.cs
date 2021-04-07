
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agencia_Pil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalPageMaster : ContentPage
    {
        public ListView ListView;

        public PrincipalPageMaster()
        {
            InitializeComponent();

            BindingContext = new PrincipalPageMasterViewModel();
        }

        class PrincipalPageMasterViewModel 
        {
           
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var pa=(MasterDetailPage)this.Parent;
            pa.Detail.Navigation.PopToRootAsync();
            pa.Detail.Navigation.PushAsync(new InventarioPage());
        }

        private void btnpventa_Clicked(object sender, EventArgs e)
        {
            var pa = (MasterDetailPage)this.Parent;
            pa.Detail.Navigation.PopToRootAsync();
            pa.Detail.Navigation.PushAsync(new VentasPage());
        }

        private void btnRegistrarPedido_Clicked(object sender, EventArgs e)
        {
            var pa = (MasterDetailPage)this.Parent;
            pa.Detail.Navigation.PopToRootAsync();
            pa.Detail.Navigation.PushAsync(new RegistrarPedidoPage());

        }

        private void btnAdministrar_Clicked(object sender, EventArgs e)
        {
            if (!stckAdminSubMenu.IsVisible)
            {
                stckAdminSubMenu.IsVisible = true;
            }
            else
            {
                stckAdminSubMenu.IsVisible = false;
            }
        }

        private void btnRegPrecios_Clicked(object sender, EventArgs e)
        {
            var pa = (MasterDetailPage)this.Parent;
            pa.Detail.Navigation.PopToRootAsync();
            pa.Detail.Navigation.PushAsync(new RegistrarPrecioPage());
        }

        private void btnReportes_Clicked(object sender, EventArgs e)
        {
            var pa = (MasterDetailPage)this.Parent;
            pa.Detail.Navigation.PopToRootAsync();
            pa.Detail.Navigation.PushAsync(new RepotesPage());
        }

        private void btnRegProductos_Clicked(object sender, EventArgs e)
        {
            var pa = (MasterDetailPage)this.Parent;
            pa.Detail.Navigation.PopToRootAsync();
            pa.Detail.Navigation.PushAsync(new AgregarProducto());
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            var pa = (MasterDetailPage)this.Parent;
            pa.Detail.Navigation.PopToRootAsync();
            pa.Detail.Navigation.PushAsync(new ActualizarPage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var pa = (MasterDetailPage)this.Parent;
            pa.Detail.Navigation.PopToRootAsync();
        }

        private void btnRegUsuario_Clicked(object sender, EventArgs e)
        {
            var pa = (MasterDetailPage)this.Parent;
            pa.Detail.Navigation.PopToRootAsync();
            pa.Detail.Navigation.PushAsync(new registrUsuario());
        }
    }
}