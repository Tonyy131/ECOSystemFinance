using ECOSystemFinance.Models;
using ECOSystemFinance.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace ECOSystemFinance.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class CashViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string itemId;
        private string text;
        private string description;

        public Command submit { get; set; }
        private ObservableCollection<string> deductionAccounts; 
        public ObservableCollection<string> DeductionAccounts
        {
            get => deductionAccounts;
            set
            {
                SetProperty(ref deductionAccounts, value);
            }
        }
        public string Id { get ; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private int _selectedIndex = -1;
        public int SelectedIndex { get => _selectedIndex;
        set => SetProperty(ref _selectedIndex, value);
        }

        private string _SelectedAccount { get; set; }
        public string SelectedAccount
        {
            get => _SelectedAccount;
            set => SetProperty(ref text, value);
        }



        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }


        public CashViewModel()
        {
            if (!App.Current.Properties.ContainsKey("isLoged"))
            {
                DeductionAccounts = new ObservableCollection<string>();
            }
            else
            {
                Id = App.Current.Properties["ClientId"].ToString();
                IsBusy = true;
                Device.InvokeOnMainThreadAsync(async () => { await LoadDeductionAccounts(); });
                IsBusy = false;
            }
            submit = new Command(Submit);
        }


        public void Submit()
        {

            if (_selectedIndex == -1)
            {
                App.Current.MainPage.DisplayAlert("Error", "Please select a Deduction account before proceeding.", "OK");
                return;
            }
            string Account = DeductionAccounts[_selectedIndex];
            // Display the popup message
            string Id = Xamarin.Forms.Application.Current.Properties["ClientId"].ToString();
            Request request = new Request(Id, Account, null, null, 1, 1);
            IsBusy = true;
            Device.InvokeOnMainThreadAsync(async () => { await SubmitAccount(request);  });
            IsBusy = false;
            // Navigate to the previous page

        }
        private async Task LoadDeductionAccounts()
        {
            
            string apiUrl = "http://192.168.8.61/api/Account_Info?id=" + Id;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(apiUrl);
                var accounts = JsonConvert.DeserializeObject<List<string>>(response);
                DeductionAccounts = new ObservableCollection<string>(accounts);
            }
        }
        private async Task SubmitAccount(Request request)
        {

            string apiUrl = "http://192.168.8.61/API/SubmitRequest";
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Done", "You have made a successful request.", "OK");

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Faild Request.", "OK");
                }
                //await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
                await App.Current.MainPage.Navigation.PopAsync();


            }
        }
    }
}
