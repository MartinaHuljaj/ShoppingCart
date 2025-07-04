using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Application.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; }

        public ProductDto(int id, string title, string description,string category, decimal price, int stock, string brand)
        {
            Id = id;
            Title = title;
            Description = description;
            Category = category;
            Price = price;
            Stock = stock;
            Brand = brand;
        }
    }
}
