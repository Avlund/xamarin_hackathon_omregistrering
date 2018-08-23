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
	public partial class ReceiptPage : ContentPage
	{
        private Boolean seller;

		public ReceiptPage ()
		{
            seller = false;
            InitializeComponent ();            
        }

        public ReceiptPage(string regNumber, Boolean seller = false)
        {
            this.seller = seller;
            InitializeComponent();

            line_1.Text = "Til lykke!";

            if (seller)
            {
                line_2.Text = "Du har nu";
                line_3.Text = "overdraget";
                line_4.Text = "ejerskabet";
            }
            else
            {
                line_2.Text = "";
                line_3.Text = "Du er nu";
                line_4.Text = "ny ejer";
            }

            line_5.Text = "af køretøjet";
            line_6.Text = "med reg. nummer:";
            line_7.Text = regNumber;
        }
    }
}