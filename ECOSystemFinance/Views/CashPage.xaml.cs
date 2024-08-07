using ECOSystemFinance.Models;
using ECOSystemFinance.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace ECOSystemFinance.Views
{
    public partial class CashPage : ContentPage
    {
        
        public CashPage()
        {
            InitializeComponent();

            myPicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
           BindingContext = new CashViewModel();
            
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Xamarin.Forms.Picker)sender;
            int selectedIndex = picker.SelectedIndex;

        }


    }

    
}