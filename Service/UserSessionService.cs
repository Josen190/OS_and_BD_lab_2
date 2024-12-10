using OS_and_BD_lab_2.Models;

namespace OS_and_BD_lab_2.Service
{
    public class UserSessionService
    {
        public string? JwtToken { get; set; }

        public User? user { get; set; }

        public bool IsAuntification()
        {
            return user != null;
        }

        public bool IsAdmin()
        {
            return user?.isAdmin ?? false;
        }
    }
}
