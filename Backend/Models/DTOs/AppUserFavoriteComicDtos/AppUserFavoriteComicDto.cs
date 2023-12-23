using Webcomic.Models.Entities;

namespace Webcomic.Models.DTOs.AppUserFavoriteComic
{
    public class AppUserFavoriteComicDto
    {
        public string AppUserId { get; set; }
        public int ComicId { get; set; }
    }
}
