using System;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace omregistrering
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellerStatusPage : ContentPage
    {
        string handoffId;
        public SellerStatusPage(string handOff)
        {
            InitializeComponent();
            this.handoffId = handOff;
            handoffLabel.Text = handOff;
            qrImage.BarcodeValue = handOff;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                HttpClient client = new HttpClient();
                var uri = new Uri(App.webServiceHost + "/handoff/status/" + handoffId);

                while (true)
                {
                    var response = client.GetAsync(uri).Result;
                    response.EnsureSuccessStatusCode();

                    var content = response.Content.ReadAsStringAsync().Result;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        statusLabel.Text = content;
                    });

                    if (content.Contains("COMPLETED"))
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(2000);
                }

                ReceiptPage receiptPage = new ReceiptPage("XY55999", true);
                Navigation.PushAsync(receiptPage);
            })).Start();
        }        
    }
}
