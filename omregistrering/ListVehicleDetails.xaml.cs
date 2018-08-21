using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace omregistrering
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListVehicleDetails : ContentPage
	{
        public string RegNumber {get; set; }
        Vehicle VehicleInstance;

        public ListVehicleDetails()
        {
            VehicleInstance = new Vehicles().getVehicle(RegNumber);

            InitializeComponent();
        }

        public ListVehicleDetails(string regNumber, bool seller)
        {
            this.RegNumber = regNumber;
            VehicleInstance = new Vehicles().getVehicle(RegNumber);
            InitializeComponent();
            regNumberLabel.Text = VehicleInstance.RegNumber;
            makeLabel.Text = VehicleInstance.Make;
            modelLabel.Text = VehicleInstance.Model;
            yearLabel.Text = VehicleInstance.Year.ToString();
            outstandingDebtLabel.Text = VehicleInstance.OutstandingDebt.ToString();

            if (seller)
            {
                actionButton.Text = "Overdrag ejerskab";
            } else
            {
                actionButton.Text = "Overtag ejerskab";
            }
        }

        private void actionButton_Pressed(object sender, EventArgs e)
        {
            HandOffResponse response = doPostCallRestService("SomeThing", RegNumber);
            handoffLabel.Text = response.handoffId;
        }

        private void actionButton_Clicked(object sender, EventArgs e)
        {
            HandOffResponse response = doPostCallRestService("SomeThing", RegNumber);
            handoffLabel.Text = response.handoffId;
        }

        private HandOffResponse doPostCallRestService(String licensId, String licensPlate)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri("http://10.105.112.115:8080/handoff/initiate/" + licensPlate);

            //var values = new Dictionary<string, string> { { "licenseId", licensId } };
            //var httpContent = new FormUrlEncodedContent(values);

            var httpContent = new StringContent("{ 'licenseId': '" + licensId + "'}", Encoding.UTF8, "application/json");

            var response = client.PostAsync(uri, httpContent).Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<HandOffResponse>(content);
        }
    }
}