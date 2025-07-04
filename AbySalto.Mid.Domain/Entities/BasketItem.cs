using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Domain.Entities
{
    public class BasketItem
    {
        public int BasketItemId { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; } = default!;
        public int Quantity { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
