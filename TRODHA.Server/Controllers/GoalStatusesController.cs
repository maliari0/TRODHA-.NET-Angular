// TRODHA.Server/Controllers/GoalStatusesController.cs
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
    [Route("api/goal-statuses")]
    [ApiController]
    [Authorize]
    public class GoalStatusesController : ControllerBase
    {
        private readonly IGoalStatusService _goalStatusService;

        public GoalStatusesController(IGoalStatusService goalStatusService)
        {
            _goalStatusService = goalStatusService;
        }

        [HttpGet("goal/{goalId}")]
        public async Task<ActionResult<IEnumerable<GoalStatusDto>>> GetByGoalId(int goalId)
        {
            try
            {
                int userId = GetCurrentUserId();
                var statuses = await _goalStatusService.GetByGoalIdAsync(goalId, userId);
                return Ok(statuses);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoalStatusDto>>> GetAllForCurrentUser()
        {
            int userId = GetCurrentUserId();
            var statuses = await _goalStatusService.GetByUserIdAsync(userId);
            return Ok(statuses);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<GoalStatusDto>>> GetByDateRange(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            try
            {
                int userId = GetCurrentUserId();
                var statuses = await _goalStatusService.GetByDateRangeAsync(userId, startDate, endDate);
                return Ok(statuses);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<GoalStatusDto>> Create(CreateGoalStatusDto createDto)
        {
            try
            {
                int userId = GetCurrentUserId();
                var status = await _goalStatusService.CreateAsync(userId, createDto);
                return CreatedAtAction(nameof(GetByGoalId), new { goalId = status.GoalId }, status);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                int userId = GetCurrentUserId();
                await _goalStatusService.DeleteAsync(id, userId);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<GoalStatusReportDto>>> GetReport(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            try
            {
                int userId = GetCurrentUserId();
                var report = await _goalStatusService.GetUserGoalReportAsync(userId, startDate, endDate);
                return Ok(report);
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
                throw new ApplicationException("Kullanýcý kimliði bulunamadý");
                
            return int.Parse(userIdClaim.Value);
        }
    }
}
