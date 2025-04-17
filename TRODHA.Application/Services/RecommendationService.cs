// TRODHA.Application/Services/RecommendationService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRODHA.Application.DTOs;
using TRODHA.Application.Services.Interfaces;
using TRODHA.Core.Interfaces;
using TRODHA.Core.Models;

namespace TRODHA.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;

        public RecommendationService(IRecommendationRepository recommendationRepository)
        {
            _recommendationRepository = recommendationRepository;
        }

        public async Task<IEnumerable<RecommendationDto>> GetAllAsync()
        {
            var recommendations = await _recommendationRepository.GetAllAsync();
            return recommendations.Select(MapToDto);
        }

        public async Task<IEnumerable<RecommendationDto>> GetActiveAsync()
        {
            var recommendations = await _recommendationRepository.GetActiveRecommendationsAsync();
            return recommendations.Select(MapToDto);
        }

        public async Task<IEnumerable<RecommendationDto>> GetForUserAsync(int userId)
        {
            var recommendations = await _recommendationRepository.GetUserRecommendationsAsync(userId);
            return recommendations.Select(MapToDto);
        }

        public async Task<RecommendationDto?> GetByIdAsync(int id)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(id);
            return recommendation != null ? MapToDto(recommendation) : null;
        }

        public async Task<RecommendationDto> CreateAsync(CreateRecommendationDto createDto)
        {
            var recommendation = new Recommendation
            {
                Content = createDto.Content,
                UserId = createDto.UserId,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            await _recommendationRepository.AddAsync(recommendation);
            return MapToDto(recommendation);
        }

        public async Task<RecommendationDto> UpdateAsync(int id, UpdateRecommendationDto updateDto)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(id);
            
            if (recommendation == null)
                throw new ApplicationException("Öneri bulunamadý");
                
            recommendation.Content = updateDto.Content;
            recommendation.UserId = updateDto.UserId;
            recommendation.IsActive = updateDto.IsActive;
            
            await _recommendationRepository.UpdateAsync(recommendation);
            return MapToDto(recommendation);
        }

        public async Task DeleteAsync(int id)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(id);
            
            if (recommendation == null)
                throw new ApplicationException("Öneri bulunamadý");
                
            await _recommendationRepository.DeleteAsync(recommendation);
        }

        public async Task<RecommendationDto?> GetRandomForUserAsync(int userId)
        {
            var recommendation = await _recommendationRepository.GetRandomUserRecommendationAsync(userId);
            return recommendation != null ? MapToDto(recommendation) : null;
        }

        public async Task<RecommendationDto?> GetRandomAsync()
        {
            var recommendation = await _recommendationRepository.GetRandomRecommendationAsync();
            return recommendation != null ? MapToDto(recommendation) : null;
        }
        
        private RecommendationDto MapToDto(Recommendation recommendation)
        {
            return new RecommendationDto
            {
                RecommendationId = recommendation.RecommendationId,
                Content = recommendation.Content,
                UserId = recommendation.UserId,
                IsActive = recommendation.IsActive,
                CreatedAt = recommendation.CreatedAt
            };
        }
    }
}
