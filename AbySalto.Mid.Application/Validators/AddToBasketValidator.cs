using AbySalto.Mid.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Application.Validators
{
    public class AddToBasketValidator
    {
        public void Validate(string userId, int quantity, ProductDto productDto)
        {
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User not authorized.");

            if (quantity <= 0)
                throw new InvalidOperationException("Quantity must be greater than zero.");

            if (productDto == null || productDto.Stock < quantity)
                throw new InvalidOperationException("There is not enough product in stock.");
        }
    }
}
