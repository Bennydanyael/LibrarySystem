using Library.App.Mobile.Models;
using Library.App.Mobile.ViewModels;
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
    public partial class NewBookPage : ContentPage
    {
        public Books Book { get; set; }
        public NewBookPage()
        {
            InitializeComponent();
            BindingContext = new NewBookViewModel();
        }
    }
}