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
	public partial class AddContractorPage : ContentPage
	{
        Contractor contractor;
        User user;

		public AddContractorPage ()
		{
			InitializeComponent ();
            contractor = new Contractor();
            stackLayoutContainer.BindingContext = contractor;
		}

        private async void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            bool isNameEmpty = string.IsNullOrEmpty(entryContractorName.Text);
            bool isAddressEmtpy = string.IsNullOrEmpty(entryContractorAddress.Text);
            bool isEmailEmpty = string.IsNullOrEmpty(entryContractorEmailAddress.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(entryContractorPassword.Text);
            bool isConfPasswordEmpty = string.IsNullOrEmpty(entryContractorConfPassword.Text);
            bool isPhoneEmpty = string.IsNullOrEmpty(entryContractorPhoneNumber.Text);

            if (isAddressEmtpy || isAddressEmtpy || isEmailEmpty || isPhoneEmpty
                || isPasswordEmpty || isConfPasswordEmpty)
            {
                await DisplayAlert("Error", "All fields are required", "Ok");
            }
            else
            {
                if (entryContractorPassword.Text != entryContractorConfPassword.Text)
                {
                    await DisplayAlert("Error", "Passwords don't match", "Ok");
                }
                else
                {
                    Register(contractor);
                    await DisplayAlert("Successful", "Added new contractor", "Ok");

                    ViewQueries.ClearFields(this.Content);
                }
            }
        }

        async void Register(Contractor contractor)
        {
            await App.MobileService.GetTable<Contractor>().InsertAsync(contractor);

            user = new User
            {
                Email = entryContractorEmailAddress.Text,
                Password = entryContractorPassword.Text,
                UserType = "Contractor",
                Agency = App.userID,
                CCID = contractor.Id
            };
            await App.MobileService.GetTable<User>().InsertAsync(user);

            await Contractor.Refresh();
            await User.Refresh();
        }
    }
}