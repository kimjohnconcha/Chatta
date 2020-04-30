using System;
using Chatta.Pages;
using Chatta.Utils;
using Chatta.ViewModel.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chatta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

        public App()
        {
            InitializeComponent();

            Common.ConfigureIocContainer();
            Common.SetMainPage();
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
