using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace omregistrering
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSellerButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListSellersVehicles());
        }

        private async void OnBuyerButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuyerFetchRequest());
        }

    }
}
