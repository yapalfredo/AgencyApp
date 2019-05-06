using AgencyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgencyApp.MenuItems;

namespace AgencyApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    //public partial class HomePage : TabbedPage
    public partial class HomePage : MasterDetailPage
    {

        //MasterDetails
        public List<MasterPageItem> MenuList { get; set; }
        string title;
        public HomePage()
        {
            InitializeComponent();
           
            //MasterDetails
            MenuList = new List<MasterPageItem>();
            //  LoadPages(userType);          

            if (App.user.UserType == "Admin")
            {
                MenuList.Add(new MasterPageItem() { Title = "Add Agency", Icon = "", TargetType = typeof(AddAgencyPage) });
                MenuList.Add(new MasterPageItem() { Title = "View Agencies", Icon = "", TargetType = typeof(ViewAgenciesPage) });
                MenuList.Add(new MasterPageItem() { Title = "Add Agency Users", Icon = "", TargetType = typeof(AddAgencyUsersPage) });
                MenuList.Add(new MasterPageItem() { Title = "View Agency Users", Icon = "", TargetType = typeof(ViewAgencyUsersPage) });

                // Initial navigation, this can be used for our home page
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(AddAgencyPage)));
            }
            else if (App.user.UserType == "Agency")
            {
                MenuList.Add(new MasterPageItem() { Title = "Add Client", Icon = "", TargetType = typeof(AddClientPage) });
                MenuList.Add(new MasterPageItem() { Title = "View Clients", Icon = "", TargetType = typeof(ViewClientPage) });
                MenuList.Add(new MasterPageItem() { Title = "Add Contractor", Icon = "", TargetType = typeof(AddContractorPage) });
                MenuList.Add(new MasterPageItem() { Title = "View Contractors", Icon = "", TargetType = typeof(ViewContractorsPage) });
                MenuList.Add(new MasterPageItem() { Title = "Create Assignment", Icon = "", TargetType = typeof(CreateAssignment) });


                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(AddClientPage)));
            }
            else if (App.user.UserType == "Client")
            {
                MenuList.Add(new MasterPageItem() { Title = "Profile", Icon = "", TargetType = typeof(ProfilePage) });

                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ProfilePage)));
            }
            else
            {
                MenuList.Add(new MasterPageItem() { Title = "Profile", Icon = "", TargetType = typeof(ProfilePage) });

                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ProfilePage)));
            }

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = MenuList;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            this.Title = item.Title;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            
            IsPresented = false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_LOGIN_PAGE);
        }
    }
}