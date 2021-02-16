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
    public class AuthorsViewModel : BaseViewModel
    {
        private Authors _selectedItem;

        public ObservableCollection<Authors> Author { get; }
        public Command LoadAuthorsCommand { get; }
        public Command AddAuthorCommand { get; }
        public Command<Authors> AuthorTapped { get; }

        public AuthorsViewModel()
        {
            Title = "Authors";
            Author = new ObservableCollection<Authors>();
            LoadAuthorsCommand = new Command(async () => await ExecuteLoadAuthorsCommand());

            AuthorTapped = new Command<Authors>(OnItemSelected);

            AddAuthorCommand = new Command(OnAddAuthor);
        }

        async Task ExecuteLoadAuthorsCommand()
        {
            IsBusy = true;

            try
            {
                Author.Clear();
                var _authors = await AuthorDataStore.GetItemsAsync(true);
                foreach (var _author in _authors)
                {
                    Author.Add(_author);
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

        public Authors SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddAuthor(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewAuthorPage));
        }

        async void OnItemSelected(Authors _authors)
        {
            if (_authors == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(AuthorDetailPage)}?{nameof(AuthorDetailViewModel.AuthorID)}={_authors.AuthorsID}");
        }
    }
}