using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECOSystemFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        private List<string> imageUrls = new List<string>
        {
            "image1.jpg",
            "image2.jpg",
            "image3.png"
        };

        private int currentIndex = 0;
        private const int slideInterval = 5; 

        public AboutPage()
        {
            InitializeComponent();

            carouselView.ItemsSource = imageUrls;
            StartTimer();
        }

        private void StartTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(slideInterval), () =>
            {
                currentIndex = (currentIndex + 1) % imageUrls.Count;
                Device.BeginInvokeOnMainThread(() =>
                {
                    carouselView.Position = currentIndex;
                });
                return true;
            });
        }
    }
}
