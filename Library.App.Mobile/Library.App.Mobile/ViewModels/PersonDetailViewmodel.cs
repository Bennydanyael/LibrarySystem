using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Library.App.Mobile.ViewModels
{
    public class PersonDetailViewModel : BaseViewModel
    {
        private string personId;
        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string PersonId
        {
            get
            {
                return personId;
            }
            set
            {
                personId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await ItemDataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Books");
            }
        }
    }
}
