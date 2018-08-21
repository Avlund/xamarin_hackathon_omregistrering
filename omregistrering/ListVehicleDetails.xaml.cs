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
            
        }

        private void actionButton_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}