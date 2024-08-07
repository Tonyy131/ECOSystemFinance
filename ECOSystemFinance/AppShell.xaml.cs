using ECOSystemFinance.ViewModels;
using ECOSystemFinance.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ECOSystemFinance
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(ShowRequestsPage), typeof(ShowRequestsPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(CashPage), typeof(CashPage));
            Routing.RegisterRoute(nameof(LoanPage), typeof(LoanPage));
            Routing.RegisterRoute(nameof(CertificatePage), typeof(CertificatePage));
            this.CurrentItem.CurrentItem = MainTab;
        }

    }
}
