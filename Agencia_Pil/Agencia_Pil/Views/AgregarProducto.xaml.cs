using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Xamarin.Forms;
using Agencia_Pil.Models;
using Xamarin.Forms.Xaml;
using SQLite;
using Firebase.Database;
using Firebase.Database.Query;
using Windows.Storage;
using Xamarin.Essentials;

namespace Agencia_Pil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarProducto : ContentPage
    {
        public Stream varImagen { get; set; }
        public string Extencion { get; set; }
        public FirebaseClient firebase  { get;set; }
        public string imagenSeleccionada { get; set; }
        public AgregarProducto()
        {
            InitializeComponent();
        }
        async void PickFile()
        {
            var customFileType =
       new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
       {
       { DevicePlatform.iOS, new[] { "rar","zip" } },
       { DevicePlatform.Android, new[] { "rar" } },
       { DevicePlatform.UWP, new[] { "Jpeg","png","jpg" } },
       });
            // Opening the File Picker - Filter with Jpeg image
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select your picture",
                FileTypes = customFileType
            });

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                if (result.FullPath.Contains(".jpeg")|| result.FullPath.Contains(".jpg"))
                {
                    if (result.FullPath.Contains(".jpg"))
                    {
                        Extencion = ".jpg";
                    }
                    Extencion = ".jpeg";
                }
                else
                {
                    Extencion = ".png";
                }
                lblimagen.Text = result.FullPath;
                var stream = await result.OpenReadAsync();
                imgProducto.Source = ImageSource.FromStream(() => stream);
                varImagen = await result.OpenReadAsync();
                using (var memoryStream = new MemoryStream())
                {
                    varImagen.CopyTo(memoryStream);
                    var bytes= memoryStream.ToArray();
                    imagenSeleccionada = Convert.ToBase64String(bytes);
                }
                
                
            }
        }
        private async void btnseleccionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Acceder:
                //FileData filedata = await CrossFilePicker.Current.PickFile();
                //// the dataarray of the file will be found in filedata.DataArray 
                //// file name will be found in filedata.FileName;
                ////etc etc.

                //if (filedata.FileName.Contains(".jpg"))
                //{
                //    Extencion = ".jpg";
                //    lblimagen.Text = filedata.FilePath;
                //    imagenSeleccionada = filedata.FileName;
                //}
                //else
                //{

                //    if (filedata.FileName.Contains(".png"))
                //    {
                //        Extencion = ".png"; 
                //        lblimagen.Text = filedata.FilePath;
                //        imagenSeleccionada = filedata.FileName;
                //    }
                //    else
                //    {
                //        await DisplayAlert("El archivo No es una imagen", "Debe seleccionar una imagen con extencion 'jpg' o 'png'", "Ok");
                //        goto Acceder;
                //    }

                //}
                PickFile();


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
                
            }


        }

        private async void btnregistrar_Clicked(object sender, EventArgs e)
        {
            //    < !--marca = "ARUBA",
            //categoria = "BEBIDAS",
            //tipo = "REFRESCOS SABORIZADOS",
            //codigo = "bars01",
            //detalle = "2 litro",
            //imagen = "bars01.jpg",
            //Presentacion = "botella",
            //nombre = "Aruba Fruit Punch 2L",-->
            if (validarDatos())
            {
                Producto prod = new Producto()
                {
                    marca = txtmarca.Text,
                    categoria = txtcategoria.Text,
                    tipo = txttipo.Text,
                    codigo = txtcodigo.Text,
                    detalle = txtdetalle.Text,
                    imagen = imagenSeleccionada,
                    Presentacion = txtpresentacion.Text,
                    nombre = txtnombre.Text
                };
                using (SQLiteConnection cone =new SQLiteConnection(App.ArchivoDBAgenciaPil))
                {
                    cone.Insert(prod);
                    var usuarios=cone.Table<Usuario>().ToList();
                    for (int i = 0; i < usuarios.Count; i++)
                    {
                        firebase = new FirebaseClient("https://agenciapil-f8472-default-rtdb.firebaseio.com/" + usuarios[i].ci_usuario);
                        await firebase
                           .Child("Productos")
                           .PostAsync(prod);

                    }
                    
                }
            }
            

        }

        private bool  validarDatos()
        {
            bool result = false;
            if (!txtmarca.Text.Equals(""))
            {
                
                if (!txtcategoria.Text.Equals(""))
                {
                    if (!txttipo.Text.Equals(""))
                    {
                        if (!txtcodigo.Text.Equals(""))
                        {
                            if (!txtdetalle.Text.Equals(""))
                            {
                                if (!lblimagen.Text.Equals(""))
                                {
                                    if (!txtpresentacion.Text.Equals(""))
                                    {
                                        if (!txtnombre.Text.Equals(""))
                                        {
                                            result = true;
                                        }
                                        else
                                        {
                                            txtnombre.Placeholder = "INTRODUZCA UN NOMBRE!!!";
                                            txtnombre.BackgroundColor = Color.Violet;
                                        }

                                    }
                                    else
                                    {
                                        txtpresentacion.Placeholder = "INTRODUZCA UNA PRESENTACION!!!";
                                            txtpresentacion.BackgroundColor = Color.Violet;
                                    }

                                }
                                else
                                {
                                    lblimagen.Text = "SELECCIONE UNA IMAGEN!!";
                                    lblimagen.BackgroundColor = Color.Violet;
                                }
                            }
                            else
                            {
                                txtdetalle.Placeholder = "INTRODUZCA UN DETALLE!!!";
                                txtdetalle.BackgroundColor = Color.Violet;
                            }
                        }
                        else
                        {
                            txtcodigo.Placeholder = "INTRODUZCA UN CODIGO !!!";
                            txtcodigo.BackgroundColor = Color.Violet;
                        }
                    }
                    else
                    {
                        txttipo.Placeholder = "INTRODUZCA UN TIPO!!!";
                        txttipo.BackgroundColor = Color.Violet;
                    }
                }
                else
                {
                    txtcategoria.Placeholder = "INTRODUZCA UNA MARCA!!!";
                    txtcategoria.BackgroundColor = Color.Violet;
                }

            }
            else
            {
                txtmarca.Placeholder = "INTRODUZCA UNA MARCA!!!";
                txtmarca.BackgroundColor = Color.Violet;
            }
            return result;
        }

     
        private void txtmarca_Focused(object sender, FocusEventArgs e)
        {
            txtmarca.BackgroundColor = Color.Transparent;
            txtcategoria.BackgroundColor = Color.Transparent;
            txttipo.BackgroundColor = Color.Transparent;
            
           
            txtpresentacion.BackgroundColor = Color.Transparent;
            lblimagen.BackgroundColor = Color.Transparent;
            txtnombre.BackgroundColor = Color.Transparent;
            txtpresentacion.BackgroundColor = Color.Transparent;
            txtdetalle.BackgroundColor = Color.Transparent;
            txtcodigo.BackgroundColor = Color.Transparent;
        }
    }
}