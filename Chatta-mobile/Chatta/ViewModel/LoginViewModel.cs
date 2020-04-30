using System;
using System.Threading.Tasks;
using Chatta.Enums;
using Chatta.Pages;
using Chatta.Services;
using Chatta.Utils;
using Chatta.ViewModel.Base;
using Prism.Commands;
using Prism.Navigation;
using Unity;
using Xamarin.Forms;

namespace Chatta.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(async () => await Login());
        }

        IUnityContainer container = new UnityContainer();

        #region Methods

        public async Task Login()
        {
            if (IsBusy)
                return;

            //Common.GoHomePage();

            var a = new HomePage();

            
            var m = container.Resolve<UserService>();
            var res = m.TestBool("mim");

            System.Diagnostics.Debug.WriteLine("output: " + res.ToString());

            await _navigationService.NavigateAsync("ContactsPage");

            //Navigation.pus
            
        }

        #endregion



        #region Fields

        private readonly INavigationService _navigationService;

        #endregion




        #region Commands

        public DelegateCommand LoginCommand { get; }

        #endregion
    }
}
