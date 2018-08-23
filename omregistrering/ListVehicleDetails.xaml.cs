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
        bool seller;
        string handoffId;

        public ListVehicleDetails()
        {
            VehicleInstance = new Vehicles().getVehicle(RegNumber);

            InitializeComponent();
        }

        public ListVehicleDetails(string regNumber, bool seller)
        {
            this.seller = seller;
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

        public ListVehicleDetails(string handoffId)
        {
            this.RegNumber = "XY55999";
            this.handoffId = handoffId;
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
            }
            else
            {
                actionButton.Text = "Overtag ejerskab";
            }
        }

        //private void actionButton_Pressed(object sender, EventArgs e)
        //{
        //    if (seller)
        //    {
        //        HandOffResponse response = doPostCallRestService("SomeThing", RegNumber);

        //        SellerStatusPage sellerStatusPage = new SellerStatusPage(response.handoffId);

        //        Navigation.PushAsync(sellerStatusPage);

        //    } else {
        //        doPutCallRestService(handoffId);

        //        Navigation.PushAsync(new ReceiptPage(RegNumber));
        //    }
        //}

        private void actionButton_Clicked(object sender, EventArgs e)
        {
            if (seller)
            {
                HandOffResponse response = doPostCallRestService("SomeThing", RegNumber);

                SellerStatusPage sellerStatusPage = new SellerStatusPage(response.handoffId);

                Navigation.PushAsync(sellerStatusPage);
            }
            else
            {
                doPutCallRestService(handoffId);

                Navigation.PushAsync(new ReceiptPage(RegNumber));
            }
        }

        private HandOffResponse doPostCallRestService(String licensId, String licensPlate)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(App.webServiceHost + "/handoff/initiate/" + licensPlate);

            var httpContent = new StringContent("{ 'licenseId': '" + licensId + "'}", Encoding.UTF8, "application/json");

            var response = client.PostAsync(uri, httpContent).Result;

            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<HandOffResponse>(content);
        }

        private void doPutCallRestService(String handoffId)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(App.webServiceHost + "/acquire/status/" + handoffId);

            var httpContent = new StringContent("{ 'status': 'COMPLETED'}", Encoding.UTF8, "application/json");

            var response = client.PutAsync(uri, httpContent).Result;

            response.EnsureSuccessStatusCode();
        }
    }
}