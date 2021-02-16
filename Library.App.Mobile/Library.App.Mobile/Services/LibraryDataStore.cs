using Library.App.Mobile.Interfaces;
using Library.App.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace Library.App.Mobile.Services
{
    public class LibraryDataStore : IDataStore<LibraryTrancs>
    {
        private readonly HttpClient _client;
        private IEnumerable<LibraryTrancs> Library;
        bool _isConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;

        public LibraryDataStore()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"{App.BackendURL}/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Library = new List<LibraryTrancs>();
        }

        public async Task<IEnumerable<LibraryTrancs>> GetItemsAsync(bool forceRefresh = false)
        {
            if(forceRefresh && _isConnected)
            {
                var _json = await _client.GetStringAsync("api/library");
                Library = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<LibraryTrancs>>(_json));
            }
            return Library;
        }

        public async Task<LibraryTrancs> GetItemAsync(string id)
        {
            if (id != null && _isConnected)
            {
                var _json = await _client.GetStringAsync($"api/library/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<LibraryTrancs>(_json));
            }
            return null;
        }

        public async Task<bool> AddItemAsync(LibraryTrancs _library)
        {
            if (_library == null || !_isConnected)
                return false;
            var _serLibrary = JsonConvert.SerializeObject(_library);
            var _respone = await _client.PostAsync($"api/library", new StringContent(_serLibrary, Encoding.UTF8, "application/json"));
            //return await Task.FromResult(true);
            return _respone.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(LibraryTrancs _library)
        {
            if (_library != null || !_isConnected)
                return false;
            var _serLibrary = JsonConvert.SerializeObject(_library);
            var _buffer = Encoding.UTF8.GetBytes(_serLibrary);
            var _byteContent = new ByteArrayContent(_buffer);
            var _respone = await _client.PutAsync(new Uri($"api/library/{_library.Id}"), _byteContent);
            //return await Task.FromResult(true);
            return _respone.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || !_isConnected)
                return false;
            var _respone = await _client.DeleteAsync($"api/library/{id}");
            //return await Task.FromResult(true);
            return _respone.IsSuccessStatusCode;
        }
    }
}