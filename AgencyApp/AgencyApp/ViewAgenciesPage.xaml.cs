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
    public partial class ViewAgenciesPage : ContentPage
    {
        public ViewAgenciesPage()
        {
            InitializeComponent();
            listViewAgencies.ItemsSource = App.agencies;
        }

        private void ListViewAgencies_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Will be casted as Agency object
            if (listViewAgencies.SelectedItem is Agency selectedAgency)
            {
                Navigation.PushAsync(new AgencyDetailsPage(selectedAgency));
            }
        }
    }
}