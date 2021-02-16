using Library.App.Mobile.Interfaces;
using Library.App.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Library.App.Mobile.Services
{
    public class BooksDataStore : IDataStore<Books>
    {
        private readonly HttpClient _client;
        private IEnumerable<Books> Books;
        bool _isConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public BooksDataStore()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"{App.BackendURL}/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Books = new List<Books>();
        }

        public async Task<IEnumerable<Books>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && _isConnected)
            {
                var _json = await _client.GetStringAsync("api/book");
                Books = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Books>>(_json));
            }
            return Books;
        }

        public async Task<Books> GetItemAsync(string id)
        {
            if (id != null && _isConnected)
            {
                var _json = await _client.GetStringAsync($"api/book/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Books>(_json));
            }
            return null;
        }

        public async Task<bool> AddItemAsync(Books _book)
        {
            if (_book == null && !_isConnected)
                return false;
            var _serBook = JsonConvert.SerializeObject(_book);
            var _respone = await _client.PostAsync($"api/book", new StringContent(_serBook, Encoding.UTF8, "application/json"));
            return _respone.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Books _book)
        {
            if (_book == null || !_isConnected)
                return false;
            var _serBook = JsonConvert.SerializeObject(_book);
            var _buffer = Encoding.UTF8.GetBytes(_serBook);
            var _byteContent = new ByteArrayContent(_buffer);
            var _respone = await _client.PutAsync(new Uri($"api/book/{_book.Id}"), _byteContent);
            return _respone.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || !_isConnected)
                return false;

            var _respone = await _client.DeleteAsync($"api/book/{id}");
            return _respone.IsSuccessStatusCode;
        }
    }
}
