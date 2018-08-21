using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace omregistrering
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListSellersVehicles : ContentPage
    {
        //public ObservableCollection<string> Items { get; set; }

        Vehicles Items;


        public ListSellersVehicles()
        {

            Items = new Vehicles();
            BindingContext = Items;

            InitializeComponent();


            //Items = new ObservableCollection<string>
            //{
            //    "Ford Ka   XY 55 999",
            //    "Ford Ka   AB 22 333",
            //    "Renault Megane BA 50 100"
            //};

            //MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            //string id = Vehicles.vehicles1.First().RegNumber;
            ListVehicleDetails listVehicleDetails = new ListVehicleDetails();
            listVehicleDetails.RegNumber = "BA50100";
            await Navigation.PushAsync(listVehicleDetails);

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
