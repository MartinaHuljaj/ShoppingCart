using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Application.DTO
{
    public class LoginUserDto
    {
        [EmailAddress(ErrorMessage = "Invalid email address format."), Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public LoginUserDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
        //public LoginUserDto Convert()
        //{

        //}
    }
}
