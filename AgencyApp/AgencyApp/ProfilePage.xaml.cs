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
	public partial class ProfilePage : ContentPage
	{
 
        Client client, tempClient;
        Contractor contractor, tempContractor;

        public ProfilePage ()
		{
			InitializeComponent ();
            buttonEditUpdate.Text = "Edit";

            if (App.user.UserType == "Client")
            {
                client = App.clients.Where(c => c.Id == App.user.CCID).FirstOrDefault();
                tempClient = client;
                stackLayoutContainer.BindingContext = client;
            }
            else if (App.user.UserType == "Contractor")
            {
                contractor = App.contractors.Where(c => c.Id == App.user.CCID).FirstOrDefault();
                tempContractor = contractor;
                stackLayoutContainer.BindingContext = contractor;                
            }
            entryEmail.Text = App.user.Email;
        }

        private async void ButtonEditUpdate_Clicked(object sender, EventArgs e)
        {
            if (buttonEditUpdate.Text == "Edit")
            {
                buttonEditUpdate.Text = "Update";
                ViewQueries.EnableFields(this.Content);
                buttonCancel.IsVisible = true;
            }
            else if (buttonEditUpdate.Text == "Update")
            {
                bool isNameEmpty = string.IsNullOrEmpty(entryName.Text);
                bool isAddressEmtpy = string.IsNullOrEmpty(entryAddress.Text);
                bool isEmailEmpty = string.IsNullOrEmpty(entryEmail.Text);
                bool isPasswordEmpty = string.IsNullOrEmpty(entryPassword.Text);
                bool isConfPasswordEmpty = string.IsNullOrEmpty(entryConfirmPassword.Text);
                bool isPhoneEmpty = string.IsNullOrEmpty(entryPhone.Text);

                if (isAddressEmtpy || isAddressEmtpy || isEmailEmpty || isPhoneEmpty
                    || isPasswordEmpty || isConfPasswordEmpty)
                {
                    await DisplayAlert("Error", "All fields are required", "Ok");
                }
                else
                {
                    if (entryPassword.Text != entryConfirmPassword.Text)
                    {
                        await DisplayAlert("Error", "Passwords don't match", "Ok");
                    }
                    else
                    {    
                        if (App.user.UserType == "Client")
                        {
                            Update(client);
                            await DisplayAlert("Successful", "Updated client details", "Ok");
                        }
                        else if (App.user.UserType == "Contractor")
                        {
                            Update(contractor);
                            await DisplayAlert("Successful", "Updated contractor details", "Ok");
                        }                        
                        await User.Refresh();
                        buttonCancel.IsVisible = false;
                        ViewQueries.DisableFields(this.Content);
                    }
                }
            }           
        }

        private async void Update(Contractor contractor)
        {
            await App.MobileService.GetTable<Contractor>().UpdateAsync(contractor);

            App.user.Email = entryEmail.Text;
            App.user.Password = entryPassword.Text;
            await App.MobileService.GetTable<User>().UpdateAsync(App.user);

            await Contractor.Refresh();            
        }

        private async void Update(Client client)
        {
            await App.MobileService.GetTable<Client>().UpdateAsync(client);

            App.user.Email = entryEmail.Text;
            App.user.Password = entryPassword.Text;
            await App.MobileService.GetTable<User>().UpdateAsync(App.user);

            await Client.Refresh();
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            ViewQueries.DisableFields(this.Content);
            buttonEditUpdate.Text = "Edit";
            buttonCancel.IsVisible = false;

            //Restores the original value when cancel is triggered
            if (App.user.UserType == "Client")
            {
                client = tempClient;
            }
            else if (App.user.UserType == "Contractor")
            {
                contractor = tempContractor;
            }
            entryEmail.Text = App.user.Email;
        }
    }
}