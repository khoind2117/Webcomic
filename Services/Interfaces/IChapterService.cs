using Webcomic.Models.Entities;

namespace Webcomic.Services.Interfaces
{
    public interface IChapterService
    {
        IEnumerable<Chapter> GetAllChapters();
        Chapter GetChapterById(int chapterId);
        bool ChapterExists(int chapterId);
        bool CreateChapter(Chapter chapter, int comicId);
        bool UpdateChapter(Chapter chapter, int comicId);
        bool DeleteChapter(Chapter chapter, int comicId);
        bool Save();
    }
}
