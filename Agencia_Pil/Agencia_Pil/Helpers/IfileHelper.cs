using System;
using System.Collections.Generic;
using System.Text;

namespace AppCotizaciones.Helpers
{
    public interface IfileHelper
    {
        string GetDirectory();
        void SuscribirseTopic(string topic);
    }
}
