using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.Mappers.Interfaces
{
    public interface IProductMapper
    {
        List<ProductDto> FavoriteItemsToDtos(IEnumerable<FavoriteItem> items);
        ProductDto FavoriteItemToDto(FavoriteItem item);
        List<BasketDto> BasketItemsToDtos(IEnumerable<BasketItem> items);
        BasketDto BasketItemToDtos(BasketItem item);
        Product ProductDtoToEntity(ProductDto item);
    }
}
