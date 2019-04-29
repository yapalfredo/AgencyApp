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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var agencies = await Agency.Read();
                listViewAgencies.ItemsSource = agencies;
            }
            catch (NullReferenceException) { }
            catch (Exception) { }
        }

        private void ListViewAgencies_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Will be casted as Agency object
            var selectedAgency = listViewAgencies.SelectedItem as Agency;

            if (selectedAgency != null)
            {
                Navigation.PushAsync(new AgencyDetailsPage(selectedAgency));
            }
        }
    }
}