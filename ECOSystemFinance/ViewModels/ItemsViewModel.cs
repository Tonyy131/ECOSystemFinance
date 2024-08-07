using ECOSystemFinance.Models;
using ECOSystemFinance.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ECOSystemFinance.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command ShowRequestsCommand { get; }
        public Command<Item> ItemTapped { get; }

        public Command<Item> ProgramChosen { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
            ProgramChosen = new Command<Item>(OnProgramSelection);
            ShowRequestsCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ShowRequestsPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        async void OnProgramSelection(Item item)
        {

            if (item == null)
                return;
            if(item.Text.Equals("Cash Program")){
                await Shell.Current.GoToAsync(nameof(CashPage));
            }
            if (item.Text.Equals("Loan Program"))
            {
                await Shell.Current.GoToAsync(nameof(LoanPage));
            }
            if (item.Text.Equals("Certificate Program"))
            {
                await Shell.Current.GoToAsync(nameof(CertificatePage));
            }

            // This will push the ItemDetailPage onto the navigation stack
        }
    }
}