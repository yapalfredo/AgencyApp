using AgencyApp.Contants;
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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            var assembly = typeof(LoginPage);
            imageLogo.Source = ImageSource.FromResource("AgencyApp.Assets.Images.agencymonitoring.png",assembly);
		}

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(entryEmail.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(entryPassword.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {
                await DisplayAlert("Error", "Please enter both email and password", "Ok");
            }
            else
            {
                if (entryEmail.Text == Constants.ADMINUN &&
                    entryPassword.Text == Constants.ADMINPW)
                {
                     App.user.UserType = "Admin";
                     LoadMasterPage();
                     MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_HOME_PAGE);
                }
                else if (await User.LoginVerification(entryEmail.Text, entryPassword.Text))
                {
                    User.LoginUserType(App.user);
                    LoadMasterPage();
                    MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_HOME_PAGE);
                }
                else
                {
                    await DisplayAlert("Error", "Incorrect login details", "Ok");
                }
            }
        }

        private async void LoadMasterPage()
        {
            await Agency.Refresh();
            await User.Refresh();
            await Client.Refresh();
            await Contractor.Refresh();
        }
    }
}