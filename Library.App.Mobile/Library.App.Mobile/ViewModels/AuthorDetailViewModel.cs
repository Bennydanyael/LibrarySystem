using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Library.App.Mobile.ViewModels
{
    [QueryProperty(nameof(AuthorID), nameof(AuthorID))]
    public class AuthorDetailViewModel : BaseViewModel
    {
        private string _authorId, _authorName, _authorBiography;
        public string AuthorID { get; set; }
        public string AuthorName
        {
            get => _authorName;
            set => SetProperty(ref _authorName, value);
        }
        public string Biography
        {
            get => _authorBiography;
            set => SetProperty(ref _authorBiography, value);
        }
        public string AuthorId
        {
            get
            {
                return _authorId;
            }
            set
            {
                _authorId = value;
                LoadAuthorId(value);
            }
        }
        public async void LoadAuthorId(string _authorId)
        {
            try
            {
                var _author = await AuthorDataStore.GetItemAsync(_authorId);
                AuthorId = _author.AuthorsID.ToString();
                AuthorName= _author.AuthorsName;
                Biography = _author.Biography;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Books");
            }
        }
    }
}
