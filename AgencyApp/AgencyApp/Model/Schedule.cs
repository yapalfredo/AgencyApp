using System;
using System.Collections.Generic;
using System.Text;

namespace AgencyApp.Model
{
    public class Schedule
    {
        public string Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ContractorId { get; set; }
        public bool Relieving { get; set; }
    }
}
