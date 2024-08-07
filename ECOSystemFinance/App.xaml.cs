using ECOSystemFinance.Services;
using ECOSystemFinance.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECOSystemFinance
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new LoginPage();

        }

        protected async override void OnStart()
        {
            //await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

        }

        protected override void OnSleep()
        {
            Application.Current.Properties["LogedOut"] = false;
        }

        protected override void OnResume()
        {
        }

    }
}
