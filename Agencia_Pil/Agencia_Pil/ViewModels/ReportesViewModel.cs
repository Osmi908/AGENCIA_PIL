using Agencia_Pil;
using Agencia_Pil.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agencia_Pil.Views
{
    public class ReportesViewModel :INotifyPropertyChanged
    {
        public List<Venta> ListaVentas
        {
            set
;
            get
;
        }
        public string[] TipoReportes = { "DIARIO", "MENSUAL", "ANUAL" };
        int nro;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int Nro
        {
            set
            {
                if (nro != value)
                {
                    nro = value;
                    SetPickerFecha(nro);
                }
            }
            get
            {
                return nro;
            }
        }

        internal void CrearRporte(DateTime date)
        {
            ListaVentas = new List<Venta>();
            using (SQLiteConnection conn= new SQLiteConnection(App.ArchivoDBAgenciaPil))
            {
                DateTime dat;
                string query;
                var ventas=conn.Table<Venta>().ToList();
                if (Nro == 0)
                {
                    for (int i = 0; i < ventas.Count; i++)
                    {
                        dat = ventas[i].fecha;

                        if (date.Day == dat.Day && date.Month == dat.Month && dat.Year == date.Year)
                        {
                            ListaVentas.Add(ventas[i]);
                        }
                    }
                    
                }
                else
                {
                    if (Nro == 1)
                    {
                        for (int i = 0; i < ventas.Count; i++)
                        {
                            dat = ventas[i].fecha;

                            if ( date.Month == dat.Month && dat.Year == date.Year)
                            {
                                ListaVentas.Add(ventas[i]);
                            }
                        }

                    }
                    else
                    {
                        for (int i = 0; i < ventas.Count; i++)
                        {
                            dat = ventas[i].fecha;

                            if ( dat.Year == date.Year)
                            {
                                ListaVentas.Add(ventas[i]);
                            }
                        }

                    }
                }
            }
        }

        private void SetPickerFecha(int nro)
        {
            string cad = "";
            if (nro==0)
            {
                cad = "dd -MMMM-yyyy";
            }
            else
            {
                if (nro==1)
                {
                    cad = "MMMM-yyyy";
                }
                else
                {
                    cad = "yyyy";
                }
            }
            FormatoFecha = cad;
            OnPropertyChanged("FormatoFecha");
        }

        public string FormatoFecha { get; set; }
       
        public ReportesViewModel()
        {
            ListaVentas = new List<Venta>();

                FormatoFecha = "dd-MM-yyyy";

        }
    }
}
