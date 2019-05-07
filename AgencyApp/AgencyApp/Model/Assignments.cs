using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgencyApp.Model
{
    public class Assignments
    {        
        public string Id { get; set; }
        public string AgencyId { get; set; }
        public string ClientId { get; set; }
        public string StartDate { get; set; }

        // False day, True Night
        public string Shift { get; set; }
        public string DayShiftContractorID { get; set; }
        public string NightShiftContractorID { get; set; }

        public string ClockIn { get; set; }
        public string ClockOut { get; set; }
        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public bool Sat { get; set; }
        public bool Sun { get; set; }

        public string SubstituteDay { get; set; }
        public string SubstituteNight { get; set; }

        public bool Active { get; set; }
        public string EndDate { get; set; }

        public static async Task<List<Assignments>> Read()
        {
            var assignments = await App.MobileService.GetTable<Assignments>().ToListAsync();
            return assignments;
        }

        //TO BE CLEANED SOON ---------------EXPERIMENTAL --------------------------------
        // FROM HERE --------------------------------------------------------------------
        public async static
Task
Refresh()
        {
            App.assignments = await Assignments.Read();
        }

        public async static void _Refresh()
        {
            App.assignments = await Assignments.Read();
        }
        // TO HERE ----------------------------------------------------------------------
    }
}
