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
    public partial class loginPage : ContentPage
    {
        public loginPage()
        {
            
            InitializeComponent();
        }

        private async void btningresar_Clicked(object sender, EventArgs e)
        {
            if (true)
            {
                string usuario = txtusuario.Text;
                string contraseña = txtcontraseña.Text;
                using (SQLiteConnection conection = new SQLiteConnection(App.ArchivoDBAgenciaPil))
                {

                    conection.CreateTable<Usuario>();
                    var usuarios=conection.Query<Usuario>("SELECT * FROM Usuario WHERE usuario like 'admin' and password= 12345");
                    if (usuarios.Count>0)
                    {
                        string nombreCompleto= "Hola!! "+usuarios[0].nombre+" "+ usuarios[0].apPaterno + " "+usuarios[0].apMaterno +" eres "+usuarios[0].rol+"'";
                        await DisplayAlert("BIENVENIDO !!", nombreCompleto, "OK");
                        App.Current.MainPage = new PrincipalPage(usuarios[0]);
                    }
                    else
                    {
                        await DisplayAlert("Datos incorrectos", "intenta nuevamente", "OK");
                    }
                }
                

            }
            else
            {
                await DisplayAlert("ingrese Datos", "Por favor ingrese un usuario  contraseña", "");
            }
            
        }
    }
}