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

        public static async Task<List<Client>> Read()
        {
            List<Client> agencies = await App.MobileService.GetTable<Client>().ToListAsync();
            return agencies;
        }

        //TO BE CLEANED SOON ---------------EXPERIMENTAL --------------------------------
        // FROM HERE --------------------------------------------------------------------
        public async static
        Task
Refresh()
        {
            App.clients = await Client.Read();
        }

        public async static void _Refresh()
        {
            App.clients = await Client.Read();
        }
        // TO HERE ----------------------------------------------------------------------
    }
}
