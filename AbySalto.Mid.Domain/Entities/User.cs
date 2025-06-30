using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Domain.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User(string email, string firstName, string lastName)
        {
            UserName = email;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
