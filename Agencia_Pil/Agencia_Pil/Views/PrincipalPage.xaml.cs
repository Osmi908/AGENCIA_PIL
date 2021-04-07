﻿using Agencia_Pil.ViewModels;
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
    public partial class PrincipalPage : MasterDetailPage
    {
        public PrincipalPage()
        {
            InitializeComponent();
            this.BindingContext = new PrincipalPageViewModel();
        }
        public PrincipalPage(Usuario usuario)
        {
            InitializeComponent();
            this.BindingContext = new PrincipalPageViewModel();
            App.usuario = usuario;
            
        }

       
    }
}