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
	public partial class HomePage : TabbedPage
	{
        public HomePage(string userType)
        {
            InitializeComponent();
            LoadPages(this, userType);
        }

        public async void LoadPages(TabbedPage thisPage, string userType)
        {            
            if (userType == "Admin")
            {
                await Agency.Refresh();
                await User.Refresh();
                thisPage.Title = "Admin (Developer Side)";
                thisPage.Children.Add(new AddAgencyPage { Title = "Add Agency" });
                thisPage.Children.Add(new ViewAgenciesPage { Title = "View Agencies" });
                thisPage.Children.Add(new AddAgencyUsersPage { Title = "Add Agency Users" });
                thisPage.Children.Add(new ViewAgencyUsersPage { Title = "View Agency Users" });
            }
            else if (userType == "Agency")
            {
                await Client.Refresh();
                await User.Refresh();
                thisPage.Title = "Agency";
                thisPage.Children.Add(new AddClientPage { Title = "Add Client" });
            }
            else if (userType == "Client")
            {

            }
            else if (userType == "Contractor")
            {

            }
        }
    }
}