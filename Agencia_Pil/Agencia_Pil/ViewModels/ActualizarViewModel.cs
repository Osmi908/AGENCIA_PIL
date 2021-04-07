using Agencia_Pil.Helpers;
using Agencia_Pil.Models;
using AppCotizaciones.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agencia_Pil.ViewModels
{
    public class ActualizarViewModel
    {
        public string Monto_Total { get; set; }
        public string Cantidad_Total { get; set; }
        public List<Venta> ventas { get; set; }
        public FirebaseHelper firebase { get; set; }
        public ActualizarViewModel()
        {
            ventas = new List<Venta>();
            firebase = new FirebaseHelper();
            ventas = firebase.GetAllArticulos();
            Monto_Total = firebase.GetMontoTotal();
            Cantidad_Total = firebase.GetCantidadTotal();
        }
    }
}
