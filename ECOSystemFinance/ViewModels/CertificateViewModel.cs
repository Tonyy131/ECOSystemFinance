using ECOSystemFinance.Models;
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

namespace ECOSystemFinance.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class CertificateViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string itemId;
        private string text;
        private string description;
        public Command submit { get; set; }
        public string Id { get; set; }
        private int _selectedIndex = -1;
        public int SelectedIndex1
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }
        private int _selectedIndex1 = -1;
        public int SelectedIndex2
        {
            get => _selectedIndex1;
            set => SetProperty(ref _selectedIndex1, value);
        }
        private int _selectedIndex2 = -1;
        public int SelectedIndex3
        {
            get => _selectedIndex2;
            set => SetProperty(ref _selectedIndex2, value);
        }
        private ObservableCollection<string> deductionAccounts;
        public ObservableCollection<string> DeductionAccounts
        {
            get => deductionAccounts;
            set
            {
                SetProperty(ref deductionAccounts, value);
            }
        }
        private ObservableCollection<string> interestAccounts;
        public ObservableCollection<string> InterestAccounts
        {
            get => interestAccounts;
            set
            {
                SetProperty(ref interestAccounts, value);
            }
        }
        private ObservableCollection<string> dueAccount;
        public ObservableCollection<string> DueAccount
        {
            get => deductionAccounts;
            set
            {
                SetProperty(ref dueAccount, value);
            }
        }

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

        public CertificateViewModel()
        {
            if (!Application.Current.Properties.ContainsKey("isLoged"))
            {
                DeductionAccounts = new ObservableCollection<string>();
                DueAccount = new ObservableCollection<string>();
                InterestAccounts = new ObservableCollection<string>();
            }
            else
            {
                Id = Application.Current.Properties["ClientId"].ToString();
                IsBusy = true;
                Device.InvokeOnMainThreadAsync(async () => {await LoadAccounts(); });
                IsBusy = false;
                
            }
            submit = new Command(Submit);
        }
        private async Task LoadAccounts()
        {

            string apiUrl = "http://192.168.8.61/api/Account_Info?id=" + Id;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(apiUrl);
                var accounts = JsonConvert.DeserializeObject<List<string>>(response);
                DeductionAccounts = new ObservableCollection<string>(accounts);
                DueAccount = new ObservableCollection<string>(accounts);
                InterestAccounts = new ObservableCollection<string>(accounts);
            }
        }

        public void Submit()
        {

            if (_selectedIndex == -1 || _selectedIndex1 == -1 || _selectedIndex2 == -1)
            {
                App.Current.MainPage.DisplayAlert("Error", "Please select all accounts before proceeding.", "OK");
                return;
            }
            string Account = DeductionAccounts[_selectedIndex];
            string Account1 = InterestAccounts[_selectedIndex1];
            string Account2 = DueAccount[_selectedIndex2];
            // Display the popup message
            string Id = Xamarin.Forms.Application.Current.Properties["ClientId"].ToString();
            Request request = new Request(Id, Account, Account1, Account2, 3, 3);
            IsBusy = true;
            Device.InvokeOnMainThreadAsync(async () => { await SubmitAccount(request); });
            IsBusy = false;
            // Navigate to the previous page

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
               
                await App.Current.MainPage.Navigation.PopAsync();


            }
        }
    }
}
