// TRODHA.Server/Controllers/RecommendationsController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TRODHA.Application.DTOs;
using TRODHA.Application.Services.Interfaces;

namespace TRODHA.Server.Controllers
{
    [Route("api/recommendations")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationsController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        // Kullan�c� �nerileri

        [HttpGet("random")]
        public async Task<ActionResult<RecommendationDto>> GetRandomRecommendation()
        {
            var recommendation = await _recommendationService.GetRandomAsync();
            
            if (recommendation == null)
                return NotFound(new { message = "Hi� �neri bulunamad�" });
                
            return Ok(recommendation);
        }

        [HttpGet("random/user")]
        [Authorize]
        public async Task<ActionResult<RecommendationDto>> GetRandomUserRecommendation()
        {
            int userId = GetCurrentUserId();
            var recommendation = await _recommendationService.GetRandomForUserAsync(userId);
            
            if (recommendation == null)
                return NotFound(new { message = "Hi� �neri bulunamad�" });
                
            return Ok(recommendation);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RecommendationDto>>> GetUserRecommendations()
        {
            int userId = GetCurrentUserId();
            var recommendations = await _recommendationService.GetForUserAsync(userId);
            return Ok(recommendations);
        }

        // Admin y�netimi (ger�ek d�nyada burada admin yetki kontrol� olurdu)

        [HttpGet]
        [Authorize]  // Normalde [Authorize(Roles = "Admin")] olmal�
        public async Task<ActionResult<IEnumerable<RecommendationDto>>> GetAll()
        {
            var recommendations = await _recommendationService.GetAllAsync();
            return Ok(recommendations);
        }

        [HttpGet("active")]
        [Authorize]  // Normalde [Authorize(Roles = "Admin")] olmal�
        public async Task<ActionResult<IEnumerable<RecommendationDto>>> GetActive()
        {
            var recommendations = await _recommendationService.GetActiveAsync();
            return Ok(recommendations);
        }

        [HttpGet("{id}")]
        [Authorize]  // Normalde [Authorize(Roles = "Admin")] olmal�
        public async Task<ActionResult<RecommendationDto>> GetById(int id)
        {
            var recommendation = await _recommendationService.GetByIdAsync(id);
            
            if (recommendation == null)
                return NotFound();
                
            return Ok(recommendation);
        }

        [HttpPost]
        [Authorize]  // Normalde [Authorize(Roles = "Admin")] olmal�
        public async Task<ActionResult<RecommendationDto>> Create(CreateRecommendationDto createDto)
        {
            try
            {
                var recommendation = await _recommendationService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = recommendation.RecommendationId }, recommendation);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]  // Normalde [Authorize(Roles = "Admin")] olmal�
        public async Task<ActionResult<RecommendationDto>> Update(int id, UpdateRecommendationDto updateDto)
        {
            try
            {
                var recommendation = await _recommendationService.UpdateAsync(id, updateDto);
                return Ok(recommendation);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]  // Normalde [Authorize(Roles = "Admin")] olmal�
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _recommendationService.DeleteAsync(id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new ApplicationException("Kullan�c� kimli�i bulunamad�");
                
            return int.Parse(userIdClaim.Value);
        }
    }
}
