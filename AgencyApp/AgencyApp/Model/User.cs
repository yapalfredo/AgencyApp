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
        public string CCID { get; set; }

        public static async Task<List<User>> Read()
        {
            var users = await App.MobileService.GetTable<User>().ToListAsync();
            return users;
        }

        public static async Task<bool> LoginVerification(string email, string password)
        {
            bool result = false;
            try
            {
                var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == email && u.Password == password).ToListAsync()).FirstOrDefault();
                if (user != null)
                {
                    App.user = user;
                    result =  true;
                }
                else
                {
                    result = false;
                }
            }
            catch (NullReferenceException) { }
            catch (Exception) { }

            return result;
        }

        
        public static string LoginUserType(User user)
        {
            App.userID = user.Id;
            string userType = user.UserType;
            return userType;
        }


        //TO BE CLEANED SOON ---------------EXPERIMENTAL --------------------------------
        // FROM HERE --------------------------------------------------------------------
        public async static
Task
Refresh()
        {
            App.users = await User.Read();
        }

        public async static void _Refresh()
        {
            App.users = await User.Read();
        }
        // TO HERE ----------------------------------------------------------------------
    }
}
