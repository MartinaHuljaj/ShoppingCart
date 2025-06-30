using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Application.DTO
{
    public class RegisterUserDto
    {
        [EmailAddress(ErrorMessage = "Invalid email address format."), Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
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
