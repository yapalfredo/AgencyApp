using System;
using System.Collections.Generic;
using System.Text;

namespace AgencyApp.Model
{
    public class Assigments
    {        
        public string Id { get; set; }
        public string AgencyId { get; set; }
        public string ClientId { get; set; }
        public string ScheduleId { get; set; }  //This is related to Schedule.Id
        public bool Active { get; set; }
    }
}
