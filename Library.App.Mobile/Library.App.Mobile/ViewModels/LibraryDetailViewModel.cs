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
    [QueryProperty(nameof(Id), nameof(Id))]
    public class LibraryDetailViewModel : BaseViewModel
    {
        private string _libraryId, _customer, _book, _description;
        private DateTime _borrow, _back;
        private int _length;
        public string Id { get; set; }
        public string Customers
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public string Book
        {
            get => _book;
            set => SetProperty(ref _book, value);
        }
        public string LibraryId
        {
            get
            {
                return _libraryId;
            }
            set
            {
                _libraryId = value;
                LoadOrderId(value);
            }
        }
        public DateTime BorrowDate
        {
            get => _borrow;
            set => SetProperty(ref _borrow, value);
        }
        public DateTime BackDate
        {
            get => _back;
            set => SetProperty(ref _back, value);
        }
        public int LengthBorrowed
        {
            get => _length;
            set => SetProperty(ref _length, value);
        }
        public async void LoadOrderId(string itemId)
        {
            try
            {
                var _library = await LibraryDataStore.GetItemAsync(itemId);
                LibraryId = _library.Id.ToString();
                Customers = _library.Customers.Username;
                Book = _library.Books.Title;
                Description = _library.LengthBorrowed.ToString();
                BorrowDate = _library.BorrowedDate;
                BackDate = _library.DateBack;
                LengthBorrowed = _library.LengthBorrowed;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Books");
            }
        }
    }
}
