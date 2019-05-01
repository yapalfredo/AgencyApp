using System;
using System.Collections.Generic;
using System.Text;

namespace AgencyApp.Model
{
    public class Contractor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AgencyId { get; set; }
    }
}
