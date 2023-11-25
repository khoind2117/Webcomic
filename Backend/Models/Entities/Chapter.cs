using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webcomic.Models.Entities
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChapterNumber { get; set; }
        public DateTime UploadDate { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string Content { get; set; }

        public int? ComicId { get; set; }
        public Comic? Comic { get; set; }
    }
}
