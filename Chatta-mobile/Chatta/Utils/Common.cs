using System;
using Chatta.Enums;
using Chatta.Helpers;
using Chatta.Pages;
using Chatta.Services;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace Chatta.Utils
{
    public static class Common
    {
        public static void ConfigureIocContainer()
        {
            if (!SimpleIoc.Default.IsRegistered<IAppNavigationService>())
            {
                var navigationService = new NavigationService();

                navigationService.Configure(AppPages.LoginPage, typeof(LoginPage));

                // Register NavigationService in IoC container:
                SimpleIoc.Default.Register<IAppNavigationService>(() => navigationService);
            }

            var dialog = new DialogService();
            if (!SimpleIoc.Default.IsRegistered<IDialogService>())
                SimpleIoc.Default.Register<IDialogService>(() => dialog);
        }

        public static void SetMainPage()
        {
            GoLoginPage();
        }

        public static void GoLoginPage()
        {
            var firstPage = new NavigationPage(new LoginPage());
            Application.Current.MainPage = firstPage;
        }
    }
}
