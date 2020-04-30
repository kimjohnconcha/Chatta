using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chatta.Enums;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace Chatta.Services
{
    public class NavigationService : INavigationService
    {
        public string CurrentPageKey => throw new NotImplementedException();

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey)
        {
            //throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            //throw new NotImplementedException();
        }
    }
}
