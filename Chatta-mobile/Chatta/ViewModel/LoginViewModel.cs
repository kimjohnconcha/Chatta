using System;
using System.Threading.Tasks;
using Chatta.Enums;
using Chatta.Pages;
using Chatta.Services;
using Chatta.Utils;
using Chatta.ViewModel.Base;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Chatta.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(IAppNavigationService navigationService)
        {
            System.Diagnostics.Debug.WriteLine("Login viewmodel");

            if (navigationService == null)
            {
                System.Diagnostics.Debug.WriteLine("nav service null");
            }


            _navigationService = navigationService;
            LoginCommand = new RelayCommand(async () => await Login());
        }


        #region Methods

        public async Task Login()
        {
            if (IsBusy)
                return;

            //Common.GoHomePage();

            var a = new HomePage();

            _navigationService.NavigateTo(AppPages.RemindersPage);

            //Navigation.pus
            
        }

        #endregion



        #region Fields

        private readonly IAppNavigationService _navigationService;

        #endregion




        #region Commands

        public RelayCommand LoginCommand { get; }

        #endregion
    }
}
