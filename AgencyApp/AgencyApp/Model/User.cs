using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyApp.Model
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string Agency { get; set; }

        public static async Task<List<User>> Read()
        {
            var users = await App.MobileService.GetTable<User>().ToListAsync();
            return users;
        }


    }
}
