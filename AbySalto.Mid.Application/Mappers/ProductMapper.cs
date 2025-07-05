using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Mappers.Interfaces;
using AbySalto.Mid.Domain.Entities;

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

        public List<BasketDto> BasketItemsToDtos(IEnumerable<BasketItem> items)
        {
            var basketItems = new List<BasketDto>();
            foreach (var item in items)
            {
                basketItems.Add(new BasketDto(
                    item.Product.ProductId,
                    item.Product.Title,
                    item.Product.Description,
                    item.Product.Category,
                    item.Product.Price,
                    item.Product.Stock,
                    item.Product.Brand,
                    item.Quantity
                    ));
            }
            return basketItems;
        }

        public BasketDto BasketItemToDtos(BasketItem item)
        {
            return new BasketDto(
                    item.Product.ProductId,
                    item.Product.Title,
                    item.Product.Description,
                    item.Product.Category,
                    item.Product.Price,
                    item.Product.Stock,
                    item.Product.Brand,
                    item.Quantity
                    );
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
