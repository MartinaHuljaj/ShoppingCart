using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Mappers.Interfaces;
using AbySalto.Mid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Application.Mappers
{
    public class ProductMapper: IProductMapper
    {
        public ProductDto FavoriteItemToDto(FavoriteItem item)
        {
            if (item == null)
            {
                return null;
            }
            return new ProductDto(
                item.Product.ProductId,
                item.Product.Title,
                item.Product.Description,
                item.Product.Category,
                item.Product.Price,
                item.Product.Stock,
                item.Product.Brand
            );
        }
        public List<ProductDto> FavoriteItemsToDtos(IEnumerable<FavoriteItem> items)
        {
            var favoriteProducts = new List<ProductDto>();
            foreach (var item in items)
            {
                favoriteProducts.Add(new ProductDto(
                    item.Product.ProductId,
                    item.Product.Title,
                    item.Product.Description,
                    item.Product.Category,
                    item.Product.Price,
                    item.Product.Stock,
                    item.Product.Brand
                    ));
            }
            return favoriteProducts;
        }

        public Product ProductDtoToEntity(ProductDto item)
        {
            return new Product
            {
                ProductId = item.Id,
                Title = item.Title,
                Description = item.Description,
                Category = item.Category,
                Price = item.Price,
                Stock = item.Stock,
                Brand = item.Brand
            };
        }

    }
}
