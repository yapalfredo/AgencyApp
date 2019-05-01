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
    public partial class ClientDetailsPage : ContentPage
    {
        Client client;
        User user;
        public ClientDetailsPage(ViewQueries selectedViewQuery, Client client, User user)
        {
            InitializeComponent();
            this.client = client;
            this.user = user;

            stackLayoutContainer.BindingContext = this.client;
            entryClientEmailAddress.Text = this.user.Email;
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            bool isNameEmpty = string.IsNullOrEmpty(entryClientName.Text);
            bool isAddressEmtpy = string.IsNullOrEmpty(entryClientAddress.Text);
            bool isEmailEmpty = string.IsNullOrEmpty(entryClientEmailAddress.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(entryClientEmailAddress.Text);
            bool isConfPasswordEmpty = string.IsNullOrEmpty(entryClientConfPassword.Text);
            bool isPhoneEmpty = string.IsNullOrEmpty(entryClientPhoneNumber.Text);

            if (isAddressEmtpy || isAddressEmtpy || isEmailEmpty || isPhoneEmpty
                || isPasswordEmpty || isConfPasswordEmpty)
            {
               await DisplayAlert("Error", "All fields are required", "Ok");
            }
            else
            {
                if (entryClientPassword.Text != entryClientConfPassword.Text)
                {
                   await DisplayAlert("Error", "Passwords don't match", "Ok");
                }
                else
                {
                    Update(client);
                    await DisplayAlert("Successful", "Updated client details", "Ok");

                   await this.Navigation.PopAsync();
                }
            }
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            await App.MobileService.GetTable<User>().DeleteAsync(user);
            await App.MobileService.GetTable<Client>().DeleteAsync(client);            
            await DisplayAlert("Successful", "Deleted client", "Ok");
            await Client.Refresh();
            await User.Refresh();

            ViewQueries.ClearFields(this.Content);
            await this.Navigation.PopAsync();
        }

        private async void Update(Client client)
        {
            await App.MobileService.GetTable<Client>().UpdateAsync(client);
            
            user.Email = entryClientEmailAddress.Text;
            user.Password = entryClientPassword.Text;
            await App.MobileService.GetTable<User>().UpdateAsync(user);

            await Client.Refresh();
            await User.Refresh();
        }
    }
}