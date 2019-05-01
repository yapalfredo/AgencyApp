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
	public partial class ContractorDetailsPage : ContentPage
	{
        Contractor contractor;
        User user;
        public ContractorDetailsPage (ViewQueries selectedViewQuery, Contractor contractor, User user)
		{
			InitializeComponent ();
            this.contractor = contractor;
            this.user = user;

            stackLayoutContainer.BindingContext = this.contractor;
            entryContractorEmailAddress.Text = this.user.Email;
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            bool isNameEmpty = string.IsNullOrEmpty(entryContractorName.Text);
            bool isAddressEmtpy = string.IsNullOrEmpty(entryContractorAddress.Text);
            bool isEmailEmpty = string.IsNullOrEmpty(entryContractorEmailAddress.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(entryContractorEmailAddress.Text);
            bool isConfPasswordEmpty = string.IsNullOrEmpty(entryContractorConfPassword.Text);
            bool isPhoneEmpty = string.IsNullOrEmpty(entryContractorPhoneNumber.Text);

            if (isAddressEmtpy || isAddressEmtpy || isEmailEmpty || isPhoneEmpty
                || isPasswordEmpty || isConfPasswordEmpty)
            {
                await DisplayAlert("Error", "All fields are required", "Ok");
            }
            else
            {
                if (entryContractorPassword.Text != entryContractorConfPassword .Text)
                {
                    await DisplayAlert("Error", "Passwords don't match", "Ok");
                }
                else
                {
                    Update(contractor);
                    await DisplayAlert("Successful", "Updated contractor details", "Ok");

                    await this.Navigation.PopAsync();
                }
            }
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            await App.MobileService.GetTable<User>().DeleteAsync(user);
            await App.MobileService.GetTable<Contractor>().DeleteAsync(contractor);
            await DisplayAlert("Successful", "Deleted contractor", "Ok");
            await Contractor.Refresh();
            await User.Refresh();

            ViewQueries.ClearFields(this.Content);
            await this.Navigation.PopAsync();
        }

        private async void Update(Contractor contractor)
        {
            await App.MobileService.GetTable<Contractor>().UpdateAsync(contractor);
            
            user.Email = entryContractorEmailAddress.Text;
            user.Password = entryContractorPassword.Text;
            await App.MobileService.GetTable<User>().UpdateAsync(user);

            await Contractor.Refresh();
            await User.Refresh();
        }
    }
}