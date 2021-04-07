using Agencia_Pil.ViewModels;
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
    public partial class InventarioPage : ContentPage
    {
        public InventarioPage()
        {
            InitializeComponent();
            var pag=App.Current.MainPage as  MasterDetailPage;
            var nav=pag.Detail as NavigationPage;
            nav.BackgroundColor = Color.LightBlue;
            nav.BarTextColor = Color.White;
        }
        protected override void OnAppearing()
        {
            BindingContext = new PrincipalPageViewModel();
        }

        private void btnañadir_Clicked(object sender, EventArgs e)
        {

        }

        private void btneliminar_Clicked(object sender, EventArgs e)
        {

        }

        private void btneditar_Clicked(object sender, EventArgs e)
        {

        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("posicion", e.ItemIndex.ToString(), "ok");
        }
    }
}