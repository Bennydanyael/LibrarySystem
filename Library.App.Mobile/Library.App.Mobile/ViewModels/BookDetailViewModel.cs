using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Library.App.Mobile.ViewModels
{
    [QueryProperty(nameof(BookId), nameof(BookId))]
    public class BookDetailViewModel : BaseViewModel
    {
        private string _Id, _title, _publishName, _author, isbn;
        private DateTime _publishDate;
        public BookDetailViewModel()
        {
            Title = "Details Book";
        }
        public string Id { get; set; }
        public string Titles
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }
        public string PublishName
        {
            get => _publishName;
            set => SetProperty(ref _publishName, value);
        }
        public DateTime PublishDate
        {
            get => _publishDate;
            set => SetProperty(ref _publishDate, value);
        }
        public string ISBN
        {
            get => isbn;
            set => SetProperty(ref isbn, value);
        }
        public string BookId
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                LoadBookId(value);
            }
        }

        public async void LoadBookId(string _bookId)
        {
            try
            {
                var _books = await BooksDataStore.GetItemAsync(_bookId);
                Id = _books.Id.ToString();
                Titles = _books.Title;
                ISBN = _books.ISBN;
                PublishDate = _books.PublishDate.Date;
                PublishName = _books.PublishName;
                Author = _books.Authories.AuthorsName;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Books");
            }
        }
    }
}
