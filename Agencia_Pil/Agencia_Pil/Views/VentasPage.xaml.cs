using Agencia_Pil.Models;
using Agencia_Pil.ViewModels;
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
    public partial class VentasPage : ContentPage
    {
        public string nit_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public int CANTIDAD_TOTAL { get; set; }
        public decimal MONTO_TOTAL { get; set; }
        public VentasViewModel ventasVM { get; set; }
        public List<Producto_Precio> listaVenta { get; set; }
        public VentasPage()
        {
            CANTIDAD_TOTAL = 0;
            InitializeComponent();
            ventasVM = new VentasViewModel();
            this.BindingContext = ventasVM;
            pkrop.SelectedItem = ventasVM.opcionesDeBusqueda[0];
            listaVenta = new List<Producto_Precio>();
            ListViewProdu.ItemsSource = listaVenta.ToList();
            lblnroventa.Text = "NUMERO DE VENTA: " + ventasVM.nroVenta.ToString();
            lblusuario.Text=" VENDEDOR : "+App.usuario.nombre+" "+ App.usuario.apPaterno;


        }
        

        private void sbarBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tipoBusqueda=pkrop.SelectedItem.ToString();
            ventasVM.buscarPor(tipoBusqueda, e.NewTextValue);
            ListViewProd.ItemsSource = ventasVM.productos.ToList();
           

        }

        private async void ListViewProd_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var prod = (producto_precio_cantidad)e.Item;
            string result = await DisplayPromptAsync("Agregar a la venta", prod.nombre,"Agregar","Cancelar","cantidad",3,Keyboard.Numeric,"0");
            try
            {
                if ( result!=null)
                {
                    if (!result.Equals("0"))
                    {
                        int cantidad = Convert.ToInt32(result);
                        if (cantidad <= ventasVM.VreificarCantidad(prod))
                        {
                            int ca = 0;
                            int c=0;
                            bool swich = true;
                            while (swich&& c<listaVenta.Count)
                            {
                                if (listaVenta.Count>0)
                                {
                                    if (prod.codigo.Equals(listaVenta[c].cod_producto))
                                    {
                                        swich = false;
                                        ca = c;
                                    }
                                    c++;
                                }
                               
                            }
                            if (swich)
                            {
                                var prdpre = new Producto_Precio(prod, cantidad);
                                listaVenta.Add(prdpre);
                                ListViewProdu.ItemsSource = listaVenta.ToList();
                                CANTIDAD_TOTAL += cantidad;
                                MONTO_TOTAL += prdpre.precio_cantidad;
                                lblCantidadTotal.Text = "MONTO A PAGAR : " + MONTO_TOTAL + " Bs";
                                lblMontoTotal.Text = "TOTAL:" + CANTIDAD_TOTAL + "";


                            }
                            else
                            {
                                var cou = listaVenta[ca].cantidad + cantidad;
                                if (cou <= ventasVM.VreificarCantidad(prod))
                                {
                                    MONTO_TOTAL -= listaVenta[ca].precio_cantidad;
                                   listaVenta[ca].cantidad = cou;
                                    listaVenta[ca].precio_cantidad = listaVenta[ca].CalculaPrecioTotal(listaVenta[ca].precio_unitario, cou);
                                    ListViewProdu.ItemsSource = listaVenta.ToList();
                                    CANTIDAD_TOTAL += cantidad;
                                   
                                    MONTO_TOTAL += listaVenta[ca].precio_cantidad;
                                    lblCantidadTotal.Text = "MONTO A PAGAR : " + MONTO_TOTAL + " Bs";
                                    lblMontoTotal.Text = "TOTAL:" + CANTIDAD_TOTAL + "";
                                }
                                else
                                {
                                    await DisplayAlert("Error Cantidad", "ya añadiste el articulo a la lista y la suma de la cantidad es mayor a las eistencias en el inventario", "Entiendo");
                                }

                            }


                        }
                        else
                        {
                            await DisplayAlert("Cantidad de producto ", "La cantidad de productos no coincide con el inventario, por favor revise las existencias del inventario y/o ingrese a la pestaña de Ventas sin Registro", "OK");
                        }
                    }
                    
                }

                
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Porfvor introduzca un valor valido para la cantidad de productos","OK");
            }
        }



        private async void btnvender_Clicked(object sender, EventArgs e)
        {
            nombre_cliente = txtnombre.Text;
            nit_cliente = txtnit.Text;
            if (nombre_cliente.Equals(""))
            {
                nombre_cliente = "S/N";
                
            }
            if (nit_cliente.Equals(""))
            {
                nit_cliente = "0000";
            }
            if (listaVenta.Count>0)
            {
                DateTime fecha = DateTime.Now;
                using (SQLiteConnection cone=new SQLiteConnection(App.ArchivoDBAgenciaPil))
                {
                    cone.CreateTable<Cliente>();
                    var clientes= cone.Query<Cliente>("Select * From Cliente WHERE nit like '" + nit_cliente+ "' and nombre like '" + nombre_cliente + "'");
                    if (clientes.Count>0)
                    {

                        cone.CreateTable<Venta>();
                        Venta venta = new Venta() { id_venta = ventasVM.nroVenta, ci_usuario = App.usuario.ci_usuario, id_cliente = int.Parse(clientes[0].nit),
                            cantidad_productos = CANTIDAD_TOTAL, Monto_total = MONTO_TOTAL, estado="finalizado", fecha=DateTime.Now };
                        cone.Insert(venta);
                    }
                    else
                    {
                        Cliente cliente = new Cliente();
                        cliente.fecha_creacion = DateTime.Now;
                        cliente.nit = nit_cliente;
                        cliente.nombre = nombre_cliente;
                        if (await DisplayAlert("Añadir Cliente", App.usuario.nombre + " Deseas Añadir a este cliente?", "Si", "No, Gracias"))
                        {
                           var telefono= await DisplayPromptAsync("Añadir Cliente " + txtnombre.Text, "Numero de Celular(Whatsapp) del cliente", "OK", "Cancelar", "Ej: 70707070", 8, Keyboard.Telephone,"");
                            cliente.Enviar_mensaje = 1;
                            if (telefono!=null)
                            {
                                cliente.telefono = int.Parse(telefono);
                            }
                            else
                            {
                                cliente.telefono = 0; 
                            }
                            
                        }
                        else
                        {
                            cliente.telefono = 0;
                            cliente.Enviar_mensaje = 0;
                        }
                        cone.Insert(cliente);
                        cone.CreateTable<Venta>();
                        Venta venta = new Venta()
                        {
                            id_venta = ventasVM.nroVenta,
                            ci_usuario = App.usuario.ci_usuario,
                            id_cliente = 0,
                            cantidad_productos = CANTIDAD_TOTAL,
                            Monto_total = MONTO_TOTAL,
                            estado = "finalizado",
                            fecha = DateTime.Now
                        };
                        cone.Insert(venta);
                        cone.CreateTable<Detalle_Venta>();
                        for (int i = 0; i < listaVenta.Count; i++)
                        {
                            cone.Insert(new Detalle_Venta()
                            {
                                cantidad = listaVenta[i].cantidad,
                                codigo_producto = listaVenta[i].cod_producto,
                                id_venta = ventasVM.nroVenta,
                                precio_total = listaVenta[i].precio_cantidad,
                                precio_unitario = listaVenta[i].precio_unitario
                            });

                            var nro_filas=cone.Execute("Update inventario set cantidad=cantidad - "+listaVenta[i].cantidad+" WHERE id_producto like '"+listaVenta[i].cod_producto+"'");
                        }
                    }
                    await DisplayAlert("EXITO!!", "SE REALIZO LA VENTA Nº " + ventasVM.nroVenta + " CON EXITO !!!","LISTO");
                    await Navigation.PopAsync();
                    var pag=(MasterDetailPage)App.Current.MainPage;
                    pag.Detail.Navigation.PushAsync(new VentasPage());


}

            }
            else
            {
                await DisplayAlert("Sin prodctos","La venta o se ´puede realizar sin registrar productos","OK");
            }
        }

        private async void ListViewProdu_ItemTapped(object sender, ItemTappedEventArgs e)
        {


        }

        private void txtnit_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (SQLiteConnection conn= new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                long ni = 0;
                if (e.NewTextValue!=null)
                {
                    if (!e.NewTextValue.Equals(""))
                    {
                        ni = long.Parse(e.NewTextValue);
                        string query = "SELECT * FROM cliente WHERE nit =" + ni;
                        var clientes = conn.Query<Cliente>(query);
                        if (clientes.Count > 0)
                        {
                            txtnombre.Text = clientes[0].nombre;
                        }
                    }
                    

                }
                
                

                
            }
        }

        private async void ListViewProdu_ItemTapped_1(object sender, ItemTappedEventArgs e)
        {
            var prod = (Producto_Precio)e.Item;
            if (await DisplayAlert("ELIMINAR!", "Seguro que quieres eliminar este producto?", "ESTOY SEGURO", "CANCELAR"))
            {
                listaVenta.Remove(prod);
                CANTIDAD_TOTAL -= prod.cantidad;
                MONTO_TOTAL -= prod.precio_cantidad;
                lblCantidadTotal.Text = "MONTO A PAGAR : " + MONTO_TOTAL + " Bs";
                lblMontoTotal.Text = "TOTAL:" + CANTIDAD_TOTAL + "";
                ListViewProdu.ItemsSource = listaVenta.ToList();
            }
        }

        private void ListViewProd_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            
        }

        private void ListViewProd_ScrollToRequested(object sender, ScrollToRequestEventArgs e)
        {
            
        }
    }
}