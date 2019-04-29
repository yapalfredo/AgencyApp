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
    public partial class AgencyUserDetailsPage : ContentPage
    {
        List<Agency> agency;
        User user;
        public AgencyUserDetailsPage(List<Agency> agency, User user)
        {
            InitializeComponent();
            this.agency = agency;
            this.user = user;
            stackLayoutContainer.BindingContext = this.user;
            pickerAgencyName.ItemsSource = agency;
            pickerAgencyName.ItemDisplayBinding = new Binding("Name");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            entryUserEmailAddress.Text = user.Email;

        }

        Agency selectedAgency;
        private void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            bool isNotSelected = pickerAgencyName.SelectedIndex < 0;
            bool isEmailEmpty = string.IsNullOrEmpty(entryUserEmailAddress.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(entryUserPassword.Text);
            bool isConfPasswordEmpty = string.IsNullOrEmpty(entryUserConfPassword.Text);

            if (isNotSelected || isEmailEmpty || isPasswordEmpty || isConfPasswordEmpty)
            {
                DisplayAlert("Error", "All fields are required", "Ok");
            }
            else
            {
                if (entryUserPassword.Text != entryUserConfPassword.Text)
                {
                    DisplayAlert("Error", "Passwords don't match", "Ok");
                }
                else
                {
                    selectedAgency = pickerAgencyName.SelectedItem as Agency;
                    user.Agency = selectedAgency.Id;
                    Update(user);
                    DisplayAlert("Successful", "Updated agency details", "Ok");
                }
            }
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            Delete(user);
            await DisplayAlert("Successful", "Deleted agency user", "Ok");
        }

        private static async void Update(User user)
        {
            await App.MobileService.GetTable<User>().UpdateAsync(user);
        }

        private static async void Delete(User user)
        {
            await App.MobileService.GetTable<User>().DeleteAsync(user);
        }

 
    }
}