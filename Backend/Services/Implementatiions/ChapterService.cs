using Microsoft.EntityFrameworkCore;
using Webcomic.Data;
using Webcomic.Models.Entities;
using Webcomic.Services.Interfaces;

namespace Webcomic.Services.Implementatiions
{
    public class ChapterService : IChapterService
    {
        private ApplicationDbContext _context;

        public ChapterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateChapter(Chapter chapter, int comicId)
        {
            var comic = _context.Comics.FirstOrDefault(c => c.Id == comicId);
            if(comic != null)
            {
                chapter.ComicId = comicId;
                chapter.Comic = comic;
                _context.Add(chapter);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool ChapterExists(int chapterId)
        {
            return _context.Chapters.Any(c => c.Id == chapterId);
        }

        public bool DeleteChapter(Chapter chapter, int comicId)
        {
            var comic = _context.Comics.FirstOrDefault(c => c.Id == comicId);
            if (comic != null)
            {
                chapter.ComicId = comicId;
                chapter.Comic = comic;
                _context.Remove(chapter);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Chapter> GetAllChapters()
        {
            return _context.Chapters.ToList();
        }

        public Chapter GetChapterById(int chapterId)
        {
            return _context.Chapters.FirstOrDefault(c => c.Id == chapterId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateChapter(Chapter chapter, int comicId)
        {
            var comic = _context.Comics.FirstOrDefault(c => c.Id == comicId);
            if (comic != null)
            {
                chapter.ComicId = comicId;
                chapter.Comic = comic;
                _context.Update(chapter);
                return Save();
            }
            else
            {
                return false;
            }
        }
    }
}
