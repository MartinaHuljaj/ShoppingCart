using AbySalto.Mid.Domain.ValidationMessages;
using System.ComponentModel.DataAnnotations;

namespace AbySalto.Mid.Application.DTO
{
    public class RegisterUserDto
    {
        [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmailFormat), Required(ErrorMessage = ValidationMessages.Required)]
        public string Email { get; set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Password { get; set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string LastName { get; set; }

        public RegisterUserDto(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
