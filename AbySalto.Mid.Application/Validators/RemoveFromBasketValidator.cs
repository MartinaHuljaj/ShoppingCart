using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Application.Validators
{
    public class RemoveFromBasketValidator
    {
        public void Validate(BasketItem item, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("User not authorized.");

            if (item == null)
                throw new InvalidOperationException("Product not found in basket.");
        }
    }
}
