using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Email обязателен")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; }
    }
}
