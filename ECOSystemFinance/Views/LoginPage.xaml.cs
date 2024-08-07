using ECOSystemFinance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECOSystemFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            viewModel = (LoginViewModel) this.BindingContext;
            
            //if (Application.Current.Properties.ContainsKey("isLoged"))
            //{
            //    bioButton.IsVisible = true;
            //}
            //else
            //{
            //    bioButton.IsVisible = false;
            //}
            this.IsBusy = false;

            bioButton.IsVisible = Application.Current.Properties.ContainsKey("isLoged");
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    // Assuming most loading is done by now, execute your method
        //    if (Application.Current.Properties.ContainsKey("isLoged"))
        //        viewModel.OnBiometricLoginClicked();
        //}

        
        //private void Btn_submit_Clicked(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(password.Text))
        //    {
        //        App.Current.MainPage.DisplayAlert("Warning", "Invalid Inputus", "OK");

        //        return;

        //    }

        //    var usernameFromDb = "som3a";
        //    var passwordFromDb = "0000";
        //    if (username.Text.Trim() == usernameFromDb && password.Text == passwordFromDb)
        //    {
        //        App.Current.MainPage.DisplayAlert("Success", "Welcome to the club!!", "OK");

        //    }
        //    else
        //    {
        //        App.Current.MainPage.DisplayAlert("Warning", "User Name or Password is incorrect", "OK");

        //    }
        //}
    }
}