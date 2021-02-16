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
    public class AuthorDataStore : IDataStore<Authors>
    {
        private readonly HttpClient _client;
        private IEnumerable<Authors> _authors;
        bool _isConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public AuthorDataStore()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"{App.BackendURL}/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _authors = new List<Authors>();
        }

        public async Task<IEnumerable<Authors>> GetItemsAsync(bool forceRefresh = false)
        {
            if(forceRefresh && _isConnected)
            {
                var _json = await _client.GetStringAsync("api/author");
                _authors = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Authors>>(_json));
            }
            return _authors;
        }

        public async Task<Authors> GetItemAsync(string id)
        {
            if (id != null && _isConnected)
            {
                var _json = await _client.GetStringAsync($"api/author/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Authors>(_json));
            }
            return null;
        }

        public async Task<bool> AddItemAsync(Authors authors)
        {
            if (authors == null || !_isConnected)
                return false;
            var _serAuthor = JsonConvert.SerializeObject(authors);
            var _respone = await _client.PostAsync($"api/author", new StringContent(_serAuthor, Encoding.UTF8, "application/json"));
            return _respone.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Authors authors)
        {
            if (authors == null || !_isConnected)
                return false;
            var _serAuthor = JsonConvert.SerializeObject(authors);
            var _buffer = Encoding.UTF8.GetBytes(_serAuthor);
            var _byteContent = new ByteArrayContent(_buffer);
            var _respone = await _client.PutAsync(new Uri($"api/author/{authors.AuthorsID}"), _byteContent);
            //return await Task.FromResult(true);
            return _respone.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || !_isConnected)
                return false;
            var _respone = await _client.DeleteAsync($"api/author/{id}");
            //return await Task.FromResult(true);
            return _respone.IsSuccessStatusCode;
        }
    }
}