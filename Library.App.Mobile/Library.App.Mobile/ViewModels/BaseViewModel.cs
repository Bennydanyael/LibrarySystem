using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Library.App.Mobile.Models;
using Library.App.Mobile.Services;
using Library.App.Mobile.Interfaces;

namespace Library.App.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> ItemDataStore => DependencyService.Get<IDataStore<Item>>();
        public IDataStore<Authors> AuthorDataStore => DependencyService.Get<IDataStore<Authors>>();
        public IDataStore<Books> BooksDataStore => DependencyService.Get<IDataStore<Books>>();
        public IDataStore<Customers> CustomerDataStore => DependencyService.Get<IDataStore<Customers>>();
        public IDataStore<LibraryTrancs> LibraryDataStore => DependencyService.Get<IDataStore<LibraryTrancs>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
