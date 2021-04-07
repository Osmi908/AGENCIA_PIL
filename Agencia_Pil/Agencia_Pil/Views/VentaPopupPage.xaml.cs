using Agencia_Pil.Models;
using Rg.Plugins.Popup.Pages;
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
    public partial class VentaPopupPage : PopupPage
    {
        public VentaPopupPage(Producto prod)
        {
            InitializeComponent();
            this.BindingContext = prod;
        }
    }
}