using Webcomic.Models.Entities;

namespace Webcomic.Services.Interfaces
{
    public interface IChapterService
    {
        Task<Chapter> GetChapterByIdAsync(int chapterId);
        Task<bool> ChapterExistsAsync(int chapterId);
        Task<bool> CreateChapterAsync(Chapter chapter);
        Task<bool> UpdateChapterAsync(Chapter chapter);
        Task<bool> DeleteChapterAsync(Chapter chapter);
        Task<bool> SaveAsync();

        IQueryable<Chapter> GetAllChaptersByComicIdAsQueryable (int comicId);
        Task<ICollection<Chapter>> GetAllChaptersByComicIdAsync(int comicId);
    }
}
