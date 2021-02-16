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
    public class LibraryViewModel : BaseViewModel
    {
        private LibraryTrancs _selectedItem;

        public ObservableCollection<LibraryTrancs> LibraryTranc { get; }
        public Command LoadLibrarysCommand { get; }
        public Command AddLibraryCommand { get; }
        public Command<LibraryTrancs> LibraryTapped { get; }

        public LibraryViewModel()
        {
            Title = "Librarys";
            LibraryTranc = new ObservableCollection<LibraryTrancs>();
            LoadLibrarysCommand = new Command(async () => await ExecuteLoadLibrarysCommand());
            LibraryTapped = new Command<LibraryTrancs>(OnLibrarySelected);
            AddLibraryCommand = new Command(OnAddLibrary);
        }

        async Task ExecuteLoadLibrarysCommand()
        {
            IsBusy = true;

            try
            {
                LibraryTranc.Clear();
                var _library = await LibraryDataStore.GetItemsAsync(true);
                foreach (var library in _library)
                {
                    LibraryTranc.Add(library);
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
            SelectedItem = null;
        }

        public LibraryTrancs SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnLibrarySelected(value);
            }
        }

        private async void OnAddLibrary(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewLibraryPage));
        }

        async void OnLibrarySelected(LibraryTrancs _library)
        {
            if (_library == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(LibraryDetailPage)}?{nameof(LibraryDetailViewModel.LibraryId)}={_library.Id}");
        }
    }
}