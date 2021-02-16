using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Library.App.Mobile.Services;
using Library.App.Mobile.Views;
using Xamarin.Essentials;

namespace Library.App.Mobile
{
    public partial class App : Application
    {
        public static string BackendURL = DeviceInfo.Platform ==
            DevicePlatform.Android ? "http://192.168.0.3:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = false;

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            if (UseMockDataStore)
            {
                DependencyService.Register<MockDataStore>();
                MainPage = new AppShell();
            }
            else
            {
                DependencyService.Register<MockDataStore>();
                DependencyService.Register<BooksDataStore>();
                DependencyService.Register<AuthorDataStore>();
                DependencyService.Register<LibraryDataStore>();
                MainPage = new AppShell();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
