using AbySalto.Mid.Domain.ValidationMessages;
using System.ComponentModel.DataAnnotations;

namespace AbySalto.Mid.Application.DTO
{
    public class LoginUserDto
    {
        [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmailFormat), Required(ErrorMessage = ValidationMessages.Required)]
        public string Email { get; set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Password { get; set; }
        public LoginUserDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
