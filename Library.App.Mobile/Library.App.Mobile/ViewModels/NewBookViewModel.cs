using Library.App.Mobile.Models;
using Library.App.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Library.App.Mobile.ViewModels
{
    public class NewBookViewModel : BaseViewModel
    {
        private string _isbn, _title, _publishName, _description;
        private int _author;
        private DateTime _publishDate;

        public NewBookViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_isbn)
                && !String.IsNullOrWhiteSpace(_title)
                && !String.IsNullOrWhiteSpace(_publishName);
        }

        public string ISBN
        {
            get => _isbn;
            set => SetProperty(ref _isbn, value);
        }
        public string Titles
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public int Authors
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }
        public DateTime PublishDate
        {
            get => _publishDate;
            set => SetProperty(ref _publishDate, value);
        }
        public string PublishName
        {
            get => _publishName;
            set => SetProperty(ref _publishName, value);
        }
        
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Books _newbooks = new Books()
            {
                ISBN = ISBN,
                Title = Titles,
                AuthorsID = Authors,
                PublishDate = PublishDate,
                PublishName = PublishName
            };

            await BooksDataStore.AddItemAsync(_newbooks);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}