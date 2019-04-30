using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AgencyApp.Model
{
    public class Agency
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public static async Task<List<Agency>> Read()
        {
            List<Agency> agencies  = await App.MobileService.GetTable<Agency>().ToListAsync();
            return agencies;            
        }

        public async static 
        Task
Refresh()
        {
            App.agencies = await Agency.Read();
        }

        public async static void _Refresh()
        {
            App.agencies = await Agency.Read();
        }
    }
}
