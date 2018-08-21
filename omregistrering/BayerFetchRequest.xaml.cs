using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace omregistrering
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BayerFetchRequest : ContentPage
	{
		public BayerFetchRequest ()
		{
			InitializeComponent ();
		}

        private async void OnEntryCompleted(object sender, EventArgs e)
        {
            //Connect to webservice send 'Entry.text' and retrieve the vehicle id            
            await Navigation.PushAsync(new ListVehicleDetails());
        }
    }
}