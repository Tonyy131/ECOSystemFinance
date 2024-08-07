using ECOSystemFinance.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace ECOSystemFinance.Views
{
    public partial class CertificatePage : ContentPage
    {
        public CertificatePage()
        {
            InitializeComponent();

            myPicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
            myPicker1.SelectedIndexChanged += OnPickerSelectedIndexChanged1;
            myPicker2.SelectedIndexChanged += OnPickerSelectedIndexChanged2;


            BindingContext = new CertificateViewModel();
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Xamarin.Forms.Picker)sender;
            int selectedIndex = picker.SelectedIndex;

        }

        void OnPickerSelectedIndexChanged1(object sender, EventArgs e)
        {
            var picker = (Xamarin.Forms.Picker)sender;
            int selectedIndex = picker.SelectedIndex;

        }

        void OnPickerSelectedIndexChanged2(object sender, EventArgs e)
        {
            var picker = (Xamarin.Forms.Picker)sender;
            int selectedIndex = picker.SelectedIndex;

        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (myPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Please select a Deduction account before proceeding.", "OK");
                return;
            }

            if (myPicker1.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Please select an Interest account before proceeding.", "OK");
                return;
            }

            if (myPicker2.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Please select a Due account before proceeding.", "OK");
                return;
            }
            // Display the popup message
            await DisplayAlert("Done", "You have made a successful request.", "OK");

            // Navigate to the previous page
            await Navigation.PopAsync();
        }
    }


}