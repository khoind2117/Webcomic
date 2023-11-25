using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Webcomic.Models.DTOs.ChapterDtos
{
    public class UpdateChapterDto
    {
        public string Name { get; set; }
        public int ChapterNumber { get; set; }
        public string Content { get; set; }
    }
}
