using System;
using System.Collections.Generic;
using Chatta.ViewModel;
using Xamarin.Forms;

namespace Chatta.Pages
{
    public partial class LoginPage : ContentPage
    {
        //LoginViewModel _vm;
        public LoginPage()
        {
            BindingContext = App.Locator.Login;

            InitializeComponent();
        }
    }
}
