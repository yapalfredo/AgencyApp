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
	public partial class ViewAgencyUsersPage : ContentPage
	{
		public ViewAgencyUsersPage ()
		{
			InitializeComponent ();
		}

        List<Agency> agencies;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                agencies = await Agency.Read();
                pickerAgencyName.ItemsSource = agencies;
                pickerAgencyName.ItemDisplayBinding = new Binding("Name");
            }
            catch (NullReferenceException) { }
            catch (Exception){ }
        }

        Agency selectedAgency;
        private async void PickerAgencyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isNotSelected = pickerAgencyName.SelectedIndex < 0;

            //populate list view if selected index is changed
            if (!isNotSelected)
            {
                selectedAgency = pickerAgencyName.SelectedItem as Agency;
                try
                {
                    IEnumerable<User> users = await App.MobileService.GetTable<User>().Where(u => u.Agency == selectedAgency.Id).ToListAsync();
                    listViewAgencyUsers.ItemsSource = users;
                    listViewAgencyUsers.IsEnabled = true;
                }
                catch (NullReferenceException) { }
                catch (Exception) { }
            }
        }

        private void ListViewAgencyUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedUser = listViewAgencyUsers.SelectedItem as User;
            if(selectedUser != null)
            {
                Navigation.PushAsync(new AgencyUserDetailsPage(agencies, selectedUser));
            }
        }
    }
}