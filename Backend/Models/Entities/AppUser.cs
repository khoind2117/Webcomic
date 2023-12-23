using Microsoft.AspNetCore.Identity;

namespace Webcomic.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<AppUserFavoriteComic>? AppUserFavoriteComics { get; set; }
    }
}
