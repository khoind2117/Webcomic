using Microsoft.EntityFrameworkCore;
using Webcomic.Data;
using Webcomic.Models.DTOs.AppUserFavoriteComic;
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
            _context.Comics.Add(comic);
            return await SaveAsync();
        }

        public async Task<bool> ComicExistsAsync(int comicId)
        {
            return await _context.Comics.AnyAsync(c => c.Id == comicId);
        }

        public async Task<bool> DeleteComicAsync(Comic comic)
        {
            _context.Comics.Remove(comic);
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
            _context.Comics.Update(comic);
            return await SaveAsync();
        }

        public IQueryable<Comic> GetAllComicsAsQueryable()
        {
            return _context.Comics.AsQueryable();
        }

        public async Task<IEnumerable<Comic>> GetLatestComicsWithLatestChapter()
        {
            var latestComicsWithLatestChapter = await _context.Comics
                .Where(c => c.Chapters.Any()) // Chỉ lấy truyện có ít nhất một chương
                .Select(c => new
                {
                    Comic = c,
                    LatestChapter = c.Chapters.OrderByDescending(ch => ch.UploadDate).FirstOrDefault() // Chọn chương mới nhất
                })
                .OrderByDescending(c => c.LatestChapter.UploadDate) // Sắp xếp theo ngày cập nhật mới nhất của chương
                .Take(15)
                .Select(c => c.Comic) // Chọn thông tin truyện
                .ToListAsync();

            return latestComicsWithLatestChapter;
        }

        public Task<bool> SaveFavoriteComic(AppUserFavoriteComic favoriteComic)
        {
            _context.AppUserFavoriteComics.Add(favoriteComic);
            return SaveAsync();
        }
        public Task<bool> UnsaveFavoriteComic(AppUserFavoriteComic favoriteComic)
        {
            _context.AppUserFavoriteComics.Remove(favoriteComic);
            return SaveAsync();
        }

        public IQueryable<Comic> GetAllComicByTagAsQueryable(int tagId)
        {
            return _context.Comics
                    .Where(c => c.ComicTags.Any(ct => ct.TagId == tagId))
                    .OrderByDescending(c => c.CreatedDate)
                    .AsQueryable();
        }
    }
}
