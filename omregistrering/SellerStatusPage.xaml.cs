using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var uri = new Uri("http://10.105.112.115:8080/handoff/status/" + handoffId);

            Boolean b = true;

            //while (b)
            //{
                var response = client.GetAsync(uri).Result;
                response.EnsureSuccessStatusCode();

                var content = response.Content.ReadAsStringAsync().Result;
                statusLabel.Text = content;

                if (content.Contains("COMPLETED"))
                {
                    b = false;
                }

                System.Threading.Thread.Sleep(3000);
            //}
        }
    }
}