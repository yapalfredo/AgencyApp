using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgencyApp.Model
{
    public class Contractor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public static async Task<List<Contractor>> Read()
        {
            List<Contractor> contractors = await App.MobileService.GetTable<Contractor>().ToListAsync();
            return contractors;
        }

        //TO BE CLEANED SOON ---------------EXPERIMENTAL --------------------------------
        // FROM HERE --------------------------------------------------------------------
        public async static
        Task
Refresh()
        {
            App.contractors = await Contractor.Read();
        }

        public async static void _Refresh()
        {
            App.contractors = await Contractor.Read();
        }
        // TO HERE ----------------------------------------------------------------------

    }
}
