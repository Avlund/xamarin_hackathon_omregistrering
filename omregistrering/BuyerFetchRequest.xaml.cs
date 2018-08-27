using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace omregistrering
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuyerFetchRequest : ContentPage
	{
		public BuyerFetchRequest()
        {
            InitializeComponent();
        }

        private async void OnEntryCompleted(object sender, EventArgs e)
        {
            try
            {
                var handoffId = ((Entry)sender).Text;
                var acquireResponse = callRestService(handoffId);

                if (acquireResponse != null)
                {
                    ListVehicleDetails listVehicleDetails = new ListVehicleDetails("XX11222", handoffId);

                    await Navigation.PushAsync(listVehicleDetails);
                }
            } catch(Exception ex) {
                Console.WriteLine("fejl: " + ex.Message);
            }
        }

        private AcquireResponse callRestService(String handoffId)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(App.webServiceHost + "/acquire/initiate/ww00007");
            var httpContent = new StringContent("{ 'handoffId': '" + handoffId + "'}", Encoding.UTF8, "application/json");

            var response = client.PostAsync(uri, httpContent).Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<AcquireResponse>(content);
        }
    }
}