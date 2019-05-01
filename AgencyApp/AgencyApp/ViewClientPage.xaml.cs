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
	public partial class ViewClientPage : ContentPage
	{
		public ViewClientPage ()
		{
			InitializeComponent ();        
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listViewClients.ItemsSource = ViewQueries.ClientUserIDView();          
        }

        private void ListViewClients_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listViewClients.SelectedItem is ViewQueries selectedView)
            {
                var selectedClient = App.clients.Where(c => c.Id == selectedView.Id).FirstOrDefault();           
                var selectedUser = App.users.Where(u => u.CCID == selectedView.CCID).FirstOrDefault();

                   Navigation.PushAsync(new ClientDetailsPage(selectedView, selectedClient, selectedUser));               
            }
        }
    }
}