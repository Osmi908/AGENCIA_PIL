using Agencia_Pil.Views;
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
    public partial class RepotesPage : TabbedPage 
    {
        public ReportesViewModel RepVM { get; set; }
        public RepotesPage()
        {
            RepVM = new ReportesViewModel();
            InitializeComponent();
            lblSemana.Text = (DateTime.Now.DayOfYear) / 7+"";
            BindingContext = RepVM;
            pkTIpoReporte.ItemsSource = RepVM.TipoReportes;
        }

        private void btnGenerar_Reporte_Clicked(object sender, EventArgs e)
        {
            RepVM.CrearRporte(DtpkFecha.Date);
            lstVentas.ItemsSource = RepVM.ListaVentas.ToList();
            scrLista.IsVisible = true;
        }

        private void pkTIpoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker res = (Picker)sender;
            RepVM.Nro= res.SelectedIndex;
           

        }
    }
}