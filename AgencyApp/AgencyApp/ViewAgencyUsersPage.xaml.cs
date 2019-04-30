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

            pickerAgencyName.ItemsSource = App.agencies;
            pickerAgencyName.ItemDisplayBinding = new Binding("Name");
        }

        Agency selectedAgency;
        private void PickerAgencyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isNotSelected = pickerAgencyName.SelectedIndex < 0;

            //populate list view if selected index is changed
            if (!isNotSelected)
            {
                selectedAgency = pickerAgencyName.SelectedItem as Agency;
                try
                {
                    //IEnumerable<User> users = await App.MobileService.GetTable<User>().Where(u => u.Agency == selectedAgency.Id).ToListAsync();
                    var users = App.users.Where(u => u.Agency == selectedAgency.Id);
                    listViewAgencyUsers.ItemsSource = users;
                    listViewAgencyUsers.IsEnabled = true;
                }
                catch (NullReferenceException) { }
                catch (Exception) { }
            }
        }

        private void ListViewAgencyUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listViewAgencyUsers.SelectedItem is User selectedUser)
            {
                Navigation.PushAsync(new AgencyUserDetailsPage(selectedUser));
            }
        }
    }
}