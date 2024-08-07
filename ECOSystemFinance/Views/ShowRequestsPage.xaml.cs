using ECOSystemFinance.Models;
using ECOSystemFinance.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECOSystemFinance.Views
{
    public partial class ShowRequestsPage : ContentPage
    {
        public Item Item { get; set; }

        public ShowRequestsPage()
        {
            InitializeComponent();
            BindingContext = new ShowRequestsViewModel();
        }
    }
}