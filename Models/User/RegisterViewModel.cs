using System.ComponentModel.DataAnnotations;

namespace Floppy.Models.UserModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Длина имени должна быть от 3 до 20 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                           + "@"
                           + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Некорректный Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(maximumLength: 30, MinimumLength = 8, ErrorMessage = "Длина пароля должна быть от 8 до 30 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}
