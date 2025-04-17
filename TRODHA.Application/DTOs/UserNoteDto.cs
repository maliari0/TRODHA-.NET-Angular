// TRODHA.Application/DTOs/UserNoteDto.cs
using System;
using System.Collections.Generic;

namespace TRODHA.Application.DTOs
{
    public class UserNoteDto
    {
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<NoteImageDto> Images { get; set; } = new List<NoteImageDto>();
    }

    public class CreateUserNoteDto
    {
        public string Content { get; set; } = null!;
    }

    public class UpdateUserNoteDto
    {
        public string Content { get; set; } = null!;
    }

    public class NoteImageDto
    {
        public int ImageId { get; set; }
        public int NoteId { get; set; }
        public string ImagePath { get; set; } = null!;
        public string ImageType { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; } = null!; // Frontend'de gösterim için URL
    }
}
