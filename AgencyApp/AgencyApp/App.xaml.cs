using Microsoft.WindowsAzure.MobileServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AgencyApp
{
    public partial class App : Application
    {
        //Connection to Azure Database
        public static MobileServiceClient MobileService = new MobileServiceClient("https://agencymonitoringapp.azurewebsites.net");

        public App()
        {
            InitializeComponent();            
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
