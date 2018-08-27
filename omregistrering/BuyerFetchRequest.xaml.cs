using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace omregistrering
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuyerFetchRequest : ContentPage
	{
        public ZXingScannerView scanner = new ZXingScannerView();
        ZXingDefaultOverlay overlay;

		public BuyerFetchRequest()
        {
            InitializeComponent();
            Scan();
        }

        public void Scan()
        {
            try
            {
                scanner.Options = new MobileBarcodeScanningOptions()
                {
                    UseFrontCameraIfAvailable = false, //update later to come from settings
                    PossibleFormats = new List<BarcodeFormat>(),
                    TryHarder = true,
                    AutoRotate = false,
                    TryInverted = true,
                    DelayBetweenContinuousScans = 2000
                };

                scanner.IsScanning = true;
                scanner.IsVisible = true;
                scanner.Options.PossibleFormats.Add(BarcodeFormat.QR_CODE);


                scanner.OnScanResult += (result) => {

                    // Stop scanning
                    scanner.IsAnalyzing = false;
                    if (scanner.IsScanning)
                    {
                        scanner.AutoFocus();
                    }

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(async () => {
                        if (result != null)
                        {
                            QrResult qrResult = JsonConvert.DeserializeObject<QrResult>(result.Text);

                            try
                            {
                                var acquireResponse = callRestService(qrResult.licensePlate, qrResult.handoffId);

                                if (acquireResponse != null)
                                {
                                    ListVehicleDetails listVehicleDetails = new ListVehicleDetails(qrResult.licensePlate, qrResult.handoffId);

                                    await Navigation.PushAsync(listVehicleDetails);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("fejl: " + ex.Message);
                            }
                        }
                    });
                };

                overlay = new ZXingDefaultOverlay
                {
                    TopText = "Hold your phone up to the barcode",
                    BottomText = "Scanning will happen automatically",
                    ShowFlashButton = scanner.HasTorch,
                    AutomationId = "zxingDefaultOverlay",
                };

                var grid = new Grid
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };

                grid.Children.Add(scanner);
                grid.Children.Add(overlay);

                Content = grid;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        private AcquireResponse callRestService(string licensePlate, string handoffId)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(App.webServiceHost + "/acquire/initiate/" + licensePlate);
            var httpContent = new StringContent("{ 'handoffId': '" + handoffId + "'}", Encoding.UTF8, "application/json");

            var response = client.PostAsync(uri, httpContent).Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<AcquireResponse>(content);
        }
    }
}