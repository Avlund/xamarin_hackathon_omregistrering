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
            BindingContext = VehicleInstance;
            InitializeComponent();
        } 
	}
}