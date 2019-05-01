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
    public partial class AddClientPage : ContentPage
    {

        Client client;
        User user;

        public AddClientPage()
        {
            InitializeComponent();
            client = new Client();
            stackLayoutContainer.BindingContext = client;
        }

        private void ButtonAdd_Clicked(object sender, EventArgs e)
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
                DisplayAlert("Error", "All fields are required", "Ok");
            }
            else
            {
                if (entryClientPassword.Text != entryClientConfPassword.Text)
                {
                    DisplayAlert("Error", "Passwords don't match", "Ok");
                }
                else
                {                 
                    Register(client);
                    DisplayAlert("Successful", "Added new client", "Ok");
                }                
            }
        }

        private async void Register(Client client)
        {
            await App.MobileService.GetTable<Client>().InsertAsync(client);

            user = new User
            {
                Email = entryClientEmailAddress.Text,
                Password = entryClientPassword.Text,
                UserType = "Client",
                Agency = App.userID,
                CCID = client.Id
            };
            await App.MobileService.GetTable<User>().InsertAsync(user);

            await Client.Refresh();
            await User.Refresh();
        }
    }
}