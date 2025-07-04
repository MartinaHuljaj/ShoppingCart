using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Application.Services.Interfaces
{
    public interface IFavoritesService
    {
        Task<IEnumerable<ProductDto>> GetFavoritesAsync(string userId);
        Task<IActionResult> AddToFavoritesAsync(string userId, int productId);
    }
}
