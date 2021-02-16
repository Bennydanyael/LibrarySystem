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
    public class BooksViewModel : BaseViewModel
    {
        private Books _selectedItem;

    public ObservableCollection<Books> Book { get; }
    public Command LoadBooksCommand { get; }
    public Command AddBookCommand { get; }
    public Command<Books> BookTapped { get; }

    public BooksViewModel()
    {
        Title = "Books";
        Book = new ObservableCollection<Books>();
        LoadBooksCommand = new Command(async () => await ExecuteLoadBooksCommand());
        BookTapped = new Command<Books>(OnItemSelected);
        AddBookCommand = new Command(OnAddItem);
    }

    async Task ExecuteLoadBooksCommand()
    {
        IsBusy = true;

        try
        {
            Book.Clear();
            var _books = await BooksDataStore.GetItemsAsync(true);
            foreach (var book in _books)
            {
                    Book.Add(book);
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

    public Books SelectedItem
    {
        get => _selectedItem;
        set
        {
            SetProperty(ref _selectedItem, value);
            OnItemSelected(value);
        }
    }

    private async void OnAddItem(object obj)
    {
        await Shell.Current.GoToAsync(nameof(NewBookPage));
    }

    async void OnItemSelected(Books _books)
    {
        if (_books == null)
            return;

        // This will push the ItemDetailPage onto the navigation stack
        await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?{nameof(BookDetailViewModel.BookId)}={_books.Id}");
    }
}
}