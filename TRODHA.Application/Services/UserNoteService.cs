// TRODHA.Application/Services/UserNoteService.cs
using Microsoft.Extensions.Configuration;
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
    public class UserNoteService : IUserNoteService
    {
        private readonly IUserNoteRepository _userNoteRepository;
        private readonly INoteImageRepository _noteImageRepository;
        private readonly string _baseUrl;

        public UserNoteService(IUserNoteRepository userNoteRepository, 
                              INoteImageRepository noteImageRepository,
                              IConfiguration configuration)
        {
            _userNoteRepository = userNoteRepository;
            _noteImageRepository = noteImageRepository;
            
            // Uygulama URL'ini yapýlandýrmadan al
            _baseUrl = configuration["AppSettings:BaseUrl"] ?? "https://localhost:5001";
        }

        public async Task<IEnumerable<UserNoteDto>> GetUserNotesAsync(int userId)
        {
            var notes = await _userNoteRepository.GetUserNotesWithImagesAsync(userId);
            return notes.Select(note => MapToDto(note));
        }

        public async Task<UserNoteDto?> GetByIdAsync(int noteId, int userId)
        {
            var note = await _userNoteRepository.GetByIdAsync(noteId);
            
            if (note == null || note.UserId != userId)
                return null;
                
            // Notun görsellerini getir
            return MapToDto(note);
        }

        public async Task<UserNoteDto> CreateAsync(int userId, CreateUserNoteDto createDto)
        {
            var note = new UserNote
            {
                UserId = userId,
                Content = createDto.Content,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _userNoteRepository.AddAsync(note);
            return MapToDto(note);
        }

        public async Task<UserNoteDto> UpdateAsync(int noteId, int userId, UpdateUserNoteDto updateDto)
        {
            var note = await _userNoteRepository.GetByIdAsync(noteId);
            
            if (note == null)
                throw new ApplicationException("Not bulunamadý");
                
            if (note.UserId != userId)
                throw new ApplicationException("Bu notu düzenleme yetkiniz yok");
                
            note.Content = updateDto.Content;
            note.UpdatedAt = DateTime.Now;
            
            await _userNoteRepository.UpdateAsync(note);
            
            // Güncellenmiþ notu tekrar getir (görselleriyle birlikte)
            var updatedNote = await _userNoteRepository.GetByIdAsync(noteId);
            return MapToDto(updatedNote!);
        }

        public async Task DeleteAsync(int noteId, int userId)
        {
            var note = await _userNoteRepository.GetByIdAsync(noteId);
            
            if (note == null)
                throw new ApplicationException("Not bulunamadý");
                
            if (note.UserId != userId)
                throw new ApplicationException("Bu notu silme yetkiniz yok");
                
            await _userNoteRepository.DeleteAsync(note);
        }
        
        private UserNoteDto MapToDto(UserNote note)
        {
            return new UserNoteDto
            {
                NoteId = note.NoteId,
                UserId = note.UserId,
                Content = note.Content,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt,
                Images = note.Images?.Select(img => new NoteImageDto
                {
                    ImageId = img.ImageId,
                    NoteId = img.NoteId,
                    ImagePath = img.ImagePath,
                    ImageType = img.ImageType,
                    CreatedAt = img.CreatedAt,
                    ImageUrl = $"{_baseUrl}/{img.ImagePath}"
                }).ToList() ?? new List<NoteImageDto>()
            };
        }
    }
}
