using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chatta.Enums;
using Xamarin.Forms;

namespace Chatta.Services
{
    public class NavigationService : IAppNavigationService
    {
        // Dictionary with registered pages in the app
        private readonly Dictionary<AppPages, Type> _pagesByKey = new Dictionary<AppPages, Type>();

        // Navigation page where MainPage is hosted
        private NavigationPage _navigation;

        // Get currently displayed page
        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (_navigation.CurrentPage == null) return null;

                    var pageType = _navigation.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey
                            .First(p => p.Value == pageType).Key
                            .ToString()
                        : null;
                }
            }
        }

        public void Configure(AppPages pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                    _pagesByKey[pageKey] = pageType;
                else
                    _pagesByKey.Add(pageKey, pageType);
            }
        }

        public void GoBack()
        {
            _navigation.PopAsync();
        }

        public void GoListPage()
        {
            _navigation.Navigation.RemovePage(
                _navigation.Navigation.NavigationStack[_navigation.Navigation.NavigationStack.Count - 2]);
            _navigation.PopAsync();
        }

        public void GoToRoot()
        {
            _navigation.PopToRootAsync();
        }

        // Initialize first app page (navigation page)
        public void Initialize(NavigationPage navigation)
        {
            _navigation = navigation;
        }

        public void InsertPageBeforeLastPage(AppPages pageKey, object parameter)
        {
            var page = GetPage(pageKey, parameter, null);
            var lastPage = _navigation.Navigation.NavigationStack.Last();
            _navigation.Navigation.InsertPageBefore(page, lastPage);
        }

        public void InsertPageBeforeLastPage(AppPages pageKey, object parameter1, object parameter2)
        {
            var page = GetPage(pageKey, parameter1, parameter2);
            var lastPage = _navigation.Navigation.NavigationStack.Last();
            _navigation.Navigation.InsertPageBefore(page, lastPage);
        }

        // NavigateTo method to navigate between pages without passing parameter
        public void NavigateTo(AppPages pageKey)
        {
            NavigateTo(pageKey, null);
        }

        // Two or more parameters
        public void NavigateTo(AppPages pageKey, object parameter1, object parameter2)
        {
            NavigateTo(pageKey, parameter1, parameter2, null, null, null, null, false);
        }

        // NavigateTo method to navigate between pages with passing parameter
        public void NavigateTo(AppPages pageKey, object parameter)
        {
            var page = GetPage(pageKey, parameter, null);
            _navigation.PushAsync(page);
        }

        private void NavigateTo(AppPages pageKey, object parameter1, object parameter2, object parameter3,
            object parameter4, object parameter5, object parameter6, bool replaceRoot)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    var type = _pagesByKey[pageKey];
                    ConstructorInfo constructor;
                    var p = new List<object>();
                    if (parameter1 != null)
                        p.Add(parameter1);
                    if (parameter2 != null)
                        p.Add(parameter2);
                    if (parameter3 != null)
                        p.Add(parameter3);
                    if (parameter4 != null)
                        p.Add(parameter4);
                    if (parameter5 != null)
                        p.Add(parameter5);
                    if (parameter6 != null)
                        p.Add(parameter6);
                    var parameters = p.ToArray();
                    constructor = GetConstructor(type, parameters);
                    if (constructor == null)
                    {
                        var exceptionMessage = $"No suitable constructor found for page {pageKey}";
                        throw new InvalidOperationException(exceptionMessage);
                    }

                    if (!replaceRoot)
                    {


                        try
                        {
                            var page = constructor.Invoke(parameters) as Page;
                            _navigation.PushAsync(page, false);
                        }
                        catch (TargetInvocationException e)
                        {
                            MessagingCenter.Send<NavigationService>(this, "TargetInvocationException");
                        }
                    }
                    else
                    {
                        var page = constructor.Invoke(parameters) as Page;
                        var root = _navigation.Navigation.NavigationStack.First();
                        _navigation.Navigation.InsertPageBefore(page, root);
                        _navigation.PopToRootAsync(false);
                    }
                }
                else
                {
                    var exceptionMessage =
                        $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?";
                    throw new ArgumentException(exceptionMessage, nameof(pageKey));
                }
            }
        }

        private ConstructorInfo GetConstructor(Type type, object[] parameters)
        {
            var parameterCount = parameters.Length;
            ConstructorInfo constructor;
            if (parameterCount > 0)
                constructor = type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(
                    c =>
                    {
                        var p = c.GetParameters();
                        return p.Count() == parameterCount && p[parameterCount - 1]
                                   .ParameterType == parameters[parameterCount - 1].GetType();
                    });
            else
                constructor = type.GetTypeInfo()
                    .DeclaredConstructors
                    .FirstOrDefault(c => !c.GetParameters().Any());
            return constructor;
        }

        private Page GetPage(AppPages pageKey, object parameter1, object parameter2)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    var type = _pagesByKey[pageKey];

                    var p = new List<object>();
                    if (parameter1 != null)
                        p.Add(parameter1);
                    if (parameter2 != null)
                        p.Add(parameter2);
                    var parameters = p.ToArray();

                    var constructor = GetConstructor(type, parameters);

                    if (constructor == null)
                        throw new InvalidOperationException(
                            "No suitable constructor found for page " + pageKey);

                    var page = constructor.Invoke(parameters) as Page;
                    //_navigation.PushAsync(page);

                    return page;
                }

                throw new ArgumentException($"No such page: {nameof(pageKey)}. Did you forget to call NavigationService.Configure?");
            }
        }
    }
}
