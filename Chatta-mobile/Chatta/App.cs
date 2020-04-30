using System;
using Chatta.Pages;
using Chatta.Services;
using Chatta.Utils;
using Chatta.ViewModel;
using Chatta.ViewModel.Base;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chatta
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer = null) : base(platformInitializer) { }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<ContactsPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.Register<IUserService, UserService>();

        }
    }
}
