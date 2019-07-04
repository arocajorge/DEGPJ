﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace APPPJ
{
    using APPPJ.ViewModels;
    using APPPJ.Views;

    public partial class App : Application
    {
        #region Propiedades
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        #endregion
        public App()
        {
            InitializeComponent();
            MainViewModel.GetInstance().Login = new LoginViewModel();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
