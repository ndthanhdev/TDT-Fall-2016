﻿using CentreGUI.Services;
using System.Windows;

namespace CentreGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MappingServices.RegisterMappings();
        }
    }
}