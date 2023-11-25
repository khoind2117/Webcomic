using Webcomic.Models.Entities;

namespace Webcomic.Models.DTOs.ComicDtos
{
    public class DetailComicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<string>? TagList { get; set; }

        // Thêm dữ liệu Tiêu đề các chương ở đây
        //public List<int>? ChapterNumber { get; set; }
        //public List<string>? ChapterName { get; set; }
    }
}
