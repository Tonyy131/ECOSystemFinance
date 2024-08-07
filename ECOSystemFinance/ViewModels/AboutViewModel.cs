using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ECOSystemFinance.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("http://www.moee.gov.eg/english_new/news_f.aspx"));
        }

        public ICommand OpenWebCommand { get; }
    }
}