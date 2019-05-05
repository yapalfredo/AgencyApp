using System;
using System.Collections.Generic;
using System.Text;

namespace AgencyApp.Model
{
    public class Timesheet
    {
        public string Id { get; set; }
        public string Day { get; set; }
        public string Date { get; set; }
        public string ClockedIn { get; set; }
        public string ClockedOut { get; set; }
        public string AssignmentId { get; set; }
    }
}
