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
	public partial class AgencyDetailsPage : ContentPage
	{
        Agency selectedAgency;

		public AgencyDetailsPage (Agency selectedAgency)
		{
			InitializeComponent ();
            this.selectedAgency = selectedAgency;
            stackLayoutContainer.BindingContext = this.selectedAgency;
		}

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            bool isNameEmpty = string.IsNullOrEmpty(entryAgencyName.Text);
            bool isAddressEmtpy = string.IsNullOrEmpty(entryAgencyAddress.Text);
            bool isEmailEmpty = string.IsNullOrEmpty(entryAgencyEmailAddress.Text);
            bool isPhoneEmpty = string.IsNullOrEmpty(entryAgencyPhoneNumber.Text);

            if (isAddressEmtpy || isAddressEmtpy || isEmailEmpty || isPhoneEmpty)
            {
               await DisplayAlert("Error", "All fields are required", "Ok");
            }
            else
            {
                Update(selectedAgency);
                await DisplayAlert("Successful", "Updated agency details", "Ok");

                await this.Navigation.PopAsync();
            }
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            Delete(selectedAgency);
            await DisplayAlert("Successful", "Deleted agency", "Ok");

            ViewQueries.ClearFields(this.Content);
            await this.Navigation.PopAsync();
        }

        private static async void Update(Agency selectedAgency)
        {
            await App.MobileService.GetTable<Agency>().UpdateAsync(selectedAgency);
            await Agency.Refresh();
        }

        private static async void Delete(Agency selectedAgency)
        {
            await App.MobileService.GetTable<Agency>().DeleteAsync(selectedAgency);
            await Agency.Refresh();
        }
    }
}