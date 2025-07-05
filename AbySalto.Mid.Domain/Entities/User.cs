using Microsoft.AspNetCore.Identity;

namespace AbySalto.Mid.Domain.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<FavoriteItem> FavoriteItems { get; set; } = [];
        public ICollection<BasketItem> BasketItems { get; set; } = [];

        public User(string email, string firstName, string lastName)
        {
            UserName = email;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
