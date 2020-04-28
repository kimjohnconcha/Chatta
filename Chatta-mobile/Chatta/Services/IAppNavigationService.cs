using System;
using Chatta.Enums;
using Xamarin.Forms;

namespace Chatta.Services
{
    public interface IAppNavigationService
    {
        void Configure(AppPages pageKey, Type pageType);
        void Initialize(NavigationPage page);
        void GoBack();
        void GoToRoot();
        void GoListPage();
        void NavigateTo(AppPages pageKey);
        void NavigateTo(AppPages pageKey, object parameter);
        void InsertPageBeforeLastPage(AppPages pageKey, object parameter);

        void InsertPageBeforeLastPage(AppPages pageKey, object parameter1, object parameter2);

        void NavigateTo(AppPages pageKey, object parameter1, object parameter2);
    }
}
