using System.ComponentModel.DataAnnotations.Schema;

namespace Webcomic.Models.Entities
{
    [Table("Comic")]
    public class Comic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<ComicTag>? ComicTags { get; set; }
        public virtual ICollection<Chapter>? Chapters { get; set; }
    }
}
