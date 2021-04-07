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
    public partial class PrincipalPageDetail : ContentPage
    {
        public PrincipalPageDetail()
        {
            InitializeComponent();
        }

        private void btninventario_Clicked(object sender, EventArgs e)
        {
            var pag = new InventarioPage();
            Navigation.PushAsync(pag);
        }
        protected override void OnAppearing()
        {
            App.Current.MainPage.BindingContext = new PrincipalPageViewModel();
        }
    }
}