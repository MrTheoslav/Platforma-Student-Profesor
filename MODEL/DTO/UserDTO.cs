using System.ComponentModel.DataAnnotations;

namespace MODEL.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Wpisz imię.")]
        public string UserFirstName { get; set; }
        [Required(ErrorMessage = "Wpisz nazwiko.")]
        public string UserLastName { get; set; }
        [Required(ErrorMessage = "Wpisz adres e-mail.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres e-mail.")]
        [MinLength(10, ErrorMessage = "Adres e-mail powinien składać się z minimum {1} znaków.")]
        [MaxLength(50, ErrorMessage = "Adres e-mail powinien składać się z maksimum {1} znaków.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Wpisz hasło.")]
        [MinLength(8, ErrorMessage = "Hasło powinno składać się z minimum {1} znaków.")]
        [MaxLength(32, ErrorMessage = "Hasło powinno składać się z maksimum {1} znaków.")]
        public string Password { get; set; }
        public DateTime EnterDate { get; set; }
        public int RoleID { get; set; }
    }
}
