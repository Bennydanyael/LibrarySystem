﻿using Library.App.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Library.App.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LibrarysPage : ContentPage
    {
        LibraryViewModel _viewModel;
        public LibrarysPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new LibraryViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}