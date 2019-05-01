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
	public partial class ViewContractorsPage : ContentPage
	{
		public ViewContractorsPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            listViewContractor.ItemsSource = ViewQueries.ContractorUserIDView();
        }

        private void ListViewContractor_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listViewContractor.SelectedItem is ViewQueries selectedView)
            {
                var selectedContractor = App.contractors.Where(c => c.Id == selectedView.Id).FirstOrDefault();
                var selectedUser = App.users.Where(u => u.CCID == selectedView.CCID).FirstOrDefault();

                Navigation.PushAsync(new ContractorDetailsPage(selectedView, selectedContractor, selectedUser));
            }
        }
    }
}