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
	public partial class CreateAssignment : ContentPage
	{
		public CreateAssignment ()
		{
			InitializeComponent ();
            pickerClientName.ItemsSource = ViewQueries.ClientUserIDView();
            pickerClientName.ItemDisplayBinding = new Binding("Name");
            pickerContractorName.ItemsSource = ViewQueries.ContractorUserIDView();
            pickerContractorName.ItemDisplayBinding = new Binding("Name");
        }

    }
}