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
        string regNumber;
        public SellerStatusPage(string regNumber, string handOff)
        {
            InitializeComponent();
            this.handoffId = handOff;
            this.regNumber = regNumber;

            string barcodeText = "{'licensePlate': '" + regNumber + "', 'handoffId': '" + handOff + "'}";
            qrImage.BarcodeValue = barcodeText;
            //handoffLabel.Text = barcodeText;
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
                        if (content.Contains("INPROGRESS"))
                        {
                            qrImage.IsVisible = false;
                            headline.Text = "Afventer køber";
                        }                       
                    });

                    if (content.Contains("COMPLETED"))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            ReceiptPage receiptPage = new ReceiptPage(regNumber, true);
                            Navigation.PushAsync(receiptPage);
                        });
                        break;
                    }
                    System.Threading.Thread.Sleep(2000);
                }                
            })).Start();            
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new MainPage());
            return true;
        }
    }
}
