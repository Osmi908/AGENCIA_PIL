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
    public partial class ActualizarPage : TabbedPage
    {
        public ActualizarPage()
        {
            InitializeComponent();
            BindingContext = new ActualizarViewModel();
        }

        private void listaProd_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void listaprod_sin_precio_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void btnRegVentas_Clicked(object sender, EventArgs e)
        {

        }
    }
}