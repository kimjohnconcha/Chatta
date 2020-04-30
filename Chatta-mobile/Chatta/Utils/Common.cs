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

                navigationService.Configure(AppPages.MasterPage, typeof(MasterPage));
                navigationService.Configure(AppPages.HomePage, typeof(HomePage));
                navigationService.Configure(AppPages.TodoListPage, typeof(TodoListPage));
                navigationService.Configure(AppPages.RemindersPage, typeof(RemindersPage));
                navigationService.Configure(AppPages.ContactsPage, typeof(ContactsPage));

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

        public static void GoHomePage()
        {
            var navigationService = SimpleIoc.Default.GetInstance<IAppNavigationService>();
            var dialog = (DialogService)SimpleIoc.Default.GetInstance<IDialogService>();
            var firstPage = new NavigationPage(new HomePage());
            dialog.Initialize(firstPage);
            navigationService.Initialize(firstPage);

            Application.Current.MainPage = new HomePage();
            var masterPage = (MasterDetailPage)Application.Current.MainPage;
            masterPage.Detail = firstPage;
        }
    }
}
