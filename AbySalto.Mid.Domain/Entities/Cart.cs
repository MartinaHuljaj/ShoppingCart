using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Domain.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public ICollection<int> ProductIds { get; set; } = [];
    }
}
