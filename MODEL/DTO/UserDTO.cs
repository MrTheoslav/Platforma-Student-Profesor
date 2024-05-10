
using System.ComponentModel.DataAnnotations;

namespace MODEL.DTO
{
    public class UserDTO
    {
      

        [Required(ErrorMessage = "Wpisz imię.")]
        public string UserFirstName { get; set; }
        [Required(ErrorMessage = "Wpisz nazwiko.")]
        public string UserLastName { get; set; }
        [Required(ErrorMessage = "Wpisz adres e-mail.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres e-mail.")]
        [MinLength(10, ErrorMessage = "Adres e-mail powinien składać się z minimum {1} znaków.")]
        [MaxLength(50, ErrorMessage = "Adres e-mail powinien składać się z maksimum {1} znaków.")]
        public string Email { get; set; }
        public DateTime EnterDate { get; set; }
        public int RoleID { get; set; }
    }
}
