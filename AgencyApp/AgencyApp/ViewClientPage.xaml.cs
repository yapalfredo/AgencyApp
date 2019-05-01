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

            //THE TWO LINES BELOW JOIN THE TABLES User and Clients
            //BUT FILTERS TO DISPLAY ONLY WHAT BELONGS TO CURRENT AGENCY THAT IS LOGGED IN
            //AND SELECTS ONLY USERS THAT ARE TAGGED AS "CLIENT"
            //AFTER THAT IT CREATES A TABLE CONTAINING THE NAME OF THE CLIENT, ITS ID, AND THE ID
            //AGENCY IT BELONG TO
            var queryUsers = App.users.Where(a => a.Agency == App.userID && a.UserType == "Client");
            var queryClient = (from a in queryUsers
                               join b in App.clients on a.CCID equals b.Id
                               select new List<string> { b.Name, b.Id, a.CCID }).ToList();
            //---------------------------------------------------------------------------------------
            //---------------------------------------------------------------------------------------

            listViewClients.ItemsSource = queryClient;

		}
	}
}