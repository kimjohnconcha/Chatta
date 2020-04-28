using System;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace Chatta.ViewModel.Base
{
    public class BaseViewModel : ViewModelBase
    {
        public Page Page { get; set; }

        public BaseViewModel()
        {
            _title = string.Empty;
            _isBusy = false;

        }

        private string _title;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        //public bool SendLocation()
        //{
        //    Common.SendLocation();
        //    return true;
        //}
    }
}
