using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Webcomic.Models.Entities;

namespace Webcomic.Models.DTOs.ChapterDtos
{
    public class CreateChapterDto
    {
        [Required(ErrorMessage = "ComicId is required")] // ComicId được chọn trước
        public int ComicId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ChapterNumber is required")]
        public int ChapterNumber { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string Content { get; set; }
    }
}
