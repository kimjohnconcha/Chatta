using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Chatta.ViewModel.Base
{
    public class ViewModelLocator
    {
        private LoginViewModel _login;

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<LoginViewModel>();
        }

        public LoginViewModel Login
        {
            get
            {
                //if (_login != null) SimpleIoc.Default.Unregister(_login);
                //_login = ServiceLocator.Current.GetInstance<LoginViewModel>();
                //return _login;

                if (!SimpleIoc.Default.IsRegistered<LoginViewModel>())
                {
                    SimpleIoc.Default.Register<LoginViewModel>();
                }
                return SimpleIoc.Default.GetInstance<LoginViewModel>();
            }
        }
    }
}
