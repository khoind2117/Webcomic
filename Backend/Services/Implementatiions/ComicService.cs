using Microsoft.EntityFrameworkCore;
using Webcomic.Data;
using Webcomic.Models.Entities;
using Webcomic.Services.Interfaces;

namespace Webcomic.Services.Implementatiions
{
    public class ComicService : IComicService
    {
        private ApplicationDbContext _context;

        public ComicService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateComicAsync(Comic comic)
        {
            _context.Add(comic);
            return await SaveAsync();
        }

        public async Task<bool> ComicExistsAsync(int comicId)
        {
            return await _context.Comics.AnyAsync(c => c.Id == comicId);
        }

        public async Task<bool> DeleteComicAsync(Comic comic)
        {
            _context.Remove(comic);
            return await SaveAsync();
        }

        public async Task<IEnumerable<Comic>> GetAllComicsAsync()
        {
            return await _context.Comics
                .Include(c => c.ComicTags)
                    .ThenInclude(ct => ct.Tag)
                .Include(c => c.Chapters)
                .ToListAsync();
        }

        public async Task<Comic> GetComicByIdAsync(int comicId)
        {
            return await _context.Comics.Where(c => c.Id == comicId)
                .Include(c => c.ComicTags)
                .ThenInclude(ct => ct.Tag)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateComicAsync(Comic comic)
        {
            _context.Update(comic);
            return await SaveAsync();
        }

        public IQueryable<Comic> GetAllComicsAsQueryable()
        {
            return _context.Comics.AsQueryable();
        }
    }
}
