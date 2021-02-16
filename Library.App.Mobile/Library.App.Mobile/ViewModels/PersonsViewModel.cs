using Library.App.Mobile.Models;
using Library.App.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Library.App.Mobile.ViewModels
{
    public class PersonsViewModel : BaseViewModel
    {
        private Customers _selectedItem;

        public ObservableCollection<Customers> Customers { get; }
        public Command LoadCustomersCommand { get; }
        public Command AddCustomersCommand { get; }
        public Command<Customers> CustomerTapped { get; }

        public PersonsViewModel()
        {
            Title = "Books";
            Customers = new ObservableCollection<Customers>();
            LoadCustomersCommand = new Command(async () => await ExecuteLoadCustomersCommand());

            CustomerTapped = new Command<Customers>(OnCustomerSelected);

            AddCustomersCommand = new Command(OnAddCustomer);
        }

        async Task ExecuteLoadCustomersCommand()
        {
            IsBusy = true;

            try
            {
                Customers.Clear();
                var _persons = await CustomerDataStore.GetItemsAsync(true);
                foreach (var _person in _persons)
                {
                    Customers.Add(_person);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedCustomer = null;
        }

        public Customers SelectedCustomer
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnCustomerSelected(value);
            }
        }

        private async void OnAddCustomer(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewPersonPage));
        }

        async void OnCustomerSelected(Customers _customer)
        {
            if (_customer == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(PersonDetailPage)}?{nameof(PersonDetailViewModel)}={_customer.Id}");
        }
    }
}