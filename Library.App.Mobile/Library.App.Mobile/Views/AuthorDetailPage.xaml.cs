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
    public partial class AuthorDetailPage : ContentPage
    {
        public AuthorDetailPage()
        {
            InitializeComponent();
            BindingContext = new AuthorsViewModel();
        }
    }
}