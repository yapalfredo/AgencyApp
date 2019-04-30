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

        public static async Task<bool> LoginVerification(string email, string password)
        {
            try
            {
                var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == email && u.Password == password).ToListAsync()).FirstOrDefault();
                if (user != null)
                {
                    App.user = user;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException) { }
            catch (Exception) { }

            return false;
        }

        private static void DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        public static string LoginUserType(User user)
        {
            string userType = user.UserType;
            return userType;
        }

    }
}
