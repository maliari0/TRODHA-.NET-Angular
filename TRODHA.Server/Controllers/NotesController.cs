// TRODHA.Server/Controllers/NotesController.cs
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
    [Route("api/notes")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly IUserNoteService _noteService;
        private readonly INoteImageService _noteImageService;

        public NotesController(IUserNoteService noteService, INoteImageService noteImageService)
        {
            _noteService = noteService;
            _noteImageService = noteImageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserNoteDto>>> GetUserNotes()
        {
            int userId = GetCurrentUserId();
            var notes = await _noteService.GetUserNotesAsync(userId);
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserNoteDto>> GetById(int id)
        {
            int userId = GetCurrentUserId();
            var note = await _noteService.GetByIdAsync(id, userId);
            
            if (note == null)
                return NotFound();
                
            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<UserNoteDto>> Create(CreateUserNoteDto createDto)
        {
            try
            {
                int userId = GetCurrentUserId();
                var note = await _noteService.CreateAsync(userId, createDto);
                return CreatedAtAction(nameof(GetById), new { id = note.NoteId }, note);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserNoteDto>> Update(int id, UpdateUserNoteDto updateDto)
        {
            try
            {
                int userId = GetCurrentUserId();
                var note = await _noteService.UpdateAsync(id, userId, updateDto);
                return Ok(note);
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
                await _noteService.DeleteAsync(id, userId);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{noteId}/images")]
        public async Task<ActionResult<IEnumerable<NoteImageDto>>> GetNoteImages(int noteId)
        {
            try
            {
                int userId = GetCurrentUserId();
                var images = await _noteImageService.GetByNoteIdAsync(noteId, userId);
                return Ok(images);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{noteId}/images")]
        public async Task<ActionResult<NoteImageDto>> UploadImage(int noteId, [FromForm] FileUploadDto uploadDto)
        {
            try
            {
                int userId = GetCurrentUserId();
                var image = await _noteImageService.UploadImageAsync(noteId, userId, uploadDto.File);
                return Ok(image);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("images/{imageId}")]
        public async Task<ActionResult> DeleteImage(int imageId)
        {
            try
            {
                int userId = GetCurrentUserId();
                await _noteImageService.DeleteAsync(imageId, userId);
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
                throw new ApplicationException("Kullanýcý kimliði bulunamadý");
                
            return int.Parse(userIdClaim.Value);
        }
    }
}
