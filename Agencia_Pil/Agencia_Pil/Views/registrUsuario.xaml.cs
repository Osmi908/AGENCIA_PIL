using Agencia_Pil.Models;
using Firebase.Database;
using Firebase.Database.Query;
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
    public partial class registrUsuario : ContentPage
    {
        public static string urlP = "https://agenciapil-f8472-default-rtdb.firebaseio.com/";
        public registrUsuario()
        {
            InitializeComponent();
            string[] val = { "ADMINISTRADOR", "VENDEDOR" };
            pkrRol.ItemsSource = val;
        }

        private async  void btnCrear_Clicked(object sender, EventArgs e)
        {
            switch (Validar(txtpaterno.Text, txtMaterno.Text, txtNombres.Text, txtCi.Text, pkrRol.SelectedIndex.ToString(), txtcontraseña.Text, txtRepContraseña.Text))
            {
                case 0:
                default:
                    await DisplayAlert("Datos Invalidos", "Porfavor intente nuevamente", "OK");
                    LimpiarText();
                    break;
                case 1:
                    Usuario usus = new Usuario();
                    using (SQLite.SQLiteConnection con = new SQLite.SQLiteConnection(App.ArchivoDBAgenciaPil))
                    {
                        
                        usus.apPaterno = txtpaterno.Text;
                        usus.apMaterno = txtMaterno.Text;
                        usus.nombre = txtNombres.Text;
                        usus.rol = pkrRol.SelectedItem.ToString();
                        usus.password = txtcontraseña.Text;
                        usus.usuario = txtusuario.Text;
                        usus.ci_usuario = int .Parse(txtCi.Text);
                        usus.telefono = int.Parse(txttelefono.Text);
                        try
                        {
                            con.Insert(usus);
                            FirebaseClient firebase = new FirebaseClient(urlP);

                            await firebase
                                .Child("Usuarios")
                                .PostAsync(usus);
                            string uri = urlP + usus.ci_usuario;
                            firebase = new FirebaseClient(uri);
                            var productos = con.Table<Producto>().ToList();
                            for (int i = 0; i < productos.Count; i++)
                            {
                                await firebase.
                               Child("Productos")
                               .PostAsync(productos[i]);
                            }

                            var precios_Mayor = con.Table<Precio_Mayor>().ToList();
                            for (int i = 0; i < precios_Mayor.Count; i++)
                            {
                                await firebase.
                                  Child("Precios_Mayor")
                                  .PostAsync(precios_Mayor[i]);

                            }

                            var Precios_Menor = con.Table<Precio>().ToList();
                            for (int i = 0; i < Precios_Menor.Count; i++)
                            {
                                await firebase.
                                Child("Precios_Menor")
                                .PostAsync(Precios_Menor[i]);
                            }
                            await DisplayAlert("Listo ", "Creo el usuario " + txtusuario.Text, "OK");
                            try
                            {
                                await Navigation.PopToRootAsync();
                            }
                            catch (Exception)
                            {

                                App.Current.MainPage = new loginPage();
                            }

                        }
                        catch (Exception ex)
                        {

                            await DisplayAlert("Ya existe el usuario", ex.Message, "OK");
                        }
                        
                        

                        
                        
                    }
                    
                    
                        break;
                case 2:
                    await DisplayAlert("Error en la contraseña", "Las contraseñas no coinciden ", "OK");
                    txtRepContraseña.Text = "";
                    txtcontraseña.Text = "";
                    break;



               
            }

        }

        private void LimpiarText()
        {
            txtCi.Text = "";
            txtcontraseña.Text = "";
            txtMaterno.Text = "";
            txtpaterno.Text = "";
            txtRepContraseña.Text = "";
            pkrRol.SelectedItem = null;



        }

        private int Validar(string pat, string mat, string nom,string rol, string usu, string con1, string con2)
        {
            int res = 0;
            if (!pat.Equals(""))
            {
                if (!mat.Equals(""))
                {
                    if (!nom.Equals(""))
                    {
                        if (rol!=null)
                        {
                            if (!usu.Equals(""))
                            {
                                if (!con1.Equals(""))
                                {
                                    if (!con2.Equals(""))
                                    {
                                        if (con1.Equals(con2))
                                        {
                                            res = 1;
                                        }
                                        else
                                        {
                                            res = 2;
                                        }

                                    }
                                    else
                                    {
                                    }

                                }
                                else
                                {

                                }

                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                        
                }

            }
            else
            {

            }
            return res;
        }
    }
}