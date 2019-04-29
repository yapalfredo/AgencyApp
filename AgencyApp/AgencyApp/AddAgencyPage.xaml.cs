using AgencyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgencyApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddAgencyPage : ContentPage
	{
        Agency agency;
		public AddAgencyPage ()
		{
			InitializeComponent ();

            //Sets the binding to Agency class
            agency = new Agency();
            stackLayoutContainer.BindingContext = agency;
		}

        private void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            bool isNameEmpty = string.IsNullOrEmpty(entryAgencyName.Text);
            bool isAddressEmtpy = string.IsNullOrEmpty(entryAgencyAddress.Text);
            bool isEmailEmpty = string.IsNullOrEmpty(entryAgencyEmailAddress.Text);
            bool isPhoneEmpty = string.IsNullOrEmpty(entryAgencyPhoneNumber.Text);

            if (isAddressEmtpy || isAddressEmtpy || isEmailEmpty || isPhoneEmpty)
            {
                DisplayAlert("Error", "All fields are required", "Ok");
            }
            else
            {
                Register(agency);
                DisplayAlert("Successful", "Added new agency", "Ok");
            }
        }

        private static async void Register(Agency agency)
        {
            await App.MobileService.GetTable<Agency>().InsertAsync(agency);
        }
    }
}