using Webcomic.Models.Entities;

namespace Webcomic.Services.Interfaces
{
    public interface IComicService
    {
        Task<IEnumerable<Comic>> GetAllComicsAsync();
        Task<Comic> GetComicByIdAsync(int comicId);
        Task<bool> ComicExistsAsync(int comicId);
        Task<bool> CreateComicAsync(Comic comic);
        Task<bool> UpdateComicAsync(Comic comic);
        Task<bool> DeleteComicAsync(Comic comic);
        Task<bool> SaveAsync();
    }
}
