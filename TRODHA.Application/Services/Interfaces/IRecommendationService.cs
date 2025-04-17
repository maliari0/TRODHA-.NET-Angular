// TRODHA.Application/Services/Interfaces/IRecommendationService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Services.Interfaces
{
    public interface IRecommendationService
    {
        Task<IEnumerable<RecommendationDto>> GetAllAsync();
        Task<IEnumerable<RecommendationDto>> GetActiveAsync();
        Task<IEnumerable<RecommendationDto>> GetForUserAsync(int userId);
        Task<RecommendationDto?> GetByIdAsync(int id);
        Task<RecommendationDto> CreateAsync(CreateRecommendationDto createDto);
        Task<RecommendationDto> UpdateAsync(int id, UpdateRecommendationDto updateDto);
        Task DeleteAsync(int id);
        Task<RecommendationDto?> GetRandomForUserAsync(int userId);
        Task<RecommendationDto?> GetRandomAsync();
    }
}
