using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AgencyApp.Model
{
    public class ViewQueries
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string CCID { get; set; }
        public StackLayout Content { get; private set; }

        public static List<ViewQueries> ClientUserIDView()
        {
            //THE TWO LINES BELOW JOIN THE TABLES User and Clients
            //BUT FILTERS TO DISPLAY ONLY WHAT BELONGS TO CURRENT AGENCY THAT IS LOGGED IN
            //AND SELECTS ONLY USERS THAT ARE TAGGED AS "CLIENT"
            //AFTER THAT IT CREATES A TABLE CONTAINING THE NAME OF THE CLIENT, ITS ID, AND THE ID
            //AGENCY IT BELONG TO
            var queryUsers = App.users.Where(a => a.Agency == App.userID && a.UserType == "Client");
            var queryClient = (from a in queryUsers
                               join b in App.clients on a.CCID equals b.Id
                               select new List<string> { b.Name, b.Id, a.CCID });
            //---------------------------------------------------------------------------------------
            //---------------------------------------------------------------------------------------

            List<ViewQueries> viewQuery = new List<ViewQueries>();
            foreach(List<string> iter in queryClient)
            {
                ViewQueries temp = new ViewQueries
                {
                    Name = iter[0],
                    Id = iter[1],
                    CCID = iter[2]
                };
                viewQuery.Add(temp);
            }
            return viewQuery;
        }


        //THIS IS USED FOR CLEARING ENTRIES / TEXT FIELDS IN THE PAGE 
        public static void ClearFields(View _content)
        {
            foreach (View item in ((StackLayout)_content).Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    E.Text = "";
                }
            }
        }
    }
}
