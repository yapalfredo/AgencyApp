using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgencyApp.Model
{
    public class Client
    {  
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }        
        public string Phone { get; set; }

        public async static Task<List<Client>> Read()
        {
            List<Client> clients = await App.MobileService.GetTable<Client>().ToListAsync();
            return clients;
        }

        //TO BE CLEANED SOON ---------------EXPERIMENTAL --------------------------------
        // FROM HERE --------------------------------------------------------------------
        public async static
        Task
Refresh()
        {
            // App.clients = await Client.Read();

            App.clients = await App.MobileService.GetTable<Client>().ToListAsync();
        }

        public async static void _Refresh()
        {
            App.clients = await Client.Read();
        }
        // TO HERE ----------------------------------------------------------------------
    }
}
