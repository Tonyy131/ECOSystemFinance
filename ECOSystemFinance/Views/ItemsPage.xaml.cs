using ECOSystemFinance.Models;
using ECOSystemFinance.ViewModels;
using ECOSystemFinance.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECOSystemFinance.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
            this.IsBusy = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
            
        //    string selectedOption = await DisplayActionSheet("Select an Account", "Cancel", null, "Account 1", "Account 2", "Account 3");

        //    if (selectedOption != "Cancel" && !string.IsNullOrWhiteSpace(selectedOption))
        //    {
        //        // Handle the selected option
        //        await DisplayAlert("Selected Option", $"You selected: {selectedOption}", "OK");
        //    }
        //}
    }
}