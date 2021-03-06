﻿using AgencyApp.Model;
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
        User user;
        public AgencyUserDetailsPage(User user)
        {
            InitializeComponent();
            this.user = user;
            stackLayoutContainer.BindingContext = this.user;
            pickerAgencyName.ItemsSource = App.agencies;
            pickerAgencyName.ItemDisplayBinding = new Binding("Name");
        }

        Agency selectedAgency;
        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            bool isNotSelected = pickerAgencyName.SelectedIndex < 0;
            bool isEmailEmpty = string.IsNullOrEmpty(entryUserEmailAddress.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(entryUserPassword.Text);
            bool isConfPasswordEmpty = string.IsNullOrEmpty(entryUserConfPassword.Text);

            if (isNotSelected || isEmailEmpty || isPasswordEmpty || isConfPasswordEmpty)
            {
               await DisplayAlert("Error", "All fields are required", "Ok");
            }
            else
            {
                if (entryUserPassword.Text != entryUserConfPassword.Text)
                {
                    await DisplayAlert("Error", "Passwords don't match", "Ok");
                }
                else
                {
                    selectedAgency = pickerAgencyName.SelectedItem as Agency;
                    user.Agency = selectedAgency.Id;
                    Update(user);
                    await DisplayAlert("Successful", "Updated agency details", "Ok");

                    await this.Navigation.PopAsync();
                }
            }
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            Delete(user);
            await DisplayAlert("Successful", "Deleted agency user", "Ok");

            ViewQueries.ClearFields(this.Content);
            await this.Navigation.PopAsync();
        }

        private static async void Update(User user)
        {
            await App.MobileService.GetTable<User>().UpdateAsync(user);
            await User.Refresh();
        }

        private static async void Delete(User user)
        {
            await App.MobileService.GetTable<User>().DeleteAsync(user);
            await User.Refresh();
        }

 
    }
}