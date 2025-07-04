using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Application.Mappers.Interfaces
{
    public interface IProductMapper
    {
        List<ProductDto> FavoriteItemsToDtos(IEnumerable<FavoriteItem> items);
        ProductDto FavoriteItemToDto(FavoriteItem item);
        Product ProductDtoToEntity(ProductDto item);
    }
}
