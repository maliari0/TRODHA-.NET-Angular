// TRODHA.Server/Controllers/GoalsController.cs
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalsController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoalDto>>> GetUserGoals()
        {
            int userId = GetCurrentUserId();
            var goals = await _goalService.GetUserGoalsAsync(userId);
            return Ok(goals);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<GoalDto>>> GetActiveUserGoals()
        {
            int userId = GetCurrentUserId();
            var goals = await _goalService.GetActiveUserGoalsAsync(userId);
            return Ok(goals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GoalDto>> GetGoal(int id)
        {
            int userId = GetCurrentUserId();
            var goal = await _goalService.GetGoalByIdAsync(id, userId);
            
            if (goal == null)
                return NotFound();
                
            return Ok(goal);
        }

        [HttpPost]
        public async Task<ActionResult<GoalDto>> CreateGoal(CreateGoalDto createGoalDto)
        {
            try
            {
                int userId = GetCurrentUserId();
                var goal = await _goalService.CreateGoalAsync(userId, createGoalDto);
                return CreatedAtAction(nameof(GetGoal), new { id = goal.GoalId }, goal);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GoalDto>> UpdateGoal(int id, UpdateGoalDto updateGoalDto)
        {
            try
            {
                int userId = GetCurrentUserId();
                var goal = await _goalService.UpdateGoalAsync(id, userId, updateGoalDto);
                return Ok(goal);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGoal(int id)
        {
            try
            {
                int userId = GetCurrentUserId();
                await _goalService.DeleteGoalAsync(id, userId);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/toggle")]
        public async Task<ActionResult> ToggleGoalStatus(int id, [FromBody] bool isActive)
        {
            try
            {
                int userId = GetCurrentUserId();
                var result = await _goalService.ToggleGoalStatusAsync(id, userId, isActive);
                return Ok(new { isActive = result });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("statistics")]
        public async Task<ActionResult> GetGoalStatistics()
        {
            int userId = GetCurrentUserId();
            var statistics = await _goalService.GetGoalStatisticsByUserIdAsync(userId);
            return Ok(statistics);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new ApplicationException("Kullanýcý kimliði bulunamadý");
                
            return int.Parse(userIdClaim.Value);
        }
    }
}
