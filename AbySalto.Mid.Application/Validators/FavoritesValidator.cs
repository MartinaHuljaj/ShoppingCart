using AbySalto.Mid.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Application.Validators
{
    public class FavoritesValidator
    {
        public void Validate(string userId, ProductDto product)
        {
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User not authorized.");
            if (product == null)
                throw new InvalidOperationException("Product doesn't exist.");
        }
        public void Validate(string userId, int productId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User not authorized.");
            if (productId <= 0)
                throw new InvalidOperationException("Product ID must be greater than zero.");
        }
    }
}
