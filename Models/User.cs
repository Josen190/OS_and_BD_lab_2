namespace OS_and_BD_lab_2.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public bool isAdmin { get; set; }

    }

    public class LoginUserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
