namespace Webcomic.Models.Entities
{
    public class AppUserFavoriteComic
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int ComicId { get; set; }
        public Comic Comic { get; set; }
    }
}
