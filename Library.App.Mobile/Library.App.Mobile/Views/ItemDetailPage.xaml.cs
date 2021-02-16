using System.ComponentModel;
using Xamarin.Forms;
using Library.App.Mobile.ViewModels;

namespace Library.App.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}