using System.ComponentModel.DataAnnotations;

namespace Floppy.Models.UserModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя или email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(maximumLength: 30, MinimumLength = 8, ErrorMessage = "Длина пароля должна быть от 8 до 30 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
