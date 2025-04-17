// TRODHA.Core/Interfaces/IRecommendationRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRODHA.Core.Models;

namespace TRODHA.Core.Interfaces
{
    public interface IRecommendationRepository : IRepository<Recommendation>
    {
        Task<IEnumerable<Recommendation>> GetActiveRecommendationsAsync();
        Task<IEnumerable<Recommendation>> GetUserRecommendationsAsync(int userId);
        Task<Recommendation?> GetRandomRecommendationAsync();
        Task<Recommendation?> GetRandomUserRecommendationAsync(int userId);
    }
}
