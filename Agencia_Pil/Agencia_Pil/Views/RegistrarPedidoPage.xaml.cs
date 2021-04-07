using Agencia_Pil.Models;
using Agencia_Pil.ViewModels;
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
    public partial class RegistrarPedidoPage : ContentPage
    {
        public int nroPedido { get; set; }
        public int cantidad_prod { get; set; }
        public decimal precio_uni { get; set; }
        public decimal precio_total { get; set; }
        List<Producto_pedido> Listaproductoss { get; set; }
        RegistrarPedidoViemModel registrarVM { get; set; }

        public RegistrarPedidoPage()
        {
            Listaproductoss = new List<Producto_pedido>();
            registrarVM = new RegistrarPedidoViemModel();
            InitializeComponent();
            BindingContext = registrarVM;
            cantidad_prod = 0;
            precio_uni = 0;
            precio_total = 0;
            lblCant_Prod.Text = cantidad_prod.ToString();
            lblMontoTotal.Text = precio_total.ToString();
            nroPedido = registrarVM.ObtenerNroRegProd();
            lblnroventa.Text = "REGISTRO DE PEDIDO Nº "+nroPedido;
        }
        protected override void OnAppearing()
        {
            registrarVM = new RegistrarPedidoViemModel();
            BindingContext = registrarVM;
        }
        private void sbarBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tipoBusqueda = pkrop.SelectedItem.ToString();
            registrarVM.buscarPor(tipoBusqueda, e.NewTextValue);
            ListViewProd.ItemsSource = registrarVM.productos.ToList();
        }

        private async void ListViewProd_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Producto_pedido pd = new Producto_pedido();
            pd.producto = new Producto();
            var es=(Producto)e.Item;

            
            string cade = "1234567891011121314151617181920212423242526272829303132333435363738394041424344454647484950515253545556575859606162636465666768698081828384858687888990919293949596979998991001102103104105106";
            var dat = await DisplayPromptAsync("CANTIDAD", "ingrese la cantidad de  " + es.nombre + " que ingresara", "Siguiente", "Cancel", "Cantidad de " + es.nombre, -1, Keyboard.Numeric, "");
            if (dat!=null)
            {
                try 
                {
                    pd.cantidad = int.Parse(dat);
                    pd.precio_unitario = decimal.Parse(await DisplayPromptAsync("PRECIO UNITARIO", "ingrese el precio por unidad del producto  " + es.nombre, "Siguiente"));
                    pd.precio_Total = decimal.Parse(await DisplayPromptAsync("PRECIO DE COMPRA", "ingrese el precio total del producto  " + es.nombre, "ACEPTAR"));
                    pd.producto = es;
                    pd.numero = Listaproductoss.Count + 1;
                    Listaproductoss.Add(pd);
                    ListView_productos.ItemsSource = Listaproductoss.ToList();
                    cantidad_prod += pd.cantidad;
                    precio_total += pd.precio_Total;
                    lblMontoTotal.Text = String.Format("MONTO TOTAL: {0:##.##} Bs.", precio_total.ToString());
                    lblCant_Prod.Text = String.Format("CANTIDAD TOTAL: {0:F2} ", cantidad_prod.ToString());
                }
                catch(Exception ex)
                {
                    await DisplayAlert("Error",ex.Message,"Aceptar");
                }
            }
           
        }

        private async void btnregistrar_Clicked(object sender, EventArgs e)
        {
            var fechares=datepkr_recepcion.Date;
            DateTime fecha_registo = DateTime.Now;
            var cantidad_pedido = cantidad_prod;
            var monto_total = precio_total;
            var id_registro_pedido=nroPedido;
            if (Listaproductoss.Count>0)
            {
                using (SQLiteConnection conexion = new SQLiteConnection(App.ArchivoDBAgenciaPil))
                {
                    conexion.CreateTable<Inventario>();
                    conexion.CreateTable<Pedido>();
                    conexion.CreateTable<DetallePedido>();
                    conexion.Insert(new Pedido() { ci_usuario = App.usuario.ci_usuario, Estado = 1, fecha_Peticion = datepkr_recepcion.Date, fecha_Recepcion = (DateTime.Parse(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"))), Importe_Total = precio_total, Cantidad_Total = cantidad_prod }); ;
                    for (int i = 0; i < Listaproductoss.Count; i++)
                    {
                        var resp = conexion.Query<Inventario>("SELECT * From inventario where id_producto like '" + Listaproductoss[i].producto.codigo + "'");
                        if (resp.Count > 0)
                        {
                            string query = "update inventario set cantidad=cantidad+" + Listaproductoss[i].cantidad + " where id_producto like '" + Listaproductoss[i].producto.codigo + "'";
                            var resps = conexion.Execute(query);


                        }
                        else
                        {
                            conexion.Insert(new Inventario()
                            {
                                id_inventario = 0,
                                cantidad = Listaproductoss[i].cantidad,
                                id_producto = Listaproductoss[i].producto.codigo,
                                detalle = "Se introducio por pedidos"

                            }); ;

                        }
                        conexion.Insert(new DetallePedido() {codigo_producto= Listaproductoss[i].producto.codigo,cantidad= Listaproductoss[i].cantidad,precio_Unitario= Listaproductoss[i].precio_unitario
                        , precio_total= Listaproductoss[i].precio_Total , id_pedido=registrarVM.ObtenerIdMax()});

                    }
                    

                }
                await DisplayAlert("LISTO!","El pedido numero "+nroPedido+" Se registro con exito!","OK");
                await Navigation.PopAsync();
                var pag = (MasterDetailPage)App.Current.MainPage;
                pag.Detail.Navigation.PushAsync(new RegistrarPedidoPage());
            }
            else
            {
                await DisplayAlert("Pedido Vacio","El pedido esta vacio!!","OK");
            }
            
            

        }

        private void Label_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void Label_Unfocused_1(object sender, FocusEventArgs e)
        {

        }

        private void txt_precio_tot_Unfocused(object sender, FocusEventArgs e)
        {

        }
    }
}