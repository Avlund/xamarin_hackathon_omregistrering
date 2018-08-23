using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace omregistrering
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListSellersVehicles : ContentPage
    {
        Vehicles Items;

        public ListSellersVehicles()
        {

            Items = new Vehicles();
            BindingContext = Items;

            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            Vehicle vehicle = (Vehicle)e.Item;
            ListVehicleDetails listVehicleDetails = new ListVehicleDetails(vehicle.RegNumber, true);

            await Navigation.PushAsync(listVehicleDetails);

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
