using ECOSystemFinance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ECOSystemFinance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private bool firstLoged = false;

        ProfileViewModel ProfilePageView;
        public ProfilePage()
        {
            InitializeComponent();
            ProfilePageView = new ProfileViewModel();
            this.BindingContext = ProfilePageView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ProfilePageView = new ProfileViewModel();
            this.BindingContext = ProfilePageView;
            if (Application.Current.Properties.ContainsKey("isLoged") && !firstLoged)
            {
                firstLoged = true;

                ProfilePageView.Id = (int)Application.Current.Properties["ClientId"];
                ProfilePageView.InitializeData();
            }
        }
        private async void Change_Picture(object sender, EventArgs e)
        {
            try
            {
                var ImageSrc = await NewImage();
                if (ImageSrc != null)
                {
                    //profilePicture.Source = ImageSrc;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error!", "Can't upload photo", "ok");
            }

        }

        private async Task<ImageSource> NewImage()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Photos>();
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Warning!", "Unable to access photos!", "OK");
                    return null;
                }

                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Pick an image"
                });

                if (result != null)
                {
                    return ImageSource.FromStream(() => result.OpenReadAsync().Result);
                }
                return null;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error!", "can't access photos", "ok");
                return null;
            }
        }


    }
}