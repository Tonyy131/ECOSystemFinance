using ECOSystemFinance.Views;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ECOSystemFinance.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command BiometricLoginCommand { get; }

        public string password { get; set; }
        private string username { get; set; }
        public string UserName
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private async Task<Log> FetchId(string userName, string password)
        {
            try
            {
                HttpClient client = new HttpClient();
                var apiUrl = "http://192.168.8.61/API/Validate_Login";

                var login = new { userName = userName, password = password };
                var json = JsonConvert.SerializeObject(login);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Log clientData = JsonConvert.DeserializeObject<Log>(jsonResponse);
                    if (clientData.Id > 0)
                    {
                        Application.Current.Properties["ClientId"] = clientData.Id;
                        Application.Current.Properties["TokenNumber"] = clientData.TokenNumber;
                        Application.Current.Properties["TokenTime"] = clientData.TokenTime;
                    }
                    return clientData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            BiometricLoginCommand = new Command(OnBiometricLoginClicked);

            if (Application.Current.Properties.ContainsKey("isLoged") && (!Application.Current.Properties.ContainsKey("LogedOut") || (Application.Current.Properties.ContainsKey("LogedOut") && !(bool)Application.Current.Properties["LogedOut"])))
                OnBiometricLoginClicked();
        }

        private void OnLoginClicked(object obj)
        {
            try
            {
                this.IsBusy = true;
                Device.InvokeOnMainThreadAsync(async () =>
                {
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        Application.Current.MainPage.DisplayAlert("Warning", "Invalid Input", "OK");
                        this.IsBusy = false;
                        return;
                    }

                    Log client = await FetchId(username, password);
                    if (client == null)
                    {
                        Application.Current.MainPage.DisplayAlert("Warning", "Wrong username or password", "OK");
                        this.IsBusy = false;
                        return;
                    }
                    int clientId = client.Id;

                    if (clientId > 0)
                    {
                        await SecureStorage.SetAsync("username", username);
                        await SecureStorage.SetAsync("password", password);
                        this.IsBusy = false;
                        await Application.Current.MainPage.DisplayAlert("Success", "Hello!", "OK");
                        Application.Current.Properties["isLoged"] = true;
                        Application.Current.SavePropertiesAsync();
                        /*RaisePropertyChanged()*/
                        ;
                        //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Warning", "Username or Password is incorrect", "OK");
                        this.IsBusy = false;
                    }
                });
            }
            catch (Exception exp)
            {
                Application.Current.MainPage.DisplayAlert("Error", exp.ToString(), "OK");
            }
        }

        //private async void OnBiometricLoginClicked()
        //{
        //    try
        //    {
        //        var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Authentication", "Authenticate to access secrets"));

        //        if (result.Authenticated)
        //        {
        //            var storedUsername = //}await SecureStorage.GetAsync("username");
        //            var storedPassword = await SecureStorage.GetAsync("password");

        //            if (!string.IsNullOrEmpty(storedUsername) && !string.IsNullOrEmpty(storedPassword))
        //            {
        //                Log client = await FetchId(storedUsername, storedPassword);
        //                if (client != null && client.Id > 0)
        //                {
        //                    await Application.Current.MainPage.DisplayAlert("Success", "Hello!", "OK");
        //                    Application.Current.Properties["isLoged"] = true;
        //                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        //                }
        //                else
        //                {
        //                    await Application.Current.MainPage.DisplayAlert("Warning", "Biometric authentication failed", "OK");
        //                }
        //            }
        //            else
        //            {
        //                await Application.Current.MainPage.DisplayAlert("Warning", "No stored credentials found", "OK");
        //            }
        //        }
        //        else
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Warning", "Biometric authentication failed", "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", $"Biometric authentication error: {ex.Message}", "OK");
        //    }


        public async void OnBiometricLoginClicked()
        {

            try
            {
                var availability = await CrossFingerprint.Current.IsAvailableAsync();
                if (!availability)
                {
                    await Device.InvokeOnMainThreadAsync(() =>
                        Application.Current.MainPage.DisplayAlert("Warning", "No Biometrics available", "OK"));
                    return;
                }

                bool isLoggedBefore = Application.Current.Properties.ContainsKey("isLoged") && (bool)Application.Current.Properties["isLoged"];

                if (isLoggedBefore)
                {

                    var authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Hi!", "Use Biometrics?"));
                    IsBusy = true;
                    if (authResult.Status == FingerprintAuthenticationResultStatus.Canceled)
                    {
                        await Device.InvokeOnMainThreadAsync(() =>
                            Application.Current.MainPage.DisplayAlert("Warning", "Authentication canceled", "OK"));
                        return;
                    }

                    if (authResult.Authenticated)
                    {
                        var storedUsername = await SecureStorage.GetAsync("username");
                        var storedPassword = await SecureStorage.GetAsync("password");

                        if (!string.IsNullOrEmpty(storedUsername) && !string.IsNullOrEmpty(storedPassword))
                        {
                            var client = await FetchId(storedUsername, storedPassword);
                            if (client != null && client.Id > 0)
                            {
                                await Device.InvokeOnMainThreadAsync(() =>
                                    Application.Current.MainPage.DisplayAlert("Success", "Hello!", "OK"));

                                Application.Current.Properties["isLoged"] = true;
                                Application.Current.MainPage = new AppShell();
                            }
                            else
                            {
                                await Device.InvokeOnMainThreadAsync(() =>
                                    Application.Current.MainPage.DisplayAlert("Warning", "Biometric authentication failed", "OK"));
                            }
                        }
                        else
                        {
                            await Device.InvokeOnMainThreadAsync(() =>
                                Application.Current.MainPage.DisplayAlert("Warning", "No stored credentials found", "OK"));
                        }
                    }
                    else
                    {
                        await Device.InvokeOnMainThreadAsync(() =>
                            Application.Current.MainPage.DisplayAlert("Warning", "Authentication failed", "OK"));
                    }
                }
                else
                {
                    await Device.InvokeOnMainThreadAsync(() =>
                        Application.Current.MainPage.DisplayAlert("Warning", "No stored credentials found", "OK"));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                await Device.InvokeOnMainThreadAsync(() =>
                    Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK"));
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
