using System.ComponentModel.DataAnnotations;

namespace Webcomic.Models.DTOs.ComicDtos
{
    public class UpdateComicDto
    {
        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public IFormFile ImageFile { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public List<int>? SelectedTagIds { get; set; } // Danh sách các ID của các Tag được chọn
    }
}
