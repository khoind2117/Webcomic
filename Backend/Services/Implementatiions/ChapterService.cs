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
        public async Task<bool> CreateChapterAsync(Chapter chapter)
        {
            _context.Add(chapter);
            return await SaveAsync();
        }

        public async Task<bool> ChapterExistsAsync(int chapterId)
        {
            return await _context.Chapters.AnyAsync(c => c.Id == chapterId);
        }

        public async Task<bool> DeleteChapterAsync(Chapter chapter)
        {
            _context.Remove(chapter);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateChapterAsync(Chapter chapter)
        {
            _context.Update(chapter);
            return await SaveAsync();
        }

        public async Task<ICollection<Chapter>> GetAllChaptersByComicIdAsync(int comicId)
        {
            return await _context.Chapters
                .Where(c => c.ComicId == comicId)
                .OrderBy(c => c.ChapterNumber)
                .ToListAsync();
        }

        public IQueryable<Chapter> GetAllChaptersByComicIdAsQueryable(int comicId)
        {
            return _context.Chapters.Where(c => c.ComicId == comicId).AsQueryable();
        }

        public async Task<Chapter> GetChapterByIdAsync(int chapterId)
        {
            return await _context.Chapters.FirstOrDefaultAsync(c => c.Id == chapterId);
        }
    }
}
