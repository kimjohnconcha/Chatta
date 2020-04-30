using System;
using Prism.Mvvm;
using Xamarin.Forms;

namespace Chatta.ViewModel.Base
{
    public class BaseViewModel : BindableBase
    {
        public BaseViewModel()
        {
            _title = string.Empty;
            _isBusy = false;

        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

    }
}
