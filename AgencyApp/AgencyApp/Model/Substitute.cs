using System;
using System.Collections.Generic;
using System.Text;

namespace AgencyApp.Model
{
    public class Substitute
    {
        public string Id { get; set; }

        // False day, True Night
        public bool ShiftDayOrNight { get; set; }

        public string ContractorId { get; set; }
        public string Date { get; set; }
    }
}
