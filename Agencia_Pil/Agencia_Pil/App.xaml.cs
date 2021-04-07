using Agencia_Pil.Models;
using Agencia_Pil.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agencia_Pil
{
    public partial class App : Application
    {
        public static Usuario usuario { get; set; }
        public static string ArchivoDBAgenciaPil = Path.Combine(ApplicationData.Current.LocalFolder.Path, "DBAgenciaPil.db3");
        public App()
        {
            using (SQLiteConnection con = new SQLiteConnection(ArchivoDBAgenciaPil))
            {

                con.CreateTable<Inventario>();
                con.CreateTable<Usuario>();
                InitializeComponent();

                var usuarios = con.Table<Usuario>().ToList();
                if (usuarios.Count > 0)
                {
                    MainPage = new loginPage();
                }
                else
                {
                    MainPage = new registrUsuario();
                }
            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
