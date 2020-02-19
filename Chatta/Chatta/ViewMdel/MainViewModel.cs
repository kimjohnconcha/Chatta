using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace Chatta.ViewMdel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ICommand NavigateCommand { get; private set; }


        public MainViewModel(INavigationService navigationService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            /// 
            /// 
            _navigationService = navigationService;
            NavigateCommand = new Command(() => Navigate());
        }

        private void Navigate()
        {
            _navigationService.NavigateTo(AppPages.DetailsPage);
        }
    }
}
