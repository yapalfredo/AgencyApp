using AgencyApp.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AgencyApp
{
    public partial class App : Application
    {
        //Connection to Azure Database
        public static MobileServiceClient MobileService = new MobileServiceClient("https://agencymonitoringapp.azurewebsites.net");

        //Global validated user login
        public static User user = new User();

        //Global for list<Agency>
        public static List<Agency> agencies = new List<Agency>();
        //Global for list<User>
        public static List<User> users = new List<User>();
        //Global for list<Client>
        public static List<Client> clients = new List<Client>();
        //Global for list<Contractor>
        public static List<Contractor> contractors = new List<Contractor>();


        public static string userID;
        public static string EVENT_LAUNCH_LOGIN_PAGE = "EVENT_LAUNCH_LOGIN_PAGE";
        public static string EVENT_LAUNCH_HOME_PAGE = "EVENT_LAUNCH_HOME_PAGE";

        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new LoginPage());

            MainPage = new LoginPage();

            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_LOGIN_PAGE, SetLoginPageAsRootPage);
            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_HOME_PAGE, SetHomePageAsRootPage);
        }

        private void SetLoginPageAsRootPage(object sender)
        {
            MainPage = new LoginPage();
        }

        private void SetHomePageAsRootPage(object sender)
        {
            MainPage = new NavigationPage(new HomePage());
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
