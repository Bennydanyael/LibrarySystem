using System;
using System.Collections.Generic;
using Library.App.Mobile.ViewModels;
using Library.App.Mobile.Views;
using Xamarin.Forms;

namespace Library.App.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(BookDetailPage), typeof(BookDetailPage));
            Routing.RegisterRoute(nameof(NewBookPage), typeof(NewBookPage));
            Routing.RegisterRoute(nameof(AuthorDetailPage), typeof(AuthorDetailPage));
            Routing.RegisterRoute(nameof(NewAuthorPage), typeof(NewAuthorPage));
            Routing.RegisterRoute(nameof(LibraryDetailPage), typeof(LibraryDetailPage));
            Routing.RegisterRoute(nameof(NewLibraryPage), typeof(NewLibraryPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
