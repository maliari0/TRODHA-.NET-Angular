// TRODHA.Infrastructure/Repositories/RecommendationRepository.cs
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRODHA.Core.Interfaces;
using TRODHA.Core.Models;
using TRODHA.Infrastructure.Data;

namespace TRODHA.Infrastructure.Repositories
{
    public class RecommendationRepository : Repository<Recommendation>, IRecommendationRepository
    {
        private readonly Random _random = new Random();

        public RecommendationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Recommendation>> GetActiveRecommendationsAsync()
        {
            return await _context.Recommendations
                .Where(r => r.IsActive && r.UserId == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recommendation>> GetUserRecommendationsAsync(int userId)
        {
            return await _context.Recommendations
                .Where(r => r.IsActive && (r.UserId == userId || r.UserId == null))
                .ToListAsync();
        }

        public async Task<Recommendation?> GetRandomRecommendationAsync()
        {
            var recommendations = await GetActiveRecommendationsAsync();
            var recommendationsList = recommendations.ToList();
            
            if (recommendationsList.Count == 0)
                return null;
                
            return recommendationsList[_random.Next(recommendationsList.Count)];
        }

        public async Task<Recommendation?> GetRandomUserRecommendationAsync(int userId)
        {
            var recommendations = await GetUserRecommendationsAsync(userId);
            var recommendationsList = recommendations.ToList();
            
            if (recommendationsList.Count == 0)
                return null;
                
            return recommendationsList[_random.Next(recommendationsList.Count)];
        }
    }
}
