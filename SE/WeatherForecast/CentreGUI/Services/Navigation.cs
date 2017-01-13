using CentreGUI.Views;
using MahApps.Metro.Controls;
using System;

namespace CentreGUI.Services
{
    public class Navigation
    {
        private static MetroNavigationWindow _window;

        public static MetroNavigationWindow Current
        {
            get { return _window ?? (_window = initialize()); }
        }

        private static MetroNavigationWindow initialize()
        {
            var window = new MetroNavigationWindow();
            window.Navigate(new RegionPage());
            return window;
        }

        private static void Navigation_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}