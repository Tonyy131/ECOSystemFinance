using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ECOSystemFinance.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;

namespace ECOSystemFinance.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {

        public Command LogOut { get; }

        private int _id;
        public int Id { get { return _id; }
        set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        private string _mobileNumber;
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { SetProperty(ref _mobileNumber, value); }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        private string _street;
        public string Street
        {
            get { return _street; }
            set { SetProperty(ref _street, value); } 
        }

        private string _building;
        public string Building
        {
            get { return _building; }
            set { SetProperty(ref _building, value); }
        }

        private string _floorNumber;
        public string FloorNumber
        {
            get { return _floorNumber; }
            set { SetProperty(ref _floorNumber, value); }
        }

        private string _appartementNumber;
        public string AppartementNumber
        {
            get { return _appartementNumber; }
            set { SetProperty(ref _appartementNumber, value); }
        }



        public ProfileViewModel()
        {
            if(!Application.Current.Properties.ContainsKey("isLoged"))
            {
                Name = "";
                Username = "nour";
                Gender = "";
                MobileNumber = "";
                City = "";
                Street = "";
                Building = "";
                FloorNumber = "";
                AppartementNumber = "";
            }
            else
            {
                Id = (int)Application.Current.Properties["ClientId"];
                InitializeData();
            }
            LogOut = new Command(Logout);

        }

        public void InitializeData()
        {
            IsBusy = true;
            Device.InvokeOnMainThreadAsync(async () =>
            {
                ClientDetails clientD = await FetchProfileData();
                Name = clientD.ClientName;
                Username = clientD.UserName;
                Gender = clientD.GenderName;
                MobileNumber = clientD.MobileNumber;
                City = clientD.CityName;
                Street = clientD.StreetName;
                Building = clientD.Building;
                FloorNumber = clientD.FloorNumber;
                AppartementNumber = clientD.Apartement;
            });
            IsBusy = false;
        }

        private async Task<ClientDetails> FetchProfileData()
        {
            try
            {
                HttpClient client = new HttpClient();
                var apiUrl = "http://192.168.8.61/api/Profile_Info?id=" + _id;
                var response = await client.GetAsync(apiUrl);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var Client = JsonConvert.DeserializeObject<ClientDetails>(jsonResponse);
                //var client2=JsonConvert.DeserializeObject<ClientDetails>(Client);
                return Client;
            }
            catch(Exception exp)
            {
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void Logout()
        {
            Application.Current.Properties["LogedOut"] = true;
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            
        }
    }
}
